namespace Api.Models
{
    public class Paycheck
    {
        public int Id { get; set; }
        public EmployeeBase Employee { get; set; }
        public string PaycheckName {
            get {
                return PayPeriod.StartDate.ToString() + " - " + PayPeriod.EndDate.ToString();
            } 
         } 
        public PayPeriodDto PayPeriod { get; set; } = new PayPeriodDto();
        public decimal GrossAmount {
            get;set;
         }
        public List<Deduction> Deductions { get; set; }=new List<Deduction>();
        public decimal NetAmount {
            get
            {
                return GrossAmount - TotalDeductionAmount;
            } 
        }

        public decimal TotalDeductionAmount
        {
            get
            {
                return Deductions.Sum(d => d.Amount); 
            }
        }
 
    }

   






}
