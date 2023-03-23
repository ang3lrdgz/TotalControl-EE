using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotalControl_EE_API.Models
{
    /// <summary>
    /// Allows you to register
    /// </summary>

    public class Register
    {
        /// <summary>
        /// Register IdRegister
        /// </summary>
        /// <value>The id is automatically incremented</value>
        [Key]
        public int IdRegister { get; set; }
        /// <summary>
        /// Gets or sets the IdEmployee registered
        /// </summary>
        [Required(ErrorMessage = "Please type the IdEmployee")]
        public int IdEmployee { get; set; }
        /// <summary>
        /// Sets the IdEmployee foreign key
        /// </summary>
        [ForeignKey("IdEmployee")]
        public Employee Employee { get; set; }
        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        [Required(ErrorMessage = "Please type the Date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the RegisterType
        /// </summary>
        [Required(ErrorMessage = "Please type the RegisterType")]
        public string RegisterType { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the BusinessLocation
        /// </summary>
        [Required(ErrorMessage = "Please type the BusinessLocation")]
        public string BusinessLocation { get; set; } = string.Empty;
        /// <summary>
        /// Employee Status
        /// </summary>
        /// <value>The Status is set automatically</value>
        [Required(ErrorMessage = "Please type the Date")]
        public string Status { get; set; } = string.Empty;
    }
}
