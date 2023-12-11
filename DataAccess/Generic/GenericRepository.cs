using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        SaleWPFAppContext _context;

        public GenericRepository(SaleWPFAppContext context)
        {
            _context = context;
        }
        public TEntity Add(TEntity entity)
        {
            //throw new NotImplementedException();
            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var found = _context.Find<TEntity>(id);
            if(found != null)
            {
                _context.Remove(found);
            } else
            {
                throw new Exception("Error code: 404 - Not found");
            }
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            //throw new NotImplementedException();
            try
            {
                var list = _context.Set<TEntity>();
                var list_query = list.AsQueryable();
                if(filter != null)
                {
                    list_query = list_query.Where(filter);
                }
                if(includeProperties != null)
                {
                    foreach(string property in includeProperties.Split(','))
                    {
                        list_query.Include(property);
                    }
                }
                return list_query.ToList();
            }
            catch
            {
                throw new Exception("Error code: 500 - Internal Error");
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            } catch
            {
                throw new Exception("Error code: 500 - Internal Error");
            }
            //throw new NotImplementedException();
        }

        public TEntity Update(int id, TEntity entity)
        {
            //throw new NotImplementedException();
            var found = _context.Find<TEntity>(id);
            if (found != null)
            {
                _context.Entry(found).CurrentValues.SetValues(entity);
            }
            else
            {
                throw new Exception("Error code: 404 - Not found");
            }
            return entity;
        }
    }
}
