using EfficiencyApp.Models.DbDataModels;
using EfficiencyApp.Models.FileDataModels;
using AutoMapper;

namespace EfficiencyApp.Models
{
    public class EmployeeBasicDetails : FileEmployee
    {
        public int PerformedJobs { get; set; }

        public EmployeeBasicDetails() { }

        public static EmployeeBasicDetails FromEmployee(Employee user)
        {
            var config = new MapperConfiguration
                (cfg => cfg.CreateMap<Employee, EmployeeBasicDetails>());

            var mapper = config.CreateMapper();

            return mapper.Map<Employee, EmployeeBasicDetails>(user);
        }
    }
}