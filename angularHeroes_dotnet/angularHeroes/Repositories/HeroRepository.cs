using angularHeroes.Data;
using angularHeroes.Helpers;
using angularHeroes.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace angularHeroes.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly HeroContext _context;
        private readonly IMapper _mapper;

        public HeroRepository(HeroContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      
        public async Task<List<HeroModelDTO>> GetAllHeroesAsync()
        {
            var heroes = await _context.Heroes.ToListAsync();
            List<HeroModel> myHeroesDecrised = heroes.OrderByDescending(h => h.CurrentPower).ToList();
            List<HeroModelDTO> myHeroesDecrisedDTO = _mapper.Map<List<HeroModel>, List<HeroModelDTO>>(myHeroesDecrised);
            return myHeroesDecrisedDTO;
        }
       
        public async Task<HeroModel> GetHeroById(string id)
        {
            var hero = await _context.Heroes.Include(h => h.Trainer).Where(h=>h.Id+"" == id).FirstOrDefaultAsync();
            return hero;
        }
       
        public async Task<List<HeroModelDTO>?> GetMyHeroes(string email)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t=>t.Email == email);
            if(trainer == null)
            {
                return null;
            }
            ICollection<HeroModel> myHeroes = trainer.Heroes;
            if(myHeroes == null)
            {
                return new List<HeroModelDTO>() ;
            }
            foreach (var hero in myHeroes)
            {
                DateTime yesterday = DateTime.Today.AddDays(-1);
                if (hero.LastTrainingDate < DateTime.Today)
                {
                    hero.DailyTrainingCount = 0;
                }
            }
            await _context.SaveChangesAsync();
            List<HeroModel> myHeroesDecrised = myHeroes.OrderByDescending(h => h.CurrentPower).ToList();
            List<HeroModelDTO> myHeroesDecrisedDTO = _mapper.Map<List<HeroModel>, List<HeroModelDTO>>(myHeroesDecrised);
            return myHeroesDecrisedDTO;
        }

        public async Task<HeroModelDTO?> AddHeroAsync(NewHeroModel newHeroModel, string email)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == email);
            if (trainer == null)
            {
                return null;
            }
            HeroModel heroModel = new() //should be replaced with autoMapper
            {
                Name = newHeroModel.Name,
                Ability = newHeroModel.Ability,
                SuitColors = newHeroModel.SuitColors,
                ImageData = newHeroModel.ImageData,
                Trainer = trainer,
               /* StartedTrainingDate = DateTime.Now,
                LastTrainingDate = DateTime.Now,*/
            };
            trainer.Heroes?.Add(heroModel);
            _context.Add(heroModel);

            await _context.SaveChangesAsync();

            return _mapper.Map <HeroModelDTO>(heroModel);

        }

        public async Task<double> TrainHero(string id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
            {
                return -1; // Invalid GUID format
            }
            var hero = await _context.Heroes.FirstOrDefaultAsync(h => h.Id == parsedId);

            if (hero == null)
            {
                return -1;
            }
            
            if (hero.LastTrainingDate < DateTime.Today)
            {
                hero.DailyTrainingCount = 0;
            }
            if (hero.DailyTrainingCount >= 5)
            {
                return -2;
            }
            hero.DailyTrainingCount++;
            hero.LastTrainingDate = DateTime.Now;
            double newPower = StaticFunctions.AddRandomNumberUpTo10Precent(hero.CurrentPower);
            hero.CurrentPower = newPower;
            await _context.SaveChangesAsync();
            return newPower;
        }
    
    }
    
}
