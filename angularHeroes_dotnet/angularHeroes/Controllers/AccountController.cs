using angularHeroes.Models;
using angularHeroes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace angularHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var res = await _accountRepository.Signup(signupModel);
            /*if (res.Succeeded)
            {
                return Ok(res.Succeeded);
            }
            return Unauthorized();*/
            if (res == "errorSignup" /*string.IsNullOrWhiteSpace(res)*/)
            {
                return Unauthorized("error could not signup. please Try again later");
            }
            if (res == "errorToken" /*string.IsNullOrWhiteSpace(res)*/)
            {
                return Unauthorized("errorToken");
            }
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel signinModel)
        {
            var res = await _accountRepository.Login(signinModel);
            if (res == "errorSignIn" /*string.IsNullOrWhiteSpace(res)*/)
            {
                return Unauthorized("error could not signin. please enter registered email and password");
            }if (res == "errorToken" /*string.IsNullOrWhiteSpace(res)*/)
            {
                return Unauthorized("errorToken");
            }
            return Ok(res);
        }
    }
}

