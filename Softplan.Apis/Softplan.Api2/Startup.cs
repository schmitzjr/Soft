using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Softplan.Commons;
using Softplan.Services;

namespace Softplan.WebApi2
{
  public class Startup
  {
    public IConfiguration _configuration { get; private set; }
    private readonly string allowOrigins = "SoftplanAllowOrigins";
    public Startup(IConfiguration configuration)
    {
      _configuration = new ConfigurationBuilder().Build();
      _configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions();
      services.Configure<AuthSettings>(_configuration.GetSection("AuthSettings"));
      services.Configure<AuthSettings>(_configuration.GetSection("AuthSettings").GetSection("SystemPowerUser"));
      services.Configure<AuthSettings>(_configuration.GetSection("AuthSettings").GetSection("ClientsConnections"));
      services.Configure<Parameters>(_configuration.GetSection("Parameters"));
      services.Configure<Parameters>(_configuration.GetSection("Parameters").GetSection("UrlCode"));
      services.AddMvc();
      ConfigureContainer(services);
      services.AddControllers();
      services.AddLogging(builder =>
          builder
          .AddDebug()
      );
      services.Configure<RequestLocalizationOptions>(options =>
      {
        var supportedCultures = new[]
        {
            new CultureInfo("pt-BR"),
            new CultureInfo("en-US"),
        };
        options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
      });
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = _configuration.GetSection("AuthSettings").GetSection("AuthJwt").GetSection("ValidAudience").Value,
          ValidIssuer = _configuration.GetSection("AuthSettings").GetSection("AuthJwt").GetSection("ValidIssuer").Value,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AuthSettings").GetSection("AuthJwt").GetSection("SymmetricSecurityKey").Value))
        };
      });
      services.AddCors(props =>
      {
        props.AddPolicy(allowOrigins, options => options
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
      });
      ConfigureSwagger(services);
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseCors(allowOrigins);

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      ConfigureSwaggerApp(app);
    }
    public void ConfigureContainer(IServiceCollection services)
    {
      services.AddTransient<IAuthenticationService, AuthenticationService>();
      services.AddTransient<ICalculaJurosService, CalculaJurosService>();
      services.AddTransient<ITokenService, TokenService>();
      services.AddTransient<IValidationErrorService, ValidationErrorService>();
      services.AddTransient<IClientConnectionService, ClientConnectionService>();
      services.AddTransient<IShowMeTheCodeService, ShowMeTheCodeService>();
    }
    private static void ConfigureSwagger(IServiceCollection services)
    {
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("softplan", new OpenApiInfo
        {
          Title = "Softplan Api2",
          Version = "1.0",
          Description = "Softplan Api2",
          Contact = new OpenApiContact
          {
            Name = "Ivan Schmitz",
            Email = "schmitzjr@gmail.com"
          }
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Please insert JWT with Bearer into field",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
          });
      });
    }
    private static void ConfigureSwaggerApp(IApplicationBuilder app)
    {
      app.UseSwagger();

      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/softplan/swagger.json", "Softplan Api2");
        options.RoutePrefix = string.Empty;
      });
    }
  }
}
