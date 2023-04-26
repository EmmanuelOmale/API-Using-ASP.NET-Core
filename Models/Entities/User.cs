using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser
    {
      
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
