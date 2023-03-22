using System.ComponentModel.DataAnnotations.Schema;

namespace TotalControl_EE_API.Models.Dto
{
    /*This code defines the SearchDto class, which is a Data Transfer Object (DTO) 
     * used to search for entries and exits within a specified date, a filter and BusinessLocation.*/

    public class SearchDto
    {

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string DescriptionFilter { get; set; }
        public string BusinessLocation { get; set; }

    }

}
