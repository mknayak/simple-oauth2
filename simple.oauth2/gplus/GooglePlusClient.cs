///----------------------------------------------------------------------- 
/// <copyright file="GooglePlusClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using RestSharp;
using simple.oauth2;

namespace simple.oauth.provider.gplus
{
    /// <summary>
    /// TwitterClient
    /// </summary>
    public class GooglePlusClient
    {
        string appId = "93975902289.apps.googleusercontent.com";
        string appSecret = "CqdSp9ssOe6Z0BPTt6QrQPzo";

        /// <summary>
        /// STEP 1: Authenticates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Redirect Url
        /// </returns>
        public string TryGetAuthenticationUrl()
        {
            RestRequest request = new RestRequest(GooglePlusConstants.REQUEST_TOKEN_URI);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/gplus");
            request.AddParameter("response_type","code");
            request.AddParameter("client_id", appId);
            request.AddParameter("scope", "https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile");
            //request.AddParameter("approval_prompt", "force");
            request.AddParameter("access_type","offline");

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
            RestRequest request = new RestRequest(GooglePlusConstants.ACCESS_TOKEN_URL, Method.POST);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/gplus");
            request.AddParameter("code", token);
            request.AddParameter("client_id", appId);
            request.AddParameter("client_secret", appSecret);
            request.AddParameter("grant_type", "authorization_code");

            IRestResponse response = client.Execute(request);
            var data = OAuthHelper.JsonToDynamic(response.Content);

            request = new RestRequest(GooglePlusConstants.GET_USER_INFO, Method.GET);
            request.AddParameter("access_token", data.access_token);

            var result = client.Execute<GooglePlusUserData>(request);

            return result.Data;
                        
        }
    }
  
    public class GooglePlusUserData : IUserData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
    }
}
