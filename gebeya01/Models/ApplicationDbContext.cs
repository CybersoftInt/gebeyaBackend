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
            // Configuration for PersonAddress
            modelBuilder.Entity<PersonAddress>()
                .HasKey(pa => new { pa.UserId, pa.AddressId });
            modelBuilder.Entity<PersonAddress>()
                .HasOne(pa => pa.Person)
                .WithMany(p => p.personAddresses)
                .HasForeignKey(pa => pa.UserId);
            modelBuilder.Entity<PersonAddress>()
                .HasOne(pa => pa.Address)
                .WithMany()
                .HasForeignKey(pa => pa.AddressId);

            // Configuration for Wishlist
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Person)
                .WithMany(p => p.Wishlist)
                .HasForeignKey(w => w.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration for WishlistItem
            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.Wishlist)
                .WithMany(w => w.WishlistItems)
                .HasForeignKey(wi => wi.WishlistID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(wi => wi.Product)
                .WithMany() // If Product has no navigation property to WishlistItem, leave it empty
                .HasForeignKey(wi => wi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);
        

        /* Additional entity configurations
        modelBuilder.Entity<Person>().HasKey(p => p.UserID);
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
        modelBuilder.Entity<WishlistItem>().HasKey(wi => wi.WishlistItemID);*/
    }

}
}