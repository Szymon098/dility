using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace EfficiencyApp.Models.FileDataModels
{
    public class FileDayActions
    {
        public DateTime Date { get; set; }
        public List<FileUserActions> UsersActions { get; set; }

        [JsonIgnore]

        public DateTime FormattedDate
        {
            get
            {
                var dateString = Date.ToString("dd.MM.yyyy");
                return Convert.ToDateTime(dateString);
            }
        }
    }
}
