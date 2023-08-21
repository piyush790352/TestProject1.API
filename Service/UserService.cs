using DemoProject1.API.Model.Domain;
using DemoProject1.API.Repository;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.Service
{
    public class UserService
    {
        public static async Task<Response<User>> AddUser(AddUserDTO addUserRequestDTO)
        {
            try
            {
                if(addUserRequestDTO == null)
                {
                    return new Response<User>
                    {
                        StatusMessage = "No recored found!."
                    };
                }
                var response = await UserRepository.AddUser(addUserRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
