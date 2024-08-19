using DealmateApi.Domain.Aggregates;

namespace DealmateApi.Infrastructure.Interfaces;

public interface IDealerRepository
{
    Task<Dealer> Create(Dealer dealer);
    Task<Dealer> Update(Dealer dealer);
    Task<Dealer> Delete(int id);
}
