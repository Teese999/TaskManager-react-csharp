using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Data.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Data.Repositories
{
    public abstract class AbstractRepository<TEntity>
            where TEntity : class, IEntity
    {
        protected AbstractRepository(AppDbContext context)
        {
            Context = context;
        }
        protected AppDbContext Context { get; private set; }

        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> criteria = null)
        {


            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (criteria != null)
            {
                query = query.Where(criteria);
            }
            return query;


        }
         public async Task<TEntity> GetByKey(Guid key)
        {
            if (key == Guid.Empty)
            {
                throw new ArgumentException($"Key {key} is missing");

            };
            IQueryable<TEntity> query = Context.Set<TEntity>();
            return await GetQuery(x => x.Id == key).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {

            return await Context.Set<TEntity>().ToArrayAsync();
        }

        public async Task Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"{typeof(TEntity)} - is Null");
            };
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var entity = Context.Set<TEntity>().First(x => x.Id == id);
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(TEntity entityCurrent, TEntity entityFrom)
        {
            await Task.Run(() => Context.Entry(entityCurrent).CurrentValues.SetValues(entityFrom));
            await Context.SaveChangesAsync();
        }

        public Task<bool> IsAttached(TEntity entity)
        {
            return Task.FromResult(Context.Set<TEntity>().Local.Any(e => e == entity));
        }

        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> criteria)
        {
            return await GetQuery(criteria).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetRange(int firstIndex, int countPerPage, Expression<Func<TEntity, bool>> criteria = null)
        {
            if (criteria == null)
            {

                return await Context.Set<TEntity>().Skip(firstIndex).Take(countPerPage).ToListAsync();
            }
            else
            {
                return await Context.Set<TEntity>().Where(criteria).Skip(firstIndex).Take(countPerPage).ToListAsync();
            }

        }
        public async Task<int> GetEntitiesCount(Expression<Func<TEntity, bool>> criteria = null)
        {

            return await GetQuery(criteria).CountAsync();
        }
    }
}

