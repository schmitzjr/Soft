using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softplan.Services;
using Softplan.ViewModels;

namespace Softplan.Api1.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TaxaJurosController : ControllerBase
  {
    private readonly ILogger<TaxaJurosController> _logger;
    private readonly ITaxaJurosService _taxaJurosService;
    private readonly IValidationErrorService _validationErrorService;

    public TaxaJurosController(ILogger<TaxaJurosController> logger, IValidationErrorService validationErrorService, ITaxaJurosService taxaJurosService)
    {
      _logger = logger;
      _validationErrorService = validationErrorService ?? throw new ArgumentNullException(nameof(validationErrorService));
      _taxaJurosService = taxaJurosService ?? throw new ArgumentNullException(nameof(taxaJurosService));
    }

    /// <summary>Taxa Juros</summary>
    /// <response code="200">Operação com sucesso</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TaxaJurosViewModel), StatusCodes.Status200OK)]
    [Authorize]
    [EnableCors("SoftplanAllowOrigins")]
    [HttpGet]
    public IActionResult TaxaJuros()
    {
      try
      {
        var taxaJuros = _taxaJurosService.RetornaJuros();

        return Ok(taxaJuros);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Exception");
        return Problem(ex.Message);
      }
    }
  }
}
