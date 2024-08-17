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
		private Mock<IMeasureUnitRepository>? _mockMeasureUnitRepository;
		private Mock<ICategoryRepository>? _mockCategoryRepository;

		[Fact(DisplayName = "Product Controller Index Returns IActionResult")]
		public async void ProductController_Index_ReturnsIActionResult()
		{
			// Arrange
			_mockProductRepository = new Mock<IProductRepository>();
			_mockCategoryRepository = new Mock<ICategoryRepository>();
			_mockMeasureUnitRepository = new Mock<IMeasureUnitRepository>();
			ProductController _productController = new (_mockProductRepository.Object, _mockMeasureUnitRepository.Object, _mockCategoryRepository.Object);
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

		[Fact(DisplayName = "Product Controller GetCreateForm Returns Task<PartialViewResult>")]
		public async void ProductController_GetCreateForm_ReturnsTaskPartialViewResult()
		{
			// Arrange
			_mockProductRepository = new Mock<IProductRepository>();
			_mockCategoryRepository = new Mock<ICategoryRepository>();
			_mockMeasureUnitRepository = new Mock<IMeasureUnitRepository>();
			ProductController _productController = new(_mockProductRepository.Object, _mockMeasureUnitRepository.Object, _mockCategoryRepository.Object);

            List<ProductCategory> categories = [];
            List<MeasureUnit> measureUnits = [];
            for (int i = 0; i < 10; i++)
            {
                categories.Add(Mock.Of<ProductCategory>());
                measureUnits.Add(Mock.Of<MeasureUnit>());
            }

			_mockCategoryRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ProductCategory>>(categories));
			_mockMeasureUnitRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<MeasureUnit>>(measureUnits));

			// Act
			var result = await _productController.GetCreateForm();

            // Assert
            result.Should().BeOfType<PartialViewResult>();
            PartialViewResult? viewResult = result as PartialViewResult;
            viewResult?.Model.Should().NotBeNull();
            var data = viewResult?.Model as ProductFormVM;
			data?.MeasureUnits.Should().HaveCount(10);
			data?.ProductCategories.Should().HaveCount(10);
        }
    }
}
