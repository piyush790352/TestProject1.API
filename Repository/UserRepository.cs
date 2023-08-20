using DemoProject1.API.JsonData;
using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using TestProject1.API.Model.DTO;

namespace DemoProject1.API.Repository
{
    public class UserRepository
    {

        private static int id = 1;
        public static int generateId()
        {
            return id++;
        }
        public static async Task<Response<List<UserDetail>>> GetUserDetails()
        {
            try
            {
                string text = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json");
                var userDetails = JsonSerializer.Deserialize<List<UserDetail>>(text);
                if (userDetails.Count == 0)
                {
                    return new Response<List<UserDetail>>
                    {
                        StatusMessage = "No recored found!."
                    };
                }
                else
                {
                    return new Response<List<UserDetail>>
                    {
                        Result = userDetails,
                        StatusMessage = "Ok"
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Response<UserDetail>> GetUserDetailById(int Id)
        {
            try
            {
                string text = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json");
                var userDetails = JsonSerializer.Deserialize<List<UserDetail>>(text);
                var userResult = userDetails.FirstOrDefault(x => x.Id == Id);
                if (userResult != null)
                {
                    return new Response<UserDetail>
                    {
                        Result = userResult,
                        StatusMessage = "Ok"
                    };
                }
                else
                {
                    return new Response<UserDetail>
                    {
                        StatusMessage = "No record found.!"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Response<UserDetail>> AddUserDetail(AddUserDetailDTO addUserDetailRequestDTO)
        {
            try
            {
                string userList = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
                var userDetails = JsonSerializer.Deserialize<List<User>>(userList);
                foreach (var itemUser in userDetails)
                {
                    if (itemUser.UserId == addUserDetailRequestDTO.UserId)
                    {
                        var user1 = new UserDetail()
                        {
                            Id = generateId(),
                            FirstName = addUserDetailRequestDTO.FirstName,
                            LastName = addUserDetailRequestDTO.LastName,
                            Gender = addUserDetailRequestDTO.Gender,
                            Email = addUserDetailRequestDTO.Email,
                            Specialization = addUserDetailRequestDTO.Specialization,
                            IsEmployee = addUserDetailRequestDTO.IsEmployee,
                            UserId = itemUser.UserId,
                        };
                        if (user1 != null)
                        {
                            string text = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json");
                            var userResults = JsonSerializer.Deserialize<List<UserDetail>>(text);
                            foreach (var item in userResults)
                            {
                                if (item.Id == addUserDetailRequestDTO.UserId)
                                {
                                    return new Response<UserDetail>
                                    {
                                        StatusMessage = "This record already exist."
                                    };
                                }
                            }
                            userResults.Add(user1);
                            string json = JsonSerializer.Serialize(userResults);
                            File.WriteAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserDetailList.json", json);

                            return new Response<UserDetail>
                            {
                                Result = user1,
                                StatusMessage = "Ok"
                            };
                        }
                        else
                        {
                            return new Response<UserDetail>
                            {
                                StatusMessage = "No Record found..!"
                            };
                        }
                    }
                    else
                    {
                        return new Response<UserDetail>
                        {
                            StatusMessage = "UserId and Id must be same."
                        };
                    }
                }
                return new Response<UserDetail>
                {
                    StatusMessage = "Please add user first"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Response<User>> AddUser(AddUserDTO addUserRequestDTO)
        {
            try
            {

                var user = new User()
                {
                    UserId = generateId(),
                    UserName = addUserRequestDTO.UserName,
                    Password = addUserRequestDTO.Password,
                };

                if (user != null)
                {
                    string text = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
                    var userResults = JsonSerializer.Deserialize<List<User>>(text);
                    foreach (var item in userResults)
                    {
                        if (item.UserId == user.UserId)
                        {
                            return new Response<User>
                            {
                                StatusMessage = "This record already exist."
                            };
                        }
                    }
                    userResults.Add(user);
                    string json = JsonSerializer.Serialize(userResults);
                    File.WriteAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json", json);

                    return new Response<User>
                    {
                        Result = user,
                        StatusMessage = "Ok"
                    };
                }
                else
                {
                    return new Response<User>
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

