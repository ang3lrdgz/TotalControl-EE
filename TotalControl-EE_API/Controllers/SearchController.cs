using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TotalControl_EE_API.Data;
using TotalControl_EE_API.Models;
using TotalControl_EE_API.Models.Dto;
using TotalControl_EE_API.Repository.IRepository;

namespace TotalControl_EE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchRepository _searchRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public SearchController(ILogger<SearchController> logger, ISearchRepository searchRepo, IMapper mapper )
        {

            _logger = logger;
            _mapper = mapper;
            _response = new();
            _searchRepo = searchRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Search(DateTime dateFrom, DateTime dateTo, string descriptionFilter, string businessLocation)
        {
            try
            {
                // No necesitas obtener el empleado, así que puedes eliminar esta línea

                _logger.LogInformation("Obtener los ingresos y egresos");

                var entries = await _searchRepo.GetEntriesCount(dateFrom, dateTo, descriptionFilter, businessLocation);

                var exits = await _searchRepo.GetExitsCount(dateFrom, dateTo, descriptionFilter, businessLocation);

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
