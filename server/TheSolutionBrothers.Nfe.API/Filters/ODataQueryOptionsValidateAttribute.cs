
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TheSolutionBrothers.Nfe.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class ODataQueryOptionsValidateAttribute : ActionFilterAttribute
    {
        private ODataValidationSettings oDataValidationSettings;

        /// <summary>
        ///  No construtor, cria uma nova configuração de validação para odata
        /// </summary>
        public ODataQueryOptionsValidateAttribute(AllowedQueryOptions allowedQueryOptions = AllowedQueryOptions.All ^ AllowedQueryOptions.Expand)
        {
            oDataValidationSettings = new ODataValidationSettings() { AllowedQueryOptions = allowedQueryOptions };
        }

        /// <summary>
        /// Ao receber uma chamada http no controller, é invocado esse método para validar as opções do odata
        /// </summary>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Any(a => a.Value != null && a.Value.GetType().Name.Contains(typeof(ODataQueryOptions).Name)))
            {
                var odataQueryOptions = (ODataQueryOptions)actionContext.ActionArguments.Where(a => a.Value != null && a.Value.GetType().Name.Contains(typeof(ODataQueryOptions).Name)).FirstOrDefault().Value;
                odataQueryOptions.Validate(oDataValidationSettings);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}