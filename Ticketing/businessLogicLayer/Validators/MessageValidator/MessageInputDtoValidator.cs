using FluentValidation;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Validators.MessageValidator;

public class MessageInputDtoValidator: AbstractValidator<MessageInputDto>
{
    public MessageInputDtoValidator()
    {
        RuleFor(m => m.Body)
            .NotNull()
            .WithMessage("the message body is required")
            .NotEmpty()
            .WithMessage($"the message body shouldn't be empty");
        RuleFor(m => m.TicketId)
            .NotNull().WithMessage("ticket Id field is required")
            .NotEmpty().WithMessage("ticket Id field shouldn't be empty")
            .GreaterThanOrEqualTo(1).WithMessage("ticket id must be greater that 1");



    }
}