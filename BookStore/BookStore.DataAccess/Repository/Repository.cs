using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly  DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            _db.Products.Include(u => u.Category).Include(u => u.CoverType);
            this.dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
             dbset.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includeProprites = null)
        {
            IQueryable<T> query = dbset;
            if(includeProprites != null)
            {
                foreach(var includeProprite in includeProprites.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                { 
                    query = query.Include(includeProprite);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProprites=null)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            if (includeProprites != null)
            {
                foreach (var includeProprite in includeProprites.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProprite);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
             dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
             dbset.RemoveRange(entities);
        }
    }
}
