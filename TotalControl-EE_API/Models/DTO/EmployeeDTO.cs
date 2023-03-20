using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
       
        [Required]
        [MaxLength(1)]
        public string Gender { get; set; } = string.Empty;
    }
}
