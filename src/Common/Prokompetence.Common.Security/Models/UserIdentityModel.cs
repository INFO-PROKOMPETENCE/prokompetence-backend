﻿namespace Prokompetence.Common.Security.Models;

public sealed class UserIdentityModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string[] Roles { get; set; }
}