using DemoProject1.API.Model.Domain;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.Repository
{
    public class UserDetailRepository<T> : IUserDetailRepository<T> where T : class
    {
        public int generateId()
        {
            int id = 1;
            string userList = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
            var users = JsonSerializer.Deserialize<List<User>>(userList);
            foreach (var item in users)
            {
                id = users.Count();
                id++;
            }
            return id;
        }
        //public static async Task<Response<List<UserDetailDTO>>> GetUserDetails()
        //{
        //    try
        //    {
        //        string userList = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
        //        var users = JsonSerializer.Deserialize<List<User>>(userList);
        //        if (users.Count == 0)
        //        {
        //            return new Response<List<UserDetailDTO>>
        //            {
        //                StatusMessage = "No recored found!."
        //            };
        //        }
        //        else
        //        {
        //            string text = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json");
        //            var userDetails = JsonSerializer.Deserialize<List<UserDetail>>(text);
        //            if (userDetails.Count == 0)
        //            {
        //                return new Response<List<UserDetailDTO>>
        //                {
        //                    StatusMessage = "No recored found!."
        //                };
        //            }
        //            else
        //            {
        //                var result = (from objuser in users
        //                              join objuserDetail in userDetails on objuser.UserId equals objuserDetail.UserId
        //                              select new UserDetailDTO()
        //                              {
        //                                  UserName = objuser.UserName,
        //                                  FirstName = objuserDetail.FirstName,
        //                                  LastName = objuserDetail.LastName,
        //                                  Email = objuserDetail.Email,
        //                                  Gender = objuserDetail.Gender,
        //                                  Specialization = objuserDetail.Specialization,
        //                                  IsEmployee = objuserDetail.IsEmployee
        //                              }).ToList();
        //                if (result.Count > 0)
        //                {
        //                    return new Response<List<UserDetailDTO>>
        //                    {
        //                        Result = result,
        //                        StatusMessage = "Ok"
        //                    };
        //                }
        //                else
        //                {
        //                    return new Response<List<UserDetailDTO>>
        //                    {
        //                        StatusMessage = "No recored found!."
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static async Task<Response<UserDetailDTO>> GetUserDetailById(int Id)
        //{
        //    try
        //    {
        //        string userList = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
        //        var users = JsonSerializer.Deserialize<List<User>>(userList);
        //        if (users.Count == 0)
        //        {
        //            return new Response<UserDetailDTO>
        //            {
        //                StatusMessage = "No recored found!."
        //            };
        //        }
        //        else
        //        {
        //            string text = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json");
        //            var userDetails = JsonSerializer.Deserialize<List<UserDetail>>(text);
        //            //var userResult = userDetails.FirstOrDefault(x => x.UserId == Id);
        //            if (userDetails != null)
        //            {

        //                var result = (from objuser in users
        //                              join objuserDetail in userDetails on objuser.UserId equals objuserDetail.UserId
        //                              where objuserDetail.UserId == Id
        //                              select new UserDetailDTO()
        //                              {
        //                                  UserName = objuser.UserName,
        //                                  FirstName = objuserDetail.FirstName,
        //                                  LastName = objuserDetail.LastName,
        //                                  Email = objuserDetail.Email,
        //                                  Gender = objuserDetail.Gender,
        //                                  Specialization = objuserDetail.Specialization,
        //                                  IsEmployee = objuserDetail.IsEmployee,
        //                              }).FirstOrDefault();
        //                if (result != null)
        //                {
        //                    return new Response<UserDetailDTO>
        //                    {
        //                        Result = result,
        //                        StatusMessage = "Ok"
        //                    };
        //                }
        //                else
        //                {
        //                    return new Response<UserDetailDTO>
        //                    {
        //                        StatusMessage = "No recored found!."
        //                    };
        //                }
        //            }
        //            else
        //            {
        //                return new Response<UserDetailDTO>
        //                {
        //                    StatusMessage = "No record found.!"
        //                };
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static async Task<Response<AddUserDetailDTO>> AddUserDetail(AddUserDetailDTO addUserDetailRequestDTO)
        //{
        //    try
        //    {
        //        int UserId = generateId();
        //        var user = new User()
        //        {
        //            UserId = UserId,
        //            UserName = addUserDetailRequestDTO.UserName,
        //            Password = addUserDetailRequestDTO.Password,

        //        };
        //        if (user != null)
        //        {
        //            string users = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
        //            var userResults = JsonSerializer.Deserialize<List<User>>(users);
        //            foreach (var item in userResults)
        //            {
        //                if (item.UserName == user.UserName && item.Password == user.Password)
        //                {
        //                    return new Response<AddUserDetailDTO>
        //                    {
        //                        StatusMessage = "User already exist."
        //                    };
        //                }
        //            }
        //            userResults.Add(user);
        //            string userJson = JsonSerializer.Serialize(userResults);
        //            File.WriteAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json", userJson);

        //            var userDetail = new UserDetail()
        //            {
        //                Id = UserId,
        //                FirstName = addUserDetailRequestDTO.FirstName,
        //                LastName = addUserDetailRequestDTO.LastName,
        //                Gender = addUserDetailRequestDTO.Gender,
        //                Email = addUserDetailRequestDTO.Email,
        //                Specialization = addUserDetailRequestDTO.Specialization,
        //                IsEmployee = addUserDetailRequestDTO.IsEmployee,
        //                UserId = UserId,
        //            };

        //            if (userDetail != null)
        //            {
        //                string userDetails = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json");
        //                var userDetailResults = JsonSerializer.Deserialize<List<UserDetail>>(userDetails);
        //                userDetailResults.Add(userDetail);
        //                string userDetailJson = JsonSerializer.Serialize(userDetailResults);
        //                File.WriteAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json", userDetailJson);

        //                return new Response<AddUserDetailDTO>
        //                {   
        //                    Result=addUserDetailRequestDTO,
        //                    StatusMessage = "Data has been added successfully!."
        //                };
        //            }
        //            else
        //            {
        //                return new Response<AddUserDetailDTO>
        //                {
        //                    StatusMessage = "No Record found..!"
        //                };
        //            }
        //        }
        //        else
        //        {
        //            return new Response<AddUserDetailDTO>
        //            {
        //                StatusMessage = "User already exist."
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public T GetUserDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetUserDetails()
        {
            throw new NotImplementedException();
        }
    }
}
