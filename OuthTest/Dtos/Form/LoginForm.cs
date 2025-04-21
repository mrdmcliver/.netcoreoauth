using System.ComponentModel.DataAnnotations;

namespace OAuthTest.Dtos.Form
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Please enter user name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [MinLength(6, ErrorMessage = "Password must have minimum of 6 characters")]
        public string Password { get; set; }

        public LoginForm(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
