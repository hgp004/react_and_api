using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Util;
using Flurl;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.IO.Pipes;
using System.IO.Pipelines;

namespace dotnet_core_api_react.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseApiForward(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ApiForwardMiddleware>();
        }
    }
    public class ApiForwardMiddleware
    {
        private RequestDelegate next;
        public ApiForwardMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider)
        {
            var req = httpContext.Request;
            var res = httpContext.Response;
            string path = req.Path;
            if (!Regex.IsMatch(path, "^/api/backend"))
            {
                await next.Invoke(httpContext);
                return;
            }
            try
            {

                string apiPath = path.Replace("/api/backend", "");
                FlurlClient client = new FlurlClient("https://localhost:5001/");
                client.AllowHttpStatus("2xx,4xx,5xx");
                IFlurlRequest fRequest = client.Request($"/api{apiPath}");
                //copy request header
                //Qustion: req.headers 和 req
                /*
                 * 1. Headers of HttpRequest, HttpResponse is KeyValues
                 * 2. Headers of HttpResponseMessage is HttpResponseHeaders : HttpHeaders : IEnumerable<keyValues>
                 * 3. Headers of HttpRequestMessage is HttpRequestHeaders: HttpHeaders : IEnumerable<keyValues>
                 * 4. Headers of HttpContent is HttpContentHeaders : HttpHeaders : IEnumerable<keyValues>
                 * 5. what are differeces of content of 2 and 4?
                 */
                req.Headers.ToList().ForEach(h => fRequest.Headers.TryAdd(h.Key, h.Value));
                req.Query.ToList().ForEach(q => fRequest = fRequest.SetQueryParam(q.Key, q.Value));
                HttpResponseMessage result = null;
                switch (req.Method.ToLower())
                {
                    case "post":
                        using (StreamContent sc = new StreamContent(req.Body))
                        {
                            result = await fRequest.PostAsync(sc);
                        }
                        break;
                    case "get":
                        result = await fRequest.GetAsync();
                        break;
                    case "put":
                        using (StreamContent sc = new StreamContent(req.Body))
                        {
                            result = await fRequest.PutAsync(sc);
                        }
                        break;
                    case "delete":
                        result = await fRequest.DeleteAsync();
                        break;
                    default:
                        await next.Invoke(httpContext);
                        return;
                }
                Stream rs = await result.Content.ReadAsStreamAsync();
                result.Content.Headers.ToList().ForEach(a =>
                {
                    res.Headers.TryAdd(a.Key, new Microsoft.Extensions.Primitives.StringValues(a.Value.ToArray()));
                });

                byte[] buffer = new byte[rs.Length];
                await rs.ReadAsync(buffer);
                res.StatusCode = (int)result.StatusCode;
                await res.Body.WriteAsync(buffer);
            }
            catch (FlurlHttpException ex)
            {
                var responseMessage = ex.Call.Response;
                res.StatusCode = (int)responseMessage.StatusCode;
                await res.WriteAsync(ex.Message);
                Console.WriteLine($"api forward middelware reqeust API error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"api forward middelware error: {ex.Message}");
                res.StatusCode = 500;
                await res.WriteAsync(ex.Message);
            }
        }
    }
}
