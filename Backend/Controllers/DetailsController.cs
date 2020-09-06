using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EfficiencyApp.Services;
using EfficiencyApp.Dtos;
using EfficiencyApp.Models.FileDataModels;
using EfficiencyApp.Models.DbDataModels;
using Microsoft.AspNetCore.Cors;

namespace EfficiencyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class DetailsController : ControllerBase
    {
        private BasicDetailsService _basicDetailsService { get; set; }
        private DatabaseService _dbService { get; set; }
        public AppContext _context { get; set; }
        private IMapper _mapper { get; set; }

        public DetailsController()
        {
            _context = new AppContext();
            _basicDetailsService = new BasicDetailsService(_context);
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<FileEmployee, Employee>()).CreateMapper();
            _dbService = new DatabaseService(_context, _mapper);
        }

        [HttpGet("/details/newest")] // localhost:123/details/newest
        public ActionResult<EmployeesBasicDetailsOnWorkday> GetEmployeeBasicDetailsOnWorkday()
        {
            if (_dbService.CheckIfAnyRecordExist())
            {
                _basicDetailsService.ChangeWorkdayDate(_dbService.GetLatestDate());
                var result = _basicDetailsService.GetEmployeesBasicDetailsOnWorkday();
                return Ok(result);
            }

            return RedirectToAction("GetEmployeeBasicDetailsOnWorkdayFromLastAddedDay");
        }

        [HttpGet("/details/newest-added-file")] // localhost:123/details/newest-added
        public ActionResult<EmployeesBasicDetailsOnWorkday> GetEmployeeBasicDetailsOnWorkdayFromLastAddedDay()
        {
            var lastDate = _dbService.GetLatestFileSavedDay();
            _basicDetailsService.ChangeWorkdayDate(lastDate);

            if (_dbService.DoesWorkdayExist(lastDate) == false)
                _dbService.AddWorkday();

            var result = _basicDetailsService.GetEmployeesBasicDetailsOnWorkday();

            return Ok(result);
        }

        [HttpGet("/details/employee/{id}/{date}")] // localhost:123/details/employee/"id"/"date"
        public ActionResult<EmployeeDetails> GetEmployeeDetails(string id, DateTime date)
        {
            _basicDetailsService.ChangeWorkdayDate(date);
            var choosenEmployeeBasicDetails = _basicDetailsService.GetEmployeesBasicDetails()
                .Find(e => e.EmployeeId == id);

            var result = new EmployeeDetailsService(_context, date)
             .GetEmployeeDetails(choosenEmployeeBasicDetails);
            return Ok(result);
        }

        [HttpGet("/details/{date}")] // localhost:123/details/"date"
        public ActionResult<EmployeesBasicDetailsOnWorkday> GetEmployeeBasicDetailsFromAnotherWorkday(DateTime date)
        {
            if (_dbService.DoesWorkdayExist(date) == false)
                return new EmployeesBasicDetailsOnWorkday();

            _basicDetailsService.ChangeWorkdayDate(date);

            var result = _basicDetailsService.GetEmployeesBasicDetailsOnWorkday();
            return Ok(result);
        }

    }
}
