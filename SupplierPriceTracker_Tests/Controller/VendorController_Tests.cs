using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SupplierPriceTracker.Controllers;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using SupplierPriceTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierPriceTracker_Tests.Controller
{
	public class VendorController_Tests
	{
		private Mock<IVendorRepository>? _mockVendorRepository;

		[Fact(DisplayName = "Vendor Controller Index")]
		public async void VendorController_Index_ReturnsIActionResult()
		{
			// Arrange
			_mockVendorRepository = new Mock<IVendorRepository>();
			VendorController _vendorController = new(_mockVendorRepository.Object);
			List<Vendor> vendors = [];
			for (int i = 0; i < 10; i++)
			{
				vendors.Add(Mock.Of<Vendor>());
			}
			_mockVendorRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Vendor>>(vendors));

			// Act
			var result = await _vendorController.Index();

			// Assert
			result.Should().BeOfType<ViewResult>();
			ViewResult? viewResult = result as ViewResult;
			viewResult?.Model.Should().NotBeNull();
			var data = viewResult?.Model as VendorIndexVM;
			data?.ViewVendors?.ToList().Count.Should().Be(10);
		}

		[Fact(DisplayName = "Vendor Controller Create")]
		public async void VendorController_Create_ReturnsIActionResult()
		{
			// Arrange
			_mockVendorRepository = new Mock<IVendorRepository>();
			VendorController _vendorController = new(_mockVendorRepository.Object);
			List<Vendor> vendors = [];
			for (int i = 0; i < 10; i++)
			{
				vendors.Add(Mock.Of<Vendor>());
			}
			_mockVendorRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Vendor>>(vendors));
			Vendor newVendor = new () { Name = "Test Vendor" };

			// Act
			var result = await _vendorController.CreateAsync(newVendor);

			// Assert
			result.Should().BeOfType<RedirectToActionResult>();
		}

		[Fact(DisplayName = "Vendor Controller SearchVendor")]
		public async void VendorController_SearchVendor_ReturnsIEnumerableVendor()
		{
			// Arrange
			_mockVendorRepository = new Mock<IVendorRepository>();
			VendorController _vendorController = new(_mockVendorRepository.Object);
			List<Vendor> vendors =  [];
			vendors.AddRange([
				new Vendor() { Id = 1, Name = "A", IsDeleted = false },
				new Vendor() { Id = 3, Name = "AB", IsDeleted = false }
				]);
			_mockVendorRepository.Setup(x => x.SearchVendor("A", false)).Returns(Task.FromResult<IEnumerable<Vendor>>(vendors));

			// Act
			var result = await _vendorController.SearchVendor("A", false);

			// Result
			result.Should().BeOfType<List<Vendor>>();
			result.Should().NotBeNull();
			result.Should().HaveCount(2);
		}
	}
}
