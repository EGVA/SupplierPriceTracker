using System.ComponentModel.DataAnnotations;

namespace SupplierPriceTracker.Models
{
    public class Product
    {
        [Key]
		[Display(Name = "ID")]
		public int Id { get; set; }
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required]
		[Display(Name = "Categoria")]
        public int ProductCategoryId { get; set; }
		public ProductCategory? ProductCategory { get; set; }

        [Required]
		[Display(Name = "Un. de Medida")]
        public int MeasureUnitId { get; set; }
		public MeasureUnit? MeasureUnit { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
