using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Logic
{
    /// <summary>
    /// Person's Dto with ids.
    /// </summary>
    public class PersonFullDto
    {
        /// <summary>
        /// Employee's id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Employee's name.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Employee's display name.
        /// </summary>
        [Required]
        public string DisplayName { get; set; }
        /// <summary>
        /// Employee's skill set.
        /// </summary>
        public ICollection<SkillDto> Skills { get; set; }
    }
}