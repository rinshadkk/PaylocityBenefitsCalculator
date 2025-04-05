using Api.Service.DeductionService.DeductionCalculators;

namespace Api.Service.DeductionService.DeductionCalculatorFactory
{
    /// <summary>
    /// Factory class to provide a list of deduction calculators.
    /// </summary>
    public class DeductionCalculatorFactory : IDeductionCalculatorFactory
    {
        /// <summary>
        /// Constructor for the DeductionCalculatorFactory.
        /// </summary>
        public DeductionCalculatorFactory() { }

        /// <summary>
        /// Retrieves a list of all available deduction calculators.
        /// </summary>
        /// <returns>A list of objects implementing the IDeductionCalculator interface.</returns>
        public List<IDeductionCalculator> GetDeductionCalculators()
        {
            // Initialize and return a list of deduction calculators
            var deductionCalculators = new List<IDeductionCalculator>
            {
                new BaseCostForBenefits(),   // Calculator for base cost of benefits
                new DependentDeduction(),   // Calculator for dependent-related deductions
                new HighSalaryDeduction()   // Calculator for deductions based on high salary
            };

            return deductionCalculators;
        }
    }
}
