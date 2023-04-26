using Prokompetence.Model.PublicApi.Interfaces;
using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Web.PublicApi.Identity;

public sealed class HttpContextUserProvider : IContextUserProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public HttpContextUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public UserIdentityModel GetUser()
    {
        var context = httpContextAccessor.HttpContext ?? throw new NullReferenceException();
        return context.User.GetUserIdentityModel();
    }
}