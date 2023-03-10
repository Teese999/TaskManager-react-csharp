using System;
namespace TaskManager.Buissines.Models
{
	public class TasksTableModel
	{
		public ICollection<ProjectTaskResponceModel> Tasks {get; set;}
		public string SummaryTime { get; set; }
	}
}

