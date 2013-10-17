///----------------------------------------------------------------------- 
/// <copyright file="GooglePlusClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 17, 2013 12:37:50 PM</date>
/// </copyright>
///-----------------------------------------------------------------------
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
            "https://www.googleapis.com/oauth2/v2/userinfo");
        /// <summary>
        /// Initializes a new instance of the <see cref="GooglePlusClient" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        public GooglePlusClient(string clientId, string clientSecret,string redirectUrl)
            : base(clientId, clientSecret, oauth_urls)
        {
            base.REDIRECT_URL = redirectUrl;
        }

        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <returns></returns>
        protected override RestRequest GetAuthenticationRequest()
        {
            var request = base.GetAuthenticationRequest();
            request.AddParameter("access_type", "offline");

            return request;
        }

        /// <summary>
        /// Gets the access token request.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        protected override RestRequest GetAccessTokenRequest(string token)
        {
            var request= base.GetAccessTokenRequest(token);
            request.AddParameter("grant_type", "authorization_code");
            request.Method = Method.POST;
            return request;
        }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public override string Scope
        {
            get { return "https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile"; }
        }
    }

}
