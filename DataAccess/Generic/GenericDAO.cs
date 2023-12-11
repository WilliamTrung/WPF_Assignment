using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generic
{
    public class GenericDAO<TEntity> : IGenericDAO<TEntity> where TEntity : class
    {
        IGenericRepository<TEntity> _repository;
        public GenericDAO()
        {
            var _context = new SaleWPFAppContext();
            _repository = new GenericRepository<TEntity>(_context);
        }
        public GenericDAO(SaleWPFAppContext context)
        {
            _repository = new GenericRepository<TEntity>(context);
        }
        public virtual TEntity Add(TEntity entity)
        {
            //throw new NotImplementedException();
            try
            {
                entity = _repository.Add(entity);
                _repository.Save();
                return entity;
            } catch
            {
                throw;
            }
            
        }

        public virtual bool Delete(int id)
        {
            //throw new NotImplementedException();
            try
            {
                _repository.Delete(id);
                _repository.Save();
            }
            catch
            {
                throw;
            }
            return true;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            //throw new NotImplementedException();
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var list = _repository.Get(filter, includeProperties);
#pragma warning restore CS8604 // Possible null reference argument.
                return list;
            }
            catch
            {
                throw;
            }
        }

        public virtual TEntity Update(int id, TEntity entity)
        {
            //throw new NotImplementedException();
            try
            {
                entity = _repository.Update(id, entity);
                _repository.Save();
                return entity;
            }
            catch
            {
                throw;
            }
        }
    }
}
