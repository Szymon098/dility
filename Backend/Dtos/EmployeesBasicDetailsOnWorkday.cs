using System;
using System.Collections.Generic;
using EfficiencyApp.Models;
namespace EfficiencyApp.Dtos
{
    public class EmployeesBasicDetailsOnWorkday
    {
        public DateTime WorkdayDate { get; set; }
        public List<EmployeeBasicDetails> EmployeesBasicDetails { get; set; }

        public EmployeesBasicDetailsOnWorkday() { }
    }
}
