using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace DealmateApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private readonly IRepository<Role> repository;
        public RoleController(IRoleRepository roleRepository, IRepository<Role> repository)
        {
            this.roleRepository = roleRepository;
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
        public async Task<IActionResult> Create(Role role)
        {
            return Ok(await roleRepository.Create(role));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Role role)
        {
            return Ok(await roleRepository.Update(role));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await roleRepository.Delete(id));
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetPermissions(int roleId)
        {
            return Ok(await roleRepository.GetPermissions(roleId));
        }
    }
}