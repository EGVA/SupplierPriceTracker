using Microsoft.EntityFrameworkCore;
using SupplierPriceTracker.Data;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Repository
{
	public class CategoryRepository (ApplicationDbContext context): ICategoryRepository
	{
		private readonly ApplicationDbContext _context = context;
		public async Task<IEnumerable<ProductCategory>> GetAllAsync()
		{
			return await _context.ProductCategories.Where(x => x.IsDeleted == false).ToListAsync();
		}
	}
}
