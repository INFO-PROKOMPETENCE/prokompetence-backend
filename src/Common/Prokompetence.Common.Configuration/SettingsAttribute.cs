namespace Prokompetence.Common.Configuration;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SettingsAttribute : Attribute
{
    public string? Scope { get; }

    public SettingsAttribute()
    {
    }

    public SettingsAttribute(string scope)
    {
        Scope = scope;
    }
}