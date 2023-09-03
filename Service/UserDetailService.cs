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

        string fullPath = null;
        public UserDetailService()
        {
            string path = "JsonData/UserDetailList.json";
            fullPath = Path.GetFullPath(path);
        }

        public async Task<Response<List<UserDetailDTO>>> GetUserDetails()
        {
            try
            {
                UserDetailService userDetailService = new UserDetailService();
                var responseUserDetail = _userDetailRepository.Get(userDetailService.fullPath);
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
                    //UserService userService = new UserService(UserFullPath);
                    var responseUser = _userRepository.Get(userListFullPath);
                    //var responseUser = _userDetailRepository.Get(userService.fu);
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
                var responseUserDetail = _userDetailRepository.GetById(userDetailService.fullPath, Id);

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
                string path = "JsonData/UserList.json";
                string userListFullPath = Path.GetFullPath(path);
                var responseUserList = _userDetailRepository.Set(userListFullPath, addUserDetailRequestDTO);
                
                if (responseUserList != null)
                {
                    int UserId = responseUserList.Count > 0 ? responseUserList[responseUserList.Count - 1].UserId + 1 : 1;                   
                    var user = new User()
                    {
                        UserId = UserId,
                        UserName = addUserDetailRequestDTO.UserName,
                        Password = addUserDetailRequestDTO.Password,

                    };
                    if (user != null)
                    {
                        string users = File.ReadAllText(userListFullPath);
                        var userResults = JsonSerializer.Deserialize<List<User>>(users);
                        foreach (var item in userResults)
                        {
                            if (item.UserName == user.UserName && item.Password == user.Password)
                            {
                                return new Response<AddUserDetailDTO>
                                {
                                    StatusMessage = "User already exist."
                                };
                            }
                        }
                        userResults.Add(user);
                        string userJson = JsonSerializer.Serialize(userResults);
                        File.WriteAllText(userListFullPath, userJson);

                        UserDetailService userDetailService = new UserDetailService();
                        var responseUserDetail = _userDetailRepository.Set(userListFullPath, addUserDetailRequestDTO);
                        if (responseUserDetail != null)
                        {
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
                                string userDetails = File.ReadAllText(userDetailService.fullPath);
                                var userDetailResults = JsonSerializer.Deserialize<List<UserDetail>>(userDetails);
                                userDetailResults.Add(userDetail);
                                string userDetailJson = JsonSerializer.Serialize(userDetailResults);
                                File.WriteAllText(userDetailService.fullPath, userDetailJson);



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
                        else
                        {
                            return new Response<AddUserDetailDTO>
                            {
                                StatusMessage = "No Record found..!"
                            };
                        }
                    }
                    else
                    {
                        return new Response<AddUserDetailDTO>
                        {
                            StatusMessage = "No Record found..!"
                        };
                    }
                }
                else
                {
                    return new Response<AddUserDetailDTO>
                    {
                        StatusMessage = "No Record found..!"
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
