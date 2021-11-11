using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Data.Model
{
    /// <summary>
    /// Skill model.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Skill id.
        /// </summary>
        public long Id { get; set; }
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
        /// <summary>
        /// Link to the person possessing the skill.
        /// </summary>
        public Person Person { get; set; }
    }
}
