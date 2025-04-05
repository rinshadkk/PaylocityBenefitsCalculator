using Api.Service.DeductionService.DeductionCalculators;

namespace Api.Service.DeductionService
{
    public interface IDeductionCalculatorFactory
    {
        public List<IDeductionCalculator> GetDeductionCalculators();
    }
}
