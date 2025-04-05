using Api.Models;

namespace Api.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetEmployeesByCompanyId(int Id);

        public Task<Employee> GetEmployeeById(int Id);
    }
}
