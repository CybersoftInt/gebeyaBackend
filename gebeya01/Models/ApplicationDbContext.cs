using Microsoft.EntityFrameworkCore;

namespace gebeya01.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasKey(p => p.UserID);
            modelBuilder.Entity<Person>()
           .HasOne(p => p.Address)
           .WithMany()
           .HasForeignKey(p => p.AddressID);
            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Address>().HasKey(a => a.AddressID);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemID);
            modelBuilder.Entity<ShoppingCart>().HasKey(sc => sc.CartID);
            modelBuilder.Entity<CartItem>().HasKey(ci => ci.CartItemID);
            modelBuilder.Entity<Payment>().HasKey(p => p.PaymentID);
            modelBuilder.Entity<Shipment>().HasKey(s => s.ShipmentID);
            modelBuilder.Entity<Wishlist>().HasKey(w => w.WishlistID);
            modelBuilder.Entity<WishlistItem>().HasKey(wi => wi.WishlistItemID);
        }
    }
}