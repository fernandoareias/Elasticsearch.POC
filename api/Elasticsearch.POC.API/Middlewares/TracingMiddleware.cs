using Elastic.Apm.Api;

namespace Elasticsearch.POC.API.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class CustomMiddleware
{
    public CustomMiddleware(RequestDelegate next, ITracer tracer)
    {
        _next = next;
        _tracer = tracer;
    }

    private readonly RequestDelegate _next;
    private readonly ITracer _tracer;
     

    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Request.Headers;
        
        var traceId = _tracer.CurrentTransaction.TraceId;
        var id = _tracer.CurrentTransaction.Id;
      
        
        foreach (var header in headers)
        {
            Console.WriteLine($"{header.Key}: {header.Value}");
        }

        await _next(context);
    }
}
