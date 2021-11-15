using System.Linq;
using HallOfFame.Data.Model;

namespace HallOfFame.Logic
{
    /// <summary>
    /// Casting Model to DTO and vice versa.
    /// </summary>
    public static class DtoAutoMapper
    {
        /// <summary>
        /// Casting Model of Skill to DTO.
        /// </summary>
        /// <param name="skill">Model Skill.</param>
        /// <returns>DTO Skill.</returns>
        public static SkillDto ToDto(this Skill skill)
        {
            return new SkillDto()
            {
                Name = skill.Name,
                Level = skill.Level
            };
        }
        /// <summary>
        /// Casting DTO of Skill to Model.
        /// </summary>
        /// <param name="skillDto">DTO Skill.</param>
        /// <returns>Model Skill.</returns>
        public static Skill ToModel(this SkillDto skillDto)
        {
            return new Skill()
            {
                Name = skillDto.Name,
                Level = skillDto.Level
            };
        }
        /// <summary>
        /// Casting Model Person to DTO.
        /// </summary>
        /// <param name="person">Model Person.</param>
        /// <returns>DTO Person.</returns>
        public static PersonShortenDto ToDto(this Person person)
        {
            return new PersonShortenDto()
            {
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills.Select(x => x.ToDto()).ToList()
            };
        }
        /// <summary>
        /// Casting DTO person to Model.
        /// </summary>
        /// <param name="personDto">DTO Person.</param>
        /// <returns>Model Person.</returns>
        public static Person ToModel(this PersonShortenDto personDto)
        {
            return new Person()
            {
                Name = personDto.Name,
                DisplayName = personDto.DisplayName,
                Skills = personDto.Skills.Select(x => x.ToModel()).ToList()
            };
        }
        /// <summary>
        /// Casting Model person to full Dto.
        /// </summary>
        /// <param name="person">Model Person to convert.</param>
        /// <returns>Full Dto with ids.</returns>
        public static PersonFullDto ToFullDto(this Person person)
        {
            return new PersonFullDto()
            {
                Id = person.Id,
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills.Select(x => x.ToDto()).ToList()
            };
        }
    }
}