///----------------------------------------------------------------------- 
/// <copyright file="LinkedInClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 17, 2013 12:37:50 PM</date>
/// </copyright>
///-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth2.providers
{
    /// <summary>
    /// LinkedIn Client
    /// </summary>
    public class LinkedInClient : OAuthClientBase
    {
        /// <summary>
        /// The urls
        /// </summary>
        static OAuth2Urls urls = new OAuth2Urls("https://www.linkedin.com/uas/oauth2/authorization",
            "https://www.linkedin.com/uas/oauth2/accessToken",
            "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,public-profile-url,picture-url,email-address)");

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedInClient" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        public LinkedInClient(string clientId, string clientSecret,string redirectUrl)
            : base(clientId, clientSecret, urls)
        {
            base.REDIRECT_URL = redirectUrl;
        }
        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string Scope
        {
            get { return "r_basicprofile r_emailaddress r_contactinfo"; }
        }

        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <returns></returns>
        protected override RestSharp.RestRequest GetAuthenticationRequest()
        {
            var request = base.GetAuthenticationRequest();
            request.AddParameter("state", Guid.NewGuid().ToString("N"));
            return request;
        }

        /// <summary>
        /// Gets the access token request.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        protected override RestSharp.RestRequest GetAccessTokenRequest(string token)
        {
            var request = base.GetAccessTokenRequest(token);
            request.AddParameter("grant_type", "authorization_code");

            return request;
        }

        /// <summary>
        /// Gets the user info request.
        /// </summary>
        /// <param name="access_token">The access_token.</param>
        /// <returns></returns>
        protected override RestSharp.RestRequest GetUserInfoRequest(string access_token)
        {
            var request= base.GetUserInfoRequest(access_token);
            request.Parameters.Clear();
            request.AddParameter("oauth2_access_token", access_token);
            request.AddParameter("format", "json");
            
            return request;
        }

        /// <summary>
        /// Gets the user data.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <returns></returns>
        public override UserData GetUserData(string jsonObject)
        {
            dynamic user = OAuthHelper.ContentToDynamic(jsonObject);

            return new UserData
            {
                Id = user.id,
                Name = user.formattedName,
                Email = user.emailAddress,
                Link = user.publicProfileUrl
            };
        }
    }
}
