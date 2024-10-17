using gebeya01.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gebeya01.Dto
{
    public class AddressDto
    {
        [Key]
        public int AddressID { get; set; }
        public int UserID { get; set; }

        [StringLength(100)]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }
        public int ZIPCode { get; set; }

        public bool IsDefault { get; set; }
    }
}
