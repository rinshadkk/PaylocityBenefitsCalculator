// filepath: Api/Controllers/PaychecksControllerTest.cs
using Api.Controllers;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Controllers
{
    public class PaychecksControllerTest
    {
        private readonly Mock<IPaycheckService> _paycheckServiceMock;
        private readonly PaychecksController _controller;

        public PaychecksControllerTest()
        {
            _paycheckServiceMock = new Mock<IPaycheckService>();
            _controller = new PaychecksController(_paycheckServiceMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithPaychecks()
        {
            // Arrange
            int employeeId = 1;
            var mockPaychecks = new List<GetPaycheckDto>
            {
                new GetPaycheckDto
                {
                    GrossAmount = 2000,
                    Deductions = new List<DeductionDto>
                    {
                        new DeductionDto { Amount = 100, Description = "Health Insurance" }
                    },
                    PayPeriod = new Dtos.Paycheck.PayPeriodDto
                    {
                        StartDate = new System.DateTime(2023, 1, 1),
                        EndDate = new System.DateTime(2023, 1, 14),
                        PayDate = new System.DateTime(2023, 1, 14)
                    }
                }
            };

            //_paycheckServiceMock
            //    .Setup(service => service.GetAllPayChecks(employeeId)).ReturnsAsync(mockPaychecks);

            // Act
            var result = await _controller.Get(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse<List<GetPaycheckDto>>>(okResult.Value);
            Assert.NotNull(response.Data);
            Assert.Single(response.Data);
            Assert.Equal(2000, response.Data[0].GrossAmount);
            Assert.Single(response.Data[0].Deductions);
            Assert.Equal(100, response.Data[0].Deductions[0].Amount);
        }
    }
}