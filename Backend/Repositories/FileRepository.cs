using EfficiencyApp.Models.FileDataModels;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EfficiencyApp.Repositories
{
    public class FileRepository
    {
        public FileDayActions GetLatestActions()
        {
            var jsonContent = GetRecentModifiedFile
                (new DirectoryInfo(@"..\Data\actions"));
            return JsonConvert.DeserializeObject<FileDayActions>(jsonContent);
        }

        public List<FileEmployee> GetUsers()
        {
            var csvContent = GetRecentModifiedFile
                (new DirectoryInfo(@"..\Data\users"));
            var result = ParseUsers(csvContent);
            Console.WriteLine(csvContent);
            return result;
        }

        private List<FileEmployee> ParseUsers(string fileContent)
        {
            var result = new List<FileEmployee>();
            var lines = fileContent.Split('\n');

            foreach (var line in lines)
            {
                var params_ = line.Split(';');
                if (params_[0].Trim() == "Id")
                    continue;
                if (params_[0].Length == 0)
                    break;

                var user = new FileEmployee()
                {
                    Index = Int32.Parse(params_[0]),
                    FirstName = params_[1],
                    LastName = params_[2],
                    EmployeeId = params_[3]
                };

                user.EmployeeId = user.EmployeeId
                    .Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", "");

                result.Add(user);
            }
            return result;
        }

        private string GetRecentModifiedFile(DirectoryInfo directory)
        {
            var file = directory.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First();
            var fileName = file.ToString();

            return System.IO.File.ReadAllText(fileName);
        }
    }
}