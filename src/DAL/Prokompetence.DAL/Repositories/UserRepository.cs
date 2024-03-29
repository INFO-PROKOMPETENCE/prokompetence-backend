﻿using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Repositories;

public interface IUserRepository
{
    Task Add(User user, CancellationToken cancellationToken);
    Task<User?> FindByLogin(string login, CancellationToken cancellationToken);
    Task<bool> ExistByLogin(string login, CancellationToken cancellationToken);
}

public sealed class UserRepository : IUserRepository
{
    private readonly IProkompetenceDbContext context;

    public UserRepository(IProkompetenceDbContext context)
    {
        this.context = context;
    }

    public async Task Add(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> FindByLogin(string login, CancellationToken cancellationToken)
        => await context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Role)
            .Where(u => u.Login == login)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<bool> ExistByLogin(string login, CancellationToken cancellationToken) =>
        await context.Users
            .Where(u => u.Login == login)
            .AnyAsync(cancellationToken);
}