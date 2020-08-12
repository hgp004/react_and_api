using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace dotnet_core_api_react.Extensions
{
    public class ChallengeSchemeHanlderOptions: AuthenticationSchemeOptions
    { 
    }
    public class ChallengeSchemeHanlder : AuthenticationHandler<ChallengeSchemeHanlderOptions>,IAuthenticationHandler
    {

        private AuthenticationScheme _scheme { get; set; }
        private HttpContext _context { get; set; }
        private ChallengeSchemeHanlderOptions _options { get; set; }

        public ChallengeSchemeHanlder(IOptionsMonitor<ChallengeSchemeHanlderOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock): base(options, logger, encoder, clock)
        {
            this._options = options.CurrentValue;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Context.Response.StatusCode = 401;
            return base.HandleChallengeAsync(properties);
        }
        //public Task<AuthenticateResult> AuthenticateAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task ChallengeAsync(AuthenticationProperties properties)
        //{
        //    this._context.Response.StatusCode = 401;
        //    return Task.CompletedTask;
        //}

        //public Task ForbidAsync(AuthenticationProperties properties)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        //{
        //    this._scheme = scheme;
        //    this._context = context;
        //    return Task.CompletedTask;
        //}
    }
}
