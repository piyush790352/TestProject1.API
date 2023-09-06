using TestProject1.API.Model.DTO;

namespace TestProject1.API.Repository
{
    public interface ILoginRepository<T> where T : class
    {
        public List<T> Login(string fullPath, LoginRequestDTO loginRequestDTO);
    }
}
