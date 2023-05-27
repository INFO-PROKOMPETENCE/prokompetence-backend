namespace Prokompetence.Common.Security.Models;

public static class UserRoleConstants
{
    public const string User = "User";
    public const string Student = "Student";
    public const string Customer = "Customer";
    public const string Admin = "Admin";

    public static readonly string[] Roles = { User, Student, Customer, Admin };
}