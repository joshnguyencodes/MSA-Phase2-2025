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

        // Many to Many relationship with group membership
        public virtual ICollection<GroupMember> GroupsMember { get; set; } = new List<GroupMember>();

        // Many to One relationship with orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // Empty constructor for EF Core
        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
