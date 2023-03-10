using System;
using TaskManager.Buissines.Contracts;
using TaskManager.Data.Contracts;
using TaskManager.Data.Models;
using Unity;

namespace TaskManager.Buissines.Sevices
{
	public abstract class AbstractService
	{
        protected readonly IUnityContainer _container;
        public AbstractService(IUnityContainer container)
        {
            _container = container;
        }

    }
}

