using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotalControl_EE_API.Models
{

    /*It is important to note that this code is a simple class definition and does not 
     * include any specific functionality. Also, for your practical use, you may need other 
     * classes, such as a DbContext that relates to the database and takes care of 
     * creating, reading, updating, and deleting records from the corresponding table.*/

    /*Properties that are decorated with attributes such as [Key], [Required], and
     * [ForeignKey("IdEmployee")], are used to set primary key, required, and foreign 
     * key constraints on the database.*/

    public class Register
    {
        [Key]
        public int IdRegister { get; set; }
        [Required]
        public int IdEmployee { get; set; }

        [ForeignKey("IdEmployee")]
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public string RegisterType { get; set; } = string.Empty;

        public string BusinessLocation { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
