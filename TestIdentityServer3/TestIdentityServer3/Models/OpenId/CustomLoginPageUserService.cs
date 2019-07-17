using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TestIdentityServer3.Models.OpenId
{
    public class CustomLoginPageUserService : IdentityServer3.Core.Services.Default.UserServiceBase
    {
        OwinContext ctx;

        public CustomLoginPageUserService(OwinEnvironmentService owinEnv)
        {
            ctx = new OwinContext(owinEnv.Environment);
        }

        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            var id = ctx.Request.Query.Get("signin");

            var redirectPath = string.Format("~/custom/login?id={0}", id);
            context.AuthenticateResult = new AuthenticateResult(redirectPath, new List<Claim>());

            return Task.FromResult(0);
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var identity = context.Subject.Identity as ClaimsIdentity;
            context.IssuedClaims = identity.Claims;
            return Task.FromResult(identity.Claims);
        }

        public override Task SignOutAsync(SignOutContext context)
        {
            return base.SignOutAsync(context);
        }

    }
}