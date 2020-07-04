using System;
using System.ComponentModel.DataAnnotations;

namespace Cashbot.Application.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Cpf { get; set; }
        public string Status { get; set; }

        public double Cashback { get; set; }
        public DateTime Date { get; set; }
    }
}
