namespace bulkbuy.api.Models
{
    public class User 
    {
        public int Id { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }
        public string? PasswordHash { get; set; }
       
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
