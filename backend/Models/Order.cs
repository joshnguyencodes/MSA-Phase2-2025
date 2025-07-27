using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace bulkbuy.api.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Many-to-One relationship with Group (FK property)
        [Required]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; } = null!;

        // Many-to-One relationship with User (FK property)
        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; } = null!;

        // One-to-Many relationship with OrderContributor
        public virtual ICollection<OrderContributor> Contributors { get; set; } = new List<OrderContributor>();

        [Required]
        [Range(1, int.MaxValue)]
        public int TargetAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Active;

        // Empty constructor for EF Core
        public Order() { }
    }

    public enum OrderStatus
    {
        Active,
        Ordering,
        Completed,
        Cancelled
    }
}