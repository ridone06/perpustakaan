using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Linq;

namespace Perpustakaan.Api.Helpers
{
    public class LogHelper
    {
        public static string RequestPayload = "";

        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            try
            {
                if (httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
                {
                    diagnosticContext.Set("UserClaims", httpContext.User.Claims.ToDictionary(v => v.Type, v => v.Value.ToString()));
                }
            }
            catch (Exception) { }

            diagnosticContext.Set("RequestBody", RequestPayload);

            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);
            diagnosticContext.Set("IpAddress", httpContext.Connection.RemoteIpAddress.ToString());

            diagnosticContext.Set("RequestHeaders", httpContext.Request.Headers.ToDictionary(v => v.Key, v => v.Value.ToString()));

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            // Only set it if available. You're not sending sensitive data in a form request
            if (request.HasFormContentType)
            {
                diagnosticContext.Set("RequestForm", httpContext.Request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) 
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }

    }
}
