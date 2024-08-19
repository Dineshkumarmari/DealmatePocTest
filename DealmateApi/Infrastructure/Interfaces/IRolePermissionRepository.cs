using DealmateApi.Domain.Aggregates;

namespace DealmateApi.Infrastructure.Interfaces;
public interface IRolePermissionRepository
{
    Task<RolePermission> Create(RolePermission rolePermission);
    Task<RolePermission> Update(RolePermission rolePermission);
    Task<RolePermission> Delete(int id);
}