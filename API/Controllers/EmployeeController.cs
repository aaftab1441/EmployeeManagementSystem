using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // GET: api/Employees    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.ToList();
        }
        // GET: api/Employees/5    
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        // PUT: api/Employees/5    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
            return NoContent();
        }
        // POST: api/Employees    
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _employeeRepository.InsertAsync(employee);
            await _employeeRepository.SaveAsync();
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }
        // DELETE: api/Employees/5    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _employeeRepository.Delete(e => e.Id == id);
            await _employeeRepository.SaveAsync(); 
            return NoContent();
        }
    }
}
