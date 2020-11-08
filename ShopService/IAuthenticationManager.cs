using DatabaseManager.Models;

namespace ShopService
{
    public interface IAuthenticationManager
    {
        bool CheckUserCredentials(string login, string password);
        bool CheckAdminAccess(int userId);
        void RegisterUser(User user);
    }
}