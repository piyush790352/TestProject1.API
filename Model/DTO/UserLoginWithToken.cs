namespace TestProject1.API.Model.DTO
{
    public class UserLoginWithToken
    {
        public int UserId { get; set; }
        public string Token { get; set; }
       public UserDetailDTO UserDetailDTO { get; set; }
    }
}
