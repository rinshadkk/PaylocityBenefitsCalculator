using Api.Models;

namespace Api.Service.DeductionService.DeductionCalculators
{
    /// <summary>
    /// Calculator for the base cost of benefits deduction.
    /// </summary>
    public class BaseCostForBenefits : IDeductionCalculator
    {
        // Base benefits cost per month (should be moved to constants or configuration for better maintainability)
        private readonly decimal baseBenefitsCostPerMonth = 1000; // TODO: Move this to constants and to Config

        /// <summary>
        /// Calculates the base cost of benefits deduction for an employee.
        /// </summary>
        /// <param name="employee">The employee for whom the deduction is being calculated.</param>
        /// <returns>A Deduction object containing the type, amount, and description of the deduction.</returns>
        public Deduction CalculateDeduction(Employee employee)
        {
            return new Deduction
            { 
                Type = DeductionType.BaseCost, // Type of deduction
                Amount = GetBaseBenefitsCostPerPayperiod(), // Amount calculated per pay period
                Description = "Base benefit cost" // Description of the deduction
            };
        }

        /// <summary>
        /// Calculates the base benefits cost per pay period.
        /// </summary>
        /// <returns>The base benefits cost for a single pay period.</returns>
        private decimal GetBaseBenefitsCostPerPayperiod()
        {
            // Calculate the annual cost and divide it by the number of pay periods in a year
            return baseBenefitsCostPerMonth * 12 / 26; // TODO: Move this calculation logic to constants or configuration
        }
    }
}
