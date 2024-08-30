using System.ComponentModel.DataAnnotations.Schema;

namespace gebeya01.Models
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public Person Person { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }

    }
}
