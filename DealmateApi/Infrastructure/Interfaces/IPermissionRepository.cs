using DealmateApi.Domain.Aggregates;

namespace DealmateApi.Infrastructure.Interfaces;

public interface IPermissionRepository
{
    Task<Permission> Create(Permission permission);
    Task<Permission> Update(Permission permission);
    Task<Permission> Delete(int id);
}