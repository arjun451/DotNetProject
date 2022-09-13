using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly ApplicationDbContext _db;

        public UniteOfWork(ApplicationDbContext db)
        {
            this._db = db;
            Category = new CategoryRepository(_db);
        }
        public ICategoryRepository Category { get; set; }

        public void Save()
        {
             _db.SaveChanges();
        }
    }
}
