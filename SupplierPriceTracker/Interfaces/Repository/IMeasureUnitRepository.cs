using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Interfaces.Repository
{
	public interface IMeasureUnitRepository
	{
		Task<IEnumerable<MeasureUnit>> GetAllAsync();
	}
}
