namespace Api.Dtos.Paycheck
{
    public class PayPeriodDto
    {
        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime EndDate { get; set; } = DateTime.Now;
        public DateTime PayDate { get; set; } = DateTime.Now;
    }
}
