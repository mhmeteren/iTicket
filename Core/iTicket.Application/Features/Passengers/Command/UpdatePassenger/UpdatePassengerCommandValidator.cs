using FluentValidation;

namespace iTicket.Application.Features.Passengers.Command.UpdatePassenger
{
    public class UpdatePassengerCommandValidator : AbstractValidator<UpdatePassengerCommandRequest>
    {
        public UpdatePassengerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(100).MinimumLength(5);
            RuleFor(x => x.Gender).NotEmpty().NotNull().MaximumLength(10).Must(x => x.Equals("Female") || x.Equals("Male"));

            RuleFor(x => x.IdentificationNo)
            .NotEmpty()
            .WithMessage("Identification number must not be empty when Is not Turkish Citizen is unchecked.")
            .Length(11)
            .WithMessage("Identification number must be 11 characters long when Is not Turkish Citizen is unchecked.")
            .When(x => !x.IsNotTurkishCitizen);


            RuleFor(x => x.IdentificationNo)
                .Empty()
                .WithMessage("Identification number must be empty when Is not Turkish Citizen is checked.")
                .When(x => x.IsNotTurkishCitizen);


            RuleFor(x => x.PassportNo)
            .NotEmpty()
            .WithMessage("PassportNo number must not be empty when Is not Turkish Citizen is checked.")
            .Length(9)
            .WithMessage("PassportNo number must be 9 characters long when Is not Turkish Citizen is checked.")
            .When(x => x.IsNotTurkishCitizen);


            RuleFor(x => x.PassportNo)
            .Empty()
            .WithMessage("PassportNo number must be empty when Is not Turkish Citizen is unchecked.")
            .When(x => !x.IsNotTurkishCitizen);


            RuleFor(x => x.Nationality)
             .NotEmpty()
             .WithMessage("Nationality must not be empty when Is not Turkish Citizen is checked.")
             .MaximumLength(50)
             .WithMessage("Nationality must be a maximum of 50 characters long when Is not Turkish Citizen is checked.")
             .When(x => x.IsNotTurkishCitizen);

            RuleFor(x => x.Nationality)
            .Empty()
            .WithMessage("Nationality number must be empty when Is not Turkish Citizen is unchecked.")
            .When(x => !x.IsNotTurkishCitizen);


            RuleFor(x => x.DateOfBirth).NotEmpty().NotNull()
                .LessThan(DateTime.Now.AddYears(-1))
                .GreaterThan(DateTime.Now.AddYears(-80));
        }
    }
}
