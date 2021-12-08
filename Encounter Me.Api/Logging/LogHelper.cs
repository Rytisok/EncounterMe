using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System.IO;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Logging
{
    public static class LogHelper
    {
        public static string RequestPayload = "";

        public static async void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            var endpoint = httpContext.GetEndpoint();
            if (endpoint is not null)
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }

            //diagnosticContext.Set("RequestBody", RequestPayload); Contains sensitive information...

            string responseBodyPayload = await ReadResponseBody(httpContext.Response);

            // Restricted, as for example - GET all users would have all users data as a payload which would clutter the logs
            if (responseBodyPayload.Length < 200)
            {
                diagnosticContext.Set("ResponseBody", responseBodyPayload);
            }

            // Set all the common properties available for every request
            // Then you can choose which to show in logs in appsetings.Development "outputTemplate"
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{responseBody}";
        }
    }
}
