using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;

namespace DealmateApi.Infrastructure.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly IRepository<RolePermission> repository;
    private readonly IRepository<Role> roleRepository;
    private readonly IRepository<Permission> permissionRepository;
    public RolePermissionRepository(IRepository<RolePermission> repository, IRepository<Role> roleRepository,
        IRepository<Permission> permissionRepository)
    {
        this.repository = repository;
        this.roleRepository = roleRepository;
        this.permissionRepository = permissionRepository;
    }

    public async Task<RolePermission> Create(RolePermission rolePermission)
    {
        var existRolePermission = await repository.FirstOrDefaultAsync
            (x => x.RoleId == rolePermission.RoleId && x.PermissionId == rolePermission.PermissionId);
        if (existRolePermission != null)
        {
            throw new Exception($"The Role {existRolePermission.Role!.Name} was already has {existRolePermission.Permission!.Name} Permission");
        }
        var role = await this.roleRepository.GetByIdAsync(rolePermission.RoleId);
        if (role == null)
        {
            throw new Exception($"The Role ID {rolePermission.RoleId} not exist");
        }

        var permission = await this.permissionRepository.GetByIdAsync(rolePermission.PermissionId);
        if (permission == null)
        {
            throw new Exception($"The Permission Id {rolePermission.PermissionId} not exist");
        }
        rolePermission = await repository.AddAsync(rolePermission);
        return rolePermission;
    }

    public async Task<RolePermission> Update(RolePermission rolePermission)
    {
        var existRolePermission = await repository.GetByIdAsync(rolePermission.Id);
        if (existRolePermission == null)
        {
            throw new Exception($"The RolePermission Id {rolePermission.Id} not exist");
        }
        existRolePermission = await repository.Update(existRolePermission);
        return existRolePermission;
    }

    public async Task<RolePermission> Delete(int id)
    {
        var rolePermission = await repository.GetByIdAsync(id);
        if (rolePermission == null)
        {
            throw new Exception($"The Role Permission Id:{id} was not found");
        }
        rolePermission = await repository.Remove(rolePermission);
        return rolePermission;
    }

}