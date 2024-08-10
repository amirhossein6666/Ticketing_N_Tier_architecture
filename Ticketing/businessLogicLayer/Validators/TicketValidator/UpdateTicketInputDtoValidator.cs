using FluentValidation;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Validators.TicketValidator;

public class UpdateTicketInputDtoValidator: AbstractValidator<UpdateTicketInputDto>
{
    public UpdateTicketInputDtoValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .WithMessage("the ticket title shouldn't be empty")
            .Length(5, 50)
            .WithMessage("the ticket title Length must be between 5 and 50");
    }
}