using Microsoft.AspNetCore.Mvc;
using SupplierPriceTracker.Interfaces.Repository;

namespace SupplierPriceTracker.Controllers
{
	public class MeasureUnitController (IMeasureUnitRepository measureUnitRepository) : Controller
	{
		IMeasureUnitRepository _measureUnitRepository = measureUnitRepository;
	}
}
