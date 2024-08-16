using Microsoft.AspNetCore.Mvc;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.ViewModels;

namespace SupplierPriceTracker.Controllers
{
	public class ProductController (IProductRepository productRepository, IMeasureUnitRepository measureUnitRepository, ICategoryRepository categoryRepository) : Controller
	{
		private readonly IProductRepository _productRepository = productRepository;
		private readonly IMeasureUnitRepository _measureUnitRepository = measureUnitRepository;
		private readonly ICategoryRepository _categoryRepository = categoryRepository;

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
			ProductFormVM vm = new();

			vm.ProductCategories = await _categoryRepository.GetAllAsync();
			vm.MeasureUnits = await _measureUnitRepository.GetAllAsync();
			
			return PartialView("_ProductForm", vm);
		}
	}
}
