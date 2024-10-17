using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gebeya01.Models
{
    public class WishlistItem
    {
        [Required]
        public int WishlistItemID { get; set; }
        [Required]
        public int WishlistID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }

        public virtual Wishlist Wishlist { get; set; }
        public virtual Product Product { get; set; }
    }
}
