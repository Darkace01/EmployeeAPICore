using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.MVC.ApiHelpers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeApi employeeAPI = new EmployeeApi();
        public async Task<IActionResult> Index()
        {
            var employees = await employeeAPI.GetAllEmployees();
            return View(employees.ToList());
        }
    }
}