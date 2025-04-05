using Api.Models;

namespace Api.Service
{
    public interface IPaycheckService
    {
        public  Task<List<Paycheck>> GetAllPayChecks(int employeeId);
    }
}
