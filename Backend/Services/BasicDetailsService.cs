using System.Collections.Generic;
using System.Linq;
using EfficiencyApp.Models.DbDataModels;
using EfficiencyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using EfficiencyApp.Dtos;

namespace EfficiencyApp.Services
{
    public class BasicDetailsService
    {
        private AppContext _context { get; set; }
        private DateTime _workdayDate { get; set; }

        public BasicDetailsService(AppContext context)
        {
            _context = context;
        }
        public BasicDetailsService(AppContext context, DateTime date)
        {
            _workdayDate = date;
            _context = context;
        }

        public void ChangeWorkdayDate(DateTime date)
        {
            _workdayDate = date;
        }

        public EmployeesBasicDetailsOnWorkday GetEmployeesBasicDetailsOnWorkday()
        {
            return new EmployeesBasicDetailsOnWorkday()
            {
                WorkdayDate = _workdayDate,
                EmployeesBasicDetails = GetEmployeesBasicDetails()
            };
        }

        public List<EmployeeBasicDetails> GetEmployeesBasicDetails()
        {
            var employees = GetLastDayEmployees();
            var countedActions = GetIdsWithCountedActionsOnDay();

            var result = new List<EmployeeBasicDetails>();
            var index = 1;

            foreach (var employee in employees)
            {
                var performedJobs = countedActions
                    .Where(a => a.Key == employee.EmployeeId)
                    .Select(a => a.Value)
                    .Single();

                var employeeBasicDetails = EmployeeBasicDetails.FromEmployee(employee);
                employeeBasicDetails.Index = index++;
                employeeBasicDetails.PerformedJobs = performedJobs;

                result.Add(employeeBasicDetails);
            }
            return result;
        }

        public List<Employee> GetLastDayEmployees()
        {
            return _context.Employees
                           .Join(
                               _context.UsersActions,
                               e => e.Id,
                               u => u.EmployeeId,
                               (employees, usersActions) => new
                               {
                                   Employees = employees,
                                   UsersActions = usersActions
                               }
                           )
                           .Join(
                               _context.DayActions,
                               u => u.UsersActions.DayActionsId,
                               d => d.Id,
                               (usersActions, dayAction) => new
                               {
                                   Date = dayAction.Date,
                                   Employees = usersActions.Employees
                               }
                               )
                           .Where(d => d.Date == _workdayDate)
                           .Select(e => e.Employees)
                           .ToList();
        }

        //return string: employeeId with int:counted actions, on day set in field
        private Dictionary<string, int> GetIdsWithCountedActionsOnDay()
        {
            var countedActions = new Dictionary<string, int>();

            var idsAndActionsCount = _context.UsersActions
                                        .Join(
                                            _context.DayActions,
                                            u => u.DayActionsId,
                                            d => d.Id,
                                            (usersActions, dayActions) => new
                                            {
                                                usersActions.Actions,
                                                dayActions.Date,
                                                usersActions.EmployeeId
                                            }
                                        )
                                        .Join(
                                            _context.Employees,
                                            u => u.EmployeeId,
                                            e => e.Id,
                                            (usersActions, employees) => new
                                            {
                                                usersActions.Date,
                                                usersActions.Actions,
                                                employees.EmployeeId
                                            }
                                        )
                                        .Where(u => u.Date == _workdayDate)
                                        .Select(u => new { u.EmployeeId, u.Actions.Count })
                                        .ToList();

            foreach (var idAndActionsCount in idsAndActionsCount)
                countedActions.Add(idAndActionsCount.EmployeeId, idAndActionsCount.Count);
            return countedActions;
        }

        public List<Models.DbDataModels.Action> GetEmployeeActionsOnDay(string id)
        {
            return _context.Actions
                .Include(a => a.UserAction)
                    .ThenInclude(ua => new
                    {
                        ua.DayAction,
                        ua.Employee,
                    })
                    .Where(a => a.UserAction.DayAction.Date == _workdayDate)
                    .Where(a => a.UserAction.Employee.EmployeeId == id)
                    .ToList();
        }
    }
}