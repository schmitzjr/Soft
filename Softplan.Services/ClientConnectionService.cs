using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Softplan.Commons;
using Softplan.DTO;
using Softplan.ViewModels;

namespace Softplan.Services
{
  public class ClientConnectionService : IClientConnectionService
  {
    private readonly AuthSettings _authSettings;
    public ClientConnectionService(IOptions<AuthSettings> authSettings)
    {
      _authSettings = authSettings?.Value ?? throw new ArgumentNullException(nameof(authSettings));
    }

    public async Task<TaxaJurosViewModel> GetTaxaJuros()
    {
      var token = await GetToken(new AuthenticateDTO
      {
        User = _authSettings.ClientsConnections.User,
        Password = _authSettings.ClientsConnections.Password
      });

      var taxa = new TaxaJurosViewModel();

      using (var httpClient = new HttpClient())
      {
        var url = _authSettings.ClientsConnections.RunInDocker ? _authSettings.ClientsConnections.UrlApi1Docker : _authSettings.ClientsConnections.UrlApi1; 
        using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{url}/api/TaxaJuros"))
        {
          request.Headers.TryAddWithoutValidation("accept", "application/json");
          request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token.Token}");

          var response = await httpClient.SendAsync(request);

          if (response.IsSuccessStatusCode)
          {
            taxa = JsonConvert.DeserializeObject<TaxaJurosViewModel>(response.Content.ReadAsStringAsync().Result);
          }
        }
      }

      return taxa;
    }
    private async Task<TokenViewModel> GetToken(AuthenticateDTO authenticateDTO)
    {
      var token = new TokenViewModel();

      using (var httpClient = new HttpClient())
      {
        var url = _authSettings.ClientsConnections.RunInDocker ? _authSettings.ClientsConnections.UrlApi1Docker : _authSettings.ClientsConnections.UrlApi1;
        using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{url}/api/Authenticate"))
        {
          request.Headers.TryAddWithoutValidation("accept", "application/json");

          request.Content = new StringContent(JsonConvert.SerializeObject(authenticateDTO));
          request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

          var response = await httpClient.SendAsync(request);

          if (response.IsSuccessStatusCode)
          {
            token = JsonConvert.DeserializeObject<TokenViewModel>(response.Content.ReadAsStringAsync().Result);
          }
        }
      }

      return token;
    }

  }
}