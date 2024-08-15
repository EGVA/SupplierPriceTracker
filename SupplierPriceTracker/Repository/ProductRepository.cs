using Microsoft.EntityFrameworkCore;
using SupplierPriceTracker.Data;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using System.Numerics;

namespace SupplierPriceTracker.Repository
{
	public class ProductRepository(ApplicationDbContext context) : IProductRepository
	{
		ApplicationDbContext _context = context;
		public async Task<bool> AddAsync(Product product)
		{
			_context.Products.Add(product);
			return await SaveAsync();
		}

		public async Task<bool> DeleteAsync(Product product)
		{
			product.IsDeleted = true;
			_context.Products.Update(product);
			return await SaveAsync();
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Products.Where(x => x.IsDeleted == false).ToListAsync();
		}

		public async Task<bool> SaveAsync()
		{
			int saved = await _context.SaveChangesAsync();
			return saved > 0;
		}

		public async Task<IEnumerable<Product>> SearchProduct(string? name, int? category, int? measureUnit, bool isDeleted = false)
		{
			// Start the query, insert the Includes and where isDeleted (who have default value)
			var query = _context.Products.Include(x => x.MeasureUnit).Include(x => x.ProductCategory).Where(x => x.IsDeleted == isDeleted).AsQueryable();

			// Add optionals search options to the query
			if (string.IsNullOrWhiteSpace(name)) query = query.Where(x => x.Name.Contains(name!));
			if (category != null) query = query.Where(x => x.ProductCategoryId == category);
			if(measureUnit != null) query = query.Where(x => x.MeasureUnitId == measureUnit);

			// Do the search
			return await query.ToListAsync();
		}

		public async Task<bool> UpdateAsync(Product product)
		{
			_context.Products.Update(product);
			return await SaveAsync();
		}
	}
}
