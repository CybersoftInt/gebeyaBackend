using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gebeya01.Models
{


public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(int.MaxValue)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public int CategoryID { get; set; }


    [StringLength(100)]
    public string Color { get; set; }

    [StringLength(32)]
    public string Size { get; set; }

    public int StockQuantity { get; set; }

    [StringLength(255)]
    public string ImageURL { get; set; }
    public bool IsInWishList { get; set; }
    public bool IsInCart { get; set; }

        [StringLength(100)]
    public string Brand { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }

        public virtual Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}