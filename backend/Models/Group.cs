using System.ComponentModel.DataAnnotations;

namespace bulkbuy.api.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public datetime CreatedAt { get; set; } = DateTime.UtcNow;

        // Many to One relationships with User
        [Required]
        public User OwnerID { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        // One to Many relationship with Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // One to Many relationship with members
        public Icollection<GroupMember> Members { get; set; } = new List<GroupMember>();

        // Default Constructor for EF core
        public Group() { }


    }

}


















