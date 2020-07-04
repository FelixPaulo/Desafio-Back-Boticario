using System.ComponentModel.DataAnnotations;

namespace Cashbot.Application.ViewModels.Dealer
{
    public class DealerViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }
    }
}
