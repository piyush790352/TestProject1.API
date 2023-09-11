namespace TestProject1.API.Model.DTO
{
    public class UserLoginWithToken
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
