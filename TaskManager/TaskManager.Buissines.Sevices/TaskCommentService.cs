using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.StaticFiles;
using TaskManager.Buissines.Contracts;
using TaskManager.Buissines.Models;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;
using Unity;

namespace TaskManager.Buissines.Sevices
{
	public class TaskCommentService : AbstractService, ITaskCommentService
	{
        private ITaskCommentRepository _repo;
        public TaskCommentService(IUnityContainer container) : base(container)
        {
            _repo = _container.Resolve<ITaskCommentRepository>();
        }

        public async Task<TaskCommentResponceModel> Add(TaskCommentReuestModel entity)
        {
            var newGuid = Guid.NewGuid();
            var content = new byte[0];
            var taskComment = new TaskComment() { Id = newGuid, CommentType = (byte)entity.CommentType, TaskId = entity.TaskId };
            switch (entity.CommentType)
            {
                case 0:
                    if (!String.IsNullOrEmpty(entity.Text))
                    {
                        taskComment.Content = Encoding.ASCII.GetBytes(entity.Text);
                    }
                    break;
                case 1:
                    if (entity.File.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            entity.File.CopyTo(ms);
                            taskComment.Content = ms.ToArray();
                            taskComment.FileName = entity.File.FileName;
                        }
                    }
                    break;
                default:
                    break;
            }


            await _repo.Add(taskComment);
            var taskrservice = _container.Resolve<IProjectTaskService>();
            var task = await taskrservice.GetBykey(entity.TaskId);
            task.UpdateTime = DateTime.UtcNow;
            await taskrservice.Update(task);
            var addedComment = await _repo.GetByKey(newGuid);

            return CommentToResponceModel(addedComment);
        }
        public async Task<(byte[],string,  string )> GetFile(Guid commentId)
        {
            var comment = await _repo.GetByKey(commentId);
            string contenttype;
            new FileExtensionContentTypeProvider().TryGetContentType(comment.FileName, out contenttype);
            if (contenttype == null)
            {
                contenttype = "application/octet-stream";
            }
            return (content: comment.Content, type: contenttype,  filename: comment.FileName);
        }
        public async Task<IEnumerable<TaskCommentResponceModel>> Delete(Guid id)
        {
            var comment = await _repo.GetByKey(id);
            var taskId = comment.TaskId;
            await _repo.Remove(id);
            return await GetByTaskId(taskId);
        }

        public async Task<IEnumerable<TaskCommentResponceModel>> GetAll()
        {
            var tasks = await _repo.GetAll();
            List<TaskCommentResponceModel> result = new();
            foreach (var task in tasks)
            {
                result.Add(CommentToResponceModel(task));
            }
            return result;
        }

        public async Task<TaskCommentResponceModel> GetBykey(Guid id)
        {
            var commment = await _repo.GetByKey(id);



            return CommentToResponceModel(commment);
        }

        public async Task<IEnumerable<TaskCommentResponceModel>> GetByTaskId(Guid taskId)
        {
            var tasks = await _repo.GetMany(x => x.TaskId ==taskId);
            List<TaskCommentResponceModel> result = new();
            foreach (var task in tasks)
            {
                result.Add(CommentToResponceModel(task));
            }
            return result;
        }

        public async Task<TaskCommentResponceModel> Update(TaskCommentReuestModel entity)
        {
            var exiting = await _repo.GetByKey(entity.TaskId);
            exiting.CommentType = (byte)entity.CommentType;
            exiting.Content = Encoding.ASCII.GetBytes(entity.Text);
            await _repo.Update(await _repo.GetByKey(entity.TaskId), exiting);
            var updated = await _repo.GetByKey(entity.TaskId);
            return CommentToResponceModel(updated);
        }


        private TaskCommentResponceModel CommentToResponceModel(TaskComment comment)
        {
            var result = new TaskCommentResponceModel();
            result.Id = comment.Id;
            result.TaskId = comment.TaskId;
            var type = (CommentType)Enum.ToObject(typeof(CommentType), comment.CommentType);
            
            result.CommentType = type;
            switch (type)
                {
                    case CommentType.Text:
                        result.Text = System.Text.Encoding.UTF8.GetString(comment.Content);
                        break;
                    case CommentType.File:
                        result.File = "upload link";
                        break;
                    default:
                        break;
                }

            return result;
        }

    }

}

