using System.Threading.Tasks;
using Softplan.DTO;
using Softplan.ViewModels;

namespace Softplan.Services
{
    public interface ICalculaJurosService
    {
        Task<CalculaJurosViewModel> CalculaJuros(CalculaJurosDTO calculaJurosDTO);
    }
}