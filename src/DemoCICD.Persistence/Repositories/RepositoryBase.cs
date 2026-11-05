namespace DemoCICD.Persistence.Repositories
{
    using System.Linq.Expressions;
    using DemoCICD.Domain.Abstractions.Entities;
    using DemoCICD.Domain.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : DomainEntity<TKey>
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();

            if(includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }

            if (predicate != null)
            {
                items = items.Where(predicate);
            }

            return items;
        }

        public TEntity FindById(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(x => x.Id.Equals(id), includeProperties).SingleOrDefault();
        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(predicate, includeProperties).SingleOrDefault();
        }

        public void Add(TEntity entity)
        {
           _context.Add(entity);
        }

        public void Remove(TEntity entity)
        {
           _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveMultiple(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
