using Api.Models;
using Api.Service.DeductionService.DeductionCalculators;

namespace Api.Service.DeductionService.DeductionCalculatorFactory
{
    /// <summary>
    /// Calculator for deductions based on high salary.
    /// </summary>
    public class HighSalaryDeduction : IDeductionCalculator
    {
        // Salary threshold above which additional deductions are applied
        private const decimal wageBaseLimit = 80000;

        // Additional benefits cost percentage for salaries above the threshold
        private const decimal additionalBenefitsCost = 0.02M;

        /// <summary>
        /// Calculates the high salary deduction for an employee.
        /// </summary>
        /// <param name="employee">The employee for whom the deduction is being calculated.</param>
        /// <returns>A Deduction object containing the type, amount, and description of the deduction.</returns>
        public Deduction CalculateDeduction(Employee employee)
        {
            decimal deductionAmount = 0;

            // Check if the employee's salary exceeds the wage base limit
            if (employee.Salary > wageBaseLimit)
            {
                // Calculate the deduction amount based on the additional benefits cost percentage
                deductionAmount = employee.Salary * additionalBenefitsCost / Constants.Constants.Payroll.totalPayperiods;
            }

            // Return the deduction details
            return new Deduction
            {
                Amount = deductionAmount, // The calculated deduction amount
                Type = DeductionType.HighSalaryDeduction, // Type of deduction
                Description = string.Empty // Description can be updated as needed
            };
        }
    }
}
