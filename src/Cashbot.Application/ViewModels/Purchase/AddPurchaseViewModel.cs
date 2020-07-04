using System;
using System.ComponentModel.DataAnnotations;

namespace Cashbot.Application.ViewModels.Purchase
{
    public class AddPurchaseViewModel
    {
        [Required(ErrorMessage = "Código é obrigatório")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Cpf do revendedor é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Data da compra é obrigatório")]
        public DateTime Date { get; set; }
    }
}
