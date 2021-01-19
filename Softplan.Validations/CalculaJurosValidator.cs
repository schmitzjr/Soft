using System;
using FluentValidation;
using Softplan.DTO;

namespace Softplan.Validations
{
  public class CalculaJurosValidator : AbstractValidator<CalculaJurosDTO>
  {
    public CalculaJurosValidator()
    {
        RuleFor(calc => calc.Meses)
            .NotNull()
            .WithMessage("não pode ser nulo.")
            .WithErrorCode("NULL_MOUNTH");

        RuleFor(calc => calc.ValorInicial)
            .NotNull()
            .WithMessage("não pode ser nulo.")
            .WithErrorCode("NULL_INITIAL_VALUE");    
    }
  }
}
