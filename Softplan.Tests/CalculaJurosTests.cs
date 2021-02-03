using System.Linq;
using FluentValidation;
using NSubstitute;
using Softplan.DTO;
using Softplan.Services;
using Softplan.ViewModels;
using Xunit;

namespace Softplan.Tests
{
  public class CalculaJurosTests
  {
    private ICalculaJurosService _sut;
    private IClientConnectionService _clientConnectionService = Substitute.For<IClientConnectionService>();
    public CalculaJurosTests()
    {
      _sut = new CalculaJurosService(_clientConnectionService);
    }
    [Fact]
    public async void CalculaJuros__ShouldReturnValidationException_WhenMounthsIsNull()
    {
      //Arrange
      var parms = new CalculaJurosDTO
      {
        Meses = null,
        ValorInicial = 100
      };
      //Act and Assert
      var result = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CalculaJuros(parms));
      var details = result.Errors
        .Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        });
      var total = details.Count();
      Assert.Equal(1, total);
      Assert.Equal("NULL_MOUNTH", details.First().Code);
    }
    [Fact]
    public async void CalculaJuros__ShouldReturnValidationException_WhenInitialValueIsNull()
    {
      //Arrange
      var parms = new CalculaJurosDTO { Meses = 5, ValorInicial = null };
      //Act and Assert
      var result = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CalculaJuros(parms));
      var details = result.Errors
        .Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        });
      var total = details.Count();
      Assert.Equal(1, total);
      Assert.Equal("NULL_INITIAL_VALUE", details.First().Code);
    }
    [Fact]
    public async void CalculaJuros__ShouldReturnValidationException_WhenMounthsAndInitialValueAreNull()
    {
      //Arrange
      var parms = new CalculaJurosDTO
      {
        Meses = null,
        ValorInicial = null
      };
      //Act and Assert
      var result = await Assert.ThrowsAsync<ValidationException>(async () => await _sut.CalculaJuros(parms));
      var details = result.Errors
        .Select(s => new Detail()
        {
          Code = s.ErrorCode,
          Message = $"{s.PropertyName} {s.ErrorMessage}",
        });
      var total = details.Count();
      Assert.Equal(2, total);
      Assert.Equal("NULL_MOUNTH", details.First().Code);
      Assert.Equal("NULL_INITIAL_VALUE", details.Skip(1).First().Code);
    }
    [Fact]
    public async void CalculaJuros__ShouldReturnCorrectValue()
    {
      //Arrange
      var parms = new CalculaJurosDTO
      {
        Meses = 5,
        ValorInicial = 100
      };
      _clientConnectionService.GetTaxaJuros().Returns(new TaxaJurosViewModel() { TaxaJuros = 0.01M });
      //Act 
      var result = await _sut.CalculaJuros(parms);
      //Assert
      Assert.Equal("105,10", result.Valor);
    }
  }
}