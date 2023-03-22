using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
    /*This class is a Data Transfer Object (DTO) that is used to transfer 
     * data from a Register entity between the business layer and the presentation layer of the application.*/

    public class RegisterDto
    {
        [Required]
        public int IdRegister { get; set; }
        [Required]
        public int IdEmployee { get; set; }

        public DateTime Date { get; set; }

        public string RegisterType { get; set; } = string.Empty;

        public string BusinessLocation { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

    }
}
