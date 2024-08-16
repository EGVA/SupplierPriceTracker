using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Interfaces.Repository
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<ProductCategory>> GetAllAsync();
	}
}
