// <copyright file="IOTests.cs" company="Kohler, Brice">
// Copyright (c) Kohler, Brice. All rights reserved.
// </copyright>

namespace ExercicePPTest.UtilitiesTest
{
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using ExercicePP.Models;
    using ExercicePP.Utilities;
    using Newtonsoft.Json;

    /// <summary>
    /// Testing IO.
    /// </summary>
    public class IOTests
    {
        /// <summary>
        /// Tests save and import data.
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <returns>Nothing.</returns>
        [TestCase(@".\TaskListTEST.txt")]
        public async Task TransformData(string filename)
        {
            var tasks = new ObservableCollection<Task_>()
            {
               new Task_
            {
                ID = 1,
                TaskName = "name",
                Description = "description",
                Responsible = "AAA",
                Delay = new DateTime(2023, 1, 1),
                Done = true,
            },
               new Task_
            {
                ID = 2,
                TaskName = "name",
                Description = "description",
                Responsible = "AAA",
                Delay = new DateTime(2023, 1, 1),
                Done = true,
            },
            };

            await IO.SaveTasksAsync(tasks, filename);

            var result = await IO.GetTasksAsync(filename);

            Assert.IsTrue(this.CompareTaskList(tasks, result));

            File.Delete(filename);
        }

        private bool CompareTaskList(ObservableCollection<Task_> l1, ObservableCollection<Task_> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i].ID != l2[i].ID ||
                    l1[i].TaskName != l2[i].TaskName ||
                    l1[i].Description != l2[i].Description ||
                    l1[i].Delay != l2[i].Delay ||
                    l1[i].Responsible != l2[i].Responsible ||
                    l1[i].Done != l2[i].Done)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tests CompareTaskList.
        /// </summary>
        [Test]
        public void CompareTaskListTest()
        {
            bool test = this.CompareTaskList(
                new ObservableCollection<Task_>()
                {
                    new Task_()
                    {
                        ID = 1,
                        TaskName = "name",
                        Description = "description",
                        Responsible = "AAA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                    new Task_()
                    {
                        ID = 2,
                        TaskName = "name",
                        Description = "description",
                        Responsible = "AAA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                },
                new ObservableCollection<Task_>()
                {
                    new Task_()
                    {
                        ID = 1,
                        TaskName = "name",
                        Description = "description",
                        Responsible = "AAA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                    new Task_()
                    {
                        ID = 2,
                        TaskName = "name",
                        Description = "description",
                        Responsible = "AAA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                });
            Assert.IsTrue(test);

            bool testfalse = this.CompareTaskList(
                new ObservableCollection<Task_>()
                {
                    new Task_()
                    {
                        ID = 1,
                        TaskName = "name1",
                        Description = "description1",
                        Responsible = "AAA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                    new Task_()
                    {
                        ID = 2,
                        TaskName = "name",
                        Description = "description",
                        Responsible = "AA1",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                },
                new ObservableCollection<Task_>()
                {
                    new Task_()
                    {
                        ID = 1,
                        TaskName = "name",
                        Description = "descr1ption",
                        Responsible = "AAA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                    new Task_()
                    {
                        ID = 2,
                        TaskName = "name",
                        Description = "description",
                        Responsible = "AbA",
                        Delay = new DateTime(2023, 1, 1),
                        Done = true,
                    },
                });
            Assert.IsTrue(test);
        }
    }
}
