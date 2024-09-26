using angularHeroes.entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace angularHeroes.Models
{
    public class HeroModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public HeroAbility Ability { get; set; }
        
        [Required]
        public string? SuitColors { get; set; }

        [Required]
        public DateTime StartedTrainingDate { get; set; } = DateTime.Now;

        [Required]
        public double StartingPower { get; set; }

        public double CurrentPower { get; set; }

        public int DailyTrainingCount { get; set; }
        public DateTime LastTrainingDate { get; set; }

        [Required]
        public Trainer Trainer { get; set; } = null!;
        
        [Required]
        public string? ImageData { get; set; }
    }
}
