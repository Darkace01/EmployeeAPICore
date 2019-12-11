using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;
using EmployeeAPI.Model;
using EmployeeAPI.DTO;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            var response = new List<EmployeeBasicResponseDTO>();
            var employee = await _context.Employee.Include(s => s.EmployeeTasks).ToListAsync();
            if (employee == null || !employee.Any())
                return Ok(response);
            response = GetEmployeeResponseAll(employee);
            return Ok(response);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (employee == null)
                return BadRequest("Request is null");
            if (!ModelState.IsValid)
                return BadRequest("Data Validation error!");

            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (employee == null)
                return BadRequest("Request is null");
            if (!ModelState.IsValid)
                return BadRequest("Data Validation error!");
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
        private EmployeeBasicResponseDTO GetEmployeeResponse(Employee emp)
        {
            var employ = new EmployeeBasicResponseDTO
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                HiredDate = emp.HiredDate,
            };
            if (emp.EmployeeTasks != null && emp.EmployeeTasks.Any())
            {
                var employeeTask = new List<EmployeeTaskBasicResponseDTO>();
                foreach (var task in emp.EmployeeTasks)
                {
                    employeeTask.Add(
                        new EmployeeTaskBasicResponseDTO
                        {
                            Id = task.Id,
                            Name = task.Name,
                            Dealine = task.Dealine,
                            StartTime = task.StartTime,
                            EmployeeId = task.EmployeeId
                        });
                }
            }
            return employ;
        }
        private List<EmployeeBasicResponseDTO> GetEmployeeResponseAll(List<Employee> employees)
        {
            var result = new List<EmployeeBasicResponseDTO>();
            foreach (var item in employees)
            {
                result.Add(GetEmployeeResponse(item));
            }
            return result;
        }
    }
}
