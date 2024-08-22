using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gebeya01.Models
{
    public class Person
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

       // public int AddressID { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        public string? Role { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
        public ICollection<WishlistItem> wishlistItems { get; set; }
        //public ICollection<OrderItem> orderitems { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public ICollection<PersonAddress> personAddresses { get; set; }
    }
}