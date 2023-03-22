using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotalControl_EE_API.Models
{

    /*It is important to note that this code is a simple class definition and does not 
     * include any specific functionality. Also, for your practical use, you may need other 
     * classes, such as a DbContext that relates to the database and takes care of 
     * creating, reading, updating, and deleting records from the corresponding table.*/

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

    }
}
