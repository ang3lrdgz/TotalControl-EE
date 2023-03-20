﻿using AutoMapper;
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
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepo, IMapper mapper )
        {

            _logger = logger;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetEmployees()
        {
            try
            {
                _logger.LogInformation("Obtener los empleados");

                IEnumerable<Employee> employeeList = await _employeeRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<EmployeeDTO>>(employeeList);
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

        [HttpGet("id:int", Name ="GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer empleado con Id" + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var employee = await _employeeRepo.Get(e => e.Id == id);

                if (employee == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
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
        public async Task<ActionResult<APIResponse>> PostEmployee([FromBody] EmployeeCreateDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _employeeRepo.Get(e => e.Name.ToLower() == createDTO.Name.ToLower() &&
                e.LastName.ToLower() == createDTO.LastName.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExistente", "El empleado ya esiste!");
                    return BadRequest(ModelState);

                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Employee model = _mapper.Map<Employee>(createDTO);

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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.Id)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Employee model = _mapper.Map<Employee>(updateDTO);

            await _employeeRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartEmployee(int id, JsonPatchDocument<EmployeeUpdateDTO> patchDTO )
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var employee = await _employeeRepo.Get(e => e.Id == id, tracked:false);

            EmployeeUpdateDTO employeeDTO = _mapper.Map<EmployeeUpdateDTO>(employee);

            if (employee == null) return BadRequest();

            patchDTO.ApplyTo(employeeDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee model = _mapper.Map<Employee>(employeeDTO);

            await _employeeRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
