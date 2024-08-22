using System.ComponentModel.DataAnnotations;

namespace gebeya01.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        //public int UserID { get; set; }
        //public  Person Person { get; set; }

        [StringLength(100)]
        public string StreetAddress { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public bool IsDefault { get; set; }
        public ICollection<PersonAddress> personAddresses { get; set; }
    }
}
