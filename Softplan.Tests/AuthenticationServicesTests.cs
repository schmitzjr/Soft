using System;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Options;
using NSubstitute;
using Softplan.Commons;
using Softplan.DTO;
using Softplan.Services;
using Softplan.ViewModels;
using Xunit;

namespace Softplan.Tests
{
  public class AuthenticationServicesTests
  {
    private IAuthenticationService _sut;
    private readonly IOptions<AuthSettings> _authSettings = Options.Create<AuthSettings>(new AuthSettings());
    private readonly ITokenService _tokenService = Substitute.For<ITokenService>();
    public AuthenticationServicesTests()
    {
      _authSettings.Value.PowerUser = "admin";
      _authSettings.Value.PowerPassword = PasswordHasher.HashPassword("admin");
      _sut = new AuthenticationService(_authSettings, _tokenService);
    }
    [Fact]
    public async void Authenticate_ShouldReturnAuthenticatedToken()
    {
      //Arrange
      var auth = new AuthenticateDTO() { User = "admin", Password = "admin" };
      _tokenService.GenerateTokenAuthentication()
                    .Returns(new TokenViewModel
                    {
                      Token = "Valid.Bearer.Token",
                      Authenticated = true,
                      Expiration = DateTime.Now.AddHours(3)
                    });
      // Act
      var token = await _sut.AuthenticateAsync(auth);
      //Assert
      Assert.NotNull(token);
      Assert.True(token.Authenticated);
    }
    [Fact]
    public async void Authenticate_ShouldReturnValidationException_WhenUserIsEmpty()
    {
      // Arrange
      var auth = new AuthenticateDTO()
      {
        User = string.Empty,
        Password = "admin"
      };
      // Act and Assert
      var result = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.AuthenticateAsync(auth));
      var details = result.Errors
        .Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        });
      var total = details.Count();
      Assert.Equal(1, total);
      Assert.Equal("EMPTY_USER", details.First().Code);
    }
    [Fact]
    public async void Authenticate_ShouldReturnValidationException_WhenPasswordIsEmpty()
    {
      // Arrange
      var auth = new AuthenticateDTO()
      {
        User = "admin",
        Password = string.Empty
      };
      // Act and Assert
      var result = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.AuthenticateAsync(auth));
      var details = result.Errors
        .Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        });
      var total = details.Count();
      Assert.Equal(1, total);
      Assert.Equal("EMPTY_PASSWORD", details.First().Code);
    }
    [Fact]
    public async void Authenticate_ShouldReturnAuthenticationException_WhenCredentialsAreInvalids()
    {
      //Arrange
      var auth = new AuthenticateDTO()
      {
        User = "invalid_user",
        Password = "invalid_password"
      };
      // Act and Assert
      var result = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.AuthenticateAsync(auth));
      var details = result.Errors
        .Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        });
      var total = details.Count();
      Assert.Equal(1, total);
      Assert.Equal("INVALID_CREDENCIALS", details.First().Code);
    }
  }
}
