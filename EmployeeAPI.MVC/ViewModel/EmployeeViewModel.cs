using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.MVC.ViewModel
{
    public class ViewEmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<EmployeeTask> EmployeeTasks { get; set; }
    }
    public class CreateEmployeeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hired Date")]
        public DateTime HiredDate { get; set; }
        [Display(Name ="Employee Task")]
        public int EmployeeTasksId { get; set; }
        public List<EmployeeTask> EmployeeTasks { get; set; }
    }
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<EmployeeTask> EmployeeTasks { get; set; }
    }
    public class EmployeeTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Dealine { get; set; }
        public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<EmployeeTask> EmployeeTasks { get; set; }
    }
}
