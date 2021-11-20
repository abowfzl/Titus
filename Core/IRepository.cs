using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(IEnumerable<TEntity> entities);
        
        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}