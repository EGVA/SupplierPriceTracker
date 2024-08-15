using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
	public class ProductRepository_Tests
	{
		protected readonly ApplicationDbContext _context;
		private DbConnection _connection;
		public ProductRepository_Tests()
		{
			_connection = new SqliteConnection("DataSource=:memory:");
			_connection.Open();
			var options = new
			DbContextOptionsBuilder<ApplicationDbContext>()
				.UseSqlite(_connection)
				.Options;
			_context = new ApplicationDbContext(options);
			_context.Database.EnsureCreated();
		}

		private async Task MockProductsContraints()
		{
			List<MeasureUnit> measureUnit = new List<MeasureUnit>();
			for (int i = 0; i < 10; i++)
			{
				measureUnit.Add(Mock.Of<MeasureUnit>());
			}
			_context.MeasureUnits.AddRange(measureUnit);

			List<ProductCategory> category = new List<ProductCategory>();
			for (int i = 0; i < 10; i++)
			{
				category.Add(Mock.Of<ProductCategory>());
			}
			_context.ProductCategories.AddRange(category);

			await _context.SaveChangesAsync();
		}

		[Fact(DisplayName = "Product Repository SearchProduct")]
		public async void ProductRepository_SearchProduct_ReturnsIEnumerableProducts()
		{
			// Assert
			await MockProductsContraints();

			List<Product> products = [
				new Product() { Id = 1, Name = "Teste", MeasureUnitId = 1, ProductCategoryId = 1, IsDeleted = false },
				new Product() { Id = 2, Name = "A", MeasureUnitId = 1, ProductCategoryId = 1, IsDeleted = false },
				new Product() { Id = 3, Name = "Teste", MeasureUnitId = 2, ProductCategoryId = 2, IsDeleted = true },
			];

			_context.Products.AddRange(products);
			await _context.SaveChangesAsync();

			ProductRepository _productsRepository = new(_context);

			// Act
			var result = await _productsRepository.SearchProduct("", null, 1, false);

			// Assert
			result.Should().NotBeEmpty();
			result.Should().BeOfType<List<Product>>();
			result.Should().HaveCount(2);
		}

		[Fact(DisplayName = "Product Repositoy AddAsync")]
		public async void ProductRepositoy_AddAsync_ReturnsBool()
		{
			// Arrange
			await MockProductsContraints();

			ProductRepository _productRepository = new ProductRepository(_context);

			// Act
			var result = await _productRepository.AddAsync(Mock.Of<Product>(x => x.MeasureUnitId == 1 && x.ProductCategoryId == 1));

			// Assert
			result.Should().BeTrue();
			_context.Products.Any().Should().BeTrue();
		}

		[Fact(DisplayName = "Product Repositoy DeleteAsync")]
		public async void ProductRepository_DeleteAsync_ReturnsBool()
		{
			// Arrange
			await MockProductsContraints();

			Product product = Mock.Of<Product>(x => x.Id == 1 && x.MeasureUnitId == 1 && x.ProductCategoryId == 1 && x.IsDeleted == false);
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			ProductRepository _productRepository = new (_context);

			// Act
			var result = await _productRepository.DeleteAsync(product);

			// Assert
			result.Should().BeTrue();
			_context.Products.FirstOrDefault(x => x.Id == 1)!.IsDeleted.Should().BeTrue();
		}

		[Fact(DisplayName = "Product Repositoy UpdateAsync")]
		public async void ProductRepository_UpdateAsync_ReturnsBool()
		{
			// Arrange
			await MockProductsContraints();

			Product product = new Product()
			{
				Id = 1,
				MeasureUnitId = 1,
				ProductCategoryId = 1,
				Name = "Test",
				IsDeleted = false
			};
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			ProductRepository _productRepository = new ProductRepository(_context);
			product.Name = "Updated";

			// Act
			var result = await _productRepository.UpdateAsync(product);

			// Assert
			result.Should().BeTrue();
			_context.Products.FirstOrDefault(x => x.Id == 1)!.Name.Should().Be("Updated");
		}

		[Fact(DisplayName = "Product Repositoy GetAllAsync")]
		public async void ProductRepository_GetAllAsync_ReturnIenumerableVendor()
		{
			// Arrange
			await MockProductsContraints();

			List<Product> products = new List<Product>();
			for (int i = 0; i < 10; i++)
			{
				products.Add(Mock.Of<Product>(x => x.MeasureUnitId == 1 && x.ProductCategoryId == 1));
			}
			_context.Products.AddRange(products);
			await _context.SaveChangesAsync();
			ProductRepository _productRepository = new ProductRepository(_context);

			// Act
			var result = await _productRepository.GetAllAsync();

			// Assert
			result.Should().NotBeEmpty();
			result.Should().HaveCount(10);
		}

		[Fact(DisplayName = "Product Repositoy SaveAsync")]
		public async void ProductRepository_SaveAsync_ReturnsBool()
		{
			// Arrange
			await MockProductsContraints();

			_context.Products.Add(Mock.Of<Product>(x => x.MeasureUnitId == 1 && x.ProductCategoryId == 1));
			ProductRepository _productRepository = new ProductRepository(_context);

			// Act
			var result = await _productRepository.SaveAsync();

			// Assert
			result.Should().BeTrue();
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
