using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EfficiencyApp.Services;
using EfficiencyApp.Dtos;
using EfficiencyApp.Models.FileDataModels;
using EfficiencyApp.Models.DbDataModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using EfficiencyApp.Extensions;

namespace EfficiencyApp.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private DatabaseService _dbService { get; set; }
        public AppContext _context { get; set; }
        private IMapper _mapper { get; set; }

        public EmployeesController()
        {
            _context = new AppContext();
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<FileEmployee, Employee>()).CreateMapper();
            _dbService = new DatabaseService(_context, _mapper);
        }

        [HttpGet("/employees")] //localhost:123/employees
        public ActionResult<List<FileEmployee>> GetFileEmployees()
        {
            var employees = _dbService.GetAllEmployees();
            var result = new List<FileEmployee>();
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, FileEmployee>()).CreateMapper();

            foreach (var employee in employees)
                result.Add(FileEmployee.FromEmployee(_mapper, employee));

            return result;
        }

        [HttpPost("/employees/files")] //localhost:123/employees/files
        public ActionResult AddEmployeesFiles([FromBody] EmployeesWithDate employeesWithDate)
        {
            employeesWithDate.Date = employeesWithDate.Date.LocalFromUtc();
            var fileService = new FileService();
            fileService.SaveEmployeesAsCsv(employeesWithDate);

            return new EmptyResult();
        }

        [HttpPost("/employee")] //localhost:123/employee
        public ActionResult AddEmployee([FromBody] FileEmployee employee)
        {
            _dbService.AddEmployee(employee);

            return new EmptyResult();
        }

        [HttpGet("/employee/is-added/{employeeId}")]
        public ActionResult<bool> isEmployeeAdded(string employeeId)
        {
            return _dbService.isEmployeeAdded(employeeId);
        }
    }
}