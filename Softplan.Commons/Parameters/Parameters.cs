namespace Softplan.Commons
{
  public class Parameters
  {
    public Fee Fee { get; set; }
    public UrlCode UrlCode { get; set; }
  }
  public class Fee
  {
    public decimal Value { get; set; }
  }
  public class UrlCode
  {
    public string Url { get; set; }
  }
}