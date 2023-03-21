using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.DTO;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IRegisterRepository _registerRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public RegisterController(ILogger<RegisterController> logger, IEmployeeRepository employeeRepo, IRegisterRepository registerRepo, IMapper mapper )
        {

            _logger = logger;
            _employeeRepo = employeeRepo;
            _registerRepo = registerRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [Route("GetRegister")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRegister()
        {
            try
            {
                _logger.LogInformation("Obtener los registros");

                IEnumerable<Register> registerlist = await _registerRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<RegisterDTO>>(registerlist);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name ="GetRegister")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRegister(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer registro con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var register = await _registerRepo.Get(r => r.IdRegister == id);

                if (register == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<RegisterDTO>(register);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostRegister([FromBody] RegisterCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _registerRepo.Get(r => r.Date == createDTO.Date &&
                r.registerType == createDTO.registerType) != null)
                {
                    ModelState.AddModelError("Registro", "El empleado ya Ingreso/Egreso!");
                    return BadRequest(ModelState);
                }

                if (await _employeeRepo.Get(e => e.Id==createDTO.IdEmployee) == null)
                {
                        ModelState.AddModelError("ClaveForanea", "El IdEmployeed no existe!");
                        return BadRequest(ModelState);
                }


                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Register model = _mapper.Map<Register>(createDTO);

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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRegister(int id, [FromBody] RegisterUpdateDTO updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.IdRegister)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _employeeRepo.Get(e =>e.Id == updateDTO.IdEmployee) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El IdEmployeed no existe!");
                return BadRequest(ModelState);
            }


            Register model = _mapper.Map<Register>(updateDTO);

            await _registerRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

        [HttpGet]
        [Route("GetEntriesAndExits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEntriesAndExits(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string businessLocation)
        {
            try
            {
                var employee = await _employeeRepo.Get(r => r.Name == descriptionFilter || r.LastName == descriptionFilter);
                if (employee == null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { "El Empleado no esta registrado!" };
                    return NotFound(_response);
                }

                _logger.LogInformation("Obtener los ingresos y egresos");

                var entries = await _registerRepo.Count(r => r.Date >= dateFrom && r.Date <= dateTo && r.registerType == "ingreso" && r.businessLocation == businessLocation && (r.Employee.Name.Contains(descriptionFilter) || r.Employee.LastName.Contains(descriptionFilter)));

                var exits = await _registerRepo.Count(r => r.Date >= dateFrom && r.Date <= dateTo && r.registerType == "egreso" && r.businessLocation == businessLocation && (r.Employee.Name.Contains(descriptionFilter) || r.Employee.LastName.Contains(descriptionFilter)));

                var result = new { Ingresos = entries, Egresos = exits };

                _response.Result = result;
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
