using System;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softplan.DTO;
using Softplan.Services;
using Softplan.ViewModels;

namespace Softplan.Api2.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CalculaJurosController : ControllerBase
  {
    private readonly ILogger<CalculaJurosController> _logger;
    private readonly ICalculaJurosService _calculaJurosService;
    private readonly IValidationErrorService _validationErrorService;

    public CalculaJurosController(ILogger<CalculaJurosController> logger, 
    IValidationErrorService validationErrorService, 
    ICalculaJurosService calculaJurosService)
    {
      _logger = logger;
      _validationErrorService = validationErrorService ?? throw new ArgumentNullException(nameof(validationErrorService));
      _calculaJurosService = calculaJurosService ?? throw new ArgumentNullException(nameof(calculaJurosService));
    }

    /// <summary>Calcula Juros</summary>
    /// <param name="calculaJurosDTO">ValorInicial (decimal), Meses (int)</param>
    /// <response code="200">Operação com sucesso</response>
    /// <response code="400">Parâmetros Inválidos</response>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CalculaJurosViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorsViewModel), StatusCodes.Status400BadRequest)]
    [Authorize]
    [EnableCors("SoftplanAllowOrigins")]
    [HttpGet]
    public async Task<IActionResult> TaxaJuros([FromQuery] CalculaJurosDTO calculaJurosDTO)
    {
      try
      {
        var valor = await _calculaJurosService.CalculaJuros(calculaJurosDTO);

        return Ok(valor);
      }
      catch (ValidationException ex)
      {
        _logger.LogError(ex, "ValidationException");
        return BadRequest(_validationErrorService.FormatError(ex));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Exception");
        return Problem(ex.Message);
      }
    }
  }
}
