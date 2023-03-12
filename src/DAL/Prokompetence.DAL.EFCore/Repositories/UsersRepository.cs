﻿using Prokompetence.DAL.Entities;
using Prokompetence.DAL.Repositories;

namespace Prokompetence.DAL.EFCore.Repositories;

public sealed class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(ProkompetenceDbContext context)
        : base(context)
    {
    }

    public Task<User?> FindByName(string name, CancellationToken cancellationToken)
        => SingleOrDefault(u => u.Name == name, cancellationToken);
}