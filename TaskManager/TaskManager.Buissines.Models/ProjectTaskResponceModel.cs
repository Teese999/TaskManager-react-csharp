using System;
using Newtonsoft.Json;
namespace TaskManager.Buissines.Models
{
	public class ProjectTaskResponceModel
	{
        public int Index { get; set; }
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string StartDate { get; set; }
        public string CancelDate { get; set; }
        public string CreateDate { get; set; }
        public string SpendedTime { get; set; }
        [JsonIgnore]
        public double SpendedMinutes { get; set; }

    }
}

