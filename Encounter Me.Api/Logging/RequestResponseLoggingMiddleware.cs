using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Logging
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Read and log request body data - contains sensitive information
            // string requestBodyPayload = await ReadRequestBody(httpContext.Request);
            // LogHelper.RequestPayload = requestBodyPayload;

            // Read and log response body data
            // Copy a pointer to the original response body stream
            var originalResponseBodyStream = httpContext.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                // Create a new memorystream -  use that for the temporary response body
                httpContext.Response.Body = responseBody;
                try
                { 
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    throw;
                } 

                // Copy new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalResponseBodyStream);
            }

        }

        //private async Task<string> ReadRequestBody(HttpRequest request)
        //{
        //    HttpRequestRewindExtensions.EnableBuffering(request);

        //    var body = request.Body;
        //    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        //    await request.Body.ReadAsync(buffer, 0, buffer.Length);
        //    string requestBody = Encoding.UTF8.GetString(buffer);
        //    body.Seek(0, SeekOrigin.Begin);
        //    request.Body = body;

        //    return $"{requestBody}";
        //}
    }
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
