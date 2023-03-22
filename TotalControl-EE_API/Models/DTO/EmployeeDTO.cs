using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
    /*This class is a Data Transfer Object (DTO) that is used to transfer 
     * data from a Employee entity between the business layer and the presentation layer of the application.*/

    public class EmployeeDto
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
