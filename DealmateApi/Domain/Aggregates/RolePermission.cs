using DealmateApi.Service.Common;

namespace DealmateApi.Domain.Aggregates;

public class RolePermission
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    [Include]
    public Role? Role { get; set; }     
    public int PermissionId { get; set; }
    [Include]
    public Permission? Permission { get; set; } 
}
