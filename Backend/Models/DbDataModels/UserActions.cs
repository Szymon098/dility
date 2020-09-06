using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EfficiencyApp.Models.FileDataModels;

namespace EfficiencyApp.Models.DbDataModels
{
    public class UserActions
    {
        public int Id { get; set; }
        [ForeignKey("DayAction")]
        public int DayActionsId { get; set; }
        public DayActions DayAction { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual List<Action> Actions { get; set; }

        public UserActions() { }
        public UserActions(FileUserActions fUserActions)
        {
            Actions = new List<Action>();

            foreach (var fileAction in fUserActions.Actions)
            {
                Actions.Add(new Action(fileAction));
            }
        }
    }
}
