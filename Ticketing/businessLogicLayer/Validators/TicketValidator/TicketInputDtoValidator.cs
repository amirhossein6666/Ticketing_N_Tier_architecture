using FluentValidation;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Validators.TicketValidator;

public class TicketInputDtoValidator: AbstractValidator<TicketInputDto>
{
    public TicketInputDtoValidator()
    {
        RuleFor(t => t.Title)
            .NotNull()
            .WithMessage("title field is required")
            .NotEmpty()
            .WithMessage("the ticket title shouldn't be empty")
            .Length(5, 50)
            .WithMessage("the ticket title Length must be between 5 and 50");
    }
}