using System;
using System.Collections.Generic;
using System.Linq;
using EfficiencyApp.Models.FileDataModels;
using EfficiencyApp.Models.DbDataModels;
using EfficiencyApp.Repositories;
using AutoMapper;

namespace EfficiencyApp.Services
{
    public class DatabaseService
    {
        private AppContext _context { get; set; }
        private FileRepository _fileRepo { get; set; }
        private IMapper _mapper { get; set; }

        public DatabaseService(AppContext context, IMapper mapper)
        {
            _context = context;
            _fileRepo = new FileRepository();
            _mapper = mapper;
        }

        public DateTime GetLatestFileSavedDay()
        {
            return _fileRepo.GetLatestActions().FormattedDate;
        }

        public DateTime GetLatestDate()
        {
            return _context.DayActions
                            .Select(da => da.Date)
                            .Max();
        }

        public bool CheckIfAnyRecordExist()
        {
            if (_context.DayActions.Any())
                return true;
            return false;
        }

        public bool DoesWorkdayExist(DateTime date)
        {
            if (_context.DayActions
                .Any(da => da.Date == date))
                return true;

            return false;
        }

        public void AddWorkday()
        {
            var lastDayActions = _fileRepo.GetLatestActions();
            var fileEmployees = _fileRepo.GetUsers();

            AddEmployees(fileEmployees);

            var dbDayActions = new DayActions(lastDayActions);
            dbDayActions.UsersActions = GetUserActions(fileEmployees, lastDayActions);

            _context.DayActions.Add(dbDayActions);
            _context.SaveChanges();
        }

        public void AddEmployees(List<FileEmployee> fileEmployees)
        {
            var dbIds = _context.Employees
                                .Select(e => e.EmployeeId)
                                .ToList();

            foreach (var employee in fileEmployees)
            {
                if (!dbIds.Contains(employee.EmployeeId))
                {
                    _context.Employees
                        .Add(Employee.FromFileEmployee(_mapper, employee));
                }
            }
            _context.SaveChanges();
        }

        public List<UserActions> GetUserActions
            (List<FileEmployee> fileEmployees, FileDayActions dayActions)
        {
            var ids = new List<string>();
            var result = new List<UserActions>();

            foreach (var employee in fileEmployees)
                ids.Add(employee.EmployeeId);

            var dbEmployees = _context.Employees
                .Where(e => ids.Contains(e.EmployeeId))
                .ToList();

            foreach (var userActions in dayActions.UsersActions)
            {
                var newUserActions = new UserActions(userActions);

                newUserActions.Employee = dbEmployees.Where
                    (e => e.EmployeeId == userActions.EmployeeId).Single();

                result.Add(newUserActions);
            }

            return result;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public bool isEmployeeAdded(string employeeId)
        {
            var dbIds = _context.Employees
                                .Select(e => e.EmployeeId)
                                .ToList();

            if (dbIds.Contains(employeeId))
                return true;

            return false;
        }

        public void AddEmployee(FileEmployee fileEmployees)
        {
            var dbIds = _context.Employees
                                .Select(e => e.EmployeeId)
                                .ToList();

            if (!dbIds.Contains(fileEmployees.EmployeeId))
            {
                _context.Employees
                    .Add(Employee.FromFileEmployee(_mapper, fileEmployees));
            }

            _context.SaveChanges();
        }
    }
}