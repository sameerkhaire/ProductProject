using MyAuth2._0.Model;

namespace MyAuth2._0.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
