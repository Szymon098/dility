using System;
using System.ComponentModel.DataAnnotations.Schema;
using EfficiencyApp.Models.FileDataModels;

namespace EfficiencyApp.Models.DbDataModels
{
    public class Action
    {
        public int Id { get; set; }
        public string Type { get; set; }
        [ForeignKey("UserAction")]
        public int UserActionId { get; set; }
        public virtual UserActions UserAction { get; set; }

        public Action() { }
        public Action(FileAction fileAction)
        {
            Type = fileAction.FixedType.ToString();
        }
    }
}
