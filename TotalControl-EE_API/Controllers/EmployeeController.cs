using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.DTO;

namespace TotalControl_EE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDbContext db, IMapper mapper )
        {

            _logger = logger;
            _db = db;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            _logger.LogInformation("Obtener los empleados");

            IEnumerable<Employee> employeeList = await _db.Employees.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employeeList));
        }

        [HttpGet("id:int", Name ="GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer empleado con Id" + id);
                return BadRequest();
            }
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee([FromBody] EmployeeCreateDTO createDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Employees.FirstOrDefaultAsync(e=>e.Name.ToLower() == createDTO.Name.ToLower() &&
            e.LastName.ToLower() == createDTO.LastName.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExistente", "El empleado ya esiste!");
                return BadRequest(ModelState);

            }

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Employee model = _mapper.Map<Employee>(createDTO);

            await _db.Employees.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetEmployee", new {id= model.Id}, model);
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee==null)
            {
                return NotFound();
            }
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO updateDTO)
        {
            if (updateDTO == null || id!= updateDTO.Id)
            {
                return BadRequest();
            }

            Employee model = _mapper.Map<Employee>(updateDTO);

            _db.Employees.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
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
            var employee = await _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            EmployeeUpdateDTO employeeDTO = _mapper.Map<EmployeeUpdateDTO>(employee);

            if (employee == null) return BadRequest();

            patchDTO.ApplyTo(employeeDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee model = _mapper.Map<Employee>(employeeDTO);

            _db.Employees.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
