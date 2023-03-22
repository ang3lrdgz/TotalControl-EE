using System.Linq.Expressions;
using TotalControl_EE_API.Models;

namespace TotalControl_EE_API.Repository.IRepository
{
    public interface ISearchRepository
    {
        Task<int> GetEntriesCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string businessLocation);
        Task<int> GetExitsCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string businessLocation);
    }

}
