using AutoMapper;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.Dto;

namespace TotalControl_EE_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();

            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();

            CreateMap<Register, RegisterDto>().ReverseMap();
            CreateMap<Register, RegisterCreateDto>().ReverseMap();
            CreateMap<Register, RegisterUpdateDto>().ReverseMap();

            CreateMap<Register, SearchDto>().ReverseMap();
            CreateMap<Employee, SearchDto>().ReverseMap();



        }
    }
}
