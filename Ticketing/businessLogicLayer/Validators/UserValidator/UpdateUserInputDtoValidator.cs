using FluentValidation;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Validators.UserValidator;

public class UpdateUserInputDtoValidator: AbstractValidator<UpdateUserInputDto>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserInputDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        RuleFor(u => u.Username)
            .NotEmpty().When(u => u.Username != null)
            .WithMessage("the user name field shouldn't be empty")
            .MustAsync(IsUnique)
            .WithMessage("user name field must be unique")
            .Length(7, 30)
            .WithMessage("username length must be between 7 and 30");

        RuleFor(u => u.Password)
            .NotEmpty().When(u => u.Password != null).WithMessage("password field shouldn't be empty")
            .MinimumLength(8).WithMessage("password must have at least 8 char")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.");
    }
    private async Task<bool> IsUnique(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.UserNameIsUnique(username);
    }
}