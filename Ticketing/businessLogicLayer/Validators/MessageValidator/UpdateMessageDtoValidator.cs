using FluentValidation;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Validators.MessageValidator;

public class UpdateMessageDtoValidator: AbstractValidator<UpdateMessageDto>
{
    public UpdateMessageDtoValidator()
    {
        RuleFor(m => m.Body)
            .NotEmpty()
            .WithMessage($"the message body shouldn't be empty");
    }
}