using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TaskManager.Data.Models
{
	public class TaskComment : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public byte CommentType { get; set; }
        public string? FileName { get; set; }
        [JsonIgnore]
        public byte[] Content { get; set; }

    }
}

