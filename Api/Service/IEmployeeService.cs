using Api.Models;

namespace Api.Service
{
    public interface IEmployeeService
    {
        public Task<Employee> GetEmployeeById(int id);
    }
}
