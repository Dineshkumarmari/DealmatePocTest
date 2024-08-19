using DealmateApi.Domain.Aggregates;

namespace DealmateApi.Service.Enforcer;

public interface IEnforcer
{
    Task EnforceAsync(string permission);
}