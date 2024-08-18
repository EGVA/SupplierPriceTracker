using System.ComponentModel.DataAnnotations;

namespace SupplierPriceTracker.Models
{
    public class Product
    {
        [Key]
		[Display(Name = "ID")]
		public int Id { get; set; }
        [Required(ErrorMessage = "É Preciso Especificar um Nome")]
        [MaxLength(100, ErrorMessage = "Maximo de 100 Caracteres Atingido")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "É Preciso Especificar uma Categoria")]
		[Display(Name = "Categoria")]
        public int ProductCategoryId { get; set; }
		public ProductCategory? ProductCategory { get; set; }

        [Required(ErrorMessage = "É Preciso Especificar uma Un. de Medida")]
		[Display(Name = "Un. de Medida")]
        public int MeasureUnitId { get; set; }
		public MeasureUnit? MeasureUnit { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
