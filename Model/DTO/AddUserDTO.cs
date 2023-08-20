using System.ComponentModel.DataAnnotations;

namespace TestProject1.API.Model.DTO
{
    public class AddUserDTO
    {
        [Required(ErrorMessage = "User Name is required.")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{6,}$", ErrorMessage = "User Name must be minimum six characters long, one letter, one number and one special character.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be minimum eight characters long, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
