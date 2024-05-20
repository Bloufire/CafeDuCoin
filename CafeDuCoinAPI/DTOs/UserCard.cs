namespace CafeDuCoinAPI.DTOs
{
    // Define the UserCard class to represent a DTO (Data Transfer Object) for displaying user information
    public class UserCard
    {
        // Constructor with a user parameter to initialize properties
        public UserCard(ApplicationUser user)
        {
            this.Login = user.UserName;
            this.Email = user.Email;
        }

        public string Login { get; set; }
        public string Email { get; set; }
    }
}
