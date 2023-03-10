using System;
using TaskManager.Buissines.Contracts;
using TaskManager.Buissines.Models;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;
using Unity;
using static Unity.Storage.RegistrationSet;

namespace TaskManager.Buissines.Sevices
{
	public class ProjectService : AbstractService, IProjectService
    {
        private IProjectRepository _repo;
        public ProjectService(IUnityContainer container) : base(container)
		{
            _repo = _container.Resolve<IProjectRepository>();
        }

        public async Task<Project> Add(Project project)
        {
            if (project.Id == Guid.Empty)
            {
                project.Id = Guid.NewGuid();
            }
            project.CreateDate = DateTime.UtcNow;
            project.UpdateTime = DateTime.UtcNow;
            await _repo.Add(project);

            return await _repo.GetByKey(project.Id);
        }

        public async Task Delete(Guid id)
        {
            await _repo.Remove(id);
        }

        public Task<IEnumerable<Project>> GetAll()
        {
            return _repo.GetAll();
        }
        public async Task<IEnumerable<ProjectResponeModel>> GetNameList()
        {
            var projects = await _repo.GetAll();
            List<ProjectResponeModel> responceModels = new();
            int index = 1;
            foreach (var proj in projects)
            {
                responceModels.Add(new ProjectResponeModel() {index = index, Id = proj.Id, Name = proj.ProjectName });
            }
            return responceModels;
        }
        public async Task<Project> GetBykey(Guid id)
        {
            return await _repo.GetByKey(id);
        }
        public async Task<String> GetName(Guid id)
        {
            var proj = await _repo.GetByKey(id);
            return proj.ProjectName;
        }
        public async Task<Project> Update(Project project)
        {
            project.UpdateTime = DateTime.UtcNow;
            await _repo.Update(await _repo.GetByKey(project.Id), project);
            return await _repo.GetByKey(project.Id);
        }
    }
}

