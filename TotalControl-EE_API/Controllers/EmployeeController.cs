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
        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDbContext db )
        {

            _logger = logger;
            _db = db;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            _logger.LogInformation("Obtener los empleados");
            return Ok(_db.Employees.ToList());
        }

        [HttpGet("id:int", Name ="GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EmployeeDTO> GetEmployee(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer empleado con Id" + id);
                return BadRequest();
            }
            var employee = _db.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmployeeDTO> PostEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Employees.FirstOrDefault(e=>e.Name.ToLower() == employeeDTO.Name.ToLower() &&
            e.LastName.ToLower() == employeeDTO.LastName.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExistente", "El empleado ya esiste!");
                return BadRequest(ModelState);

            }

            if (employeeDTO ==null)
            {
                return BadRequest(employeeDTO);
            }
            if (employeeDTO.Id >0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Employee model = new()
            {
                Name = employeeDTO.Name,
                LastName = employeeDTO.LastName,
                Gender = employeeDTO.Gender
            };

            _db.Employees.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetEmployee", new {id= employeeDTO.Id}, employeeDTO);
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmployee(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var employee = _db.Employees.FirstOrDefault(e=>e.Id == id);
            if (employee==null)
            {
                return NotFound();
            }
            _db.Employees.Remove(employee);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO==null || id!=employeeDTO.Id)
            {
                return BadRequest();
            }

            Employee model = new()
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                LastName = employeeDTO.LastName,
                Gender = employeeDTO.Gender

            };

            _db.Employees.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartEmployee(int id, JsonPatchDocument<EmployeeDTO> patchDTO )
        {
            if (patchDTO == null || id != 0)
            {
                return BadRequest();
            }
            var employee = _db.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);

            EmployeeDTO employeeDTO = new()
            {
                Id = employee.Id,
                Name = employee.Name,
                LastName = employee.LastName,
                Gender = employee.Gender
            };

            if (employee == null) return BadRequest();

            patchDTO.ApplyTo(employeeDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee model = new()
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                LastName = employeeDTO.LastName,
                Gender = employeeDTO.Gender
            };
            _db.Employees.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
