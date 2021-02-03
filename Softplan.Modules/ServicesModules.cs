using Autofac;
using Softplan.Services;

namespace Softplan.Modules
{
  public class ServicesModules : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
      builder.RegisterType<CalculaJurosService>().As<ICalculaJurosService>();
      builder.RegisterType<TaxaJurosService>().As<ITaxaJurosService>();
      builder.RegisterType<TokenService>().As<ITokenService>();
      builder.RegisterType<ValidationErrorService>().As<IValidationErrorService>();
      builder.RegisterType<ClientConnectionService>().As<IClientConnectionService>();
      builder.RegisterType<ShowMeTheCodeService>().As<IShowMeTheCodeService>();
    }
  }
}
