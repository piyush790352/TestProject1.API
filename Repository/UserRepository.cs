using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using TestProject1.API.Model.DTO;

namespace DemoProject1.API.Repository
{
    public class UserRepository
    {

        public static int generateId()
        {
            int id = 1;
            string userList = File.ReadAllText(@"D:\DotnetCoreProjects\TestProject\TestProject1.API\JsonData\UserList.json");
            var users = JsonSerializer.Deserialize<List<User>>(userList);
            foreach(var item in users)
            {
               id = users.Count();
               id++;
            }
            return id;
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
                        if (item.UserName == user.UserName && item.Password == user.Password)
                        {
                            return new Response<User>
                            {
                                StatusMessage = "User already exist."
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

