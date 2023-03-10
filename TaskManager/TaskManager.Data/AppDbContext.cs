using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManager.Data.DbMappers;
using TaskManager.Data.Models;

namespace TaskManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new ProjectTaskMap());
            modelBuilder.ApplyConfiguration(new TaskCommentMap());

            var projects = new List<Project>() {
                 new Project() {ProjectName = "TestName1", Id = Guid.NewGuid(), CreateDate = DateTime.Now, UpdateTime = DateTime.Now },
                 new Project() {ProjectName = "TestName2", Id = Guid.NewGuid(), CreateDate = DateTime.Now, UpdateTime = DateTime.Now },
                 new Project() {ProjectName = "TestName3", Id = Guid.NewGuid(), CreateDate = DateTime.Now, UpdateTime = DateTime.Now },
                 new Project() {ProjectName = "TestName4", Id = Guid.NewGuid(), CreateDate = DateTime.Now, UpdateTime = DateTime.Now },

            };
            modelBuilder.Entity<Project>().HasData(projects);

            var tasks = new List<ProjectTask>() {
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[0].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName1"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[0].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName2"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[1].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName3"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[1].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName4"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[2].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName5"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[2].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName6"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[3].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName7"  },
                new ProjectTask() {Id = Guid.NewGuid(), ProjectId = projects[3].Id, StartDate = DateTime.Now, CancelDate = DateTime.Now, CreateDate = DateTime.Now, UpdateTime = DateTime.Now, TaskName = "TaskName8"  },

            };
            modelBuilder.Entity<ProjectTask>().HasData(tasks);

            var taskComments = new List<TaskComment>() {
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[0].Id, Content = new byte[] {1}  },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[0].Id, Content = new byte[] {1}  },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[1].Id, Content = new byte[] {1}  },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[1].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[2].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[2].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[3].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[3].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[4].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[4].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[5].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[5].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[6].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[6].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[7].Id, Content = new byte[] {1}   },
                new TaskComment() {Id = Guid.NewGuid(), CommentType = 1, TaskId = tasks[7].Id, Content = new byte[] {1}   },

            };
            modelBuilder.Entity<TaskComment>().HasData(taskComments);

        }
    }
}