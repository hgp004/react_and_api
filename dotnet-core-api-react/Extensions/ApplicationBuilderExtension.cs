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
            string apiPath = path.Replace("/api/backend", "");
            FlurlClient client = new FlurlClient("https://localhost:5001/");
            IFlurlRequest fRequest = client.Request($"/api{apiPath}");
            //copy request header
            //Qustion: req.headers 和 req
            req.Headers.ToList().ForEach(h => fRequest.Headers.TryAdd(h.Key, h.Value));
            HttpResponseMessage result = null;
            switch (req.Method.ToLower())
            {
                case "post":
                    using (StreamReader sr = new StreamReader(req.Body))
                    {
                        string bodyContent = await sr.ReadToEndAsync();
                        StringContent stringContent = new StringContent(bodyContent);
                        result = await fRequest.PostAsync(stringContent);
                        //HttpRequestHeaders
                        //HttpResponseHeaders
                        //HttpRequestMessage
                        //System.Net.Http.content

                        //using (StreamContent sc = new StreamContent(req.Body))
                        //{
                        //    result = await fRequest.PostAsync(sc);
                        //    //set response headers

                        //    //set response body
                        //    //PipeWriter pw = PipeWriter.Create(await result.Content.ReadAsStreamAsync());
                        //    //pw.w
                        //    //set content Type
                        //    //res.ContentType
                        //    //result.RequestMessage
                        //}
                        Stream rs = await result.Content.ReadAsStreamAsync();

                        result.Content.Headers.ToList().ForEach(a => {
                            res.Headers.TryAdd(a.Key, new Microsoft.Extensions.Primitives.StringValues(a.Value.ToArray()));
                        });
                        byte[] buffer = new byte[rs.Length];
                        await rs.ReadAsync(buffer);
                        await res.Body.WriteAsync(buffer);                  
                        return;
                    }
                default:
                    break;
            }
            //await res.WriteAsync("OK");
            await next.Invoke(httpContext);
        }
    }
}
