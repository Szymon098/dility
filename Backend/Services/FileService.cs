using System;
using EfficiencyApp.Models.FileDataModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using EfficiencyApp.Dtos;
using EfficiencyApp.Models.DbDataModels;

namespace EfficiencyApp.Services
{
    public class FileService
    {
        public void SaveDayActionsInJsonFile(FileDayActions dayActions)
        {
            using (StreamWriter file = File.CreateText(GetFileNameByDate(dayActions.FormattedDate, ".json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, dayActions);
            }
        }

        public void SaveEmployeesAsCsv(EmployeesWithDate employeesWithDate)
        {
            var content = GetEmployeesAsCsvContent(employeesWithDate.Employees);

            File.WriteAllText(GetFileNameByDate(employeesWithDate.Date, ".csv"), content);
        }

        private string GetEmployeesAsCsvContent(List<FileEmployee> employees)
        {
            StringBuilder content = new StringBuilder();
            content.Append("Id;First Name; Last Name;Identifier number\n");
            var index = 1;
            foreach (var employee in employees)
            {
                content.Append(index);
                content.Append(";");
                content.Append(employee.FirstName);
                content.Append(";");
                content.Append(employee.LastName);
                content.Append(";");
                content.Append(employee.EmployeeId);
                content.Append("\n");

                index++;
            }

            return content.ToString();
        }

        private string GetFileNameByDate(DateTime date, string extension)
        {
            StringBuilder fileName = new StringBuilder();
            if (extension == ".json")
                fileName.Append(@"..\Data\actions\");
            else if (extension == ".csv")
                fileName.Append(@"..\Data\users\");

            var fixedDate = date.ToShortDateString().Replace('.', '-');
            fileName.Append(fixedDate);
            fileName.Append(extension);

            return fileName.ToString();
        }
    }
}
