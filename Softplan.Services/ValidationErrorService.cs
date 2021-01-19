using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class ValidationErrorService : IValidationErrorService
  {
    public ValidationErrorsViewModel FormatError(ValidationException ex)
    {
      return new ValidationErrorsViewModel()
      {
        Date = DateTime.Now,
        Title = "Validation Failed",
        Details = ex.Errors.Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        }).ToList(),
      };
    }

    public ValidationErrorsViewModel FormatError(Exception exception)
    {
      return new ValidationErrorsViewModel()
      {
        Date = DateTime.Now,
        Title = "Validation Failed",
        Details = new List<Detail>()
        {
            new Detail()
            {
                Code = exception.HelpLink,
                Message = exception.Message
            }
        }
      };
    }
  }
}