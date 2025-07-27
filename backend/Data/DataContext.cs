
using Microsoft.EntityFrameworkCore;
using bulkbuy.api.Models;

namespace bulkbuy.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderContributor> OrderContributors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Group to Creator relationship so that User can't be deleted if they own a group
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Owner)
                .WithMany(o => o.OwnedGroups)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure composite primary key for GroupMember
            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.UserId, gm.GroupId });

            // Configure GroupMember relationships as the join entity
            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMemberships)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order relationships so when group is deleted, all orders are also deleted
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Group)
                .WithMany(g => g.Orders)
                .HasForeignKey(o => o.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order to Owner relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Owner)
                .WithMany(u => u.OwnedOrders)
                .HasForeignKey(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure composite primary key for OrderContributor
            modelBuilder.Entity<OrderContributor>()
                .HasKey(oc => new { oc.UserId, oc.OrderId });

            // Configure OrderContributor relationships as the join entity
            modelBuilder.Entity<OrderContributor>()
                .HasOne(oc => oc.User)
                .WithMany(u => u.OrderContributions)
                .HasForeignKey(oc => oc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderContributor>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.Contributors)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderContributor>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.Contributors)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

