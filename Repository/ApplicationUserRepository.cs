using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private AppDbContext _db;
        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        bool IApplicationUserRepository.CheckExist(string email, string personalNumber)
        {
            throw new NotImplementedException();
        }
    }
}
