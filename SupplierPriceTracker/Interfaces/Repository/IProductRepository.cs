using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Interfaces.Repository
{
	public interface IProductRepository
	{	
		Task<IEnumerable<Product>> GetAllAsync();
		Task<IEnumerable<Product>> SearchProduct(string? name, int? category, int? measureUnit, bool isDeleted = false);
		Task<bool> AddAsync(Product product);
		Task<bool> DeleteAsync(Product product);
		Task<bool> UpdateAsync(Product product);
		Task<bool> SaveAsync();
	}
}
