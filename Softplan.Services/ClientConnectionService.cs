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

      using (var httpClientHandler = new HttpClientHandler())
      {
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
        using (var client = new HttpClient(httpClientHandler))
        {
          client.BaseAddress = new Uri(_authSettings.ClientsConnections.UrlApi1);
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

          HttpResponseMessage response = await client.GetAsync("api/TaxaJuros");
          var result = response.Content.ReadAsStringAsync();

          if (response.IsSuccessStatusCode)
          {
            taxa = JsonConvert.DeserializeObject<TaxaJurosViewModel>(result.Result);
          }
        }
      }

      return taxa;
    }
    private async Task<TokenViewModel> GetToken(AuthenticateDTO authenticateDTO)
    {
      var token = new TokenViewModel();

      using (var httpClientHandler = new HttpClientHandler())
      {
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
        using (var client = new HttpClient(httpClientHandler))
        {
          client.BaseAddress = new Uri(_authSettings.ClientsConnections.UrlApi1);
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          var jsonContent = JsonConvert.SerializeObject(authenticateDTO);
          var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
          contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

          HttpResponseMessage response = await client.PostAsync("api/Authenticate", contentString);
          var result = response.Content.ReadAsStringAsync();

          if (response.IsSuccessStatusCode)
          {
            token = JsonConvert.DeserializeObject<TokenViewModel>(result.Result);
          }
        }
      }

      return token;
    }
  }
}