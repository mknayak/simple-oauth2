///----------------------------------------------------------------------- 
/// <copyright file="OAuthClientBase.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace simple.oauth2
{

    /// <summary>
    /// OAuthClientBase
    /// </summary>
    public abstract class OAuthClientBase : IOAuthClient
    {
        string clientId;
        string clientSecret;
        OAuth2Urls urls;
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthClientBase"/> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        public OAuthClientBase(string clientId, string clientSecret, OAuth2Urls urls)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.urls = urls;
        }

        /// <summary>
        /// Tries to get the authentication URL.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <returns></returns>
        public virtual Uri GetAuthenticationUrl(IEnumerable<Parameter> additionalParameters)
        {
            RestRequest request = new RestRequest(urls.REQUEST_ACCESS_URL, Method.GET);
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", urls.REDIRECT_URL);
            if (additionalParameters != null && additionalParameters.Any())
            {
                foreach (var param in additionalParameters)
                {
                    request.AddParameter(param);
                }
            }

            return new RestClient().BuildUri(request);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="accessTokenUrl">The access token URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <returns></returns>
        public virtual string GetAccessToken(string token, Method httpMethod, IEnumerable<Parameter> additionalParameters)
        {
            if (ValidateToken(token))
                throw new ArgumentException("Invalid token.");

            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            request = new RestRequest(urls.ACCESS_TOKEN_URL, httpMethod);
            request.AddParameter("code", token);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            if (additionalParameters != null && additionalParameters.Any())
            {
                foreach (var param in additionalParameters)
                {
                    request.AddParameter(param);
                }
            }

            IRestResponse response = client.Execute(request);
            var data = OAuthHelper.JsonToDynamic(response.Content);
            return data.access_token;
        }

        /// <summary>
        /// Authorizes the specified access_token.
        /// </summary>
        /// <param name="access_token">The access_token.</param>
        /// <param name="userProfileUrl">The user profile URL.</param>
        /// <returns></returns>
        public virtual IUserData Authorize(string access_token, Method httpMethod)
        {
            RestRequest request = new RestRequest(urls.USER_PROFILE_URL, httpMethod);
            request.AddParameter("access_token", access_token);
            RestClient client = new RestClient();

            var result = client.Execute(request);

            return GetUserData(result.Content);
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public abstract bool ValidateToken(string token);

        /// <summary>
        /// Gets the user data.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <returns></returns>
        public abstract IUserData GetUserData(string jsonObject);
    }
}
