using angularHeroes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace angularHeroes.Data
{
    public class HeroContext :IdentityDbContext<Trainer>
    {
        public HeroContext(DbContextOptions<HeroContext> options): base(options) { }
        public DbSet<HeroModel> Heroes { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

    }
}
