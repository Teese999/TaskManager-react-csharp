using System;
using TaskManager.Buissines.Models;
using TaskManager.Data.Models;

namespace TaskManager.Buissines.Contracts
{
	public interface IProjectService 
	{
		Task<Project> GetBykey(Guid id);
        Task<IEnumerable<Project>> GetAll();
		Task<Project> Add(Project entity);
		Task Delete(Guid id);
        Task<Project> Update(Project entity);
		Task<String> GetName(Guid id);
		Task<IEnumerable<ProjectResponeModel>> GetNameList();
    }
}

