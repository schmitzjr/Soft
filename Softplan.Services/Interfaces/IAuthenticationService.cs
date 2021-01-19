using System.Threading.Tasks;
using Softplan.DTO;
using Softplan.ViewModels;

namespace Softplan.Services
{
    public interface IAuthenticationService
    {
        Task<TokenViewModel> AuthenticateAsync(AuthenticateDTO authDTO);
    }
}