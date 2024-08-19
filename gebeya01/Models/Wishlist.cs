namespace gebeya01.Models
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
