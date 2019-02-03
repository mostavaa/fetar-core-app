using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        DbSet<User> Users { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasMany(o => o.ItemDetails).WithOne(o => o.OrderDetails).HasForeignKey(o => o.OrderDetailsId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>().HasMany(o => o.OrderDetails).WithOne(o => o.Order).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<User>().HasMany(o => o.OrderDetails).WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Item>().HasMany(o => o.ItemDetails).WithOne(o => o.Item).HasForeignKey(o => o.ItemId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
