using AutoMapper;

namespace angularHeroes.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<HeroModel,HeroModelDTO>().ForMember((m)=>m.Ability,(opt)=>opt.MapFrom(p=>p.Ability.ToString()));
        }
    }
}
