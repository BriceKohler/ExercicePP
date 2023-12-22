// <copyright file="IO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExercicePP.Utilities
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using ExercicePP.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to import and export data.
    /// </summary>
    public static class IO
    {

        /// <summary>
        /// Getting Data from "filename" file.
        /// </summary>
        /// <param name="filename"> Filename's adress.</param>
        /// <returns>ObservableCollection if tasks.</returns>
        public static async Task<ObservableCollection<Task_>> GetTasksAsync(string filename)
        {
            ObservableCollection<Task_> list = new ObservableCollection<Task_>();
            await Task.Run(() =>
            {
                string text = GetText(filename);

                if (text != string.Empty)
                {
                    list = JsonConvert.DeserializeObject<ObservableCollection<Task_>>(text);
                }
                else
                {
                    list = TestData();
                }
            });
            return list;
        }

        /// <summary>
        /// Imports text from file's filename.
        /// </summary>
        /// <param name="filename">Filename.</param>
        /// <returns>File text.</returns>
        public static string GetText(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllText(filename);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Saving Data to "filename" file.
        /// </summary>
        /// <param name="tasks">Tasks' list.</param>
        /// <param name="filename">Filename.</param>
        /// <returns>Nothing.</returns>
        public static async Task SaveTasksAsync(ObservableCollection<Task_> tasks, string filename)
        {
            await Task.Run(() =>
            {
                string list = JsonConvert.SerializeObject(tasks);
                File.WriteAllText(filename, list);
            });
        }

        /// <summary>
        /// Get a sample of task list.
        /// </summary>
        /// <returns>A sample of task list.</returns>
        public static ObservableCollection<Task_> TestData()
        {
            var list = new ObservableCollection<Task_>()
                {
                    new Task_
                    {
                        ID = 1,
                        TaskName = "ExpiredUndone",
                        Description = "This task has expired and is not done => Red ",
                        Responsible = "BKO",
                        Delay = DateTime.Now.AddDays(-2),
                        Done = false,
                    },
                    new Task_
                    {
                        ID = 2,
                        TaskName = "EmergencyUndone",
                        Description = "This task expires soon and is not done => Orange ",
                        Responsible = "CZA",
                        Delay = DateTime.Now.AddDays(2),
                        Done = false,
                    },
                    new Task_
                    {
                        ID = 3,
                        TaskName = "NotEmergencyUndone",
                        Description = "This task expires late (>7 days) and is not done => Green ",
                        Responsible = "BKO",
                        Delay = DateTime.Now.AddDays(8),
                        Done = false,
                    },
                    new Task_
                    {
                        ID = 4,
                        TaskName = "ExpiredDone",
                        Description = "This task has expired and is done => Green ",
                        Responsible = "ETH",
                        Delay = DateTime.Now.AddDays(-2),
                        Done = true,
                    },
                    new Task_
                    {
                        ID = 5,
                        TaskName = "EmergencyDone",
                        Description = "This task expires soon and is done => Green ",
                        Responsible = "BKO",
                        Delay = DateTime.Now.AddDays(2),
                        Done = true,
                    },
                    new Task_
                    {
                        ID = 6,
                        TaskName = "NotEmergencyDone",
                        Description = "This task expires late (>7 days) and is done => Green ",
                        Responsible = "CZA",
                        Delay = DateTime.Now.AddDays(8),
                        Done = true,
                    },
                };

            return list;
        }
    }
}