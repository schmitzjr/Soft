using System;
using System.Collections.Generic;
using System.Linq;
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
  public class ShowMeTheCodeController : ControllerBase
  {
    public ShowMeTheCodeController()
    {
    }

    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CalculaJurosViewModel), StatusCodes.Status200OK)]
    [Authorize]
    [EnableCors("SoftplanAllowOrigins")]
    [HttpGet]
    public IActionResult ShowMeTheCode()
    {
      try
      {
        return Ok(new { Url = "https://github.com/schmitzjr/soft" });
      }
      catch (Exception ex)
      {
        return Problem(ex.Message);
      }
    }
  }
}
