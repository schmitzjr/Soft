using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softplan.Services;
using Softplan.ViewModels;

namespace Softplan.Api2.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ShowMeTheCodeController : ControllerBase
  {
    private readonly IShowMeTheCodeService _showMeTheCodeServiceService;
    public ShowMeTheCodeController(IShowMeTheCodeService showMeTheCodeServiceService)
    {
      _showMeTheCodeServiceService = showMeTheCodeServiceService ?? throw new ArgumentNullException(nameof(showMeTheCodeServiceService));
    }

    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ShowMeTheCodeViewModel), StatusCodes.Status200OK)]
    [Authorize]
    [EnableCors("SoftplanAllowOrigins")]
    [HttpGet]
    public IActionResult ShowMeTheCode()
    {
      try
      {
        var result = _showMeTheCodeServiceService.RetornaUrl();
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(ex.Message);
      }
    }
  }
}
