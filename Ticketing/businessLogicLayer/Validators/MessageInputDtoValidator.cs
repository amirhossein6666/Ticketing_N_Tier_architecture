using FluentValidation;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Validators;

public class MessageInputDtoValidator: AbstractValidator<MessageInputDto>
{
    public MessageInputDtoValidator()
    {
        RuleFor(m => m.Body)
            .NotEmpty()
            .WithMessage($"body is required");
    }
}