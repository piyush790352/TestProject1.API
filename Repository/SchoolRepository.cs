using DemoProject1.API.Model.Domain;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text.Json;
using TestProject1.API.Model.DTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestProject1.API.Repository
{
    public class SchoolRepository<T> : ISchoolRepository<T> where T : class
    {

        public List<T> Get(string path)
        {
            string jsonUserData = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(jsonUserData);
        }

        public List<T> GetById(string path, int id)
        {
            var jsonUserData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T>>(jsonUserData);

        }
        public void Set(string path, List<T> addDataRequest)
        {
            string resultData = JsonSerializer.Serialize(addDataRequest);
            File.WriteAllText(path, resultData);
        }
    }
}
