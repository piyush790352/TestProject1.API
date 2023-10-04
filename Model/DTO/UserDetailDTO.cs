using DemoProject1.API.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace TestProject1.API.Model.DTO
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        public string Specialization { get; set; }
        public bool IsEmployee { get; set; }      
    }
}
