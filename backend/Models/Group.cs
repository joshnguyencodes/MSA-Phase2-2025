using System.ComponentModel.DataAnnotations;

namespace bulkbuy.api.Models
{
    public class Group
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public string Theme { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        
        // Navigation properties
        public virtual ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}