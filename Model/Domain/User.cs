using System.ComponentModel.DataAnnotations;

namespace DemoProject1.API.Model.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only characters are allow.")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "First Name must be at least 4 characters long and maximum upto 15 characters.")]
        public string UserName { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only characters are allow.")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Last Name must be at least 4 characters long and maximum upto 15 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only characters are allow.")]
        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Gender Must be at least 4 characters long and maximum upto 8 characters.")]
        public string UserType { get; set; }
    }
}
