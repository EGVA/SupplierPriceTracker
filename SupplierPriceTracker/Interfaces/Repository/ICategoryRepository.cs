using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Interfaces.Repository
{
	public interface ICategoryRepository
	{
		public Task<IEnumerable<ProductCategory>> GetAllAsync();
	}
}
