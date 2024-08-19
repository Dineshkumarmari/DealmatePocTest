using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace DealmateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionRepository rolePermissionRepository;
        private readonly IRepository<RolePermission> repository;
        public RolePermissionController(IRolePermissionRepository rolePermissionRepository, IRepository<RolePermission> repository)
        {
            this.rolePermissionRepository = rolePermissionRepository;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await repository.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await repository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolePermission rolePermission)
        {
            return Ok(await rolePermissionRepository.Create(rolePermission));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RolePermission rolePermission)
        {
            return Ok(await rolePermissionRepository.Update(rolePermission));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await rolePermissionRepository.Delete(id));
        }
    }
}