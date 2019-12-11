using EmployeeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.DTO
{
    public class EmployeeBasicResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<EmployeeTask> EmployeeTasks { get; set; }
    }
    public class EmployeeTaskBasicResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Dealine { get; set; }
        public int EmployeeId { get; set; }
    }
}
