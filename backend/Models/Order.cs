using System.ComponentModel.DataAnnotations;

namespace bulkbuy.api.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        // Many-to-One relationship with Group (FK property)
        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public required Group Group { get; set; }

        // Many-to-One relationship with User (FK property)
        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public required User Owner { get; set; }

        // Many-to-Many relationship with User (FK property)
        public virtual List<User> Contributors { get; set; }

        [Required]
        public int TargetAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Active;


    }

    public enum OrderStatus
    {
        Active,
        Ordering,
        Completed
    }
}