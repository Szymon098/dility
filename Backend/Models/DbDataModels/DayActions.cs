using System;
using System.Collections.Generic;
using System.IO;
using EfficiencyApp.Models.FileDataModels;

namespace EfficiencyApp.Models.DbDataModels
{
    public class DayActions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual List<UserActions> UsersActions { get; set; }

        public DayActions()
        {
        }

        public DayActions(FileDayActions fileDay)
        {
            Date = fileDay.FormattedDate;
        }
    }
}
