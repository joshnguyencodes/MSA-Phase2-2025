namespace bulkbuy.api.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required User Owner { get; set; }
        public List<User> Contributors { get; set; }

        public int GoalAmount { get; set; }
    


    }
}