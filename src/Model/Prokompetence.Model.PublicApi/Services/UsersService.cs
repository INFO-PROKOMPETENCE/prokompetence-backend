using Prokompetence.Common.Security;
using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;
using Prokompetence.Model.PublicApi.Interfaces;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Services;

public interface IUsersService
{
    Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken);
    Task<SignInResult> SignIn(string login, string password, CancellationToken cancellationToken);
    Task<RefreshTokenResult> RefreshToken(string refreshToken, CancellationToken cancellationToken);
    Task<UserModel> GetUserByLogin(string login, CancellationToken cancellationToken);
}

public sealed class UsersService : IUsersService
{
    private readonly IUsersRepository usersRepository;
    private readonly IRolesRepository rolesRepository;
    private readonly IAccessTokenGenerator accessTokenGenerator;
    private readonly Func<IContextUserProvider> contextUserProviderFactory;

    public UsersService(IUsersRepository usersRepository,
        IRolesRepository rolesRepository,
        IAccessTokenGenerator accessTokenGenerator,
        Func<IContextUserProvider> contextUserProviderFactory)
    {
        this.usersRepository = usersRepository;
        this.rolesRepository = rolesRepository;
        this.accessTokenGenerator = accessTokenGenerator;
        this.contextUserProviderFactory = contextUserProviderFactory;
    }

    public async Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var login = request.Login;
        var password = request.Password;
        var passwordSalt = CryptographyHelper.GenerateRandomBytes(8);
        var passwordHash = CryptographyHelper.GenerateMd5Hash(password, passwordSalt);
        var role = await rolesRepository.GetByName("User", cancellationToken);
        var user = new User
        {
            Login = login,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = role
        };
        await usersRepository.Add(user, cancellationToken);
    }

    public async Task<SignInResult> SignIn(string login, string password, CancellationToken cancellationToken)
    {
        var user = await usersRepository.FindByLogin(login, cancellationToken);
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
            Role = ""
        };
        var accessToken = accessTokenGenerator.GenerateAccessToken(userModel);
        user.RefreshToken = accessToken.RefreshToken;
        await usersRepository.Update(user, cancellationToken);
        return new SignInResult
        {
            Success = true,
            Result = accessToken
        };
    }

    public async Task<RefreshTokenResult> RefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var userModel = contextUserProviderFactory.Invoke().GetUser();
        var user = await usersRepository.FindByLogin(userModel.Login, cancellationToken);
        if (user == null || user.RefreshToken != refreshToken)
        {
            return new RefreshTokenResult { Success = false };
        }

        var newAccessToken = accessTokenGenerator.GenerateAccessToken(userModel);
        user.RefreshToken = newAccessToken.RefreshToken;
        await usersRepository.Update(user, cancellationToken);
        return new RefreshTokenResult
        {
            Success = true,
            Result = newAccessToken
        };
    }

    public async Task<UserModel> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        var user = await usersRepository.FindByLogin(login, cancellationToken);
        if (user is null)
        {
            throw new Exception($"User with login {login} not found");
        }

        return new UserModel
        {
            Name = user.Login
        };
    }
}