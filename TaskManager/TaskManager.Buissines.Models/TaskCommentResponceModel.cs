using System;
namespace TaskManager.Buissines.Models
{
	public class TaskCommentResponceModel
	{
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public CommentType CommentType { get; set; }
        public string Text { get; set; }
        public object File { get; set; }

    }
}

