using angularHeroes.Models;
using Microsoft.AspNetCore.Identity;

namespace angularHeroes.Repositories
{
    public interface IAccountRepository
    {
        Task<object> Signup(SignupModel signupModel);
        Task<object> Login(LoginModel loginModel);
    }
}