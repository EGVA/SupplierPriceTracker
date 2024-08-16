using Microsoft.AspNetCore.Mvc;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.ViewModels;

namespace SupplierPriceTracker.Controllers
{
	public class ProductController (IProductRepository productRepository) : Controller
	{
		private readonly IProductRepository _productRepository = productRepository;
		public async Task<IActionResult> Index()
		{
			ProductIndexVM vm = new()
			{
				Products = await _productRepository.GetAllAsync()
			};
			return View(vm);
		}

		public PartialViewResult CreateForm()
		{
			// To Do : Get products measure unit list.
			// To Do : Get products category list.
			return PartialView("_ProductForm");
		}
	}
}
