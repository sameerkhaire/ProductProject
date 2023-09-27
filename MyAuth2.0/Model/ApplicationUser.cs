using Microsoft.AspNetCore.Identity;

namespace MyAuth2._0.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
     

    }
}
