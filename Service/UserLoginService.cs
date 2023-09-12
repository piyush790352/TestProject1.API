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
        private readonly ILoginRepository<UserDetail> _userLoginDetailRepository;
        public readonly IConfiguration iconfiguration;
        public UserLoginService(ILoginRepository<User> userLoginRepository, ILoginRepository<UserDetail> userLoginDetailRepository, IConfiguration configuration)
        {
            _userLoginRepository = userLoginRepository;
            _userLoginDetailRepository = userLoginDetailRepository;
            this.iconfiguration = configuration;
        }


        string path1 = null;
        string path2 = null;
        public UserLoginService()
        {
            path1 = @".\JsonData\UserList.json";
            path2 = @".\JsonData\UserDetailList.json";
        }
        public async Task<Response<UserLoginWithToken>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                UserLoginService userLoginService = new UserLoginService();
                var responseUserResult = _userLoginRepository.Login(userLoginService.path1, loginRequestDTO);
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
                    var responseUserDetailResult = _userLoginDetailRepository.Login(userLoginService.path2, loginRequestDTO);
                    var resDetailUser = responseUserDetailResult.FirstOrDefault(x => x.UserId == resUser.UserId);
                    var result = (from objuser in responseUserResult
                                  join objuserDetail in responseUserDetailResult on objuser.UserId equals objuserDetail.UserId 
                                  where objuser.UserId == resUser.UserId
                                  select new UserDetailDTO()
                                  {
                                      UserName = loginRequestDTO.UserName,
                                      FirstName = objuserDetail.FirstName,
                                      LastName = objuserDetail.LastName,
                                      Email = objuserDetail.Email,
                                      Gender = objuserDetail.Gender,
                                      Specialization = objuserDetail.Specialization,
                                      IsEmployee = objuserDetail.IsEmployee
                                  }).FirstOrDefault();

                    UserLoginWithToken userResult = new UserLoginWithToken()
                    {
                        UserId = resUser.UserId,
                        Token = tokenHandler.WriteToken(token),
                        UserDetailDTO = result
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
