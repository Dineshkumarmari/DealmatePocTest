using DealmateApi.Service.Common;

namespace DealmateApi.Domain.Aggregates;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BranchId { get; set; }
    [Include]
    public Branch? Branch { get; set; }
    public int RoleId { get; set; }
    [Include]
    public Role? Role { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public static class Permissions
    {
        public const string Name = nameof(Employee);
        public const string Get = $"{Name}:get";
        public const string List = $"{Name}:list";
        public const string Create = $"{Name}:create";
        public const string Update = $"{Name}:update";
        public const string Delete = $"{Name}:delete";
    }
}
