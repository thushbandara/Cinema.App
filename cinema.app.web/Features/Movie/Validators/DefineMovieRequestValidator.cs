using cinema.app.web.Features.Movie.DTOs;
using cinema.app.web.Infrastructure.Common;
using FluentValidation;

namespace cinema.app.web.Features.Movie.Validators
{
    public class DefineMovieRequestValidator : ModelValidate<DefineMovieRequestDto>
    {
        public DefineMovieRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title are required.");

            RuleFor(x => x.Rows)
               .NotNull().WithMessage("Rows are required.")
               .Must(a => a > 0)
                .WithMessage("At least one row must be added.");

            RuleFor(x => x.SeatsPerRow)
               .NotNull().WithMessage("Seats per row is required.")
               .Must(a => a > 0)
                .WithMessage("At least one seat per row");
        }
    }
}