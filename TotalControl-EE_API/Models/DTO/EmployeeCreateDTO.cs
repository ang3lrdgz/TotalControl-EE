using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
    //This class is used to create a new registers through the TotalControl_EE API.

    public class EmployeeCreateDto
    {
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
       
        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = string.Empty;
    }
}
