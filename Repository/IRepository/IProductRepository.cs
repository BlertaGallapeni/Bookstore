using WebApp.Models;
using WebApp.Repository.IRepository;

namespace WebApp.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByClient(string UserId);
    }
}