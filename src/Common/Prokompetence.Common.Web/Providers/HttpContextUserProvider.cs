using Microsoft.AspNetCore.Http;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.Common.Security.Models;
using Prokompetence.Common.Web.Extensions;

namespace Prokompetence.Common.Web.Providers;

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