﻿namespace Prokompetence.DAL.Entities;

public sealed class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ISet<User> Users { get; set; }
}