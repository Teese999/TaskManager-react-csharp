using System;
namespace TaskManager.Buissines.Models
{
	public class ProjectTaskAddRequestModel
	{
		public string Name { get; set; } = "defaultTaskName";
        public Guid ProjectId { get; set; }
        public DateTime StartDate { get; set; }

    }
}

