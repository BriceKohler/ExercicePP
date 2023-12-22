// <copyright file="Task_.cs" company="Kohler, Brice">
// Copyright (c) Kohler, Brice. All rights reserved.
// </copyright>

namespace ExercicePP.Models
{
    using System;

    /// <summary>
    /// Task object.
    /// </summary>
    public class Task_
    {
        /// <summary>
        /// Gets or sets id of task.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets name of task.
        /// </summary>
        public string? TaskName { get; set; }

        /// <summary>
        /// Gets or sets description of task.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets responsible of task.
        /// </summary>
        public string? Responsible { get; set; }

        /// <summary>
        /// Gets or sets delay of task.
        /// </summary>
        public DateTime Delay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whetherthe task is done or not.
        /// </summary>
        public bool Done { get; set; }

        /// <summary>
        /// Get Copy of object.
        /// </summary>
        /// <returns>Copy of task.</returns>
        public Task_ Copy()
        {
            return new Task_()
            {
                ID = this.ID,
                TaskName = this.TaskName,
                Description = this.Description,
                Responsible = this.Responsible,
                Delay = this.Delay,
                Done = this.Done,
            };
        }
    }
}
