using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreWiki.Helpers
{
    public class ArticleNotFoundInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            var request = telemetry as RequestTelemetry;

            if (request != null && request.ResponseCode.Equals(((int)HttpStatusCode.NotFound).ToString(), StringComparison.OrdinalIgnoreCase))
            {
                telemetry.Context.Properties["Missing Document"] = request.Url.Segments.LastOrDefault();
            }
        }
    }
}
