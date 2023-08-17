using System.ComponentModel.DataAnnotations;

namespace DemoProject1.API.Model.Domain
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only characters are allow.")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "First Name must be at least 4 characters long and maximum upto 15 characters.")]
        public string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only characters are allow.")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Last Name must be at least 4 characters long and maximum upto 15 characters.")]
        public string? LastName { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only characters are allow.")]
        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Gender Must be at least 4 characters long and maximum upto 8 characters.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address (Email address must be in the form of user@xyz.com).")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        public string Specialization { get; set; }

        public bool IsEmployee { get; set; }


    }
}
