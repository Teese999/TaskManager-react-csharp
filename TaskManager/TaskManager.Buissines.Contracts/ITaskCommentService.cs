using System;
using TaskManager.Buissines.Models;
using TaskManager.Data.Models;

namespace TaskManager.Buissines.Contracts
{
	public interface ITaskCommentService
	{
        Task<TaskCommentResponceModel> GetBykey(Guid id);
        Task<TaskCommentResponceModel> Add(TaskCommentReuestModel entity);
        Task<IEnumerable<TaskCommentResponceModel>> Delete(Guid id);
        Task<TaskCommentResponceModel> Update(TaskCommentReuestModel entity);
        Task<IEnumerable<TaskCommentResponceModel>> GetAll();
        Task<IEnumerable<TaskCommentResponceModel>> GetByTaskId(Guid taskId);
        Task<(byte[], string, string)> GetFile(Guid commentId);

    }
}

