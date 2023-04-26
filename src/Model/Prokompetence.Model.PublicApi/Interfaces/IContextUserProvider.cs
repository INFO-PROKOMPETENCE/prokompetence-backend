using Prokompetence.Model.PublicApi.Models.Users;

namespace Prokompetence.Model.PublicApi.Interfaces;

public interface IContextUserProvider
{
    UserIdentityModel GetUser();
}