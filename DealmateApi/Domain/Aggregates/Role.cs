namespace DealmateApi.Domain.Aggregates;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Types.Roles RoleType { get; set; }


    public static class Types
    {
        public enum Roles
        {
            Admin = 0,
            Associate = 1,
            Managerial = 2
        }
    }
}
