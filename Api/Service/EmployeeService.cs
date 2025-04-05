using Api.Models;
using Api.Repositories;

namespace Api.Service
{
    public class EmployeeService : IEmployeeService
    { 
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor to initialize the EmployeeService with the required repository.
        /// </summary>
        /// <param name="employeeRepository">The repository to interact with employee data.</param>
        public EmployeeService(IEmployeeRepository employeeRepository) {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Retrieves a list of employees for a specific company.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of employees.</returns>
        public Task<List<Employee>> GetEmployees()
        {
            // Fetch employees for the company with ID 100
            var employees = _employeeRepository.GetEmployeesByCompanyId(100);

            return employees;
        }

        /// <summary>
        /// Retrieves an employee by their unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the employee object.</returns>
        public async Task<Employee> GetEmployeeById(int id)
        {
            // Fetch the employee with the specified ID
            var employee = await _employeeRepository.GetEmployeeById(id);
            return employee;
        }
    }
}
