using System.Collections.Generic;

namespace EfficiencyApp.Models.FileDataModels
{
    public class FileUserActions
    {
        public string EmployeeId { get; set; }
        public List<FileAction> Actions { get; set; }
    }
}