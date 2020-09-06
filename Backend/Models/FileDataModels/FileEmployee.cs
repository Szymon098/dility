using EfficiencyApp.Models.DbDataModels;
using AutoMapper;

namespace EfficiencyApp.Models.FileDataModels
{
    public class FileEmployee
    {
        public int Index { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static FileEmployee FromEmployee(IMapper mapper, Employee employee)
        {
            return mapper.Map<Employee, FileEmployee>(employee);
        }
    }

}
