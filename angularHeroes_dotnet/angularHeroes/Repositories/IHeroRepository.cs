using angularHeroes.Models;

namespace angularHeroes.Repositories
{
    public interface IHeroRepository
    {
        Task<List<HeroModelDTO>> GetAllHeroesAsync();
        Task<HeroModel> GetHeroById(string id);
        Task<List<HeroModelDTO>?> GetMyHeroes(string email);
        Task<HeroModelDTO?> AddHeroAsync(NewHeroModel newHeroModel,string email);
        Task<double> TrainHero(string id);
    }
}