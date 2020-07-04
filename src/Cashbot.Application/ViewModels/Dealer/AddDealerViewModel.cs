using System.ComponentModel.DataAnnotations;

namespace Cashbot.Application.ViewModels.Dealer
{
    public class AddDealerViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Cpf é obrigatório")]
        public string Cpf { get; set; }
    }
}
