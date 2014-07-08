using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EProductivity.Core.Exceptions;
using EProductivity.Core.Resources;

namespace EProductivity.Core.Model.Data.EF
{
    public class ModelCollection<T, TKey> : IModelCollection<T, TKey> where T : class
    {
        private readonly IDbSet<T> _dbSet;

        public ModelCollection(IDbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public Type ElementType
        {
            get
            {
                return _dbSet.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return _dbSet.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return _dbSet.Provider;
            }
        }

        public virtual T this[TKey key]
        {
            get { return _dbSet.Find(key); }
        }

        public virtual IModelCollection<T, TKey> Add(T entity)
        {
            _dbSet.Add(entity);
            return this;
        }

        public virtual IModelCollection<T, TKey> Remove(T entity)
        {
            _dbSet.Remove(entity);
            return this;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return _dbSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual T WithId(TKey key)
        {
            var entity = _dbSet.Find(key);
            if (entity == null)
            {
                var type = typeof(T).Name;
                throw new NotFoundException(string.Format(DataResource.EntityNotFound, type, key));
            }

            return entity;
        }
    }
}
