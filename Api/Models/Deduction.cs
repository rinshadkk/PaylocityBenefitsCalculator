namespace Api.Models
{
    public class Deduction
    {
        public DeductionType Type { get; set; } = DeductionType.None;
        public decimal Amount { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
}
