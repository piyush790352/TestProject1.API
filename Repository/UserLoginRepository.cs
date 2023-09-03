using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.Repository
{
    public class UserLoginRepository
    {
        public readonly IConfiguration iconfiguration;
        public UserLoginRepository(IConfiguration configuration)
        {
            this.iconfiguration = configuration;
        }

        public UserLoginRepository()
        {
        }
        public async Task<Response<UserLoginWithToken>> UserLogin(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                string userList = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
                var users = JsonSerializer.Deserialize<List<User>>(userList);
                var resUser = users.FirstOrDefault(x => x.UserName == loginRequestDTO.UserName && x.Password == loginRequestDTO.Password);
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
