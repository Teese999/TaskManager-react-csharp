using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class ProjectRepository : AbstractRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

    }
}

