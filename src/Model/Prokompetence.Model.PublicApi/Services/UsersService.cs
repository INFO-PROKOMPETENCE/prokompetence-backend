using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Security;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.Common.Security.Models;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;
using Prokompetence.Model.PublicApi.Exceptions;
using Prokompetence.Model.PublicApi.Interfaces;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Services;

public interface IUsersService
{
    Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken);
    Task<SignInResult> SignIn(string login, string password, CancellationToken cancellationToken);
    Task<RefreshTokenResult> RefreshToken(string refreshToken, CancellationToken cancellationToken);
    Task<UserModel> GetUserByLogin(string login, CancellationToken cancellationToken);
    Task SetContacts(string contacts, CancellationToken cancellationToken);
    Task SetAcademicGroup(string academicGroup, CancellationToken ct);
}

public sealed class UsersService : IUsersService
{
    private readonly IUserRepository userRepository;
    private readonly IRoleRepository roleRepository;
    private readonly IAccessTokenGenerator accessTokenGenerator;
    private readonly Func<IContextUserProvider> contextUserProviderFactory;
    private readonly IUnitOfWork unitOfWork;
    private readonly IProkompetenceDbContext dbContext;

    public UsersService(IUserRepository userRepository,
        IRoleRepository roleRepository,
        IAccessTokenGenerator accessTokenGenerator,
        Func<IContextUserProvider> contextUserProviderFactory,
        IUnitOfWork unitOfWork,
        IProkompetenceDbContext dbContext)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
        this.accessTokenGenerator = accessTokenGenerator;
        this.contextUserProviderFactory = contextUserProviderFactory;
        this.unitOfWork = unitOfWork;
        this.dbContext = dbContext;
    }

    public async Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var login = request.Login.ToLower();
        if (await userRepository.ExistByLogin(login, cancellationToken))
        {
            throw new UserExistsException(login);
        }

        var password = request.Password;
        var passwordSalt = CryptographyHelper.GenerateRandomBytes(8);
        var passwordHash = CryptographyHelper.GenerateMd5Hash(password, passwordSalt);
        var user = new User
        {
            Name = request.Name,
            Login = login,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
        await userRepository.Add(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var role = await roleRepository.GetByName("User", cancellationToken);
        await roleRepository.AddRoleForUser(user.Id, role.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<SignInResult> SignIn(string login, string password, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByLogin(login, cancellationToken);
        if (user == null)
        {
            return new SignInResult { Success = false };
        }

        var inputPasswordHash = CryptographyHelper.GenerateMd5Hash(password, user.PasswordSalt);
        if (!inputPasswordHash.SequenceEqual(user.PasswordHash))
        {
            return new SignInResult { Success = false };
        }

        var userModel = new UserIdentityModel
        {
            Id = user.Id,
            Login = user.Login,
            Roles = user.Roles.Select(r => r.Role.Name).ToArray()
        };
        var accessToken = accessTokenGenerator.GenerateAccessToken(userModel);
        user.RefreshToken = accessToken.RefreshToken;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SignInResult
        {
            Success = true,
            Result = accessToken
        };
    }

    public async Task<RefreshTokenResult> RefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var userModel = contextUserProviderFactory.Invoke().GetUser();
        var user = await userRepository.FindByLogin(userModel.Login, cancellationToken);
        if (user == null || user.RefreshToken != refreshToken)
        {
            return new RefreshTokenResult { Success = false };
        }

        var newAccessToken = accessTokenGenerator.GenerateAccessToken(userModel);
        user.RefreshToken = newAccessToken.RefreshToken;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new RefreshTokenResult
        {
            Success = true,
            Result = newAccessToken
        };
    }

    public async Task<UserModel> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByLogin(login, cancellationToken);
        if (user is null)
        {
            throw new Exception($"User with login {login} not found");
        }

        return new UserModel
        {
            Name = user.Name
        };
    }

    public async Task SetContacts(string contacts, CancellationToken cancellationToken)
    {
        var user = contextUserProviderFactory.Invoke().GetUser();
        var userEntity = await dbContext.Users
            .Where(u => u.Id == user.Id)
            .FirstAsync(cancellationToken);
        userEntity.Contacts = contacts;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetAcademicGroup(string academicGroup, CancellationToken ct)
    {
        var user = contextUserProviderFactory.Invoke().GetUser();
        var userEntity = await dbContext.Users
            .Where(u => u.Id == user.Id)
            .FirstAsync(ct);
        userEntity.AcademicGroup = academicGroup;
        await dbContext.SaveChangesAsync(ct);
    }
}