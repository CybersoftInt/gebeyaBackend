﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gebeya01.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        [ForeignKey("Person")]
        public int UserID { get; set; }
        public  Person Person { get; set; }

        [StringLength(100)]
        public string StreetAddress { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string Country { get; set; }
        public int ZIPCode { get; set; }

        public bool IsDefault { get; set; }
    }
}
