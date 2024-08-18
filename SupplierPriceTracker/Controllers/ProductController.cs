using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.ViewModels;

namespace SupplierPriceTracker.Controllers
{
	public class ProductController (IProductRepository productRepository, IMeasureUnitRepository measureUnitRepository, ICategoryRepository categoryRepository, ILogger<ProductController> logger) : Controller
	{
		private readonly IProductRepository _productRepository = productRepository;
		private readonly IMeasureUnitRepository _measureUnitRepository = measureUnitRepository;
		private readonly ICategoryRepository _categoryRepository = categoryRepository;
		private readonly ILogger<ProductController> _logger = logger;

		public async Task<IActionResult> Index()
		{
			ProductIndexVM vm = new()
			{
				Products = await _productRepository.GetAllAsync()
			};
			return View(vm);
		}

		public async Task<PartialViewResult> GetCreateForm()
		{
			ProductFormVM vm = new()
			{
				ProductCategories = await _categoryRepository.GetAllAsync(),
				MeasureUnits = await _measureUnitRepository.GetAllAsync()
			};

			return PartialView("_CreateProductForm", vm);
		}

		[HttpPost("product")]
		public async Task<IActionResult> CreateProductAsync([Bind("Name", "ProductCategoryId", "MeasureUnitId")] Product product)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				return await GetCreateForm(); // Return a new form with validation error
			}
            return Created();
        }
    }
}
