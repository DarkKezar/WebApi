using FluentValidation;
using Service.DTO;

namespace Service.Validators;

public class UserCMValidator : AbstractValidator<UserCreationModel>
{
    public UserCMValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty().WithMessage("Please enter FirstName");
        RuleFor(u => u.SecondName).NotEmpty().WithMessage("Please enter SecondName");
        RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("Please enter correct Email");
        RuleFor(u => u.Password).NotEmpty().MinimumLength(8).WithMessage("Please enter FirstName");
    }
}