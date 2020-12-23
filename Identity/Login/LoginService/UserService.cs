using System.Linq;
using Identity.Login.Context;
using Identity.Models;

namespace Identity.Login.LoginService
{
    public class UserService : ILogin
    {
        LoginContext context;
        public UserService(LoginContext log)
        {
           this.context=log;
        }
        public User AccountValidate(string username, string password)
        {
                 
            var temp=this.context.Users.Where(s=>s.Username==username && s.Password==password).FirstOrDefault();
            return temp; 
        }

        public void CreateAccount(User obj)
        {
              this.context.Add(obj);
              this.context.SaveChanges();
        }

      
    }
    
}