using System;
using Newtonsoft.Json;
using TheSolutionBrothers.Nfe.API.Controllers.Common;
using TheSolutionBrothers.Nfe.Infra.Logger;
using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.Nfe.API.Exceptions
{
    public class ExceptionPayload
    {
        [JsonIgnore]
        public Exception Exception { get; set; }
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public static ExceptionPayload New<T>(T exception) where T : Exception
        {
            int errorCode;
            if (exception is BusinessException)
                errorCode = (exception as BusinessException).ErrorCode.GetHashCode();
            else if (exception is InvalidRequestException)
                errorCode = (exception as InvalidRequestException).ErrorCode.GetHashCode();
            else
                errorCode = ErrorCodes.Unauthorized.GetHashCode();

            TraceLogManager.Error(string.Format("CODE: {0} {1} - STACK: {2}", errorCode, exception.Message, exception.StackTrace));

            return new ExceptionPayload
            {
                ErrorCode = errorCode,
                ErrorMessage = exception.Message,
                Exception = exception,
            };
        }

    }
}