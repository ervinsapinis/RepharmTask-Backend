using FluentValidation;
using RepharmTaskBackend.Commands;

namespace RepharmTaskBackend.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .Length(1, 100).WithMessage("{PropertyName} length can't be less than 1 and higher than 500");

            RuleFor(x => x.Model.Surname)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .Length(1, 100).WithMessage("{PropertyName} length can't be less than 1 and higher than 500");

            RuleFor(x => x.Model.Sex)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");

            RuleFor(x => x.Model.PersonCode)
                .MaximumLength(13).WithMessage("{PropertyName} length can't be higher than 13");

            RuleFor(x => x.Model.DoctorId)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");
        }
    }
}