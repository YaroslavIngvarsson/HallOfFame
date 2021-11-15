using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Logic;
using HallOfFame.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.Api.Controllers
{
    /// <summary>
    /// CRUD on <see cref="PersonShortenDto"/> and return a set of Persons.
    /// </summary>
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : Controller
    {
        /// <summary>
        /// Service for accessing the Db and converting models to Dto and vice versa.
        /// </summary>
        private readonly IDtoService _dtoService;
        /// <summary>
        /// Controller for <see cref="PersonShortenDto"/>.
        /// </summary>
        /// <param name="dtoService">Database repository</param>
        public PersonController(IDtoService dtoService)
        {
            _dtoService = dtoService;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("/api/v1/Persons")]
        public async Task<ActionResult<IEnumerable<PersonShortenDto>>> GetPeople()
        {
            return Ok(await _dtoService.GetPeople());
        }
        /// <summary>
        /// Creates an instance of a person.
        /// </summary>
        /// <param name="personDto">Data Transfer Object of a person.</param>
        /// <returns>New person.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/v1/person")]
        [HttpPost("")]
        public async Task<ActionResult<PersonShortenDto>> CreatePerson([FromBody] PersonShortenDto personDto)
        {
            var newPerson = await _dtoService.CreatePerson(personDto);
            return (newPerson is null) ? BadRequest() : Ok(newPerson);
        }
        /// <summary>
        /// Read an instance of a person.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>Person with current id.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/v1/person/{id:long}")]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<PersonShortenDto>> GetPerson(long id)
        {
            var dtoPerson = await _dtoService.GetPerson(id);
            return (dtoPerson is null) ? NotFound() : Ok(dtoPerson);
        }
        /// <summary>
        /// Update a person.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <param name="personDto">Person with current id.</param>
        /// <returns>Updated person with current id.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/v1/person/{id:long}")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<PersonShortenDto>> UpdatePerson(long id, [FromBody] PersonShortenDto personDto)
        {
            var updatedPerson = await _dtoService.UpdatePerson(id, personDto);
            return (updatedPerson is null) ? NotFound() : Ok(personDto);
        }
        /// <summary>
        /// Delete a person.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>Status of deletion.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/v1/person/{id:long}")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<PersonShortenDto>> DeletePerson(long id)
        {
            var isSuccessful = await _dtoService.DeletePerson(id);
            return isSuccessful ? Ok() : NotFound();
        }
    }
}