using Cashbot.Domain.Core.Models;
using FluentValidation;

namespace Cashbot.Domain.Models
{
    public class Dealer : Entity<Dealer>
    {
        protected Dealer()
        {
        }

        public Dealer(string name, string cpf, string email, string password)
        {
            this.Name = name;
            this.Cpf = cpf;
            this.Email = email;
            this.Password = password;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public override bool IsValid()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O nome é obrigatório");
            RuleFor(c => c.Cpf)
               .NotEmpty().WithMessage("O cpf é obrigatório");
            RuleFor(c => c.Password)
            .NotEmpty().WithMessage("A senha é obrigatório");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public bool IsAproved()
        {
            return this.Cpf == "153.509.460-56";
        }
    }
}
