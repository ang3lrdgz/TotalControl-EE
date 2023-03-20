using TotalControl_EE_API.Models.DTO;

namespace TotalControl_EE_API.Data
{
    public static class EmployeeData
    {
        public static List<EmployeeDTO> employeeList = new()
        {
                new EmployeeDTO{Id = 1, Name="Angel", LastName="Rodriguez", Gender="M" },
                new EmployeeDTO{Id = 2, Name="Rosmaira", LastName="Martinez", Gender="F" }
        };
    }
}
