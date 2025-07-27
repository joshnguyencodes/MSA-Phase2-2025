using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace bulkbuy.api.Models
{
    public class OrderContributor
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; } = null!;

        [Range(0.01, double.MaxValue)]
        public decimal ContributionAmount { get; set; }

        public DateTime ContributedAt { get; set; } = DateTime.UtcNow;
    }
}
