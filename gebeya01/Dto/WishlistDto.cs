using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gebeya01.Dto
{
    public class WishlistDto
    {
        public int WishlistID { get; set; }

        public int UserID { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
