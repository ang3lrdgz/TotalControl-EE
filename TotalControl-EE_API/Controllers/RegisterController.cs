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
    /// <summary>
    /// Services to Create, Read, Update or Delete registers
    /// of products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        // Fields of the class
        private readonly ILogger<RegisterController> _logger;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IRegisterRepository _registerRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        
        // Constructor of the class, which takes arguments and assigns them to the corresponding fields
        public RegisterController(ILogger<RegisterController> logger, IEmployeeRepository employeeRepo, IRegisterRepository registerRepo, IMapper mapper )
        {

            _logger = logger;
            _employeeRepo = employeeRepo;
            _registerRepo = registerRepo;
            _mapper = mapper;
            _response = new();
        }

        /// <summary>
        /// Get all registered
        /// </summary>
        /// <returns>All registers </returns>
        // GET: api/Register
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRegister()
        {
            try
            {
                // Record input and output information to the registry
                _logger.LogInformation("Gettings the logs");

                /*The GetAll() method of the _registerRepo object is called to get all
                 *the available records, and they are stored in a list called registerlist.*/
                IEnumerable<Register> registerlist = await _registerRepo.GetAll();

                /*A _mapper object is used to convert each item in the registerlist to a
                 *RegisterDto object and they are stored in a list called _response.Result.*/
                _response.Result = _mapper.Map<IEnumerable<RegisterDto>>(registerlist);
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


        /// <summary>
        /// Get an Register according to their Id
        /// </summary>
        /// <returns>Register data</returns>
        /// <param name="id">Register ID</param>
        // GET: api/Register
        [HttpGet("id:int", Name ="GetRegister")]
        //The ProducesResponseType attribute specifies the HTTP status codes that can be returned by the method.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        //This method is asynchronous and returns an ActionResult object that encapsulates an APIResponse object.
        public async Task<ActionResult<APIResponse>> GetRegister(int id)
        {
            //A try-catch block is started to handle exceptions.
            try
            {
                //It is checked if the id is equal to zero and a BadRequest error is returned if so.
                if (id == 0)
                {
                    /*An error message is logged in the log file, and the HTTP status code 
                     * and the IsSuccessful property are set on the _response object.*/
                    _logger.LogError("Error al traer registro con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                //The record corresponding to the id is obtained using the Get method of the record repository.
                var register = await _registerRepo.Get(r => r.IdRegister == id);

                //If the record is null, the HTTP status code is set and a NotFound response is returned.
                if (register == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                /*If the record is found, it is mapped to the RegisterDto object
                 *using AutoMapper and the Result property of the _response object is set.*/
                _response.Result = _mapper.Map<RegisterDto>(register);
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


        /// <summary>
        /// Allows to Register a new Register of entries or exits
        /// </summary>
        /// <returns>The data of the added Register</returns>
        // POST: api/Register
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostRegister([FromBody] RegisterCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _registerRepo.Get(r => r.Date == createDto.Date &&
                r.RegisterType == createDto.RegisterType) != null)
                {
                    ModelState.AddModelError("Registro", "El empleado ya Ingreso/Egreso!");
                    return BadRequest(ModelState);
                }

                if (await _employeeRepo.Get(e => e.Id==createDto.IdEmployee) == null)
                {
                        ModelState.AddModelError("ClaveForanea", "El IdEmployeed no existe!");
                        return BadRequest(ModelState);
                }


                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Register model = _mapper.Map<Register>(createDto);

                model.Status = "Unmodified";
                await _registerRepo.Create(model);
                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetRegister", new { id = model.IdRegister }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        /// <summary>
        /// Allows you to delete an Register
        /// </summary>
        /// <param name="id">Id of the Register to delete</param>
        // DELETE: api/Register
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRegister(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var register = await _registerRepo.Get(r => r.IdRegister == id);
                if (register == null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _registerRepo.Remove(register);
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


        /// <summary>
        /// Modify an Register
        /// </summary>
        /// <returns>No Content if modified successfully</returns>
        /// <param name="id">Id of the Register to Modify</param>
        // PUT: api/Register
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRegister(int id, [FromBody] RegisterUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.IdRegister)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _employeeRepo.Get(e =>e.Id == updateDto.IdEmployee) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El IdEmployeed no existe!");
                return BadRequest(ModelState);
            }


            Register model = _mapper.Map<Register>(updateDto);

            await _registerRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartRegister(int id, JsonPatchDocument<RegisterUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var register = await _registerRepo.Get(r => r.IdRegister == id, tracked: false);

            RegisterUpdateDto registerDto = _mapper.Map<RegisterUpdateDto>(register);

            if (register == null) return BadRequest();

            patchDto.ApplyTo(registerDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Register model = _mapper.Map<Register>(registerDto);

            await _registerRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }
}
