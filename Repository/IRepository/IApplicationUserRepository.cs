using WebApp.Models;
using WebApp.Repository.IRepository;

namespace WebApp.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        bool CheckExist(string email, string personalNumber);
    }
}