using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gebeya01.Models
{
    public class WishlistItem
    {
        public int WishlistItemID { get; set; }
        public int WishlistID { get; set; }
        public int ProductID { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual Wishlist Wishlist { get; set; }
        public virtual Product Product { get; set; }
    }
}
