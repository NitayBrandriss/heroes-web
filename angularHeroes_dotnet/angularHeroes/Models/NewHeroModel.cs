using angularHeroes.entities;
using System.ComponentModel.DataAnnotations;

namespace angularHeroes.Models
{
    public class NewHeroModel
    {
        [Required(ErrorMessage ="please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "please enter Ability")]
        public HeroAbility Ability { get; set; }

        [Required(ErrorMessage = "please enter suit colors")]
        public string SuitColors { get; set; }

        public string? ImageData { get; set; }


    }
}
