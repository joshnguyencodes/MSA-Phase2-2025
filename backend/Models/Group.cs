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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Many to One relationships with User
        [Required]
        public int OwnerID { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        // One to Many relationship with Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // Many to Many relationship with group members
        public virtual Icollection<GroupMember> Members { get; set; } = new List<GroupMember>();

        // Default Constructor for EF core
        public Group() { }


    }

}


















