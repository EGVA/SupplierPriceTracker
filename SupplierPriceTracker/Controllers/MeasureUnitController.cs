using Microsoft.AspNetCore.Mvc;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.Repository;

namespace SupplierPriceTracker.Controllers
{
	public class MeasureUnitController (IMeasureUnitRepository measureUnitRepository) : Controller
	{
		IMeasureUnitRepository _measureUnitRepository = measureUnitRepository;

		public async Task<IEnumerable<MeasureUnit>> GetAllAsync()
		{
			return await _measureUnitRepository.GetAllAsync();
		}
	}
}
