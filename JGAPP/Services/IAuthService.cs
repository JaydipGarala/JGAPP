using Firebase.Auth;
using JGAPP.Model;

namespace JGAPP.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterLoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string name);
    }
}
