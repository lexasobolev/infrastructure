using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repository<TObject>
                : IRepository<TObject> where TObject : class
    {
        public Repository(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; private set; }

        protected DbSet<TObject> DbSet
        {
            get
            {
                return Context.Set<TObject>();
            }
        }

        public virtual IQueryable<TObject> Query(params Expression<Func<TObject, object>>[] includes)
        {
            return includes.Aggregate(DbSet.AsQueryable(),
                (current, include) => current.Include(include));
        }

        public virtual IQueryable<TObject> AsNoTracking(params Expression<Func<TObject, object>>[] includes)
        {
            return includes.Aggregate(DbSet.AsQueryable(),
                (current, include) => current.Include(include))
                .AsNoTracking();
        }

        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }

        public virtual IQueryable<TObject> Filter(
            Expression<Func<TObject, bool>> filter,
            out int total,
            int index = 0,
            int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Local.FirstOrDefault(predicate.Compile()) ?? DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            return newEntry;
        }

        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public virtual void Delete(TObject TObject)
        {
            DbSet.Remove(TObject);
        }

        public virtual int Update(TObject TObject)
        {
            var entry = Context.Entry(TObject);
            DbSet.Attach(TObject);
            entry.State = EntityState.Modified;
            return 0;
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);

            return 0;
        }
    }
}
