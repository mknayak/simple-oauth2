///----------------------------------------------------------------------- 
/// <copyright file="YammerClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 17, 2013 12:37:50 PM</date>
/// </copyright>
///-----------------------------------------------------------------------
using RestSharp;
using System;

namespace simple.oauth2.providers
{
    public class YammerClient:OAuthClientBase
    {
        /// <summary>
        /// The urls
        /// </summary>
        static OAuth2Urls urls = new OAuth2Urls("https://www.yammer.com/dialog/oauth",
            "https://www.yammer.com/oauth2/access_token.json",
            "https://www.yammer.com/api/v1/users/current.json");

        /// <summary>
        /// Initializes a new instance of the <see cref="YammerClient" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        public YammerClient(string clientId,string clientSecret,string redirectUrl):
            base(clientId,clientSecret,urls)
        {
            base.REDIRECT_URL = redirectUrl;
        }
        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public override string Scope
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Gets the user info request.
        /// </summary>
        /// <param name="access_token">The access_token.</param>
        /// <returns></returns>
        protected override RestRequest GetUserInfoRequest(string access_token)
        {
            return null;
        }

        /// <summary>
        /// Gets the user data.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <returns></returns>
        public override UserData GetUserData(string jsonObject)
        {
            var data = OAuthHelper.ContentToDynamic(jsonObject);

            return new UserData
            {
                Id = Convert.ToString(data.access_token.user_id),
                Name = data.user.full_name,
                Link = data.user.web_url,
                Email = data.user.contact.email_addresses[0].address,
            };
        }
    }
}
