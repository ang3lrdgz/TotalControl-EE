using AutoMapper;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.DTO;

namespace TotalControl_EE_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();

            CreateMap<Register, RegisterDTO>().ReverseMap();
            CreateMap<Register, RegisterCreateDTO>().ReverseMap();
            CreateMap<Register, RegisterUpdateDTO>().ReverseMap();

        }
    }
}
