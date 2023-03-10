using Prokompetence.Model.PublicApi.Models.HelloWorld;

namespace Prokompetence.Model.PublicApi.Services;

public interface IHelloWorldService
{
    Task<string> GetHelloWorld(HelloWorldRequest helloWorldRequest);
}

public sealed class HelloWorldService : IHelloWorldService
{
    public Task<string> GetHelloWorld(HelloWorldRequest helloWorldRequest)
    {
        var name = helloWorldRequest.Name ?? "World";
        return Task.FromResult($"Hello {name}!");
    }
}