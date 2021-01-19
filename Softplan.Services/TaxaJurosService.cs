using System;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class TaxaJurosService : ITaxaJurosService
  {
    public TaxaJurosViewModel RetornaJuros()
    {
      try
      {
        return new TaxaJurosViewModel
        {
          TaxaJuros = 0.01M
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
