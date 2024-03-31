using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Repository.IRepository;

namespace WebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public UnitOfWork(AppDbContext db) 
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Product = new ProductRepository(_db);
            UserProduct = new UserProductRepository(_db);
        }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IProductRepository Product { get; private set; }

        public IUserProductRepository UserProduct { get; private set; }
		
		public void Save()
        {
            _db.SaveChanges();
        }
    }
}
