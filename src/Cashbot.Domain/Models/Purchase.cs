using Cashbot.Domain.Core.Models;
using FluentValidation;
using System;

namespace Cashbot.Domain.Models
{
    public class Purchase : Entity<Purchase>
    {
        protected Purchase()
        {
        }

        public Purchase(int dealerId, string code, double value, string status, DateTime date)
        {
            this.DealerId = dealerId;
            this.Code = code;
            this.Value = value;
            this.Status = status;
            this.Date = date;
        }

        public int Id { get; private set; }
        public string Code { get; private set; }

        public string Status { get; private set; }

        public double Value { get; private set; }
        public DateTime Date { get; private set; }

        public int DealerId { get; private set; }
        public virtual Dealer Dealer { get; set; }

        public (int, double) CalcCashback()
        {
            int percentageCashback;
            double valueCashback;

            if (this.Value <= 1000)
            {
                percentageCashback = 10;
                valueCashback = this.Value * 0.10;
                return (percentageCashback, valueCashback);
            }
            else if(this.Value > 1000 && this.Value <= 1500)
            {
                percentageCashback = 15;
                valueCashback = this.Value * 0.15;
                return (percentageCashback, valueCashback);
            }
            else
            {
                percentageCashback = 20;
                valueCashback = this.Value * 0.20;
                return (percentageCashback, valueCashback);
            }
        }

        public override bool IsValid()
        {
            RuleFor(c => c.Code)
               .NotEmpty().WithMessage("O código é obrigatório");
            RuleFor(c => c.Value)
               .NotEmpty().WithMessage("O valor é obrigatório");
            RuleFor(c => c.Date)
            .NotEmpty().WithMessage("A data é obrigatório");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
