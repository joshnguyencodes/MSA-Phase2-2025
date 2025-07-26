namespace bulkbuy.api.Models
{
    public class OrderContributor
    {

        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
        public int Amount { get; set; } = 0;
        
    }
}
