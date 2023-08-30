using DemoProject1.API.Model.Domain;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;

namespace TestProject1.API.Service
{
    public class UserLoginService
    {
        UserLoginRepository userLoginRepository = new UserLoginRepository();
        public async Task<Response<UserLoginWithToken>> UserLogin(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (loginRequestDTO == null)
                {
                    return new Response<UserLoginWithToken>
                    {
                        StatusMessage = "No recored found!."
                    };
                }
                var response = await userLoginRepository.UserLogin(loginRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
