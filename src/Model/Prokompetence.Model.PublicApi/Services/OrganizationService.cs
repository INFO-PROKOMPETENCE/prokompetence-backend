using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Organization;

namespace Prokompetence.Model.PublicApi.Services;

public interface IOrganizationService
{
    Task AddOrganization(AddOrganizationRequest request, CancellationToken ct);
    Task<OrganizationModel[]> GetCurrentUsersOrganizations(CancellationToken ct);
}

public sealed class OrganizationService : IOrganizationService
{
    private readonly IProkompetenceDbContext dbContext;
    private readonly Func<IContextUserProvider> contextUserProviderFactory;

    public OrganizationService(IProkompetenceDbContext dbContext, Func<IContextUserProvider> contextUserProviderFactory)
    {
        this.dbContext = dbContext;
        this.contextUserProviderFactory = contextUserProviderFactory;
    }

    public async Task AddOrganization(AddOrganizationRequest request, CancellationToken ct)
    {
        var organization = request.Adapt<Organization>();
        await dbContext.Organizations.AddAsync(organization, ct);
        await dbContext.SaveChangesAsync(ct);
        var currentUser = contextUserProviderFactory.Invoke().GetUser();
        await dbContext.UserOrganizationAccesses.AddAsync(new UserOrganizationAccess
            { OrganizationId = organization.Id, UserId = currentUser.Id }, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<OrganizationModel[]> GetCurrentUsersOrganizations(CancellationToken ct)
    {
        var currentUser = contextUserProviderFactory.Invoke().GetUser();
        var accesses = dbContext.UserOrganizationAccesses.Where(a => a.UserId == currentUser.Id);
        var query =
            from access in accesses
            join organization in dbContext.Organizations on access.OrganizationId equals organization.Id
            select organization;
        return await query.ProjectToType<OrganizationModel>().ToArrayAsync(ct);
    }
}