using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck
{
    public class GetPaycheckDto
    {
        public int Id { get; set; }
        public EmployeeBaseDto Employee { get; set; }
        public string PaycheckName { get; set; } = string.Empty;
        public PayPeriodDto PayPeriod { get; set; } = new PayPeriodDto();
        public decimal GrossAmount { get; set; }
        public List<DeductionDto> Deductions { get; set; }=new List<DeductionDto>();
        public decimal NetAmount { get; set; }

        public decimal TotalDeductionAmount
        {
            get
            {
                return Deductions.Sum(d => d.Amount); 
            }
        }
 
    }

   






}
