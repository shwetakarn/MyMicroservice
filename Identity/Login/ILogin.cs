using Identity.Models;

namespace Identity.Login
{
    public interface ILogin
    {
         void CreateAccount(User obj);

         User AccountValidate(string username, string password);
    }
}