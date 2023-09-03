using DemoProject1.API.Model.Domain;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text.Json;
using TestProject1.API.Model.DTO;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TestProject1.API.Repository
{
    public class UserDetailRepository<T> : ISchoolRepository<T> where T : class
    {

        public List<T> Get(string path)
        {
            var jsonUserData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T>>(jsonUserData);
        }

        public List<T> GetById(string path, int id)
        {
            var jsonUserData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T>>(jsonUserData);

        }
        public List<T> Set(string path, AddUserDetailDTO addUserDetailRequestDTO)
        {
            var jsonUserData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T>>(jsonUserData);
        }
    }
}
