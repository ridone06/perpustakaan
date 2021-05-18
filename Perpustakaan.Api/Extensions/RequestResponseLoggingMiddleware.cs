using Microsoft.AspNetCore.Http;
using Perpustakaan.Api.Helpers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Extensions
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Read and log request body data
            string requestBodyPayload = await ReadRequestBody(context.Request);
            LogHelper.RequestPayload = requestBodyPayload;

            // Read and log response body data
            // Copy a pointer to the original response body stream
            var originalResponseBodyStream = context.Response.Body;

            // Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);

            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            string requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"{requestBody}";
        }
    }
}
