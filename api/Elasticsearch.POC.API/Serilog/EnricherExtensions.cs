using Microsoft.AspNetCore.Http.Features;
using Serilog;

namespace Elasticsearch.POC.API.Serilog;

public static class EnricherExtensions
    {
        public static string RequestPayload = "";
        public static async void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
            
            diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name);
            diagnosticContext.Set("ClientIP", httpContext?.Connection?.RemoteIpAddress?.ToString());
            diagnosticContext.Set("UserAgent", request?.Headers?["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("Resource", httpContext?.GetMetricsCurrentResourceName());
            diagnosticContext.Set("RequestBody", RequestPayload);

            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);

            string responseBodyPayload;
            try
            {
                responseBodyPayload = await ReadResponseBody(httpContext.Response);
            }
            catch (Exception)
            {
                responseBodyPayload = string.Empty;
            }


            diagnosticContext.Set("ResponseBody", responseBodyPayload);

           

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }

        public static string? GetMetricsCurrentResourceName(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var endpoint = httpContext?.Features?.Get<IEndpointFeature>()?.Endpoint;

            return endpoint?.Metadata?.GetMetadata<EndpointNameMetadata>()?.EndpointName;
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{responseBody}";
        }
    }