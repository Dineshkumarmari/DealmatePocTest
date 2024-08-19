using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;

namespace DealmateApi.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly IRepository<Permission> repository;
    public PermissionRepository(IRepository<Permission> repository)
    {
        this.repository = repository;
    }

    public async Task<Permission> Create(Permission permission)
    {
        var existPermission = await repository.FirstOrDefaultAsync(x => x.Name == permission.Name);
        if (existPermission != null)
        {
            throw new Exception($"The Permission {permission.Name} was already exist");
        }
        permission = await repository.AddAsync(permission);
        return permission;
    }

    public async Task<Permission> Update(Permission permission)
    {
        var existPermission = await repository.GetByIdAsync(permission.Id);
        if (existPermission == null)
        {
            throw new Exception($"The Permission Id {permission.Id} was not exist");
        }
        var alreadyExists = await repository.FirstOrDefaultAsync(x => x.Name == permission.Name);
        if (alreadyExists != null)
        {
            throw new Exception($"The Permission {permission.Name} was already exist");
        }
        existPermission.Name = permission.Name;
        existPermission = await repository.Update(existPermission);
        return existPermission;
    }

    public async Task<Permission> Delete(int id)
    {
        var permission = await repository.GetByIdAsync(id);
        if (permission == null)
        {
            throw new Exception($"The Permission Id:{id} was not found");
        }
        permission = await repository.Remove(permission);
        return permission;
    }

}
