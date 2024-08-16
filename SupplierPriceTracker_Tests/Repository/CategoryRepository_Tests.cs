using FluentAssertions;
using Moq;
using SupplierPriceTracker.Data;
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
	public class CategoryRepository_Tests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private DbConnection? _connection;
        public CategoryRepository_Tests()
        {
            Utils.InstantiateDB(out _connection, out _context!);
        }

		public async void CategoryRepository_GetAllAsync_ReturnsIEnumerableProductCategory()
		{
			// Arrange
			List<ProductCategory> productCategory = new List<ProductCategory>();
			for (int i = 0; i < 10; i++)
			{
				productCategory.Add(Mock.Of<ProductCategory>());
			}
			_context.ProductCategories.AddRange(productCategory);
			ApplicationDbContext _context1 = _context;
			await _context1.SaveChangesAsync();
			CategoryRepository _categoryRepository = new(_context);

			// Act
			var result = await _categoryRepository.GetAllAsync();

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
