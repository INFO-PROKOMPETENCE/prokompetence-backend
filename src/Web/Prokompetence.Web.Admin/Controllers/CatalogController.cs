using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Web.Admin.Dto.Catalog;

namespace Prokompetence.Web.Admin.Controllers;

public sealed class CatalogController
{
    private readonly IProkompetenceDbContext dbContext;

    public CatalogController(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    [Route("api/admin/catalogs/{catalogName}")]
    public async Task AddCatalogEntry([FromBody] CatalogDto body, [FromRoute] CatalogName catalogName)
    {
        switch (catalogName)
        {
            case CatalogName.KeyTechnologies:
                dbContext.KeyTechnologies.Add(new KeyTechnology { Id = body.Id, Name = body.Name });
                break;
            case CatalogName.LifeScenarios:
                dbContext.LifeScenarios.Add(new LifeScenario { Id = body.Id, Name = body.Name });
                break;
            case CatalogName.TeamRoles:
                dbContext.TeamRoles.Add(new TeamRole { Id = body.Id, Name = body.Name });
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(catalogName), catalogName, null);
        }

        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}