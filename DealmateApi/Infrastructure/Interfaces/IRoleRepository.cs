using DealmateApi.Domain.Aggregates;

namespace DealmateApi.Infrastructure.Interfaces;

public interface IRoleRepository
{
    Task<Role> Create(Role role);
    Task<Role> Update(Role role);
    Task<Role> Delete(int id);
    Task<List<Permission?>> GetPermissions(int id);
}
