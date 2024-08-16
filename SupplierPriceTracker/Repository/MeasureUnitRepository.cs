using Microsoft.EntityFrameworkCore;
using SupplierPriceTracker.Data;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;

namespace SupplierPriceTracker.Repository
{
	public class MeasureUnitRepository(ApplicationDbContext context) : IMeasureUnitRepository
	{
		private readonly ApplicationDbContext _context = context;
		public async Task<IEnumerable<MeasureUnit>> GetAllAsync()
		{
			return await _context.MeasureUnits.Where(x => x.IsDeleted == false).ToListAsync();
		}
	}
}
