using FluentValidation;
using Service.DTO;

namespace Service.Validators;

public class FarmCMValidator : AbstractValidator<FarmCreationModel>
{
    public FarmCMValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter Name");
    }
}