using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealmateApi.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IRepository<Employee> repository;
        public EmployeeController(IEmployeeRepository employeeRepository, IRepository<Employee> repository)
        {
            this.employeeRepository = employeeRepository;
            this.repository = repository;
        }

        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await repository.ListAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await repository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            return Ok(await employeeRepository.Create(employee));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            return Ok(await employeeRepository.Update(employee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await employeeRepository.Delete(id));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            return Ok(await employeeRepository.LogIn(email, password));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string email, string password)
        {
            return Ok(await employeeRepository.ChangePassword(email, password));
        }
                
    }
    
}
