using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace bulkbuy.api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [NotMapped]
        public string? Password { get; set; }
        
        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<GroupMember> GroupMemberships { get; set; } = new List<GroupMember>();
        public virtual ICollection<Order> OwnedOrders { get; set; } = new List<Order>();
        public virtual ICollection<Group> OwnedGroups { get; set; } = new List<Group>();
        public virtual ICollection<OrderContributor> OrderContributions { get; set; } = new List<OrderContributor>();

        // Empty constructor for EF Core
        public User() { }
        
        // Constructor for application use
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
