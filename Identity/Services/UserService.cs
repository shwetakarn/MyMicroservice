using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Login;
using Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;


namespace Identity.Services
{

     public interface IUserService
    {
        Models.SecurityToken Authenticate(string username, string password);
        void RegisterUser(User temp);
    }

    public class UserService : IUserService
    {
       private List<User> _users = new List<User>
        {
            new User { AccountNumber = 3628101, Currency = "EUR", FullName = "Simon Peter", Username = "pawan", Password = "test" },
            new User { AccountNumber = 3637897, Currency = "EUR", FullName = "Glen Woodhouse", Username = "gwoodhouse", Password = "pass@123" },
            new User { AccountNumber = 3648755, Currency = "EUR", FullName = "John Smith", Username = "jsmith", Password = "admin@123" },
        };

        private readonly AppSettings _appSettings;
        ILogin temps;
        public UserService(IOptions<AppSettings> appSettings,ILogin ctr)
        {
            _appSettings = appSettings.Value;
            temps=ctr;
        }

        public Models.SecurityToken Authenticate(string username, string password)
        {
               
          var user= temps.AccountValidate(username,password);
          if(user==null)
          {
             return null;
          }
          else
          {
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("accountnumber", user.AccountNumber.ToString()),
                    new Claim("currency", user.Currency),
                    new Claim("name", user.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Models.SecurityToken() { auth_token = jwtSecurityToken };
        }
      }
      
        public void RegisterUser(User t)
        {
            temps.CreateAccount(t);

        }

    }
}