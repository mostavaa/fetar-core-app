using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Data.Repositories
{
    public class Repository<T> where T : class
    {
        private DataContext Context { get; set; }
        public Repository(DataContext context)
        {
            Context = context;
        }
        internal void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        internal void Delete(int entity)
        {
            T existing = Context.Set<T>().Find(entity);
            if (existing != null) Context.Set<T>().Remove(existing);
        }
        internal IQueryable<T> Get()
        {
            return Context.Set<T>();
        }
        internal IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }
        internal void Update(T entity)
        {
            //Context.Attach<T>(entity);
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
