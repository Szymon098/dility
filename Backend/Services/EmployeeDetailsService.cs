using System;
using System.Collections.Generic;
using EfficiencyApp.Models;
using EfficiencyApp.Dtos;

namespace EfficiencyApp.Services
{
    public class EmployeeDetailsService
    {
        private BasicDetailsService _basicDetailsService { get; set; }

        public EmployeeDetailsService(AppContext context, DateTime date)
        {
            _basicDetailsService = new BasicDetailsService(context, date);
        }

        public EmployeeDetails GetEmployeeDetails(EmployeeBasicDetails basicDetails)
        {
            var actions = _basicDetailsService.GetEmployeeActionsOnDay
                (basicDetails.EmployeeId);
            var performedMinutes = CountPerformedMinutes(actions);
            var efficiency = CalculateEfficiency(performedMinutes);
            var countedTypes = CountPerformedTypes(actions);
            var mostPerformedType = GetMostPerformedTypes(countedTypes);
            var leastPerformedType = GetLeastPerformedTypes(countedTypes);

            var result = EmployeeDetails.EmployeeDetailsFromBasicDetails(basicDetails);

            result.PerformedMinutes = performedMinutes;
            result.Efficiency = efficiency;
            result.MostPerformedType = mostPerformedType;
            result.LeastPerformedType = leastPerformedType;

            return result;
        }

        private float CalculateEfficiency(int performedMinutes)
        {
            var result = 0f;
            result += performedMinutes;
            result /= 480;

            return (float)Math.Round(result * 100f) / 100f;
        }

        private int CountPerformedMinutes
            (List<Models.DbDataModels.Action> actions)
        {
            var result = 0;

            foreach (var action in actions)
                result += (int)Enum.Parse(typeof(ActionType), action.Type);

            return result;
        }

        //string: Action name, int: action quantity        
        private Dictionary<ActionType, int> CountPerformedTypes
            (List<Models.DbDataModels.Action> actions)
        {
            var types = new Dictionary<ActionType, int>();

            foreach (var action in actions)
            {
                var type = (ActionType)Enum.Parse(typeof(ActionType), action.Type);

                if (types.ContainsKey(type))
                    types[type]++;
                else
                    types[type] = 1;
            }
            return types;
        }

        private ActionType GetMostPerformedTypes(Dictionary<ActionType, int> countedTypes)
        {
            var max = 0;
            ActionType result = 0;
            foreach (var countedType in countedTypes)
            {
                if (max < countedType.Value)
                {
                    result = countedType.Key;
                    max = countedType.Value;
                }
                else if (max == countedType.Value)
                    result = countedType.Key;
            }
            return result;
        }

        private ActionType GetLeastPerformedTypes(Dictionary<ActionType, int> countedTypes)
        {
            var min = 99999;
            ActionType result = 0;

            foreach (var countedType in countedTypes)
            {
                if (min > countedType.Value)
                {
                    result = countedType.Key;
                    min = countedType.Value;
                }
                else if (min == countedType.Value)
                    result = countedType.Key;
            }
            return result;
        }
    }
}