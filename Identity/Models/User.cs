using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class User
    {
          [Key]
        public int AccountNumber { get; set; }
        public string FullName { get; set; }
        public string Currency { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string conPassword { get; set; }

         public string connPassword { get; set; }

    }
}