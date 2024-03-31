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
    public class UserProductRepository : Repository<UserProduct>, IUserProductRepository
	{
        private AppDbContext _db;
        public UserProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
