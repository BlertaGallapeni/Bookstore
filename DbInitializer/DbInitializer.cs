using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;


        public DbInitializer(
            AppDbContext db, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            { 

            }


            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Client")).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "049000111",
                Gender = 'F',
                Address = "Prishtine,Kosovo"

            }, "WebApp1.").GetAwaiter().GetResult();
                
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u=>u.Email == "admin@gmail.com");

            _userManager.AddToRoleAsync(user,"Admin").GetAwaiter().GetResult();
           }
        return;
       }
    }
}
