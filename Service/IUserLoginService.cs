using DemoProject1.API.Model.Domain;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.Service
{
    public interface IUserLoginService
    {
        Task<Response<UserLoginWithToken>> Login(LoginRequestDTO loginRequestDTO);
    }
}
