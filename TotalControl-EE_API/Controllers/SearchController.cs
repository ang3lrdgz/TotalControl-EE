// Import required namespaces
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Repository.IRepository;

// Declare the controller namespace
namespace TotalControl_EE_API.Controllers
{
    /// <summary>
    /// Service to Read registers
    /// of products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // Fields of the class
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchRepository _searchRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        // Constructor of the class, which takes arguments and assigns them to the corresponding fields
        public SearchController(ILogger<SearchController> logger, ISearchRepository searchRepo, IMapper mapper )
        {

            _logger = logger;
            _mapper = mapper;
            _response = new();
            _searchRepo = searchRepo;
        }
        /// <summary>
        /// Get an average Register according to their dateFrom and dateTo
        /// </summary>
        /// <returns>Register data</returns>
        /// <param name="dateFrom">Register date</param>
        /// <param name="dateTo">Register date</param>
        /// <param name="descriptionFilter">Employee name or lastname</param>
        /// <param name="BusinessLocation">Register location</param>
        // GET: api/Register
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Search(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string BusinessLocation)
        {
            //A try-catch block is started to handle exceptions.
            try
            {
                // Record entries and exits information to the registry
                _logger.LogInformation("Getting entries and exits");

                // Get the repository entries and exits count
                var entries = await _searchRepo.GetEntriesCount(dateFrom, dateTo, descriptionFilter, BusinessLocation);

                var exits = await _searchRepo.GetExitsCount(dateFrom, dateTo, descriptionFilter, BusinessLocation);

                // If the results are null, set the status code and return a response
                if (entries == null || exits == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Create an anonymous object with the entries and exits counts and assign it to the Result property of the response
                var result = new { Ingresos = entries, Egresos = exits };

                _response.Result = result;
                _response.statusCode = HttpStatusCode.OK;
                
                // Return the response
                return Ok(_response);
            }
            // Handle any exceptions that may occur and set the IsSuccessful property to false
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            // Return the response
            return _response;
        }

    }
}
