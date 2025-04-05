using Api.Models;

namespace Api.Dtos.Paycheck
{
    public class DeductionDto
    {
        public DeductionType Type { get; set; } = DeductionType.None;
        public decimal Amount { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
}
