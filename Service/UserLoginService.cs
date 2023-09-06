using DemoProject1.API.Model.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TestProject1.API.IService;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;

namespace TestProject1.API.Service
{
    public class UserLoginService : IUserLoginService
    {
        private readonly ILoginRepository<User> _userLoginRepository;
        public readonly IConfiguration iconfiguration;
        public UserLoginService(ILoginRepository<User> userLoginRepository, IConfiguration configuration)
        {
            _userLoginRepository = userLoginRepository;
            this.iconfiguration = configuration;
        }


        string fullPath = null;
        public UserLoginService()
        {
            string path = "JsonData/UserList.json";
            fullPath = Path.GetFullPath(path);
        }
        public async Task<Response<UserLoginWithToken>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                UserLoginService userLoginService = new UserLoginService();
                var responseUserResult = _userLoginRepository.Login(userLoginService.fullPath, loginRequestDTO);
                var resUser = responseUserResult.FirstOrDefault(x => x.UserName == loginRequestDTO.UserName && x.Password == loginRequestDTO.Password);
                if (resUser == null)
                {
                    return new Response<UserLoginWithToken>
                    {
                        StatusMessage = "Invalid username or password!."
                    };
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var Key = "4899028db7a44673a3f27ce81ea53785";
                    var secretKey = Encoding.UTF8.GetBytes(Key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                                new Claim(ClaimTypes.Name,resUser.UserName)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    UserLoginWithToken userResult = new UserLoginWithToken()
                    {
                        UserName = loginRequestDTO.UserName,
                        Password = loginRequestDTO.Password,
                        Token = tokenHandler.WriteToken(token)
                    };
                    return new Response<UserLoginWithToken>
                    {
                        Result = userResult,
                        StatusMessage = "Ok"
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
