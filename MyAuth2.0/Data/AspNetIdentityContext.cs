using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAuth2._0.Model;

namespace MyAuth2._0.Data
{
    public class AspNetIdentityContext:IdentityDbContext<IdentityUser>
    {

            public AspNetIdentityContext(DbContextOptions<AspNetIdentityContext> options) : base(options)
            {

            }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
