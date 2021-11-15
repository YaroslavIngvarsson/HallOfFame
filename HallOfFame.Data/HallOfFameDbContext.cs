using HallOfFame.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Data
{
    /// <summary>
    /// Database context for the Model.
    /// </summary>
    public class HallOfFameDbContext : DbContext
    {
        /// <summary>
        /// Initializing database context. 
        /// </summary>
        /// <param name="options">Database context options.</param>
        public HallOfFameDbContext(DbContextOptions<HallOfFameDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Set of people.
        /// </summary>
        public DbSet<Person> People { get; set; }
        /// <summary>
        /// Set of skills.
        /// </summary>
        public DbSet<Skill> Skills { get; set; }
    }
}