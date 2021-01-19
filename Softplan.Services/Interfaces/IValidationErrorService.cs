using System;
using FluentValidation;
using Softplan.ViewModels;

namespace Softplan.Services
{
    public interface IValidationErrorService
    {
        ValidationErrorsViewModel FormatError(ValidationException ex);
        ValidationErrorsViewModel FormatError(Exception exception);
    }
}