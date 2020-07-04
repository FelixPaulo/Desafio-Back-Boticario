using System;
using System.ComponentModel.DataAnnotations;

namespace Cashbot.Application.ViewModels.Purchase
{
    public class ListPurchaseViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string PercentCashback { get; set; }
        public double ValueCashback { get; set; }
        public string Status { get; set; }
    }
}
