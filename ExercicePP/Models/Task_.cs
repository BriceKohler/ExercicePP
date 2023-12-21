using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicePP.Models
{
    public class Task_
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public DateTime Delay { get; set; }
        public bool Done { get; set; }

        public Task_ Copy()
        {
            return new Task_()
            {

                ID = ID,
                TaskName = TaskName,
                Description = Description,
                Responsible = Responsible,
                Delay = Delay,
                Done = Done
            };
            
        }
    }
}
