using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Buissines.Models;
public class TaskCommentReuestModel
{
    public Guid TaskId { get; set; }
    public int? CommentType { get; set; }
    public string? Text { get; set; }
    public IFormFile? File { get; set; }

}

