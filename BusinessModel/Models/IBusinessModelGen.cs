using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BusinessModel
{
    public interface IBusinessModelGen<TEntity> where TEntity : class
    {

        TEntity Get(int id);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        bool Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
