using IdentityServer.Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static IdentityServer.Data.DbInitializer;

namespace IdentityServer.Data
{
    public class DbInitializer: IDbinitalizer
    {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly AspNetIdentityContext _db;



            public DbInitializer(
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                AspNetIdentityContext db)
            {
                _roleManager = roleManager;
                _userManager = userManager;
                _db = db;
            }





            public void Initialize()
            {
                //migrations if they are not applied
                try
                {
                    if (_db.Database.GetPendingMigrations().Count() > 0)
                    {
                        _db.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {



                }



                //create roles if they are not created
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp)).GetAwaiter().GetResult();



                    //if roles are not created, then we will create admin user as well



                    _userManager.CreateAsync(new IdentityUser
                    {
                        UserName = "angella",
                        Email = "angella.freeman@email.com",
                        EmailConfirmed = true
                    }, "Admin123*").GetAwaiter().GetResult();



                    IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == "angella.freeman@email.com");



                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();



                }
                return;
            }
    }
}

