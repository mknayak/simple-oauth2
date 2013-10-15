///----------------------------------------------------------------------- 
/// <copyright file="YammerClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using RestSharp;
using simple.oauth2;
using System;

namespace simple.oauth.provider.yammer
{
    /// <summary>
    /// TwitterClient
    /// </summary>
    public class YammerClient
    {
        string clientId = "cJZFagfrLYF1f2RCDMkYNg";
        string clientSecret = "PaOA1HYq1OwUKzyRorIZqd9FRKDp17nhnQetBF2Y";

        /// <summary>
        /// STEP 1: Authenticates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Redirect Url
        /// </returns>
        public string TryGetAuthenticationUrl()
        {
            RestRequest request = new RestRequest(YammerConstants.REQUEST_TOKEN_URI);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/yammer");
            request.AddParameter("client_id", clientId);
            
            RestClient client = new RestClient();
            var uri = client.BuildUri(request);
            
            return uri.AbsoluteUri;
        }

        /// <summary>
        /// STEP 2: Authorizes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="access_token">The access_token.</param>
        /// <param name="access_verifier">The access_verifier.</param>
        public IUserData Authorize(string token)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(YammerConstants.ACCESS_TOKEN_URL, Method.POST);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/gplus");
            request.AddParameter("code", token);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);

            IRestResponse response = client.Execute(request);
            var data = OAuthHelper.JsonToDynamic(response.Content);

            return new YammerUserData
            {
                id = Convert.ToString(data.access_token.user_id),
                name = data.user.full_name,
                link = data.user.web_url,
                email = data.user.contact.email_addresses[0].address,
            };
                        
        }
    }
  
    public class YammerUserData : IUserData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
    }
}
