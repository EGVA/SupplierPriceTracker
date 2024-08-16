using Microsoft.AspNetCore.Mvc;
using SupplierPriceTracker.Data;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using System.Diagnostics.CodeAnalysis;

namespace SupplierPriceTracker.Controllers
{
	public class ProductCategoryController(ICategoryRepository categoryRepository) : Controller
	{
		private readonly ICategoryRepository _categoryRepository = categoryRepository;

		public async Task<IEnumerable<ProductCategory>> GetAllAsync()
		{
			return await _categoryRepository.GetAllAsync();
		}
	}
}
