using Mapster;
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
    Task<RefreshTokenResult> RefreshToken(string accessToken, string refreshToken, CancellationToken cancellationToken);
}

public sealed class UsersService : IUsersService
{
    private readonly IUsersRepository repository;
    private readonly IAccessTokenGenerator accessTokenGenerator;

    public UsersService(IUsersRepository repository, IAccessTokenGenerator accessTokenGenerator)
    {
        this.repository = repository;
        this.accessTokenGenerator = accessTokenGenerator;
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
        var userModel = user.Adapt<UserIdentityModel>();
        var accessToken = accessTokenGenerator.GenerateAccessToken(userModel);
        return new SignInResult
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<RefreshTokenResult> RefreshToken(string accessToken, string refreshToken,
        CancellationToken cancellationToken)
    {
        var userModel = accessTokenGenerator.TryGetUserModelFromAccessToken(accessToken);
        if (userModel == null)
        {
            return new RefreshTokenResult { Success = false };
        }

        var user = await repository.FindByLogin(userModel.Login, cancellationToken);
        if (user == null || user.RefreshToken != refreshToken)
        {
            return new RefreshTokenResult { Success = false };
        }

        var newRefreshToken = JwtHelper.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await repository.Update(user, cancellationToken);
        userModel = user.Adapt<UserIdentityModel>();
        var newAccessToken = accessTokenGenerator.GenerateAccessToken(userModel);
        return new RefreshTokenResult
        {
            Success = true,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}