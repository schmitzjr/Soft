﻿using System;
using Microsoft.Extensions.Options;
using Softplan.Commons;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class TaxaJurosService : ITaxaJurosService
  {
    private readonly Parameters _parameters;
    public TaxaJurosService(IOptions<Parameters> parameters)
    {
      _parameters = parameters?.Value ?? throw new ArgumentNullException(nameof(parameters));
    }
    public TaxaJurosViewModel RetornaJuros()
    {
      try
      {
        return new TaxaJurosViewModel
        {
          TaxaJuros = _parameters.Fee.Value
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
