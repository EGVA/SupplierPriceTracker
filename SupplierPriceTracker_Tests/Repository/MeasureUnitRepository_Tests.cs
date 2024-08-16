using FluentAssertions;
using Moq;
using SupplierPriceTracker.Data;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierPriceTracker_Tests.Repository
{
	public class MeasureUnitRepository_Tests : IDisposable
	{
		private readonly ApplicationDbContext _context;
		private DbConnection? _connection;

		public MeasureUnitRepository_Tests()
		{
			Utils.InstantiateDB(out _connection, out _context!);
		}

		[Fact]
		public async Task MeasureUnitRepository_GetAllAsync_ReturnsIEnumerableMeasureUnitsAsync()
		{
			// Arrange
			List<MeasureUnit> measureUnits = new List<MeasureUnit>();
			for (int i = 0; i < 10; i++)
			{
				measureUnits.Add(Mock.Of<MeasureUnit>());
			}
			_context.MeasureUnits.AddRange(measureUnits);
			ApplicationDbContext _context1 = _context;
			await _context1.SaveChangesAsync();
			MeasureUnitRepository _measureUnitRepository = new(_context);

			// Act
			var result = await _measureUnitRepository.GetAllAsync();

			// Assert
			result.Should().NotBeEmpty();
			result.Should().HaveCount(10);
		}
		public void Dispose()
		{
			if (_connection != null)
			{
				_connection.Dispose();
				_connection = null;
			}
		}
	}
}
