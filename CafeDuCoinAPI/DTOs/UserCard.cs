namespace CafeDuCoinAPI.DTOs
{
    public class UserCard
    {
        public UserCard(ApplicationUser user)
        {
            this.Login = user.UserName;
            this.Email = user.Email;
        }

        public string Login { get; set; }
        public string Email { get; set; }
    }
}
