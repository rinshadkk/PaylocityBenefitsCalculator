using Api.Models;
using System.ComponentModel;

namespace Api.Service.DeductionService.DeductionCalculators
{
    /// <summary>
    /// Calculator for dependent-related deductions.
    /// </summary>
    public class DependentDeduction : IDeductionCalculator
    {
        // Monthly cost for dependents (should be moved to constants or configuration for better maintainability)
        private readonly decimal dependentCostPerMonth = 600; // TODO: Move this to config and constants

        // Additional monthly cost for dependents over 50 years old (should be moved to constants or configuration)
        private readonly decimal dependentCostPerMonthFor50PlusOld = 200; // TODO: Move this to config and constants

        /// <summary>
        /// Calculates the total dependent deduction for an employee.
        /// </summary>
        /// <param name="employee">The employee for whom the deduction is being calculated.</param>
        /// <returns>A Deduction object containing the type, amount, and description of the deduction.</returns>
        public Deduction CalculateDeduction(Employee employee)
        {
            // Calculate the total deduction for all dependents
            var dependentDeduction = CalculateDependentDeductionTotal((List<Dependent>)employee.Dependents);

            return new Deduction 
            { 
                Amount = dependentDeduction, 
                Type = DeductionType.DependentSpouse, 
                Description = string.Empty // Description can be updated as needed
            };
        }

        /// <summary>
        /// Calculates the total deduction amount for all dependents.
        /// </summary>
        /// <param name="dependents">A list of dependents for the employee.</param>
        /// <returns>The total deduction amount for all dependents.</returns>
        private decimal CalculateDependentDeductionTotal(List<Dependent> dependents)
        {
            // Sum up the deduction amounts for all dependents
            return dependents.Sum(x => GetDeductionAmount(x));
        }

        /// <summary>
        /// Calculates the deduction amount for a single dependent.
        /// </summary>
        /// <param name="dependent">The dependent for whom the deduction is being calculated.</param>
        /// <returns>The deduction amount for the dependent.</returns>
        private decimal GetDeductionAmount(Dependent dependent)
        {
            decimal amount = 0;

            if (dependent != null)
            {
                // Base deduction amount for the dependent
                amount += dependentCostPerMonth * 12 / 26;

                // Additional deduction if the dependent is over 50 years old
                if (dependent.DateOfBirth.AddYears(50) > DateTime.UtcNow)
                {
                    amount += dependentCostPerMonthFor50PlusOld * 12 / 26;
                }
            }

            return amount;
        }
    }
}
