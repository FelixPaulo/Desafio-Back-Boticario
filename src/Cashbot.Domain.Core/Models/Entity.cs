using FluentValidation;
using FluentValidation.Results;

namespace Cashbot.Domain.Core.Models
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool IsValid();
        public ValidationResult ValidationResult { get; protected set; }
    }
}
