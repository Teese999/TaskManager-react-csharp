using System;
using TaskManager.Buissines.Models;
using TaskManager.Data.Models;

namespace TaskManager.Buissines.Contracts
{
	public interface IProjectTaskService
	{
        Task<ProjectTask> GetBykey(Guid id);
        Task<IEnumerable<ProjectTask>> GetAll();
        Task<ProjectTask> Add(ProjectTaskAddRequestModel entity);
        Task<TasksTableModel> GetTable(ProjectTasksFilterOptions? options = null);
        Task Delete(Guid id);
        Task<ProjectTask> Update(ProjectTaskUpdateRequestModel entity);
        Task<ProjectTask> Update(ProjectTask entity);
    }
}

