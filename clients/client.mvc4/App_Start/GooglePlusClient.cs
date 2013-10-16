using RestSharp;
using simple.oauth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace client.mvc4
{
    class GooglePlusClient : OAuthClientBase
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
            var scope = "https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";

            Parameter accessType = new Parameter { Name = "access_type", Value = "offline", Type = ParameterType.GetOrPost };

            return base.GetAuthenticationRequest(scope, accessType);            
        }
        protected override string GetAccessToken(string token)
        {
            Parameter granttype = new Parameter { Name = "grant_type", Value = "authorization_code",Type=ParameterType.GetOrPost };
            return base.GetAccessToken(token, Method.POST, granttype);
        }
        protected override IUserData Authorize(string access_token)
        {
            return base.Authorize(access_token, Method.GET);
        }

        public override IUserData GetUserData(string jsonObject)
        {
            dynamic obj = OAuthHelper.JsonToDynamic(jsonObject);

            return new GoogleUserData
            {
                id = obj.id,
                name = obj.name,
                link = obj.link,
                email = obj.email
            };
        }
    }

    class GoogleUserData : IUserData
    {
        public string id;
        public string name;
        public string link;
        public string email;
    }
}
