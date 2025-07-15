
using Microsoft.EntityFrameworkCore;
using bulkbuy.api.Models;




namespace bulkbuy.api.data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }

}

