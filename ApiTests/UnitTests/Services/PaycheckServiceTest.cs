// filepath: Api/Service/PaycheckServiceTest.cs
using Api.Models;
using Api.Service;
using Api.Service.DeductionService.DeductionCalculatorFactory;
using Api.Service.DeductionService.DeductionCalculators;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Service
{
    public class PaycheckServiceTest
    {
        private readonly Mock<IDeductionCalculatorFactory> _deductionCalculatorFactoryMock;
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly PaycheckService _paycheckService;

        public PaycheckServiceTest()
        {
            _deductionCalculatorFactoryMock = new Mock<IDeductionCalculatorFactory>();
            _employeeServiceMock = new Mock<IEmployeeService>();
            _paycheckService = new PaycheckService(_deductionCalculatorFactoryMock.Object, _employeeServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPayChecks_ShouldReturnCorrectNumberOfPaychecks()
        {
            // Arrange
            int employeeId = 1;
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "John Doe",
                Salary = 52000 // Annual salary
            };

            var mockDeductionCalculator = new Mock<IDeductionCalculator>();
            mockDeductionCalculator
                .Setup(dc => dc.CalculateDeduction(It.IsAny<Employee>()))
                .Returns(new Deduction { Amount = 100 });

            _employeeServiceMock
                .Setup(es => es.GetEmployeeById(employeeId))
                .ReturnsAsync(employee);

            _deductionCalculatorFactoryMock
                .Setup(dcf => dcf.GetDeductionCalculators())
                .Returns(new List<IDeductionCalculator> { mockDeductionCalculator.Object });

            // Act
            var paychecks = await _paycheckService.GetAllPayChecks(employeeId);

            // Assert
            Assert.NotNull(paychecks);
            Assert.Equal(26, paychecks.Count); // 26 bi-weekly pay periods
            foreach (var paycheck in paychecks)
            {
                Assert.Equal(employee, paycheck.Employee);
                Assert.Equal(employee.Salary / 26, paycheck.GrossAmount);
                Assert.Single(paycheck.Deductions);
                Assert.Equal(100, paycheck.Deductions.First().Amount);
            }
        }
    }
}