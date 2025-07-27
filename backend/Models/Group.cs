using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace bulkbuy.api.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Theme { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Many to One relationship with User (Owner)
        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; } = null!;

        // One to Many relationship with Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // Many to Many relationship with group members
        public virtual ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();

        // Default Constructor for EF core
        public Group() { }


    }

}


















