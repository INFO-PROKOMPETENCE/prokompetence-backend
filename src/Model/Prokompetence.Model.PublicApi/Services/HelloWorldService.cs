using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;
using Prokompetence.Model.PublicApi.Models.HelloWorld;

namespace Prokompetence.Model.PublicApi.Services;

public interface IHelloWorldService
{
    Task<string> GetHelloWorld(HelloWorldRequest helloWorldRequest, CancellationToken cancellationToken);
}

public sealed class HelloWorldService : IHelloWorldService
{
    private readonly IUsersRepository usersRepository;

    public HelloWorldService(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    public async Task<string> GetHelloWorld(HelloWorldRequest helloWorldRequest, CancellationToken cancellationToken)
    {
        var name = helloWorldRequest.Name ?? "World";
        var user = await usersRepository.FindByName(name, cancellationToken)
                   ?? await usersRepository.Add(new User { Name = name }, cancellationToken);

        return $"Hello, {user.Name}!";
    }
}