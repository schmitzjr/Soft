using System;
using Microsoft.Extensions.Options;
using Softplan.Commons;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class ShowMeTheCodeService : IShowMeTheCodeService
  {
    private readonly Parameters _parameters;
    public ShowMeTheCodeService(IOptions<Parameters> parameters)
    {
      _parameters = parameters?.Value ?? throw new ArgumentNullException(nameof(parameters));
    }
    public ShowMeTheCodeViewModel RetornaUrl()
    {
      try
      {
        var url = new ShowMeTheCodeViewModel()
        {
          Url = _parameters.UrlCode.Url
        };
        return url;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
