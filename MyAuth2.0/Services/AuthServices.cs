using Microsoft.AspNetCore.Identity;
using MyAuth2._0.Data;
using MyAuth2._0.DTO_s;
using MyAuth2._0.IServices;
using MyAuth2._0.Model;

namespace MyAuth2._0.Services
{
    public class AuthServices : IAuthService
    {
        private readonly AspNetIdentityContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthServices(AspNetIdentityContext aspNetIdentityContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = aspNetIdentityContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user= _db.ApplicationUsers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user=_db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid= await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(user==null || isValid==false) 
            {
                return new LoginResponseDto() { userDto = null, Token = "" };
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            UserDto userDto = new()
            {
                Email=user.Email,
                ID=user.Id,
                Name=user.Name,
                PhoneNumber=user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                userDto = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName= registerationRequestDto.Email,
                Email= registerationRequestDto.Email,
                NormalizedEmail=registerationRequestDto.Email.ToUpper(),
                Name=registerationRequestDto.Name,
                PhoneNumber=registerationRequestDto.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var usetoreturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registerationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = usetoreturn.Email,
                        ID = usetoreturn.Id,
                        Name = usetoreturn.Name,
                        PhoneNumber = usetoreturn.PhoneNumber

                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Exception Occurs";
        }
    }
}
