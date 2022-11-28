using FluentValidation;
using Service.DTO;

namespace Service.Validators;

public class UserUMValidator : AbstractValidator<UserUpdateModel>
{
    public UserUMValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty().WithMessage("Please enter FirstName");
        RuleFor(u => u.SecondName).NotEmpty().WithMessage("Please enter SecondName");
        RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("Please enter correct Email");
        RuleFor(u => u.OldPassword).NotEmpty().MinimumLength(8).WithMessage("Please enter correct password");
        RuleFor(u => u.NewPassword).NotEmpty().MinimumLength(8).WithMessage("Please enter correct new password");
        RuleFor(u => u.OldPassword).NotEqual(x => x.OldPassword).WithMessage("New password can't repeat Old password");
    }
}