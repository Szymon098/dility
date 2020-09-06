using System;
using Newtonsoft.Json;

namespace EfficiencyApp.Models.FileDataModels
{
    public class FileAction
    {
        //public int Index { get; set; }
        public int Hour { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public ActionType FixedType
        {
            get
            {
                return Type switch
                {
                    "dodawanie" => ActionType.Add,
                    "dodanie" => ActionType.Add,
                    "przesuwanie" => ActionType.Move,
                    "przesuniecie" => ActionType.Move,
                    "usuniecie" => ActionType.Delete,
                    "usuwanie" => ActionType.Delete,
                    "Add" => ActionType.Add,
                    "Delete" => ActionType.Delete,
                    "Move" => ActionType.Move,
                    _ => throw new Exception(),
                };
            }
        }
    }
}
