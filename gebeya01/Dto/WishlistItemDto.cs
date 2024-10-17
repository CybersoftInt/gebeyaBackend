using System;

namespace gebeya01.Dto
{
    public class WishlistItemDto
    {
        public int WishlistItemID { get; set; }
        public int WishlistID { get; set; }
        public int ProductID { get; set; }
        public DateTime AddedDate { get; set; }
        // Optionally include UserID if needed for convenience
        // public int UserID { get; set; }
    }
}
