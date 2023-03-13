using Prokompetence.Common.Security;
using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Services;

public interface IUsersService
{
    Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken);
    Task<SignInResult> SignIn(string login, string password, CancellationToken cancellationToken);
    Task<RefreshTokenResult> RefreshToken(string login, string refreshToken, CancellationToken cancellationToken);
}

public sealed class UsersService : IUsersService
{
    private readonly IUsersRepository repository;

    public UsersService(IUsersRepository repository)
    {
        this.repository = repository;
    }

    public async Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken)
    {
        var login = request.Login;
        var password = request.Password;
        var passwordSalt = CryptographyHelper.GenerateRandomBytes(8);
        var passwordHash = CryptographyHelper.GenerateMd5Hash(password, passwordSalt);
        var user = new User
        {
            Login = login,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        await repository.Add(user, cancellationToken);
    }

    public async Task<SignInResult> SignIn(string login, string password, CancellationToken cancellationToken)
    {
        var user = await repository.FindByLogin(login, cancellationToken);
        if (user == null)
        {
            return new SignInResult { Success = false };
        }

        var inputPasswordHash = CryptographyHelper.GenerateMd5Hash(password, user.PasswordSalt);
        if (!inputPasswordHash.SequenceEqual(user.PasswordHash))
        {
            return new SignInResult { Success = false };
        }

        var refreshToken = JwtHelper.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        await repository.Update(user, cancellationToken);
        return new SignInResult
        {
            Success = true,
            RefreshToken = refreshToken
        };
    }

    public async Task<RefreshTokenResult> RefreshToken(string login, string refreshToken,
        CancellationToken cancellationToken)
    {
        var user = await repository.FindByLogin(login, cancellationToken);
        if (user == null || user.RefreshToken != refreshToken)
        {
            return new RefreshTokenResult { Success = false };
        }

        var newRefreshToken = JwtHelper.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await repository.Update(user, cancellationToken);
        return new RefreshTokenResult
        {
            Success = true,
            RefreshToken = newRefreshToken
        };
    }
}