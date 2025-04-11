using Api.Dtos.Paycheck;
using Api.Models;

namespace Api.Mappers
{
    public interface IMapper
    {
        public List<GetPaycheckDto> MaptoPaychecksDto(List<Paycheck> paychecks);
    }
}
