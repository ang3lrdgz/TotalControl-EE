namespace TotalControl_EE_API.Models.Dto
{
    /*This code defines the AverageDto class which is a Data Transfer Object (DTO) 
     * used to represent the averages of entries and exits of men and women for a month and BusinessLocation.*/

    public class AverageDto
    {
        public string BusinessLocation { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double MaleEntryAverage { get; set; }
        public double FemaleEntryAverage { get; set; }
        public double MaleExitAverage { get; set; }
        public double FemaleExitAverage { get; set; }
    }

}
