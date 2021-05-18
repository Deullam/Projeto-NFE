using System;
using TheSolutionBrothers.Nfe.API.Exceptions;

namespace TheSolutionBrothers.Nfe.API.Logger
{
    public class LogMetadata
    {
        public string RequestUri { get; set; }
        public string Erros { get; set; }

        public string RequestMethod { get; set; }

        public DateTime RequestTimestamp { get; set; }

        public int? ResponseStatusCode { get; set; }

        public DateTime ResponseTimestamp { get; set; }

        public ExceptionPayload ResponseExceptionPayLoad { get; set; }
    }
}