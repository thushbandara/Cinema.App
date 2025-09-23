using FluentValidation;
using FluentValidation.Results;

namespace cinema.app.web.Infrastructure.Common
{
    public abstract class ModelValidate<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validation = base.Validate(context);

            if (!validation.IsValid)
            {
                RaiseValidationException(context, validation);
            }

            return validation;
        }
    }
}
