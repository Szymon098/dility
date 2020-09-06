using EfficiencyApp.Models.FileDataModels;
using AutoMapper;
using System.Collections.Generic;

namespace EfficiencyApp.Models.DbDataModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<UserActions> UserActions { get; set; }

        public static Employee FromFileEmployee(IMapper mapper, FileEmployee fEmployee)
        {
            return mapper.Map<FileEmployee, Employee>(fEmployee);
        }
    }
}