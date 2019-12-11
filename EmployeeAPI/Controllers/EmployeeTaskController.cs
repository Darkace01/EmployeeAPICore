﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;
using EmployeeAPI.Model;

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
            return await _context.EmployeeTask.Include(e => e.Employee).ToListAsync();
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

        // PUT: api/EmployeeTask/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTask(int id, EmployeeTask employeeTask)
        {
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

            return Ok(employeeTask);
        }

        // POST: api/EmployeeTask
        [HttpPost]
        public async Task<ActionResult<EmployeeTask>> PostEmployeeTask(EmployeeTask employeeTask)
        {
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
    }
}
