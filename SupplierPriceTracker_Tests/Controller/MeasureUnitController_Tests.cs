using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;
using SupplierPriceTracker.Controllers;
using SupplierPriceTracker.Interfaces.Repository;
using SupplierPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierPriceTracker_Tests.Controller
{
	public class MeasureUnitController_Tests
	{
		private Mock<IMeasureUnitRepository>? _mockMUnitRepository;

		[Fact]
		public async Task MeasureUnitController_GetAllAsync_ReturnsIEnumerableMeasureUnitAsync()
		{
			// Assert
			_mockMUnitRepository = new Mock<IMeasureUnitRepository>();
			MeasureUnitController mUnitController = new(_mockMUnitRepository.Object);
			List<MeasureUnit> measureUnits = [];
			for (int i = 0; i < 10;  i++)
			{
				measureUnits.Add(Mock.Of<MeasureUnit>());
			}
			_mockMUnitRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult<IEnumerable<MeasureUnit>>(measureUnits));

			// Act
			var result = await mUnitController.GetAllAsync();

			// Assert
			result.Should().BeOfType<List<MeasureUnit>>();
			result.Should().HaveCount(10);
		}
	}
}
