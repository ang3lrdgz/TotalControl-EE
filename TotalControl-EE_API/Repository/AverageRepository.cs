using Microsoft.EntityFrameworkCore;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models.Dto;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Repository
{
    /*This code is an implementation of a repository to obtain averages
     * of entries and exits of employees by month and branch within a range of dates.
     *
     *The repository is called AverageRepository and it has three dependencies: 
     *IEmployeeRepository to access the employees, IRegisterRepository to access 
     *the records, and ApplicationDbContext which is the context class for the database.*/

    public class AverageRepository : IAverageRepository
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IRegisterRepository _registerRepo;
        private readonly ApplicationDbContext _db;

        public AverageRepository(IEmployeeRepository employeeRepo, IRegisterRepository registerRepo, ApplicationDbContext db)
        {
            _employeeRepo = employeeRepo;
            _registerRepo = registerRepo;
            _db = db;
        }

        /*The GetAverages(DateTime dateFrom, DateTime dateTo) method is responsible for obtaining the averages. 
         * First get all the records within the specified date range using the Include()
         * method to load the related employee data.*/

        public async Task<List<AverageDto>> GetAverages(DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<AverageDto>();

            // Gets all registers within the specified date range
            var registers = await _db.Registers
                .Include(r => r.Employee)
                .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
                .ToListAsync();

            // Groups the registers by month and location
            var groups = registers.GroupBy(r => new { Month = r.Date.Month, Year = r.Date.Year, BusinessLocation = r.BusinessLocation });

            // For each group, counts the amount of entries and exits of men and women and calculate the average
            foreach (var group in groups)
            {
                var entryCount = group.Count(r => r.RegisterType == "ingreso");
                var exitCount = group.Count(r => r.RegisterType == "egreso");

                var malesEntryCount = group.Count(r => r.RegisterType == "ingreso" && r.Employee.Gender == "M");
                var femalesEntryCount = group.Count(r => r.RegisterType == "ingreso" && r.Employee.Gender == "F");

                var malesExitCount = group.Count(r => r.RegisterType == "egreso" && r.Employee.Gender == "M");
                var femalesExitCount = group.Count(r => r.RegisterType == "egreso" && r.Employee.Gender == "F");

                //AverageDto object containing the results is created and added to the result list

                       var dto = new AverageDto
                {
                    BusinessLocation = group.Key.BusinessLocation,
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    MaleEntryAverage = entryCount > 0 ? (double)malesEntryCount / entryCount : 0,
                    FemaleEntryAverage = entryCount > 0 ? (double)femalesEntryCount / entryCount : 0,
                    MaleExitAverage = exitCount > 0 ? (double)malesExitCount / exitCount : 0,
                    FemaleExitAverage = exitCount > 0 ? (double)femalesExitCount / exitCount : 0
                };

                result.Add(dto);
            }

            return result;
        }
    }

}
