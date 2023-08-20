using System.ComponentModel.DataAnnotations;

namespace DemoProject1.API.Model.Domain
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Gender { get; set; }
        public string? Email { get; set; }
        public string Specialization { get; set; }
        public bool IsEmployee { get; set; }
        public int UserId { get; set; }
    }
}
