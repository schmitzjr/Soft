using FluentValidation;
using Softplan.Commons.Criptografy;
using Softplan.Commons.Settings;
using Softplan.DTO;

namespace Softplan.Validations
{
  public class AuthenticateValidator : AbstractValidator<AuthenticateDTO>
  {
    public AuthenticateValidator(AuthSettings authSettings)
    {
      RuleFor(auth => auth.User)
        .NotEmpty()
        .WithMessage("não pode ser nulo.")
        .WithErrorCode("EMPTY_USER");

      RuleFor(auth => auth.Password)
        .NotEmpty()
        .WithMessage("não pode ser nulo.")
        .WithErrorCode("EMPTY_PASSWORD");

      RuleFor(auth => auth)
     .Must((auth, cancellation) =>
     {
       var result = auth.User.ToLower().Equals(authSettings.PowerUser.ToLower()) &&
                    PasswordHasher.ConfirmHashedPassword(authSettings.PowerPassword, auth.Password);
       return result;
     })
     .When(auth => !string.IsNullOrEmpty(auth.User) && !string.IsNullOrEmpty(auth.Password))
     .WithMessage("credenciais inválidas.")
     .WithErrorCode("INVALID_CREDENCIALS");
    }
  }
}