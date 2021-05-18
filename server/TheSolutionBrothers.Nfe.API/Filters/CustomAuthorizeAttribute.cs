using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Http.Controllers;
using TheSolutionBrothers.Nfe.Infra.Logger;

namespace TheSolutionBrothers.Nfe.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool response = base.IsAuthorized(actionContext);
            if (response)
            {
                TraceLogManager.Info(string.Format("Usuário autenticado {0} {1}", actionContext.Request.Method, actionContext.Request.RequestUri));
            }
            else
            {
                TraceLogManager.Info(string.Format("Usuário não autenticado {0} {1}", actionContext.Request.Method, actionContext.Request.RequestUri));

            }
            return response;
        }

    }
}