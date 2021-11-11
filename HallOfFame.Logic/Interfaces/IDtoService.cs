using System.Collections.Generic;
using System.Threading.Tasks;

namespace HallOfFame.Logic.Interfaces
{
    /// <summary>
    /// Interface hiding Db and DTO conversion from controller for CRUD implementation.
    /// </summary>
    public interface IDtoService
    {
        /// <summary>
        /// Get a person by their id.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>An instance of a PersonDto if exists, otherwise null.</returns>
        Task<PersonShortenDto> GetPerson(long id);
        /// <summary>
        /// Get all the PersonDto instances.
        /// </summary>
        /// <returns>Set of people.</returns>
        Task<ICollection<PersonFullDto>> GetPeople();
        /// <summary>
        /// Delete an instance of a person by their id.
        /// </summary>
        /// <param name="id">Person's id</param>
        /// <returns>True if successfully deleted.</returns>
        Task<bool> DeletePerson(long id);
        /// <summary>
        /// Create a new instance of a Person.
        /// </summary>
        /// <param name="personDto">Person to create.</param>
        /// <returns>Created person.</returns>
        Task<PersonShortenDto> CreatePerson(PersonShortenDto personDto);
        /// <summary>
        /// Update an instance of a person.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <param name="personDto"></param>
        /// <returns>An updated person.</returns>
        Task<PersonShortenDto> UpdatePerson(long id, PersonShortenDto personDto);
    }
}
