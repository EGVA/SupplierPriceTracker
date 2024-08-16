using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.ViewModels;
namespace SupplierPriceTracker.Controllers
{
	[Authorize]
	public class VendorController(IVendorRepository vendorRepository) : Controller
	{
		private readonly IVendorRepository _vendorRepository = vendorRepository;

		public async Task<IActionResult> Index()
		{
			var vendors = await _vendorRepository.GetAllAsync();
			return View(new VendorIndexVM() { ViewVendors = vendors });
		}

		[HttpPost("/Vendor/Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAsync([Bind("Name")] Vendor vendor)
		{
			if (!ModelState.IsValid)
			{
				var vendors = await _vendorRepository.GetAllAsync();
				return View("Index", new VendorIndexVM() { ViewVendors = vendors, Vendor = vendor });
			}
			await _vendorRepository.AddAsync(vendor);
			return RedirectToAction("Index");
		}

		public async Task<IEnumerable<Vendor>> SearchVendor(string name, bool? isDeleted)
		{
			if (String.IsNullOrWhiteSpace(name) && isDeleted == null)
			{
				return await _vendorRepository.GetAllAsync();
			}
			var searchResult = await _vendorRepository.SearchVendor(name, isDeleted);
			return searchResult;
		}
	}
}
