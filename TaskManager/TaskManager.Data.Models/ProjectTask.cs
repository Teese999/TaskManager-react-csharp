using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Data.Models
{
	public class ProjectTask : IEntity, ICloneable
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CancelDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual ICollection<TaskComment> TaskComments { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

