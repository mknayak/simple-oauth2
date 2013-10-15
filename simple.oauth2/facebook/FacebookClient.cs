///----------------------------------------------------------------------- 
/// <copyright file="FacebookClient.cs" company="CreekWorm">
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

namespace simple.oauth.provider.facebook
{
    /// <summary>
    /// TwitterClient
    /// </summary>
    public class FacebookClient
    {
        string appId = "544743042263211";
        string appSecret = "28bfa573917062a012d7697c9417cd00";
        /// <summary>
        /// STEP 1: Authenticates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Redirect Url
        /// </returns>
        public string TryGetAuthenticationUrl()
        {
            RestRequest request = new RestRequest(FacebookConstants.REQUEST_TOKEN_URI);
            request.AddParameter("client_id", appId);
            request.AddParameter("redirect_uri","http://oauthtest.manas.com/home/facebook");
            request.AddParameter("scope","email");

            RestClient client = new RestClient();
            return client.BuildUri(request).AbsoluteUri;
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
            RestRequest request = new RestRequest(FacebookConstants.ACCESS_TOKEN_URL);
            request.AddParameter("client_id", appId);
            request.AddParameter("client_secret", appSecret);
            request.AddParameter("redirect_uri", "http://oauthtest.manas.com/home/facebook");
            request.AddParameter("code", token);
            var tres = client.ExecuteAsGet(request, "GET");

            var access_token = HttpUtility.ParseQueryString(tres.Content)["access_token"];

            request = new RestRequest(FacebookConstants.REQUEST_AUTHORIZE_URI);
            request.AddParameter("access_token", access_token);
            var res = client.Execute<FacebookUserData>(request);
            
            return res.Data;
        }
    }

    public class FacebookUserData : IUserData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
