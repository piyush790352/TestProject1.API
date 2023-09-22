using DemoProject1.API.Model.Domain;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.IService
{
    public interface IUserDetailService
    {
        Task<Response<List<UserDetailDTO>>> GetUserDetails();
        Task<Response<UserDetailDTO>> GetUserDetailById(int Id);
        Task<Response<AddUserDetailDTO>> AddUserDetail(AddUserDetailDTO addUserDetailRequestDTO);
        Task<Response<string>> AddMarksheetDetail(AddMarksheetDetailDTO addMarksheetDetailDTO);
    }
}
