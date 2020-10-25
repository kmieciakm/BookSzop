namespace ShopService
{
    public interface IAuthenticationManager
    {
        bool CheckUserCredentials(string login, string password);
    }
}