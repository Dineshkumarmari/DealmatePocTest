using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;

namespace DealmateApi.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IRepository<Role> repository;
    private readonly IRepository<RolePermission> rolePermissionRepository;
    public RoleRepository(IRepository<Role> repository, IRepository<RolePermission> rolePermissionRepository)
    {
        this.repository = repository;
        this.rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<Role> Create(Role role)
    {
        var existRole = await repository.FirstOrDefaultAsync(x => x.Name == role.Name);
        if (existRole != null)
        {
            throw new Exception($"The Role {role.Name} was already exist");
        }
        role = await repository.AddAsync(role);
        return role;
    }

    public async Task<Role> Update(Role role)
    {
        var existRole = await repository.GetByIdAsync(role.Id);
        if (existRole == null)
        {
            throw new Exception($"The RoleID {role.Id} not exist");
        }
        existRole.Name = role.Name;
        existRole = await repository.Update(existRole);
        return existRole;
    }

    public async Task<Role> Delete(int id)
    {
        var role = await repository.GetByIdAsync(id);
        if (role == null)
        {
            throw new Exception($"The Role Id:{id} was not found");
        }
        role = await repository.Remove(role);
        return role;
    }
    public async Task<List<Permission?>> GetPermissions(int id)
    {
        var role = await this.repository.GetByIdAsync(id);
        if (role == null)
        {
            throw new Exception($"The Role ID {id} not exist");
        }
        var permissions = await this.rolePermissionRepository.FindAsync(x => x.RoleId == id);
        return permissions.Select(x => x.Permission).ToList();
    }

}
