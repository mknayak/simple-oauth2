///----------------------------------------------------------------------- 
/// <copyright file="LinkedInClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using RestSharp;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RestSharp.Serializers;
using simple.oauth2;

namespace simple.oauth.provider.linkedin
{
    /// <summary>
    /// TwitterClient
    /// </summary>
    public class LinkedInClient
    {
        string apiKey = "ien56lc81q6b";
        string secretKey = "ur3jLRxsivEcXZLc";
        string oauthUserToken = "449ef816-b6ae-4cf6-bae4-449f6f2877b5";
        string oauthUserSecret = "3cac021c-6903-4ae1-9d86-572a660d01a4";

        /// <summary>
        /// STEP 1: Authenticates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Redirect Url
        /// </returns>
        public string TryGetAuthenticationUrl()
        {
            RestRequest request = new RestRequest(LinkedInConstants.REQUEST_TOKEN_URI);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/linkedin");
            request.AddParameter("response_type", "code");
            request.AddParameter("client_id", apiKey);
            request.AddParameter("scope", LinkedInConstants.SCOPES);
            request.AddParameter("state", Guid.NewGuid().ToString("N"));

            RestClient client = new RestClient();
            var uri = client.BuildUri(request);

            return uri.AbsoluteUri;
        }

        /// <summary>
        /// STEP 2: Authorizes the specified context.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public IUserData Authorize(string token)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(LinkedInConstants.ACCESS_TOKEN_URL, Method.POST);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/linkedin");
            request.AddParameter("code", token);
            request.AddParameter("client_id", apiKey);
            request.AddParameter("client_secret", secretKey);
            request.AddParameter("grant_type", "authorization_code");

            IRestResponse response = client.Execute(request);
            var data = OAuthHelper.JsonToDynamic(response.Content);
            string access_token = data.access_token;
            request = new RestRequest(LinkedInConstants.GET_USER_INFO, Method.GET);
            request.AddParameter("oauth2_access_token", access_token);
            request.AddParameter("format", "json");

            var profile = client.Execute<LinkedInUserData>(request);
            return profile.Data;

        }
    }

    public class LinkedInUserData : IUserData
    {
        public string id { get; set; }
        public string formattedName { get; set; }
        public string publicProfileUrl { get; set; }
        public string emailAddress { get; set; }
        public string pictureUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
