using Prokompetence.Common.Security.Models;

namespace Prokompetence.Common.Security.Abstractions;

public interface IContextUserProvider
{
    UserIdentityModel GetUser();
}