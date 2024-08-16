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
	public class ProductController_Tests
	{
		private Mock<IProductRepository>? _mockProductRepository;

		[Fact(DisplayName = "Product Controller Index Returns IActionResult")]
		public async void ProductController_Index_ReturnsIActionResult()
		{
			// Arrange
			_mockProductRepository = new Mock<IProductRepository>();
			ProductController _productController = new	 (_mockProductRepository.Object);
			List<Product> products = [];
			for (int i = 0; i < 10; i++) { 
				products.Add(Mock.Of<Product>());
			}
			_mockProductRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Product>>(products));

			// Act
			var result = await _productController.Index();

			// Assert
			result.Should().BeOfType<ViewResult>();
			ViewResult? viewResult = result as ViewResult;
			viewResult?.Model.Should().NotBeNull();
			var data = viewResult?.Model as List<Product>;
			data?.ToList().Count.Should().Be(10);
		}
	}
}
