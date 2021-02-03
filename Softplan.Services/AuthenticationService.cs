using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Options;
using Softplan.Commons;
using Softplan.DTO;
using Softplan.Validations;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class AuthenticationService : IAuthenticationService
  {
    private readonly AuthSettings _authSettings;
    private readonly ITokenService _tokenService;
    public AuthenticationService(IOptions<AuthSettings> authSettings, ITokenService tokenService)
    {
      _authSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));
      _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }
    public async Task<TokenViewModel> AuthenticateAsync(AuthenticateDTO authDTO)
    {
      var authValidator = new AuthenticateValidator(_authSettings);

      await authValidator.ValidateAndThrowAsync(authDTO);

      return _tokenService.GenerateTokenAuthentication();
    }
    
  }
}