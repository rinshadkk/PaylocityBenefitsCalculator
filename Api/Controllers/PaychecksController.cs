using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
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

        public PaychecksController(IPaycheckService paycheckService)
        {
            _paycheckService = paycheckService;
        }

        [SwaggerOperation(Summary = "Get paychecks by Employee Id")]
        [HttpGet("employee/{id}")]
        public async Task<ActionResult<ApiResponse<List<GetPaycheckDto>>>> Get(int id)
        {
            var employee = await _paycheckService.GetAllPayChecks(id);
            return Ok(employee);
        }
    }
}
