using System;
using SimpleInjector.Integration.WebApi;
using SimpleInjector;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using TheSolutionBrothers.NFe.Application.Mappers;
using TheSolutionBrothers.NFe.IoC.Features.Containers;
using TheSolutionBrothers.Nfe.API.Extensions;
using TheSolutionBrothers.Nfe.API.Filters;
using TheSolutionBrothers.Nfe.API.Logger;
using TheSolutionBrothers.Nfe.API.Authentications;
using TheSolutionBrothers.NFe.Application.Features.Users;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using TheSolutionBrothers.NFe.Infra.Data.Features.Users;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(TheSolutionBrothers.Nfe.API.Startup))]
namespace TheSolutionBrothers.Nfe.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings();
            Container container = SimpleInjectorContainer.RegisterServices();

            HttpConfiguration config = new HttpConfiguration()
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            config.MapApiRoutes();
            config.EnableOdata();

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.ConfigureJsonSerialization();
            config.ConfigureXMLSerialization();
            config.Filters.Add(new ExceptionHandlerAttribute());

            ActivateAccessToken(app);

            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
            log4net.ThreadContext.Properties["customer"] = "My Customer Name";

            log4net.Config.XmlConfigurator.Configure();
            config.MessageHandlers.Add(new CustomLogHandler());

        }

        private void ActivateAccessToken(IAppBuilder app)
        {
            ContextNfe _contexto = new ContextNfe();
            UserRepository _userRepository = new UserRepository(_contexto);
            UserService _userService = new UserService(_userRepository);

            var tokenConfigurationOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new AccessTokenProvider(_userService)
            };

            app.UseOAuthAuthorizationServer(tokenConfigurationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}