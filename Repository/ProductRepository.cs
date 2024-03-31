using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;
        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public List<Product> GetProductsByClient(string UserId)
        {
            return _db.UserProducts.Where(x=>x.UserId==UserId).Select(x=>x.Product).ToList();
           
        }
    }
}
