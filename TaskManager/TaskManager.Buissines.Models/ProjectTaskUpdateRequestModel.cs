using System;
namespace TaskManager.Buissines.Models
{
	public class ProjectTaskUpdateRequestModel
	{
		public string TaskName { get; set; }
		public Guid Id { get; set; }
		public DateTime StartDate { get; set; }
        public DateTime CancelDate { get; set; }

    }
}

