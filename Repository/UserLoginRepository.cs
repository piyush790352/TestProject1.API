using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.Repository
{
    public class UserLoginRepository<T> : ILoginRepository<T> where T : class
    {
      
        public List<T> Login(string path, LoginRequestDTO loginRequestDTO)
        {
            var jsonUserData = new WebClient().DownloadString(path);
            return JsonConvert.DeserializeObject<List<T>>(jsonUserData);
        }
    }
}
