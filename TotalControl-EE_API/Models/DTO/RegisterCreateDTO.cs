using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
    //This class is used to create a new registers through the TotalControl_EE API.

    public class RegisterCreateDto
    {
        [Required]
        public int IdRegister { get; set; }
        [Required]
        public int IdEmployee { get; set; }

        public DateTime Date { get; set; }

        public string RegisterType { get; set; } = string.Empty;

        public string BusinessLocation { get; set; } = string.Empty;
    }
}
