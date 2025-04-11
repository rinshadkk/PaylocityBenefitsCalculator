// filepath: Api/Controllers/PaychecksControllerTest.cs
using Api.Controllers;
using Api.Dtos.Paycheck;
using Api.Mappers;
using Api.Models;
using Api.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Controllers
{
    public class PaychecksControllerTest
    {
        private readonly Mock<IPaycheckService> _paycheckServiceMock;
        private readonly PaychecksController _controller;
        private readonly Mock<IMapper> _mapperMock;

        public PaychecksControllerTest()
        {
            _paycheckServiceMock = new Mock<IPaycheckService>();
            _mapperMock = new Mock<IMapper>();

            _controller = new PaychecksController(_paycheckServiceMock.Object,_mapperMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithPaychecks()
        {
            // Arrange
            int employeeId = 2;
            var mockPaychecks = new List<Paycheck>
            {
                new Paycheck
                {
                    GrossAmount = 2000,
                    Deductions = new List<Deduction>
                    {
                        new Deduction { Amount = 100, Description = "Health Insurance" }
                    },
                    PayPeriod = new PayPeriod
                    {
                        StartDate = new System.DateTime(2023, 1, 1),
                        EndDate = new System.DateTime(2023, 1, 14),
                        PayDate = new System.DateTime(2023, 1, 14)
                    }
                }
            };

            var paychecksDto = mockPaychecks.Select(paycheck => new GetPaycheckDto
            {
                GrossAmount = paycheck.GrossAmount,
                Deductions = paycheck.Deductions.Select(d => new DeductionDto
                {
                    Amount = d.Amount,
                    Description = d.Description
                }).ToList(),
                PayPeriod = new PayPeriodDto
                {
                    StartDate = paycheck.PayPeriod.StartDate,
                    EndDate = paycheck.PayPeriod.EndDate,
                    PayDate = paycheck.PayPeriod.PayDate
                }
            }).ToList();

            _paycheckServiceMock
                .Setup(service => service.GetAllPayChecks(It.IsAny<int>())).ReturnsAsync(mockPaychecks);

            _mapperMock.Setup(mapper => mapper.MaptoPaychecksDto(mockPaychecks)).Returns(paychecksDto);

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