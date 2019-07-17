using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIdentityServer3.Models.OpenId
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
            new Client
            {
                Enabled = true,
                ClientName = "MVC Client",
                ClientId = "mvc",
                ClientSecrets= new List<Secret>(){
                    new Secret("bla".Sha256())
                },
                RequireConsent = false,
                Flow = Flows.AuthorizationCode,

                RedirectUris = new List<string>
                {
                    "https://localhost:44319/",
                    "http://localhost:8080/callback"
                },

                AllowAccessToAllScopes = true,
                Claims = new List<System.Security.Claims.Claim>()
                {
                    new System.Security.Claims.Claim("brand", "brief")
                }
            }
        };
        }
    }
}