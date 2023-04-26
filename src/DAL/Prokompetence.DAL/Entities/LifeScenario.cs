﻿namespace Prokompetence.DAL.Entities;

public sealed class LifeScenario
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ISet<Project>? Projects { get; set; } // для связи 1 ко многим
}