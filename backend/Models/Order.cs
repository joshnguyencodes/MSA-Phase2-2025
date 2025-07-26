namespace bulkbuy.api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required Group BuyingGroup { get; set; }

        public required User Owner { get; set; }
        public List<User> Contributors { get; set; }
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