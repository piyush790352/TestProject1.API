using DemoProject1.API.Model.Domain;
using Newtonsoft.Json;
using System.Net;
using TestProject1.API.IService;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;

namespace TestProject1.API.Service
{
    public class UserService 
    {
        //private readonly ISchoolRepository<User> _userRepository;

        //public UserService(ISchoolRepository<User> userRepository)
        //{
        //    _userRepository = userRepository;
        //}

        string userJsonFullPath = null;
        public UserService()
        {
            string path = "JsonData/UserList.json";
            userJsonFullPath = Path.GetFullPath(path);            
        }
        
        //public async Task<Response<List<UserDetailDTO>>> GetUserDetails()
        //{
        //    try
        //    {
        //        UserService userService = new UserService();
        //        var response = _userRepository.Get(userService.fullPath);
        //        if (response == null)
        //        {
        //            return new Response<List<UserDetailDTO>>
        //            {
        //                StatusMessage = "No Record Found!."
        //            };
        //        }

                
        //        return new Response<List<UserDetailDTO>>
        //        {
                    
        //            StatusMessage = "Ok."
        //        };
        //        //var result = (from objuser in response
        //        //              join objuserDetail in userDetailResult on objuser.UserId equals objuserDetail.UserId
        //        //              select new UserDetailDTO()
        //        //              {
        //        //                  UserName = objuser.UserName,
        //        //                  FirstName = objuserDetail.FirstName,
        //        //                  LastName = objuserDetail.LastName,
        //        //                  Email = objuserDetail.Email,
        //        //                  Gender = objuserDetail.Gender,
        //        //                  Specialization = objuserDetail.Specialization,
        //        //                  IsEmployee = objuserDetail.IsEmployee
        //        //              }).ToList();
        //        //if (result.Count > 0)
        //        //{
        //        //    return new Response<List<UserDetailDTO>>
        //        //    {
        //        //        Result = result,
        //        //        StatusMessage = "Ok"
        //        //    };
        //        //}
        //        //else
        //        //{
        //        //    return new Response<List<UserDetailDTO>>
        //        //    {
        //        //        StatusMessage = "No recored found!."
        //        //    };
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
