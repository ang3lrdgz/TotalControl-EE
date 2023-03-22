using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TotalControl_EE_API.Models.Dto
{
    public class SearchDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public string RegisterType { get; set; }
        public string BusinessLocation { get; set; }
    }

}
