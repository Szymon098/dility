using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfficiencyApp.Models.FileDataModels;

namespace EfficiencyApp.Dtos
{
    public class EmployeesWithDate
    {
        public List<FileEmployee> Employees { get; set; }
        public DateTime Date { get; set; }

        public EmployeesWithDate() { }

    }

}
