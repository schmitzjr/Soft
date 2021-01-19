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
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticateController : ControllerBase
  {
    private readonly ILogger<AuthenticateController> _logger;
    private readonly IAuthenticationService _authenticationService;
    private readonly IValidationErrorService _validationErrorService;
    public AuthenticateController(ILogger<AuthenticateController> logger, IValidationErrorService validationErrorService, IAuthenticationService authenticationService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validationErrorService = validationErrorService ?? throw new ArgumentNullException(nameof(validationErrorService));
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }
    
    /// <summary>Autenticação</summary>
    /// <param name="authDTO">Usuario (string), Senha (string)</param>
    /// <response code="200">Operação com sucesso</response>
    /// <response code="400">Parâmetros Inválidos</response>
    /// <response code="401">Falha na autenticação</response>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorsViewModel), StatusCodes.Status400BadRequest)]
    [EnableCors("SoftplanAllowOrigins")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateDTO authDTO)
    {
      try
      {
        var result = await _authenticationService.AuthenticateAsync(authDTO);
        return Ok(result);
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