using System.Web.Http.Filters;
using TheSolutionBrothers.Nfe.API.Extensions;

namespace TheSolutionBrothers.Nfe.API.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = context.HandleExecutedContextException();
        }

    }
}