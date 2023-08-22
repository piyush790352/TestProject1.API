using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;

namespace TestProject1.API.Service
{
    public class UserDetailService
    {
        public static async Task<Response<List<UserDetailDTO>>> GetUserDetails()
        {
            try
            {
                var response = await UserDetailRepository.GetUserDetails();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Response<UserDetailDTO>> GetUserDetailById(int Id)
        {
            try
            {
                //return UserRepository.GetUserDetails(Id);
                var response = await UserDetailRepository.GetUserDetailById(Id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Response<AddUserDetailDTO>> AddUserDetail(AddUserDetailDTO addUserDetailRequestDTO)
        {
            try
            {
                if (addUserDetailRequestDTO == null)
                {
                    return new Response<AddUserDetailDTO>
                    {
                        StatusMessage = "No recored found!."
                    };
                }
                var response = await UserDetailRepository.AddUserDetail(addUserDetailRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
