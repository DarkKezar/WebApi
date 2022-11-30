using System.Data;
using Core.Entities;
using FluentValidation;
using Service.DTO;

namespace Service.Validators;

public class PetCMValidator : AbstractValidator<PetCreationModel>
{
    public PetCMValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter Name");
       // RuleFor(x=> x.BodyType).NotEmpty().IsInEnum().WithMessage("Please enter correct Body Type");
       // RuleFor(x=> x.EyeType).NotEmpty().IsInEnum().WithMessage("Please enter correct Eye Type");
       // RuleFor(x=> x.NoseType).NotEmpty().IsInEnum().WithMessage("Please enter correct Nose Type");
       // RuleFor(x=> x.MouthType).NotEmpty().IsInEnum().WithMessage("Please enter correct Mouth Type");
    }
}