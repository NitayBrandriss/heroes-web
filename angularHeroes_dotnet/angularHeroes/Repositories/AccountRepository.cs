using angularHeroes.Migrations;
using angularHeroes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace angularHeroes.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Trainer> _userManager;
        private readonly SignInManager<Trainer> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountRepository(UserManager<Trainer> userManager, SignInManager<Trainer> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public async Task<object> Signup(SignupModel signupModel)
        {
            Trainer trainer = new()
            {
                UserName = signupModel.Email,
                Email = signupModel.Email
            };
            var result = await _userManager.CreateAsync(trainer, signupModel.Password);
            if (!result.Succeeded)
            {
                return "errorSignup";
            }
            string token = NewToken(signupModel.Email);
            if (string.IsNullOrEmpty(token))
            {
                return "errorToken";
            }
            return new { token = token };
        }
        public async Task<object> Login(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return "errorSignIn";
            }
            string token = NewToken(loginModel.Email);
            if (string.IsNullOrEmpty(token))
            {
                return "errorToken";
            }
            return new {token = token};
        }

        private string NewToken(string email)
        {
            var authClaims = new List< Claim>
            { 
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
