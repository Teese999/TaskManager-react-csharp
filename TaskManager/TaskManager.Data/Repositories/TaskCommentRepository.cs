using System;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskCommentRepository : AbstractRepository<TaskComment>, ITaskCommentRepository
    {
        public TaskCommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}

