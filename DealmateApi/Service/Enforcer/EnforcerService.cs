using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.DB;
using DealmateApi.Service.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace DealmateApi.Service.Enforcer;

public class EnforcerService : IEnforcer
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public EnforcerService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        this._context = context;
    }
    public async Task EnforceAsync(string permission)
    {
        var userClaims = _httpContextAccessor.HttpContext?.User.Claims;

        if (userClaims == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        var name = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var roleClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(roleClaim))
        {
            throw new UnauthorizedAccessException("Role claim is missing.");
        }
        var roleId = Convert.ToInt64(roleClaim);
        if (!await CheckUserPermissions(roleId, permission))
        {
            throw new UnauthorizedAccessException($"{name} is not have {permission} Permission for this.");
        }
    }

    private async Task<bool> CheckUserPermissions(long roleId, string permission)
    {
        var includeProperties = GetIncludeProperties(typeof(RolePermission));
        IQueryable<RolePermission> _query = _context.Set<RolePermission>();
        foreach (var includeProperty in includeProperties)
        {
            _query = _query.Include(includeProperty);
        }
        var rolePermissions = await _query.Where(x => x.RoleId == roleId).ToListAsync();
        return rolePermissions.Any(rp => rp.Permission!.Name.Equals(permission, StringComparison.OrdinalIgnoreCase));
    }
    private IEnumerable<string> GetIncludeProperties(Type type)
    {
        var includeProperties = new List<string>();

        var properties = type.GetProperties()
            .Where(p => p.GetCustomAttribute<IncludeAttribute>() != null);

        foreach (var property in properties)
        {
            includeProperties.Add(property.Name);
            var propertyType = property.PropertyType;

            // Handle nested properties
            if (propertyType.IsClass && propertyType != typeof(string))
            {
                // Recursively get nested includes
                includeProperties.AddRange(GetNestedIncludes(propertyType, property.Name));
            }
        }

        return includeProperties;
    }

    private static IEnumerable<string> GetNestedIncludes(Type type, string parentProperty)
    {
        var nestedIncludes = new List<string>();
        var properties = type.GetProperties()
            .Where(p => p.GetCustomAttribute<IncludeAttribute>() != null);

        foreach (var property in properties)
        {
            var propertyName = property.Name;
            nestedIncludes.Add($"{parentProperty}.{propertyName}");

            var propertyType = property.PropertyType;

            // Handle further nesting
            if (propertyType.IsClass && propertyType != typeof(string))
            {
                nestedIncludes.AddRange(GetNestedIncludes(propertyType, $"{parentProperty}.{propertyName}"));
            }
        }

        return nestedIncludes;
    }
}