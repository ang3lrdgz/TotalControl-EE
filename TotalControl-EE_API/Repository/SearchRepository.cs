using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IRegisterRepository _registerRepo;

        public SearchRepository(IEmployeeRepository employeeRepo, IRegisterRepository registerRepo)
        {
            _employeeRepo = employeeRepo;
            _registerRepo = registerRepo;
        }

        public async Task<int> GetEntriesCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string businessLocation)
        {
            var entries = await _registerRepo.Count(r => r.Date >= dateFrom && r.Date <= dateTo && r.registerType == "ingreso" && r.businessLocation == businessLocation && (r.Employee.Name.Contains(descriptionFilter) || r.Employee.LastName.Contains(descriptionFilter)));

            return entries;
        }

        public async Task<int> GetExitsCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string businessLocation)
        {
            var exits = await _registerRepo.Count(r => r.Date >= dateFrom && r.Date <= dateTo && r.registerType == "egreso" && r.businessLocation == businessLocation && (r.Employee.Name.Contains(descriptionFilter) || r.Employee.LastName.Contains(descriptionFilter)));

            return exits;
        }
    }

}
