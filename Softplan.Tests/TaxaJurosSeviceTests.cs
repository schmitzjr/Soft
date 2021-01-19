using Softplan.Services;
using Xunit;

namespace Softplan.Tests
{
  public class TaxaJurosSeviceTests
  {
    private ITaxaJurosService _sut;
    public TaxaJurosSeviceTests()
    {
      _sut = new TaxaJurosService();
    }
    [Fact]
    public void TaxaJuros_ShouldReturnFee()
    {
      //Arrange and Act
      var taxaJuros = _sut.RetornaJuros();
      //Assert
      Assert.NotNull(taxaJuros);
      Assert.Equal(0.01M, taxaJuros.TaxaJuros);
    }
  }

}