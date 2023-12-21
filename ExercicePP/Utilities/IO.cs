using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Documents;
using ExercicePP.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ExercicePP.Utilities
{
    public static class IO
    {

        /// <summary>
        /// Getting Data from "filename" file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static async Task<ObservableCollection<Task_>> GetTasksAsync(string filename)
        {
            ObservableCollection<Task_> list = new ObservableCollection<Task_>();
            string text = GetText(filename);

            if (text != "") return JsonConvert.DeserializeObject<ObservableCollection<Task_>>(text);
            return TestData();
        }

        public static string GetText(string filename)
        {
            if (File.Exists(filename)) return File.ReadAllText(filename);
            else return "";
        }

        /// <summary>
        /// Saving Data to "filename" file
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static async Task SaveTasksAsync(ObservableCollection<Task_> tasks, string filename)
        {
            string list = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(filename, list);
        }


        public static ObservableCollection<Task_> TestData()
        {
            var list = new ObservableCollection<Task_>()
                {   new Task_
                    {
                        ID = 1,
                        TaskName =    "ExpiredUndone",
                        Description = "This task has expired and is not done => Red ",
                        Responsible = "BKO",
                        Delay = DateTime.Now.AddDays(-2),
                        Done = false
                    },
                    new Task_
                    {
                            ID = 2,
                        TaskName =    "EmergencyUndone",
                        Description = "This task expires soon and is not done => Orange ",
                        Responsible = "CZA",
                        Delay = DateTime.Now.AddDays(2),
                        Done = false
                    },
                    new Task_
                    {
                        ID = 3,
                        TaskName =    "NotEmergencyUndone",
                        Description = "This task expires late (>7 days) and is not done => Green ",
                        Responsible = "BKO",
                        Delay = DateTime.Now.AddDays(8),
                        Done = false
                    },
                     new Task_
                    {
                        ID = 4,
                        TaskName = "ExpiredDone",
                        Description ="This task has expired and is done => Green ",
                        Responsible = "ETH",
                        Delay =DateTime.Now.AddDays(-2),
                        Done = true
                    },
                    new Task_
                    {
                            ID = 5,
                        TaskName =   "EmergencyDone",
                        Description ="This task expires soon and is done => Green ",
                        Responsible = "BKO",
                        Delay = DateTime.Now.AddDays(2),
                        Done = true
                    },
                    new Task_
                    {
                        ID = 6,
                        TaskName =    "NotEmergencyDone",
                        Description = "This task expires late (>7 days) and is done => Green ",
                        Responsible = "CZA",
                        Delay = DateTime.Now.AddDays(8),
                        Done = true
                    }
                };

            return list;
        }
    }

}

