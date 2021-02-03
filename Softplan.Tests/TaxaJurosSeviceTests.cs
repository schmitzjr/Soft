using Microsoft.Extensions.Options;
using NSubstitute;
using Softplan.Commons;
using Softplan.Services;
using Xunit;

namespace Softplan.Tests
{
  public class TaxaJurosSeviceTests
  {
    private ITaxaJurosService _sut;
    private readonly IOptions<AuthSettings> _authSettings = Options.Create<AuthSettings>(new AuthSettings());
    public TaxaJurosSeviceTests()
    {
      _sut = new TaxaJurosService(_authSettings);
    }
    [Fact]
    public void TaxaJuros_ShouldReturnFee()
    {
      //Arrange and Act
      _authSettings.Value.fee = new Fee{
        Value = 0.01M
      };
      var taxaJuros = _sut.RetornaJuros();
      //Assert
      Assert.NotNull(taxaJuros);
      Assert.Equal(0.01M, taxaJuros.TaxaJuros);
    }
  }
}