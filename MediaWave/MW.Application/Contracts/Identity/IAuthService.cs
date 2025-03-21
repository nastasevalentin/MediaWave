using MW.Application.Models.Identity;

namespace MW.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model);
        Task<(int, string)> Login(LoginModel model);
        Task<(int, string)> Logout();
    }
}