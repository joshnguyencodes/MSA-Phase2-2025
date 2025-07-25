namespace bulk.api.Models
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required User Owner { get; set; }
        public required List<User> Members { get; set; }

        public List<Order> Orders { get; set; }
    

    }
}