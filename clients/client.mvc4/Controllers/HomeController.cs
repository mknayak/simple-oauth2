using simple.oauth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace client.mvc4.Controllers
{
    public class HomeController : Controller
    {
        IOAuthFactory<IOAuthClient> factory = OAuthStore.Current.Factory;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RedirectTo(string provider)
        {
            var client = factory.GetInstance(provider);

            if (client == null)
                throw new Exception("You must configure a provider before use");

            var redirectUrl = client.GetClientRedirectUri(null);

            return new RedirectResult(redirectUrl.AbsoluteUri);
        }

        public JsonResult OauthCallback(string provider, string code, string state)
        {
            var client = factory.GetInstance(provider);

            if (client == null)
                throw new Exception("You must configure a provider before use");
            IDictionary<string, string> stateDict;
            var user = client.ValidateTokenAndGetUserInfo(code, state,out stateDict);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}
