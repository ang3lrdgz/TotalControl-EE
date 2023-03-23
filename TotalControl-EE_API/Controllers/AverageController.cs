// Import required namespaces
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.Dto;
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
    public class AverageController : ControllerBase
    {
        // Fields of the class
        private readonly ILogger<AverageController> _logger;
        private readonly IAverageRepository _averageRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        
        // Constructor of the class, which takes arguments and assigns them to the corresponding fields
        public AverageController(ILogger<AverageController> logger, IAverageRepository averageRepo, IMapper mapper )
        {

            _logger = logger;
            _mapper = mapper;
            _response = new();
            _averageRepo = averageRepo;
        }
        /// <summary>
        /// Get an average Register according to their dateFrom and dateTo
        /// </summary>
        /// <returns>Register data</returns>
        /// <param name="dateFrom">Register date</param>
        /// <param name="dateTo">Register date</param>
        // GET: api/Register
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Search(DateTime dateFrom, DateTime dateTo)
        {
            //A try-catch block is started to handle exceptions.
            try
            {
                // Record entries and exits information to the registry
                _logger.LogInformation("Getting entries and exits");

                // Get the repository average
                var average = await _averageRepo.GetAverages(dateFrom, dateTo);

                // If the results are null, set the status code and return a response
                if (average == null || !average.Any())
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                /*If the record is found, it is mapped to the AverageDto object
                 *using AutoMapper and the Result property of the _response object is set.*/
                var result = _mapper.Map<List<AverageDto>>(average);

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
