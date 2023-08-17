using DemoProject1.API.JsonData;
using DemoProject1.API.Model.Domain;
using System.Text.Json;

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
                string text = File.ReadAllText(@"D:\DotnetCoreProjects\DemoProject\DemoProject1.API\JsonData.json");
                var userDetails = JsonSerializer.Deserialize<List<UserDetail>>(text);
                if (userDetails.Count == 0)
                {
                    var userResults = UserData.GetUsers().ToList();
                    if (userResults != null)
                    {
                        string json = JsonSerializer.Serialize(userResults);
                        File.WriteAllText(@"D:\DotnetCoreProjects\DemoProject\DemoProject1.API\JsonData.json", json);
                        return new Response<List<UserDetail>>
                        {
                            Result = userResults,
                            StatusMessage = "Ok"
                        };
                    }
                    else
                    {
                        return new Response<List<UserDetail>>
                        {
                            StatusMessage = "No recored found!."
                        };
                    }
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
    }
}
