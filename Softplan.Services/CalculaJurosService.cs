using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Options;
using Softplan.Commons.Settings;
using Softplan.DTO;
using Softplan.Validations;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class CalculaJurosService : ICalculaJurosService
  {
    private readonly IClientConnectionService _clientConnectionService;
    public CalculaJurosService(IClientConnectionService clientConnectionService)
    {
      _clientConnectionService = clientConnectionService ?? throw new ArgumentNullException(nameof(clientConnectionService));
    }
    public async Task<CalculaJurosViewModel> CalculaJuros(CalculaJurosDTO calculaJurosDTO)
    {
      try
      {
        new CalculaJurosValidator().ValidateAndThrow(calculaJurosDTO);

        var fee = await _clientConnectionService.GetTaxaJuros();
        var rate = 1D + (double)fee.TaxaJuros;
        var value = (double)calculaJurosDTO.ValorInicial.Value * Math.Pow((rate), calculaJurosDTO.Meses.Value);

        return new CalculaJurosViewModel
        {
          Valor = string.Format("{0:0.00}", (Math.Truncate(100 * value) / 100))
        };

      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}