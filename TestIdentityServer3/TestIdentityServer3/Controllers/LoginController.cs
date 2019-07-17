using IdentityServer3.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TestIdentityServer3.Models;
using TestIdentityServer3.Models.OpenId;

namespace TestIdentityServer3.Controllers
{
    public class LoginController : Controller
    {
        [Route("core/custom/login")]
        public ActionResult Index(string id)
        {
            var env = Request.GetOwinContext().Environment;
            var signinmessage = env.GetSignInMessage(id);

            var client = Clients.Get().Single(r => r.ClientId == signinmessage.ClientId);
            var model = new LoginModel
            {
                Titel = client.ClientName,
                Name = "1234"
            };
            return View(model);
        }

        [Route("core/custom/login")]
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (model.Name != "1234")
            {
                var env = Request.GetOwinContext().Environment;
                env.IssueLoginCookie(new IdentityServer3.Core.Models.AuthenticatedLogin
                {
                    Subject = model.Name,
                    Name = model.Name,
                    Claims = new List<Claim>()
                {
                    new Claim("nummer", model.Name)
                }
                });

                var msg = env.GetSignInMessage(model.Id);
                var returnUrl = msg.ReturnUrl;

                env.RemovePartialLoginCookie();

                return Redirect(returnUrl);
            }

            model.FoutMelding = "A big error accurred";
            return View(model);
        }
    }
}