using FluentValidation;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Validators.UserValidator;

public class CreateUserInputDtoValidator: AbstractValidator<CreateUserInputDto>
{
    private readonly IUserRepository _userRepository;
    public CreateUserInputDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        RuleFor(u => u.Username)
            .NotNull()
            .WithMessage("user name field is reqired")
            .NotEmpty()
            .WithMessage("the user name field shouldn't be empty")
            .MustAsync(IsUnique)
            .WithMessage("user name field must be unique")
            .Length(7, 30)
            .WithMessage("username length must be between 7 and 30");

        RuleFor(u => u.password)
            .NotNull()
            .WithMessage("password field is required")
            .NotEmpty().WithMessage("password field shouldn't be empty")
            .MinimumLength(8).WithMessage("password must have at least 8 char")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.");

        RuleFor(u => u.Role)
            .NotNull().WithMessage("Role file is required")
            .NotEmpty().WithMessage("Role field shouldn't be empty");
    }
    private async Task<bool> IsUnique(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.UserNameIsUnique(username);
    }
}