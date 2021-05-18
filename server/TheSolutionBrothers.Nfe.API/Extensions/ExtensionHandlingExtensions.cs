using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using TheSolutionBrothers.Nfe.API.Exceptions;

namespace TheSolutionBrothers.Nfe.API.Extensions
{
    public static class ExtensionHandlingExtensions
    {

        public static HttpResponseMessage HandleExecutedContextException(this HttpActionExecutedContext context)
        {
            return context.Request.CreateResponse(HttpStatusCode.InternalServerError, ExceptionPayload.New(context.Exception));
        }

    }
}