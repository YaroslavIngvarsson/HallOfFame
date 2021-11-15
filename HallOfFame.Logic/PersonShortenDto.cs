using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Logic
{
    /// <summary>
    /// Employee's Data Transfer Object model.
    /// </summary>
    public class PersonShortenDto
    {
        /// <summary>
        /// Person's name.
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