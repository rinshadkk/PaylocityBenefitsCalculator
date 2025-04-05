using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class GetEmployeeDto : EmployeeBaseDto
{
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();
}
