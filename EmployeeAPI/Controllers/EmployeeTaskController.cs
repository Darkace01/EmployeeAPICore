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
    public class EmployeeTaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTask
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetEmployeeTask()
        {
            var response = new List<EmployeeTaskBasicResponseDTO>();
            var employeeTask = await _context.EmployeeTask.Include(e => e.Employee).ToListAsync();
            if (employeeTask == null || !employeeTask.Any())
                return Ok(response);
            response = GetEmployeeTaskResponseAll(employeeTask);
            return Ok(response);
        }

        // GET: api/EmployeeTask/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTask>> GetEmployeeTask(int id)
        {
            var employeeTask = await _context.EmployeeTask.FindAsync(id);

            if (employeeTask == null)
            {
                return NotFound();
            }

            return employeeTask;
        }

        [HttpGet("byEmployeeId/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetEmployeeTaskByEmployeeId(int id)
        {
            var employeeTask = await _context.EmployeeTask.Where(e => e.EmployeeId == id).ToListAsync();
            return employeeTask;
        }

        // PUT: api/EmployeeTask/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTask(int id, EmployeeTask employeeTask)
        {
            if (employeeTask == null)
                return BadRequest("Request is null");
            if (!ModelState.IsValid)
                return BadRequest("Data Validation error!");
            if (id != employeeTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTaskExists(id))
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

            return Ok(employeeTask);
        }

        // POST: api/EmployeeTask
        [HttpPost]
        public async Task<ActionResult<EmployeeTask>> PostEmployeeTask(EmployeeTask employeeTask)
        {
            if (employeeTask == null)
                return BadRequest("Request is null");
            if (!ModelState.IsValid)
                return BadRequest("Data Validation error!");
            _context.EmployeeTask.Add(employeeTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeTask", new { id = employeeTask.Id }, employeeTask);
        }

        // DELETE: api/EmployeeTask/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeTask>> DeleteEmployeeTask(int id)
        {
            var employeeTask = await _context.EmployeeTask.FindAsync(id);
            if (employeeTask == null)
            {
                return NotFound();
            }

            _context.EmployeeTask.Remove(employeeTask);
            await _context.SaveChangesAsync();

            return employeeTask;
        }

        private bool EmployeeTaskExists(int id)
        {
            return _context.EmployeeTask.Any(e => e.Id == id);
        }

        private EmployeeTaskBasicResponseDTO GetEmployeeTaskResponse(EmployeeTask emp)
        {
            var employ = new EmployeeTaskBasicResponseDTO
            {
                Id = emp.Id,
                Dealine = emp.Dealine,
                Name = emp.Name,
                StartTime = emp.StartTime,
                Employee = emp.Employee,
                EmployeeId = emp.EmployeeId
            };
            
            return employ;
        }
        private List<EmployeeTaskBasicResponseDTO> GetEmployeeTaskResponseAll(List<EmployeeTask> employeeTask)
        {
            var result = new List<EmployeeTaskBasicResponseDTO>();
            foreach (var item in employeeTask)
            {
                result.Add(GetEmployeeTaskResponse(item));
            }
            return result;
        }

    }
}
