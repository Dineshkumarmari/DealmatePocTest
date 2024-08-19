using DealmateApi.Domain.Aggregates;

namespace DealmateApi.Infrastructure.Interfaces;

public interface IBranchRepository
{
    Task<Branch> Create(Branch branch);
    Task<Branch> Update(Branch branch);
    Task<Branch> Delete(int id);
}
