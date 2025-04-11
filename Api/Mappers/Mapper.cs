using Api.Dtos.Paycheck;
using Api.Models;

namespace Api.Mappers
{
    public class Mapper : IMapper
    {
        /// <summary>
        /// Custom Mapper
        /// </summary>
        /// <param name="paychecks"></param>
        /// <returns></returns>
        public List<GetPaycheckDto> MaptoPaychecksDto(List<Paycheck> paychecks)
        {
            // Map Paycheck objects to GetPaycheckDto objects
            var paycheckDtos = paychecks.Select(paycheck => new GetPaycheckDto
            {
                GrossAmount = paycheck.GrossAmount,
                Deductions = paycheck.Deductions.Select(d => new DeductionDto
                {
                    Amount = d.Amount,
                    Description = d.Description
                }).ToList(),
                PayPeriod = new PayPeriodDto
                {
                    StartDate = paycheck.PayPeriod.StartDate,
                    EndDate = paycheck.PayPeriod.EndDate,
                    PayDate = paycheck.PayPeriod.PayDate
                }
            }).ToList();

            return paycheckDtos;
        }
    }
}
