using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class ProjectTaskRepository : AbstractRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(AppDbContext context) : base(context)
        {
        }
    }
}

