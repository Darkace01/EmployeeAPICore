using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.MVC.ApiHelpers;
using EmployeeAPI.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeApi employeeAPI = new EmployeeApi();
        EmployeeTaskApi employeeTaskAPI = new EmployeeTaskApi();
        public async Task<IActionResult> Index()
        {
            var employees = await employeeAPI.GetAllEmployees();
            return View(employees.ToList());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Task = await employeeTaskAPI.GetAllEmployeeTasks();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = await employeeTaskAPI.GetEmployeeTaskById(model.EmployeeTasksId);
                EmployeeTask t = new EmployeeTask()
                {
                    Name = task.Name,
                    StartTime = task.StartTime,
                    Dealine = task.Dealine,
                    EmployeeId = model.Id,
                    Id= task.Id
                };
                model.EmployeeTasks.Add(t);
                // save
                await employeeAPI.CreateEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}