using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Login.Context
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions opt): base(opt)
        {

        }

        public DbSet<User> Users {get; set;}
    }
}