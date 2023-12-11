using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generic
{
    public interface IGenericDAO<TEntity>
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
        TEntity Update(int id, TEntity entity);
        bool Delete(int id);
    }
}
