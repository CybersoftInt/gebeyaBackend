using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gebeya01.Dto
{
    public class ProductDto
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(int.MaxValue)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }


        [StringLength(100)]
        public string Color { get; set; }

        [StringLength(32)]
        public string Size { get; set; }

        public int StockQuantity { get; set; }

        [StringLength(255)]
        public string ImageURL { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }
    }
}
