using FluentAssertions;
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
	public class VendorRepository_Tests : IDisposable
	{
		protected readonly ApplicationDbContext _context;
		private DbConnection _connection;
		public VendorRepository_Tests()
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

		[Fact(DisplayName = "Vendor Repositoy AddAsync")]
		public async void VendorRepositoy_AddAsync_ReturnsBool()
		{
			// Arrange
			VendorRepository _vendorRepository = new VendorRepository(_context);

			// Act
			var result = await _vendorRepository.AddAsync(Mock.Of<Vendor>());

			// Assert
			result.Should().BeTrue();
			_context.Vendors.Any().Should().BeTrue();
		}

		[Fact(DisplayName = "Vendor Repositoy DeleteAsync")]
		public async void VendorRepository_DeleteAsync_ReturnsBool()
		{
			// Arrange
			Vendor vendor = Mock.Of<Vendor>(x => x.IsDeleted == false);
			_context.Vendors.Add(vendor);
			await _context.SaveChangesAsync();
			VendorRepository _vendorRepository = new VendorRepository(_context);

			// Act
			var result = await _vendorRepository.DeleteAsync(vendor);

			// Assert
			result.Should().BeTrue();
			_context.Vendors.FirstOrDefault(x => x.Id == 1)!.IsDeleted.Should().BeTrue();
		}

		[Fact(DisplayName = "Vendor Repositoy UpdateAsync")]
		public async void VendorRepository_UpdateAsync_ReturnsBool()
		{
			// Arrange
			Vendor vendor = new Vendor()
			{
				Id = 1,
				Name = "Test",
				IsDeleted = false
			};
			_context.Vendors.Add(vendor);
			await _context.SaveChangesAsync();
			VendorRepository _vendorRepository = new VendorRepository(_context);
			vendor.Name = "Updated";

			// Act
			var result = await _vendorRepository.UpdateAsync(vendor);

			// Assert
			result.Should().BeTrue();
			_context.Vendors.FirstOrDefault(x => x.Id == 1)!.Name.Should().Be("Updated");
		}

		[Fact(DisplayName = "Vendor Repositoy GetAllAsync")]
		public async void VendorRepository_GetAllAsync_ReturnIenumerableVendor()
		{
			// Arrange
			List<Vendor> vendors = new List<Vendor>();
			for (int i = 0; i < 10; i++)
			{
				vendors.Add(Mock.Of<Vendor>());
			}
			_context.Vendors.AddRange(vendors);
			await _context.SaveChangesAsync();
			VendorRepository _vendorRepository = new VendorRepository(_context);

			// Act
			var result = await _vendorRepository.GetAllAsync();

			// Assert
			result.Should().NotBeEmpty();
			result.Should().HaveCount(10);
		}

		[Fact(DisplayName = "Vendor Repository SearchVendor")]
		public async void VendorResository_SearchVendor_ReturnIEnumerableVendor()
		{
			// Arrange
			List<Vendor> vendors = [
				new Vendor() { Id = 1, Name = "A", IsDeleted = false },
				new Vendor() { Id = 2, Name = "B", IsDeleted = false },
				new Vendor() { Id = 3, Name = "AB", IsDeleted = false },
				new Vendor() { Id = 4, Name = "AB", IsDeleted = true },
			];
			_context.Vendors.AddRange(vendors);
			await _context.SaveChangesAsync();
			VendorRepository _vendorRepository = new (_context);

			// Act
			var result = await _vendorRepository.SearchVendor("A");
			var resultIsDeleted = await _vendorRepository.SearchVendor("A", true);

			// Assert
			result.Should().BeOfType<List<Vendor>>();
			result.Should().HaveCount(2);
			resultIsDeleted.Should().HaveCount(1);
		}

		[Fact(DisplayName = "Vendor Repositoy SaveAsync")]
		public async void VendorRepository_SaveAsync_ReturnsBool()
		{
			// Arrange
			_context.Vendors.Add(Mock.Of<Vendor>());
			VendorRepository _vendorRepository = new VendorRepository(_context);

			// Act
			var result = await _vendorRepository.SaveAsync();

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
