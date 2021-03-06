namespace Softplan.Commons
{
  public class AuthSettings
  {
    public AuthJwtToken AuthJwt { get; set; }
    public string PowerUser { get; set; }
    public string PowerPassword { get; set; }
    public ClientsConnections ClientsConnections { get; set; }
    
  }
  public class AuthJwtToken
  {
    public string SymmetricSecurityKey { get; set; }
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public int TokenExpires { get; set; }
  }
  public class ClientsConnections
  {
    public string UrlApi1 { get; set; }
    public string UrlApi2 { get; set; }
    public string UrlApi1Docker { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public bool RunInDocker { get; set; }
  }
  
}