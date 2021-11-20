using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EntityRepository(BaseDbContext baseDbContext)
        {
            _context = baseDbContext ?? throw new ArgumentNullException(nameof(baseDbContext));
            _dbSet = _context.Set<TEntity>();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var baseEntities = entities.ToList();

            using var transaction = new TransactionScope();

            _dbSet.BulkInsert(baseEntities);

            _context.SaveChanges();
            
            transaction.Complete();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}