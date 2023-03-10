using Microsoft.Extensions.Configuration;
using TaskManager.Buissines.Contracts;
using Unity;
using Unity.Lifetime;

namespace TaskManager.Buissines.Sevices
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes<TLifetime>(IUnityContainer container, IConfiguration configuration)
           where TLifetime : ITypeLifetimeManager, new()
        {
            TaskManager.Data.ContainerConfiguration.RegisterTypes<HierarchicalLifetimeManager>(container, configuration);

            container.RegisterType<IProjectService, ProjectService>(new TLifetime());
            container.RegisterType<IProjectTaskService, ProjectTaskService>(new TLifetime());
            container.RegisterType<ITaskCommentService, TaskCommentService>(new TLifetime());

        }
    }
}