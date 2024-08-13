using System.ComponentModel.DataAnnotations;

namespace SupplierPriceTracker.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }

        [Required]
        public int MeasureUnitId { get; set; }
        public MeasureUnit? MeasureUnit { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
