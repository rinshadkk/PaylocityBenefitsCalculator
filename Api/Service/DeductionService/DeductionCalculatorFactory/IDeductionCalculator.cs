using Api.Models;

namespace Api.Service.DeductionService.DeductionCalculators
{
    public interface IDeductionCalculator
    {
        public Deduction CalculateDeduction(Employee employee);
    }
}
