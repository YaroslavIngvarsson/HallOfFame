using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Data.Model;

namespace HallOfFame.Data.Interfaces
{
    /// <summary>
    /// CRUD for the <see cref="Person"/> model.
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// Get a Person by their id.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns> An instance of a Person if exists, otherwise null.</returns>
        Task<Person> GetPerson(long id);
        /// <summary>
        /// Get all the Person instances.
        /// </summary>
        /// <returns>Set of people.</returns>
        Task<ICollection<Person>> GetPeople();
        /// <summary>
        /// Delete an instance of a person by their id.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>True if successfully deleted.</returns>
        Task<bool> DeletePerson(long id);
        /// <summary>
        /// Create a new instance of a Person.
        /// </summary>
        /// <param name="person">Model to create.</param>
        /// <returns>True if successfully created.</returns>
        Task<bool> CreatePerson(Person person);
        /// <summary>
        /// Update an instance of a person.
        /// </summary>
        /// <param name="id">Id of the person to update.</param>
        /// <param name="person">New model.</param>
        /// <returns>True if successfully updated.</returns>
        Task<bool> UpdatePerson(long id, Person person);
    }
}