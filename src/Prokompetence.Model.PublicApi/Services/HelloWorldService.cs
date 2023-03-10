namespace Prokompetence.Model.PublicApi.Services;

public interface IHelloWorldService
{
    Task<string> GetHelloWorld();
}

public sealed class HelloWorldService : IHelloWorldService
{
    public Task<string> GetHelloWorld()
    {
        return Task.FromResult("Hello world!");
    }
}