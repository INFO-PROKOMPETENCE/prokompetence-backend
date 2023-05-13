namespace Prokompetence.Model.PublicApi.Exceptions;

public sealed class UserExistsException : Exception
{
    public UserExistsException(string login)
        : base($"Пользователь с логином {login} уже существует")
    {
    }
}