using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheSolutionBrothers.Nfe.API.Exceptions;
using TheSolutionBrothers.Nfe.Infra.Logger;

namespace TheSolutionBrothers.Nfe.API.Logger
{
    public class CustomLogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logMetadata = BuildLogMetadata(request);

            WriteStartLog(logMetadata);

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    logMetadata.ResponseStatusCode = (int)response.StatusCode;
                    logMetadata.ResponseTimestamp = DateTime.Now;

                    if (response.Content != null && response.Content is ObjectContent<ExceptionPayload>)
                    {
                        logMetadata.ResponseExceptionPayLoad = (response.Content as ObjectContent<ExceptionPayload>).Value as ExceptionPayload;
                    }
                    if (response.Content != null && response.Content is ObjectContent<IList<ValidationFailure>>)
                    {
                        var erros = (response.Content as ObjectContent<IList<ValidationFailure>>).Value as IList<ValidationFailure>;
                        StringBuilder builder = new StringBuilder();
                        erros.ToList().ForEach(e => builder.AppendLine(e.ErrorMessage));
                        logMetadata.Erros = builder.ToString();
                    }

                    WriteEndLog(logMetadata);

                    return response;
                }, cancellationToken);
        }


        private LogMetadata BuildLogMetadata(HttpRequestMessage request)
        {
            return new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestUri = request.RequestUri.ToString(),
                RequestTimestamp = DateTime.Now
            };
        }

        private void WriteStartLog(LogMetadata logMetadata)
        {
            var message = string.Format("[{0}] - Início: {1}", logMetadata.RequestMethod, logMetadata.RequestUri);

            TraceLogManager.Debug(message);
        }

        private void WriteEndLog(LogMetadata logMetadata)
        {
            if (logMetadata.ResponseExceptionPayLoad != null)
            {
                TraceLogManager.Error("[{0}] - Exception - Status: {1} - Message: {2}\r\nStackTrace: {3}", logMetadata.RequestMethod, logMetadata.ResponseExceptionPayLoad.ErrorCode,
                    logMetadata.ResponseExceptionPayLoad.ErrorMessage, logMetadata.ResponseExceptionPayLoad.Exception.StackTrace);
            }
            if (!String.IsNullOrEmpty(logMetadata.Erros))
            {
                TraceLogManager.Error("[{0}] - Exception - Erros: {1}", logMetadata.RequestMethod,
                    logMetadata.Erros);
            }

            var executionTime = logMetadata.ResponseTimestamp.Subtract(logMetadata.RequestTimestamp);

            var message = string.Format("[{0}] - Fim: {1} [Tempo de Execução: {2}] [Status: {3}]", logMetadata.RequestMethod, logMetadata.RequestUri, executionTime, logMetadata.ResponseStatusCode);

            TraceLogManager.Debug(message);
        }
    }
}