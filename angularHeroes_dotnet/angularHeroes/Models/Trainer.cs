using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace angularHeroes.Models
{
    public class Trainer: IdentityUser
    {
        public ICollection<HeroModel?>? Heroes { get; set; }
        
    }
}
