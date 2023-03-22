using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    /*This code defines a SearchRepository class that implements 
     *the ISearchRepository interface with the GetEntriesCount() 
     *and GetExitsCount() methods.*/

     /*The SearchRepository class has two private member variables 
     *IEmployeeRepository and IRegisterRepository that are initialized 
     *by the class's constructor. These variables are used to call the 
     *corresponding methods in the Employee and Register repositories.*/

    public class SearchRepository : ISearchRepository
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IRegisterRepository _registerRepo;

        public SearchRepository(IEmployeeRepository employeeRepo, IRegisterRepository registerRepo)
        {
            _employeeRepo = employeeRepo;
            _registerRepo = registerRepo;
        }

        /*The GetEntriesCount() method counts the entry records in the 
         * database that match the specified search criteria. 
         * The dateFrom and dateTo parameters specify the date range 
         * in which to search for records. The descriptionFilter 
         * parameter specifies the filter to use to find records i
         * n the database. The BusinessLocation parameter specifies 
         * the business location to search for records. This method 
         * uses the Register repository's Count() method to count the 
         * records that match the search criteria and returns the result.*/

        public async Task<int> GetEntriesCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string BusinessLocation)
        {
            var entries = await _registerRepo.Count(r => r.Date >= dateFrom && r.Date <= dateTo && r.RegisterType == "ingreso" && r.BusinessLocation == BusinessLocation && (r.Employee.Name.Contains(descriptionFilter) || r.Employee.LastName.Contains(descriptionFilter)));

            return entries;
        }

        /*The GetExitsCount() method counts the exit records in the 
         * database that match the specified search criteria. 
         * This method has the same parameters as the GetEntriesCount() method. 
         * This method uses the Register repository's Count() method to count 
         * the records that match the search criteria and returns the result.*/

        public async Task<int> GetExitsCount(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string BusinessLocation)
        {
            var exits = await _registerRepo.Count(r => r.Date >= dateFrom && r.Date <= dateTo && r.RegisterType == "egreso" && r.BusinessLocation == BusinessLocation && (r.Employee.Name.Contains(descriptionFilter) || r.Employee.LastName.Contains(descriptionFilter)));

            return exits;
        }
    }

}
