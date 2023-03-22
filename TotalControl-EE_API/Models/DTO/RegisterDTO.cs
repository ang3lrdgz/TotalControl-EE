using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
    public class RegisterDto
    {
        [Required]
        public int IdRegister { get; set; }
        [Required]
        public int IdEmployee { get; set; }

        public DateTime Date { get; set; }

        public string registerType { get; set; } = string.Empty;

        public string businessLocation { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

    }
}
