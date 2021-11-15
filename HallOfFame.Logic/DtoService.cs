using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HallOfFame.Data.Interfaces;
using HallOfFame.Logic.Interfaces;
using HallOfFame.Data.Model;

namespace HallOfFame.Logic
{
    /// <summary>
    /// Implementation of controller-repository interlayer.
    /// </summary>
    public class DtoService : IDtoService
    {
        /// <summary>
        /// Db repository.
        /// </summary>
        private readonly IPeopleRepository _repository;
        /// <summary>
        /// Initializing Dto service.
        /// </summary>
        /// <param name="repository">People repository.</param>
        public DtoService(IPeopleRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Getting a person from Db.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>Person's DTO.</returns>
        public async Task<PersonShortenDto> GetPerson(long id)
        {
            var person = await _repository.GetPerson(id);
            return person?.ToDto();
        }
        /// <summary>
        /// Getting all the people from Db.
        /// </summary>
        /// <returns>Set of people.</returns>
        public async Task<ICollection<PersonFullDto>> GetPeople()
        {
            return (await _repository.GetPeople())
                .Select(x => x.ToFullDto()).ToList();
        }
        /// <summary>
        /// Deleting a person from db.
        /// </summary>
        /// <param name="id">Person's id</param>
        /// <returns>True if deleted.</returns>
        public async Task<bool> DeletePerson(long id)
        {
            return await _repository.DeletePerson(id);
        }
        /// <summary>
        /// Adding a person to Db.
        /// </summary>
        /// <param name="personDto">Person's DTO.</param>
        /// <returns>Created person.</returns>
        public async Task<PersonShortenDto> CreatePerson(PersonShortenDto personDto)
        {
            var newPerson = new Person()
            {
                Name = personDto.Name,
                DisplayName = personDto.DisplayName,
                Skills = personDto.Skills.Select(x => x.ToModel()).ToList()
            };
            var isSuccessful = await _repository.CreatePerson(newPerson);
            return isSuccessful ? newPerson.ToDto() : null;
        }
        /// <summary>
        /// Updating a person.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <param name="personDto">Person's info to update.</param>
        /// <returns>Updated person.</returns>
        public async Task<PersonShortenDto> UpdatePerson(long id, PersonShortenDto personDto)
        {
            var isSuccessful = await _repository.UpdatePerson(id, personDto.ToModel());
            return isSuccessful ? personDto : null;
        }
    }
}