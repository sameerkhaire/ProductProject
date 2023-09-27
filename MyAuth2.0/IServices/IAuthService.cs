using MyAuth2._0.DTO_s;

namespace MyAuth2._0.IServices
{
    public interface IAuthService
    {
        Task<string> Register (RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
