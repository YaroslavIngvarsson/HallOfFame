using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HallOfFame.Data.Interfaces;
using HallOfFame.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HallOfFame.Data
{
    /// <summary>
    /// Implementation of CRUD.
    /// </summary>
    public class PeopleRepository : IPeopleRepository
    {
        private readonly HallOfFameDbContext _context;
        private readonly ILogger _logger;
        /// <summary>
        /// Initializing repository.
        /// </summary>
        /// <param name="context">Data base context.</param>
        /// <param name="logger">File logger.</param>
        public PeopleRepository(HallOfFameDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adding a person to Db.
        /// </summary>
        /// <param name="person">An instance of a person.</param>
        /// <returns>True if successfully added.</returns>
        public async Task<bool> CreatePerson(Person person)
        {
            await _context.AddAsync(person);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return false;
            }
        }
        /// <summary>
        /// Deleting a person to Db.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>True if successfully deleted.</returns>
        public async Task<bool> DeletePerson(long id)
        {
            var person = await _context.People.FindAsync(id);
            if (person is null)
                return false;
            try
            {
                var skillsToRemove = _context.Skills
                    .Where(x => x.Person == person)
                    .Select(x => x);
                _context.Skills.RemoveRange(skillsToRemove);

                _context.People.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return false;
            }
        }
        /// <summary>
        /// Getting a set of people from Db.
        /// </summary>
        /// <returns>A set of people.</returns>
        public async Task<ICollection<Person>> GetPeople()
        {
            try
            {
                return await _context.People
                    .Include(x => x.Skills)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return null;
            }
            
        }
        /// <summary>
        /// Getting a person from Db.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <returns>An instance of a person.</returns>
        public async Task<Person> GetPerson(long id)
        {
            try
            {
                return await _context.People
                    .Where(x => x.Id == id)
                    .Include(x => x.Skills)
                    .FirstOrDefaultAsync();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return null;
            }
        }
        /// <summary>
        /// Updating a person in Db.
        /// </summary>
        /// <param name="id">Person's id.</param>
        /// <param name="person">An instance of a person.</param>
        /// <returns>True if successfully updated.</returns>
        public async Task<bool> UpdatePerson(long id, Person person)
        {
            var oldPerson = await _context.People.FindAsync(id);
            if (oldPerson is null)
                return false;

            var skillsToRemove = _context.Skills
                .Where(x => x.Person == oldPerson)
                .Select(x => x);
            _context.Skills.RemoveRange(skillsToRemove);

            oldPerson.Name = person.Name;
            oldPerson.DisplayName = person.DisplayName;
            oldPerson.Skills = person.Skills;

            try
            {
                _context.People.Update(oldPerson);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return false;
            }
        }
    }
}

