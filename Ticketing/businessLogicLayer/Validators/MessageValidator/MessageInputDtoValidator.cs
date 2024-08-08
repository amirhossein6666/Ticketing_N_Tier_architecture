using FluentValidation;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Validators.MessageValidator;

public class MessageInputDtoValidator: AbstractValidator<MessageInputDto>
{
    public MessageInputDtoValidator()
    {
        RuleFor(m => m.Body)
            .NotEmpty()
            .WithMessage($"the message body shouldn't be empty");

    }
}