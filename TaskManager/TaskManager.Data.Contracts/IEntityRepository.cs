
using System.Linq.Expressions;

namespace TaskManager.Data.Contracts
{
	public interface IEntityRepository<IEntity>
    {
        public Task<IEntity> GetByKey(Guid key);
        public Task Add(IEntity entity);
        public Task Remove(Guid id);
        public Task Update(IEntity entityCurrent, IEntity entityFrom);
        public Task<bool> IsAttached(IEntity entity);
        public Task<IEntity> GetSingle(Expression<Func<IEntity, bool>> criteria);
        public Task<IEnumerable<IEntity>> GetMany(Expression<Func<IEntity, bool>> criteria);
        public Task<IEnumerable<IEntity>> GetAll();
        public Task<IEnumerable<IEntity>> GetRange(int firstIndex, int lastIndex, Expression<Func<IEntity, bool>> criteria = null);
        public Task<int> GetEntitiesCount(Expression<Func<IEntity, bool>> criteria = null);
    }
}

