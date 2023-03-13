using Prokompetence.Common.Security;
using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Services;

public interface IUsersService
{
    Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken);
    Task<bool> SignIn(string login, string password, CancellationToken cancellationToken);
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

    public async Task<bool> SignIn(string login, string password, CancellationToken cancellationToken)
    {
        var user = await repository.FindByLogin(login, cancellationToken);
        if (user == null)
        {
            return false;
        }

        var inputPasswordHash = CryptographyHelper.GenerateMd5Hash(password, user.PasswordSalt);
        return inputPasswordHash.SequenceEqual(user.PasswordHash);
    }
}