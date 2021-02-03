using System;
using Microsoft.Extensions.Options;
using Softplan.Commons;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class TaxaJurosService : ITaxaJurosService
  {
    private readonly AuthSettings _authSettings;
    public TaxaJurosService(IOptions<AuthSettings> authSettings)
    {
      _authSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));
    }
    public TaxaJurosViewModel RetornaJuros()
    {
      try
      {
        return new TaxaJurosViewModel
        {
          TaxaJuros = _authSettings.fee.Value
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
