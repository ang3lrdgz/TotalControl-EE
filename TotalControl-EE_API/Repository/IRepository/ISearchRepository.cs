namespace TotalControl_EE_API.Repository.IRepository
{
    /*Both methods return an asynchronous task that represents the 
     * count of inputs or outputs that meet the criteria specified in the parameters.*/

    public interface ISearchRepository
    {
        Task<int> GetEntriesCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string BusinessLocation);
        Task<int> GetExitsCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string BusinessLocation);
    }

}
