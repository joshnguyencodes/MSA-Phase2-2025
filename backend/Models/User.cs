using System.ComponentModel.DataAnnotations;

namespace bulkbuy.api.Models
{
    public class User
    {
  
        public int Id { get; set; }

        [Required]  
        public required string Username { get; set; }

        [NotMapped]
        [Required]
        public required string Password { get; set; }
        public string? PasswordHash { get; set; }

        // Empty constructor for EF Core
        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
