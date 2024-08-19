using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;

namespace DealmateApi.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly IRepository<Branch> repository;
    private readonly IRepository<Dealer> dealerRepository;
    public BranchRepository(IRepository<Branch> repository, IRepository<Dealer> dealerRepository)
    {
        this.repository = repository;
        this.dealerRepository = dealerRepository;
    }

    public async Task<Branch> Create(Branch branch)
    {
        var existBranch = await repository.FirstOrDefaultAsync(x => x.Name == branch.Name);
        if (existBranch != null)
        {
            throw new Exception($"The Branch {branch.Name} was already exist");
        }
        branch = await repository.AddAsync(branch);
        return branch;
    }

    public async Task<Branch> Update(Branch branch)
    {
        var existBranch = await repository.GetByIdAsync(branch.Id);
        if (existBranch == null)
        {
            throw new Exception($"The BranchID {branch.Id} not exist");
        }
        existBranch.Name = branch.Name;
        if (branch.DealerId != existBranch.DealerId)
        {
            var dealer = await dealerRepository.GetByIdAsync(branch.DealerId);
            if (dealer == null)
            {
                throw new Exception($"The DealerId {branch.DealerId} not exist");
            }
            existBranch.DealerId = branch.DealerId;
        }
        existBranch = await repository.Update(existBranch);
        return existBranch;
    }

    public async Task<Branch> Delete(int id)
    {
        var branch = await repository.GetByIdAsync(id);
        if (branch == null)
        {
            throw new Exception($"The Branch Id:{id} was not found");
        }
        branch = await repository.Remove(branch);
        return branch;
    }

}
