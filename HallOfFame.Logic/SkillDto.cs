using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Logic
{
    /// <summary>
    /// Skill Data Transfer Object model.
    /// </summary>
    public class SkillDto
    {
        /// <summary>
        /// Skill name.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Skill level.
        /// </summary>
        [Required]
        [Range(1, 10, ErrorMessage = "Unacceptable skill level. It has to be in range from 1 to 10.")]
        public byte Level { get; set; }
    }
}