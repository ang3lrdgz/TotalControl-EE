using TotalControl_EE_API.Models.Dto;

namespace TotalControl_EE_API.Repository.IRepository
{
    /*The interface has a method called GetAverages, which returns a list of AverageDto objects.
     * This method takes two parameters of type DateTime, dateFrom and dateTo, which are used to 
     * filter the list of AverageDto objects that are returned.
     *
     *This class will be in charge of performing the reading operations of the Average entity in 
     *the database and converting the results into AverageDto objects to return them to the REST API.*/

    public interface IAverageRepository
    {
        Task<List<AverageDto>> GetAverages(DateTime dateFrom, DateTime dateTo);
    }

}
