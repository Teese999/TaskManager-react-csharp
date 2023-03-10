using System;
using Microsoft.Extensions.Configuration;
using TaskManager.Data.Contracts;
using TaskManager.Data.Repositories;
using Unity;
using Unity.Lifetime;

namespace TaskManager.Data
{
	public class ContainerConfiguration
	{
        public static void RegisterTypes<TLifetime>(IUnityContainer container, IConfiguration configuration)
           where TLifetime : ITypeLifetimeManager, new()
        {

            container.RegisterType<AppDbContext>();


            container.RegisterType<IProjectRepository, ProjectRepository>(new TLifetime());
            container.RegisterType<IProjectTaskRepository, ProjectTaskRepository>(new TLifetime());
            container.RegisterType<ITaskCommentRepository, TaskCommentRepository>(new TLifetime());

        }
    }
}

