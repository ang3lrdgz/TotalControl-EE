using AutoMapper;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.Dto;

namespace TotalControl_EE_API
{
    /*The MappingConfig class inherits from the Profile class provided by AutoMapper
     *Within the class constructor, the mappings between different model classes and DTOs
     *are defined using the CreateMap() method*/

    /*Mappings allow the assignment of properties from one object
     *to another, making it easier to communicate data between 
     *different application components.*/

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            /*This code sets up a bi-directional mapping(using the ReverseMap() method).
             *This means that the properties of ExampleClass can be mapped to ExampleDto to and vice versa.*/

            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();

            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();

            CreateMap<Register, RegisterDto>().ReverseMap();
            CreateMap<Register, RegisterCreateDto>().ReverseMap();
            CreateMap<Register, RegisterUpdateDto>().ReverseMap();


            /*This line establishes a bidirectional mapping between 
             *the class ExampleClass and ExampleDto. This mapping is 
             *used to allow searching for records and averages
             *respectively using the information from the objects.*/

            CreateMap<Register, SearchDto>().ReverseMap();
            CreateMap<Employee, SearchDto>().ReverseMap();

            CreateMap<Register, AverageDto>().ReverseMap();
            CreateMap<Employee, AverageDto>().ReverseMap();

        }
    }
}
