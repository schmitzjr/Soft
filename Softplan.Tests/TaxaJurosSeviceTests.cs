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
    private readonly IOptions<Parameters> _parameters = Options.Create<Parameters>(new Parameters());
    public TaxaJurosSeviceTests()
    {
      _sut = new TaxaJurosService(_parameters);
    }
    [Fact]
    public void TaxaJuros_ShouldReturnFee()
    {
      //Arrange and Act
      _parameters.Value.Fee = new Fee
      {
        Value = 0.01M
      };
      var taxaJuros = _sut.RetornaJuros();
      //Assert
      Assert.NotNull(taxaJuros);
      Assert.Equal(0.01M, taxaJuros.TaxaJuros);
    }
  }
}