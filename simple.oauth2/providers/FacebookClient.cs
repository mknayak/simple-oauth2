using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth2.providers
{
    public class FacebookClient : OAuthClientBase
    {
        static OAuth2Urls urls = new OAuth2Urls("https://www.facebook.com/dialog/oauth",
            "https://graph.facebook.com/oauth/access_token",
            "https://graph.facebook.com/me",
            "http://oauthtest.manas.com/home/oauthcallback?provider=facebook"            
            );
        public FacebookClient(string appId, string appSecret)
            : base(appId, appSecret, urls)
        {
        }

        public override string Scope
        {
            get { return "email"; }
        }
    }
}
