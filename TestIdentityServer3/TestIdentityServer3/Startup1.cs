using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Web.Helpers;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using Owin;
using TestIdentityServer3.Models.OpenId;

[assembly: OwinStartup(typeof(TestIdentityServer3.Startup1))]

namespace TestIdentityServer3
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub"; // Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.Map("/core", coreApp =>
            {
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

                factory.UserService = new Registration<IUserService, CustomLoginPageUserService>();
                
                var options = new IdentityServerOptions
                {
                    SiteName = "IdentityServer",

                    SigningCertificate = LoadCertificate(),
                    Factory = factory,
                    EventsOptions = new EventsOptions
                    {
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true,
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true
                    }
                };

                coreApp.UseIdentityServer(options);
            });

            X509Certificate2 LoadCertificate()
            {
                return new X509Certificate2(
                    string.Format(@"{0}\bin\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
            }
        }
    }
}
