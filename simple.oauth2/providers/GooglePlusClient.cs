using RestSharp;
using simple.oauth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simple.oauth2.providers
{
    public class GooglePlusClient : OAuthClientBase
    {
        static OAuth2Urls oauth_urls = new OAuth2Urls(
            "https://accounts.google.com/o/oauth2/auth",
            "https://accounts.google.com/o/oauth2/token",
            "https://www.googleapis.com/oauth2/v2/userinfo",
            "http://localhost:10409/home/oauthcallback?provider=googleplus");
        public GooglePlusClient(string clientId, string clientSecret)
            : base(clientId, clientSecret, oauth_urls)
        {
        }

        protected override RestRequest GetAuthenticationRequest()
        {
            Parameter accessType = new Parameter { Name = "access_type", Value = "offline", Type = ParameterType.GetOrPost };

            return base.GetAuthenticationRequest(accessType);
        }
        protected override string GetAccessToken(string token)
        {
            Parameter granttype = new Parameter { Name = "grant_type", Value = "authorization_code", Type = ParameterType.GetOrPost };
            var response= base.GetAccessTokenResponse(token, Method.POST, granttype);
            var data = OAuthHelper.ContentToDynamic(response.Content);
            return data.access_token;
        }
        protected override UserData Authorize(string access_token)
        {
            return base.Authorize(access_token, Method.GET);
        }
        
        public override string Scope
        {
            get { return "https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile"; }
        }
    }

}
