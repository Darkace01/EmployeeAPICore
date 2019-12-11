using EmployeeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.DTO
{
    public class EmployeeTaskBasicResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Dealine { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
