using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models.Authentication.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName is reqauired")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }    
    }
}
