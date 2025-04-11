using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Mappers;
using Api.Models;
using Api.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{

    


    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaychecksController : Controller
    {
        private readonly IPaycheckService _paycheckService;
        private readonly IMapper _mapper;

        public PaychecksController(IPaycheckService paycheckService, IMapper mapper)
        {
            _paycheckService = paycheckService;
            _mapper = mapper;
        }

        [SwaggerOperation(Summary = "Get paychecks by Employee Id")]
        [HttpGet("employee/{id}")]
        public async Task<ActionResult<ApiResponse<List<GetPaycheckDto>>>> Get(int id)
        {
            var paychecks = await _paycheckService.GetAllPayChecks(id);
            return Ok(_mapper.MaptoPaychecksDto(paychecks));
        }
    }
}
