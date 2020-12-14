using ShopService.Models.UserModel;

namespace ShopService.Authentication
{
    public interface IAuthenticationManager
    {
        bool CheckUserCredentials(string login, string password);
        int? GetUserIdByLogin(string login);
        bool CheckAdminAccess(int userId);
        void RegisterUser(IUserCreate user);
    }
}