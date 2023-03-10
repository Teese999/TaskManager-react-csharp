using System;
using System.Threading.Tasks;
using TaskManager.Buissines.Contracts;
using TaskManager.Buissines.Models;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;
using Unity;

namespace TaskManager.Buissines.Sevices
{
	public class ProjectTaskService : AbstractService, IProjectTaskService
	{
        private IProjectTaskRepository _repo;
        public ProjectTaskService(IUnityContainer container) : base(container)
        {
            _repo = _container.Resolve<IProjectTaskRepository>();
        }

        public async Task<ProjectTask> Add(ProjectTaskAddRequestModel entity)
        {
            var newGuid = Guid.NewGuid();

            var projectTask = new ProjectTask();
            projectTask.CreateDate = DateTime.UtcNow;
            projectTask.UpdateTime = DateTime.UtcNow;
            projectTask.Id = newGuid;
            projectTask.TaskName = entity.Name;
            projectTask.ProjectId = entity.ProjectId;
            projectTask.StartDate = entity.StartDate;
            await _repo.Add(projectTask);

            var projectRepo = _container.Resolve<IProjectService>();
            var project = await projectRepo.GetBykey(entity.ProjectId);
            project.UpdateTime = DateTime.UtcNow;
            await projectRepo.Update(project);
            return await _repo.GetByKey(newGuid);
        }

        public async Task Delete(Guid id)
        {
            await _repo.Remove(id);
        }

        public async Task<IEnumerable<ProjectTask>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<ProjectTask> GetBykey(Guid id)
        {
            return await _repo.GetByKey(id);
        }
        public async Task<TasksTableModel> GetTable(ProjectTasksFilterOptions? options = null)
        {
            var allTasks = await _repo.GetAll();
            if (options != null && options.FromDate != DateTime.MinValue)
            {
                allTasks = allTasks.Where(x => x.StartDate >= options.FromDate);
            }
            if (options != null && options.ProjectId != null)
            {
                allTasks = allTasks.Where(x => x.ProjectId == options.ProjectId);
            }
            var reponceModelTasks = await ProjectTaskToResponceModelMapper(allTasks);

            double minutes = 0;

            foreach (var task in reponceModelTasks)
            {
                minutes += task.SpendedMinutes;

            }   
            TimeSpan time = TimeSpan.FromMinutes(minutes);
           
            return  new TasksTableModel() { Tasks = reponceModelTasks, SummaryTime = string.Format("{0:00}:{1:00}", time.TotalHours, time.Minutes)};
        }
        public async Task<ProjectTask> Update(ProjectTaskUpdateRequestModel entity)
        {
            var currentTask = await _repo.GetByKey(entity.Id);
            var updatedTask = (ProjectTask)currentTask.Clone();
            updatedTask.TaskName = entity.TaskName;
            updatedTask.UpdateTime = DateTime.UtcNow;
            updatedTask.StartDate = entity.StartDate;
            updatedTask.CancelDate = entity.CancelDate;

            await _repo.Update(currentTask, updatedTask);
            return await _repo.GetByKey(entity.Id);
        }
        public async Task<ProjectTask> Update(ProjectTask entity)
        {
            entity.UpdateTime = DateTime.UtcNow;
            await _repo.Update(await _repo.GetByKey(entity.Id), entity);
            return await _repo.GetByKey(entity.Id);

        }
        private async Task<List<ProjectTaskResponceModel>> ProjectTaskToResponceModelMapper(IEnumerable<ProjectTask> allTasks)
        {
            var result = new List<ProjectTaskResponceModel>();

            int index = 1;
            foreach (var task in allTasks)
            {
                var projService = _container.Resolve<IProjectService>();
                var projectName = await projService.GetName(task.ProjectId);
                double spendedMinutes = 0;
                string spendedTime = "00:00";
                string cancelDate = "00:00";
                string startDate = "00:00";
                DateTime computedCancelDate = DateTime.UtcNow;
                if (task.StartDate != DateTime.MinValue)
                {
                    startDate = task.StartDate.ToString("HH:mm");
                }
                if (task.CancelDate != DateTime.MinValue)
                {
                    computedCancelDate = task.CancelDate;
                }
                    spendedTime = string.Format("{0:00}:{1:00}", TimeSpan.FromMinutes(computedCancelDate.Subtract(task.StartDate).TotalMinutes).TotalHours, TimeSpan.FromMinutes(computedCancelDate.Subtract(task.StartDate).TotalMinutes).Minutes);
                if (task.CancelDate != DateTime.MinValue)
                {

                    cancelDate = computedCancelDate.ToString("HH:mm");
                }
                    spendedMinutes = TimeSpan.FromMinutes(computedCancelDate.Subtract(task.StartDate).TotalMinutes).TotalMinutes;
                if (spendedMinutes < 0)
                {

                     spendedTime = "00:00";
                    spendedMinutes = 0;
                }
                
                result.Add(new ProjectTaskResponceModel()
                {
                    Index = index,
                    Id = task.Id,
                    TaskName = task.TaskName,
                    ProjectId = task.ProjectId,
                    ProjectName = projectName,
                    CreateDate = task.CreateDate.ToString("HH:mm"),
                    SpendedMinutes = spendedMinutes,
                    SpendedTime = spendedTime,
                    CancelDate = cancelDate,
                    StartDate = startDate,

                }) ;
                index++;
            }

            return result;
        }  
    }
}

