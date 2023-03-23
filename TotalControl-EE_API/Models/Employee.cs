using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotalControl_EE_API.Models
{
    /// <summary>
    /// Allows you to register employees
    /// </summary>


    public class Employee
    {
        /// <summary>
        /// Employee ID
        /// </summary>
        /// <value>The id is automatically incremented</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the category name
        /// </summary>
        [Required(ErrorMessage = "Please type the Name field")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the last name of the category
        /// </summary>
        [Required(ErrorMessage = "Please enter the last name field ")]
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the gender of the category
        /// </summary>
        [Required(ErrorMessage = "Please type the gender field")]
        public string Gender { get; set; } = string.Empty;
        /// <summary>
        /// Employee Status
        /// </summary>
        /// <value>The Status is set automatically</value>
        public string Status { get; set; } = string.Empty;

    }
}
