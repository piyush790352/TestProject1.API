using DemoProject1.API.Model.Domain;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using TestProject1.API.IService;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;

namespace TestProject1.API.Service
{
    public class UserDetailService : IUserDetailService
    {
        private readonly ISchoolRepository<UserDetail> _userDetailRepository;
        private readonly ISchoolRepository<User> _userRepository;
        public UserDetailService(ISchoolRepository<UserDetail> userDetailRepository, ISchoolRepository<User> userRepository)
        {
            _userDetailRepository = userDetailRepository;
            _userRepository = userRepository;
        }

        string path1 = null;
        string path2 = null;
        public UserDetailService()
        {
            path1 = @".\JsonData\UserList.json";
            path2 = @".\JsonData\UserDetailList.json";
        }

        public async Task<Response<List<UserDetailDTO>>> GetUserDetails()
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseUserDetail = _userDetailRepository.Get(userDetailService.path2);
                if (responseUserDetail == null)
                {
                    return new Response<List<UserDetailDTO>>
                    {
                        StatusMessage = "No Record Found!."
                    };
                }
                else
                {
                    string path = "JsonData/UserList.json";
                    string userListFullPath = Path.GetFullPath(path);
                    var responseUser = _userRepository.Get(userListFullPath);
                    if (responseUser == null)
                    {
                        return new Response<List<UserDetailDTO>>
                        {
                            StatusMessage = "No Record Found!."
                        };
                    }
                    else
                    {
                        var result = (from objuser in responseUser
                                      join objuserDetail in responseUserDetail on objuser.UserId equals objuserDetail.UserId
                                      select new UserDetailDTO()
                                      {
                                          UserName = objuser.UserName,
                                          FirstName = objuserDetail.FirstName,
                                          LastName = objuserDetail.LastName,
                                          Email = objuserDetail.Email,
                                          Gender = objuserDetail.Gender,
                                          Specialization = objuserDetail.Specialization,
                                          IsEmployee = objuserDetail.IsEmployee
                                      }).ToList();
                        if (result.Count > 0)
                        {
                            return new Response<List<UserDetailDTO>>
                            {
                                Result = result,
                                StatusMessage = "Ok"
                            };
                        }
                        else
                        {
                            return new Response<List<UserDetailDTO>>
                            {
                                StatusMessage = "No recored found!."
                            };
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<UserDetailDTO>> GetUserDetailById(int Id)
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseUserDetail = _userDetailRepository.GetById(userDetailService.path2, Id);

                string path = "JsonData/UserList.json";
                string userListFullPath = Path.GetFullPath(path);
                //UserService userService = new UserService(UserFullPath);
                var responseUser = _userRepository.GetById(userListFullPath, Id);

                var result = (from objuser in responseUser
                              join objuserDetail in responseUserDetail on objuser.UserId equals objuserDetail.UserId
                              where objuserDetail.UserId == Id
                              select new UserDetailDTO()
                              {
                                  UserName = objuser.UserName,
                                  FirstName = objuserDetail.FirstName,
                                  LastName = objuserDetail.LastName,
                                  Email = objuserDetail.Email,
                                  Gender = objuserDetail.Gender,
                                  Specialization = objuserDetail.Specialization,
                                  IsEmployee = objuserDetail.IsEmployee
                              }).FirstOrDefault();
                if (result != null)
                {
                    return new Response<UserDetailDTO>
                    {
                        Result = result,
                        StatusMessage = "Ok"
                    };
                }
                else
                {
                    return new Response<UserDetailDTO>
                    {
                        StatusMessage = "No recored found!."
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<AddUserDetailDTO>> AddUserDetail(AddUserDetailDTO addUserDetailRequestDTO)
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var userListRepository = new SchoolRepository<User>();
                var responseUserList = userListRepository.ReadJsonData(userDetailService.path1);

                var usercheck = (from obj in responseUserList
                                 where obj.UserName.Equals(addUserDetailRequestDTO.UserName) &&
                                 obj.Password.Equals(addUserDetailRequestDTO.Password)
                                 select obj).Count();


                if (usercheck > 0)
                {
                    return new Response<AddUserDetailDTO>
                    {

                        StatusMessage = "User already exists"
                    };
                }
                else
                {

                    int UserId = responseUserList.Count > 0 ? responseUserList[responseUserList.Count - 1].UserId + 1 : 1;
                    var user = new User()
                    {
                        UserId = UserId,
                        UserName = addUserDetailRequestDTO.UserName,
                        Password = addUserDetailRequestDTO.Password,

                    };
                    responseUserList.Add(user);
                    userListRepository.Set(userDetailService.path1, responseUserList);

                    var userDetailRepository = new SchoolRepository<UserDetail>();
                    var responseUserDetail = userDetailRepository.ReadJsonData(userDetailService.path2);

                    var userDetail = new UserDetail()
                    {
                        Id = UserId,
                        FirstName = addUserDetailRequestDTO.FirstName,
                        LastName = addUserDetailRequestDTO.LastName,
                        Gender = addUserDetailRequestDTO.Gender,
                        Email = addUserDetailRequestDTO.Email,
                        Specialization = addUserDetailRequestDTO.Specialization,
                        IsEmployee = addUserDetailRequestDTO.IsEmployee,
                        UserId = UserId,
                    };

                    if (userDetail != null)
                    {
                        responseUserDetail.Add(userDetail);
                        userDetailRepository.Set(userDetailService.path2, responseUserDetail);


                        return new Response<AddUserDetailDTO>
                        {
                            Result = addUserDetailRequestDTO,
                            StatusMessage = "Data has been added successfully!."
                        };
                    }
                    else
                    {
                        return new Response<AddUserDetailDTO>
                        {
                            StatusMessage = "No Record found..!"
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
