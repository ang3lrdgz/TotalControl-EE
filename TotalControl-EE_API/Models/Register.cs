using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotalControl_EE_API.Models
{
    public class Register
    {
        [Key]
        public int IdRegister { get; set; }
        [Required]
        public int IdEmployee { get; set; }

        [ForeignKey("IdEmployee")]
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public string registerType { get; set; } = string.Empty;

        public string businessLocation { get; set; } = string.Empty;
    }
}
