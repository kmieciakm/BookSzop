using DatabaseManager.Models;

namespace ShopService.Exceptions.Authentication
{
    public interface IAuthenticationManager
    {
        bool CheckUserCredentials(string login, string password);
        bool CheckAdminAccess(int userId);
        void RegisterUser(User user);
    }
}