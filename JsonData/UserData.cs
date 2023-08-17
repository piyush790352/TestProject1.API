using DemoProject1.API.Model.Domain;
using DemoProject1.API.Repository;

namespace DemoProject1.API.JsonData
{
    public class UserData
    {
        public static List<UserDetail> GetUsers()
        {
            List<UserDetail> userDetails = new List<UserDetail>();

            userDetails.Add(new UserDetail { Id = UserRepository.generateId(), FirstName = "Ajit", LastName = "Kumar", Gender = "Male", Email = "ajit@gmail.com", IsEmployee = true });
            userDetails.Add(new UserDetail { Id = UserRepository.generateId(), FirstName = "Archana", LastName = "Singh", Gender = "Female", Email = "archana@gmail.com", IsEmployee = true });
            userDetails.Add(new UserDetail { Id = UserRepository.generateId(), FirstName = "Anikt",  LastName = "Sharma", Gender = "male", Email = "ankit@gmail.com", IsEmployee = true });
            userDetails.Add(new UserDetail { Id = UserRepository.generateId(), FirstName = "Roshni", LastName = "Kumari", Gender = "Female", Email = "roshni@gmail.com", IsEmployee = true });
            userDetails.Add(new UserDetail { Id = UserRepository.generateId(), FirstName = "Richa", LastName = "Das", Gender = "Female", Email = "richa@gmail.com", IsEmployee = true });
            return userDetails;
        }
    }
}