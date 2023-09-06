using DemoProject1.API.Model.Domain;
using System.Globalization;
using TestProject1.API.Model.DTO;

namespace TestProject1.API.Repository
{
    public interface ISchoolRepository<T> where T : class
    {
        public List<T> Get(string path);
        public List<T> GetById(string path,int id);
        public void Set(string path, List<T> addDataRequest);
    }
}
