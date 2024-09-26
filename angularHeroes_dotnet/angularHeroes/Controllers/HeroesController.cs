using angularHeroes.Models;
using angularHeroes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace angularHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroRepository _heroRepository;
        public HeroesController(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }



        [HttpGet("")]
        public async Task<IActionResult> GetAllHeroes()
        {
            var res = await _heroRepository.GetAllHeroesAsync();
            if (res?.Count() > 0)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeroById([FromRoute] string Id)
        {
            var res = await _heroRepository.GetHeroById(Id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
               
        [HttpGet("my-heroes")]
        [Authorize]
        public async Task<IActionResult> GetMyHeroes()
        {
            string userMail = User.Identity.Name;
            var myHeroes = await _heroRepository.GetMyHeroes(userMail);
            if(myHeroes == null)
            {
                return BadRequest();
            }
            return Ok(myHeroes);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<HeroModelDTO>> AddNewHero([FromBody] NewHeroModel newHeroModel)
        {
            string userMail = User!.Identity!.Name!;
            var hero = await _heroRepository.AddHeroAsync(newHeroModel, userMail);
            return StatusCode(StatusCodes.Status201Created,hero);
        }

        [HttpPatch("{id}/train")]
        public async Task<IActionResult> TrainHero([FromRoute] string id)
        {
            var res = await _heroRepository.TrainHero(id);
            if (res<0)
            {
                if (res == -1)
                {
                    return BadRequest("couldent find the hero"); 
                }
                if (res == -2)
                {
                    return BadRequest("hero already trined 5 times");
                }
            }
            return Ok(res);

        }
      
    }
}
