using TotalControl_EE_API.Models.Dto;

namespace TotalControl_EE_API.Data
{
    public static class EmployeeData
    {
        public static List<EmployeeDto> employeeList = new()
        {
                new EmployeeDto{Id = 1, Name="Angel", LastName="Rodriguez", Gender="M" },
                new EmployeeDto{Id = 2, Name="Rosmaira", LastName="Martinez", Gender="F" }
        };
    }
}
