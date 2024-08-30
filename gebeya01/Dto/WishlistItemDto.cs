using System;
using System.ComponentModel.DataAnnotations;

namespace gebeya01.Dto
{
    public class WishlistItemDto
    {
        [Required]
        public int WishlistItemID { get; set; }

        [Required]
        public int WishlistID { get; set; }

        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }
    }
}
