using Softplan.ViewModels;

namespace Softplan.Services
{
    public interface ITokenService
    {
        TokenViewModel GenerateTokenAuthentication();
    }
}