using angularHeroes.entities;
using System.ComponentModel.DataAnnotations;

namespace angularHeroes.Models
{
    public class HeroModelDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Ability { get; set; }

        [Required]
        public string? SuitColors { get; set; }

        [Required]
        public DateTime StartedTrainingDate { get; set; }

        [Required]
        public double StartingPower { get; set; }

        public double CurrentPower { get; set; }

        public int DailyTrainingCount { get; set; }
        public DateTime LastTrainingDate { get; set; }
        public string? ImageData { get; set; }
    }
}
