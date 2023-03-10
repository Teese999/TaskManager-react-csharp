using System;
using TaskManager.Data.Models;

namespace TaskManager.Data.Contracts
{
	public interface ITaskCommentRepository : IEntityRepository<TaskComment>
	{
	}
}

