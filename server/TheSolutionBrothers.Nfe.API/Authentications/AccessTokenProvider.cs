using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using SimpleInjector.Lifestyles;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TheSolutionBrothers.Nfe.Infra.Logger;
using TheSolutionBrothers.NFe.IoC.Features.Containers;
using TheSolutionBrothers.NFe.Application.Features.Users;
using TheSolutionBrothers.NFe.Domain.Features.Users;

namespace TheSolutionBrothers.Nfe.API.Authentications
{
    public class AccessTokenProvider : OAuthAuthorizationServerProvider
    {
        private IUserService _userService;

        public AccessTokenProvider(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = default(User);

            try
            {
                using (AsyncScopedLifestyle.BeginScope(SimpleInjectorContainer.Instance))
                {
                    var userService = SimpleInjectorContainer.Instance.GetInstance<IUserService>();
                    user = userService.Login(context.UserName, context.Password);
                    TraceLogManager.Info(string.Format("Token gerado para usuário: [{0}]", context.UserName));

                }
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
                TraceLogManager.Error(string.Format("Erro ao emitir token. Erro: {0}", ex.Message));
                return;
            }

            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));

            var ticket = new AuthenticationTicket(identity, null);

            context.Validated(ticket);
        }
    }

}