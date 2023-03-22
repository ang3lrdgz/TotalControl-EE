// Import required namespaces
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.Dto;
using TotalControl_EE_API.Repository.IRepository;

// Declare the controller namespace
namespace TotalControl_EE_API.Controllers
{
    // Base path of the controller and that it is an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Fields of the class
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        // Constructor of the class, which takes arguments and assigns them to the corresponding fields
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepo, IMapper mapper )
        {

            _logger = logger;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _response = new();
        }


        // Action method that is called when an HTTP GET request is made to the api/Search route
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetEmployees()
        {
            try
            {
                // Record input and output information to the registry
                _logger.LogInformation("Obtener los empleados");

                /*The GetAll() method of the _employeeRepo object is called to get all
                 *the available records, and they are stored in a list called registerlist.*/
                IEnumerable<Employee> employeeList = await _employeeRepo.GetAll();

                /*A _mapper object is used to convert each item in the registerlist to a
                 *EmployeeDto object and they are stored in a list called _response.Result.*/
                _response.Result = _mapper.Map<IEnumerable<EmployeeDto>>(employeeList);
                _response.statusCode = HttpStatusCode.OK;

                //The statusCode property of _response is updated with the HttpStatusCode.OK status code.
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            //Finally, an OkObjectResult object is returned with the updated _response object as its value.
            return _response;
        }

        /*The first line specifies the route of the HTTP request using 
         * the HttpGet attribute and a route template that expects an int parameter named "id".*/
        [HttpGet("id:int", Name ="GetEmployee")]
        //The ProducesResponseType attribute specifies the HTTP status codes that can be returned by the method.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //This method is asynchronous and returns an ActionResult object that encapsulates an APIResponse object.
        public async Task<ActionResult<APIResponse>> GetEmployee(int id)
        {
            //A try-catch block is started to handle exceptions.
            try
            {
                //It is checked if the id is equal to zero and a BadRequest error is returned if so.
                if (id == 0)
                {
                    /*An error message is logged in the log file, and the HTTP status code 
                     * and the IsSuccessful property are set on the _response object.*/
                    _logger.LogError("Error al traer empleado con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                //The record corresponding to the id is obtained using the Get method of the record repository.
                var employee = await _employeeRepo.Get(e => e.Id == id);

                //If the record is null, the HTTP status code is set and a NotFound response is returned.
                if (employee == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                /*If the record is found, it is mapped to the EmployeeDto object
                 *using AutoMapper and the Result property of the _response object is set.*/
                _response.Result = _mapper.Map<EmployeeDto>(employee);
                _response.statusCode = HttpStatusCode.OK;
                //The HTTP status code is set and an Ok response is returned with the object
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

        /*This attribute is used to mark a controller action method that should be used to process a
         * POST request. The method can then retrieve any data sent in the request body or 
         * headers, process the data as needed, and return a response to the client.*/

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostEmployee([FromBody] EmployeeCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _employeeRepo.Get(e => e.Name.ToLower() == createDto.Name.ToLower() &&
                e.LastName.ToLower() == createDto.LastName.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExistente", "El empleado ya esiste!");
                    return BadRequest(ModelState);

                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Employee model = _mapper.Map<Employee>(createDto);

                model.Status = "Unmodified";
                await _employeeRepo.Create(model);
                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetEmployee", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        /*This attribute is used to mark a controller action method that should be used to
         * process a DELETE request for a specific resource identified by the integer id. 
         * 
         * The method can then use the id parameter to perform the appropriate logic to 
         * delete the resource from the database.*/

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var employee = await _employeeRepo.Get(e => e.Id == id);
                if (employee == null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _employeeRepo.Remove(employee);

                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return BadRequest(_response);
        }

        /*This attribute is used to mark a controller action method that should be 
         * used to process a PUT request for a specific resource identified by the integer id.
         * The method can then use the id parameter to perform the appropriate logic to update 
         * the resource in the database.*/

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.Id)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Employee model = _mapper.Map<Employee>(updateDto);

            await _employeeRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartEmployee(int id, JsonPatchDocument<EmployeeUpdateDto> patchDto )
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var employee = await _employeeRepo.Get(e => e.Id == id, tracked:false);

            EmployeeUpdateDto employeeDto = _mapper.Map<EmployeeUpdateDto>(employee);

            if (employee == null) return BadRequest();

            patchDto.ApplyTo(employeeDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee model = _mapper.Map<Employee>(employeeDto);

            await _employeeRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
