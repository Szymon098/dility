using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using EfficiencyApp.Services;
using EfficiencyApp.Models.FileDataModels;
using EfficiencyApp.Extensions;

namespace EfficiencyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]

    public class WorkdayController : ControllerBase
    {
        private DatabaseService _dbService { get; set; }
        public AppContext _context { get; set; }
        private IMapper _mapper { get; set; }


        public WorkdayController()
        {
            _context = new AppContext();
            _dbService = new DatabaseService(_context, _mapper);
        }

        [HttpGet("/workday/is-exist/{date}")] //localhost:123/workday/is-exist/"date"
        public ActionResult<bool> IsDayExistInDb(DateTime date)
        {
            return _dbService.DoesWorkdayExist(date);
        }

        [HttpPost("/workday/actions")] //localhost:123/workday/actions
        public ActionResult AddDayActions([FromBody] FileDayActions dayActions)
        {
            dayActions.Date = dayActions.Date.LocalFromUtc();
            var fileService = new FileService();
            fileService.SaveDayActionsInJsonFile(dayActions);
            _dbService.AddWorkday();

            return new EmptyResult();
        }
    }
}
