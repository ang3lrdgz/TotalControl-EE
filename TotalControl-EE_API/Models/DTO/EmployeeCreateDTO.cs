using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
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
