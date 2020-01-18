using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace galaxyGeoloc.Repository
{
    public interface IRepository<T,C>
        where T: class
        where C: DbContext
    {
        IEnumerable<T> FindBy(Expression<Func<T, bool>> expr);
        IQueryable<T> GetAll();

        void Create(T entity);
        void Edit(T Entity);
        void Delete(T Entity);

        void Save();
        Task SaveAsync();

    }

    public class Repository<T, C> : IRepository<T, C>
        where T : class
        where C : DbContext
    {
        public readonly DbContext _context;
        
        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public void Delete(T Entity)
        {
            this._context.Set<T>().Remove(Entity);
        }

        public void Edit(T Entity)
        {
            this._context.Entry(Entity).State = EntityState.Modified;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> expr)
        {
           return this._context.Set<T>().Where(expr);
        }

        public IQueryable<T> GetAll()
        {
            return this._context.Set<T>().AsQueryable();//.ToList();
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
