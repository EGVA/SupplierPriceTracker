using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.ViewModels
{
	public class ProductFormVM
	{
		public string Name { get; set; } = string.Empty;
		public int MeasureUnitId { get; set; }
		public int CategoryId { get; set; }

		public IEnumerable<MeasureUnit>? MeasureUnits { get; set; }
        public IEnumerable<ProductCategory>? ProductCategories { get; set; }
    }
}
