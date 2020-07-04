using System.ComponentModel.DataAnnotations;

namespace Cashbot.Application.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
