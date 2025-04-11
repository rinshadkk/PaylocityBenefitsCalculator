using Api.Models;
using Api.Service.DeductionService;
using Api.Service.DeductionService.DeductionCalculatorFactory;
using static Api.Constants.Constants;

namespace Api.Service
{
    public class PaycheckService : IPaycheckService
    {
        private readonly IDeductionCalculatorFactory _deductionCalculatorFactory;
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Constructor to initialize the PaycheckService with required dependencies.
        /// </summary>
        /// <param name="deductionCalculatorFactory">Factory to create deduction calculators.</param>
        /// <param name="employeeService">Service to retrieve employee data.</param>
        public PaycheckService(IDeductionCalculatorFactory deductionCalculatorFactory, IEmployeeService employeeService)
        {
            _deductionCalculatorFactory = deductionCalculatorFactory;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Retrieves all paychecks for a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of paychecks.</returns>
        public async Task<List<Paycheck>> GetAllPayChecks(int employeeId)
        {
            // Get Employee Information
            var employee = await _employeeService.GetEmployeeById(employeeId);

            // Generate Paychecks
            var payPeriods = GeneratePayperiods();
            List<Paycheck> paychecks = new();
            foreach (var payperiod in payPeriods)
            {
                paychecks.Add(GetPaycheck(employee, payperiod));
            }

            return paychecks;
        }

        /// <summary>
        /// Creates a paycheck for a specific employee and pay period.
        /// </summary>
        /// <param name="employee">The employee for whom the paycheck is being generated.</param>
        /// <param name="payPeriod">The pay period for the paycheck.</param>
        /// <returns>A Paycheck object containing details of the paycheck.</returns>
        private Paycheck GetPaycheck(Employee employee, PayPeriod payPeriod)
        {
            var deductions = GetDeductions(employee, payPeriod);
            return new Paycheck()
            {
                Deductions = deductions,
                Employee = employee,
                PayPeriod = payPeriod,
                GrossAmount = employee.Salary / Constants.Constants.Payroll.totalPayperiods
            };
        }

        /// <summary>
        /// Calculates all deductions for a specific employee and pay period.
        /// </summary>
        /// <param name="employee">The employee for whom deductions are being calculated.</param>
        /// <param name="payPeriod">The pay period for which deductions are being calculated.</param>
        /// <returns>A list of deductions for the employee.</returns>
        private List<Deduction> GetDeductions(Employee employee, PayPeriod payPeriod)
        {
            //TODO: Move this to a facade
            List<Deduction> deductions = new();
            var deductionCalculators = _deductionCalculatorFactory.GetDeductionCalculators();
            foreach (var deducationCalculator in deductionCalculators)
            {
                //Assembling All Deductions
                deductions.Add(deducationCalculator.CalculateDeduction(employee));
            }
            return deductions;
        }

        /// <summary>
        /// Generates a list of pay periods for the current year.
        /// </summary>
        /// <returns>A list of PayPeriodDto objects representing bi-weekly pay periods.</returns>
        private static List<PayPeriod> GeneratePayperiods()
        {
            DateTime startDate = new(DateTime.Now.Year, 1, 1); // Assuming that we are generating the paystubs for the current pay period
            List<PayPeriod> payPeriods = new();

            // Generate 26 bi-weekly pay periods
            for (int i = 0; i < Constants.Constants.Payroll.totalPayperiods; i++)
            {
                DateTime startPeriod = startDate.AddDays(i * Payroll.DaysInPayPeriod);  // Start date of each pay period
                DateTime endPeriod = startPeriod.AddDays(Payroll.DaysInPayPeriod - 1);  // End date of each pay period (14th day is inclusive)

                payPeriods.Add(new PayPeriod
                {
                    StartDate = startPeriod,
                    EndDate = endPeriod,
                    PayDate = endPeriod
                });
            }

            return payPeriods;
        }
    }
}
