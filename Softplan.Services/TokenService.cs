using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Softplan.Commons.Settings;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class TokenService : ITokenService
  {
    private readonly AuthSettings _authSettings;
    public TokenService(IOptions<AuthSettings> authSettings)
    {
      _authSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));
    }
    public TokenViewModel GenerateTokenAuthentication()
    {
      var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.AuthJwt.SymmetricSecurityKey));

      var authClaims = new[]
      {
        new Claim("ClaimId", Guid.NewGuid().ToString()),
        new Claim("UserName", _authSettings.PowerUser),
        new Claim("Authenticated", "true")
      };

      var jwtToken = new JwtSecurityToken(
        issuer: _authSettings.AuthJwt.ValidIssuer,
        audience: _authSettings.AuthJwt.ValidAudience,
        expires: DateTime.Now.AddHours(_authSettings.AuthJwt.TokenExpires),
        claims: authClaims,
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
      );

      return new TokenViewModel()
      {
        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
        Expiration = jwtToken.ValidTo,
        Authenticated = true
      };
    }
  }
}