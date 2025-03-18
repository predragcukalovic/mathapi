using Serilog.Context;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Papi.GameServer.Math.Api.DelegatingHandlers
{
    public class TraceIdHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var traceId = Guid.NewGuid().ToString();

            using (LogContext.PushProperty("TraceId", traceId))
            {

                request.Headers.Add("X-TraceId", traceId);
                var response = await base.SendAsync(request, cancellationToken);
                response.Headers.Add("X-TraceId", traceId);

                return response;
            }
        }
    }

}