using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SupplierPriceTracker.Controllers;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierPriceTracker_Tests.Controller
{
	public class ProductCategoryController_Tests
	{
		private Mock<ICategoryRepository>? _categoryRepository;

		[Fact(DisplayName = "ProductCategory GetAllAsync Returns <IEnumerable<ProductCategory>>")]
		public async void ProductCategory_GetAllAsync_ReturnsIEnumerableProductCategory()
		{
			// Arrange
			_categoryRepository = new Mock<ICategoryRepository>();
			ProductCategoryController _productCategoryController = new(_categoryRepository.Object);
			List<ProductCategory> pCategories = [];
			for (int i = 0; i < 10; i++)
			{
				pCategories.Add(Mock.Of<ProductCategory>());
			}
			_categoryRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<ProductCategory>>(pCategories));

			// Act
			var result = await _productCategoryController.GetAllAsync();

			// Assert
			result.Should().BeOfType<List<ProductCategory>>();
			result.Should().HaveCount(10);
		}
	}
}
