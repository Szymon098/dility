using AutoMapper;
using System.Collections.Generic;

using EfficiencyApp.Models;

namespace EfficiencyApp.Dtos
{
    public class EmployeeDetails : EmployeeBasicDetails
    {
        public ActionType MostPerformedType { get; set; }
        public ActionType LeastPerformedType { get; set; }
        public float Efficiency { get; set; }
        public int PerformedMinutes { get; set; }

        public static EmployeeDetails EmployeeDetailsFromBasicDetails(EmployeeBasicDetails basicDetails)
        {
            var config = new MapperConfiguration
                (cfg => cfg.CreateMap<EmployeeBasicDetails, EmployeeDetails>());

            var mapper = config.CreateMapper();

            return mapper.Map<EmployeeBasicDetails, EmployeeDetails>(basicDetails);
        }
    }
}