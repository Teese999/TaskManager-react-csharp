using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Data.Models
{
	public class Project : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
		public string ProjectName { get; set; }
		public DateTime CreateDate { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}

