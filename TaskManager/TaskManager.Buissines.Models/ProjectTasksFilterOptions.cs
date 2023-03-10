using System;
namespace TaskManager.Buissines.Models
{
	public class ProjectTasksFilterOptions
	{
		public Guid? ProjectId { get; set; }
		public DateTime FromDate { get; set; }
	}
}

