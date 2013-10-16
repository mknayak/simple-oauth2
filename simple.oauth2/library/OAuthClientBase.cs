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
        private string clientId;
        private string clientSecret;
        private OAuth2Urls urls;
        private string providerName;
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
            this.providerName = GetType().Name;
        }

        #region Implemented Methods

        /// <summary>
        /// Tries to get the authentication URL.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <returns></returns>
        protected RestRequest GetAuthenticationRequest(params Parameter[] additionalParameters)
        {
            RestRequest request = new RestRequest(urls.REQUEST_ACCESS_URL, Method.GET);
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", urls.REDIRECT_URL);
            request.AddParameter("scope", Scope);
            request.AddParameter("response_type", "code");
            if (additionalParameters != null && additionalParameters.Any())
            {
                foreach (var param in additionalParameters)
                {
                    request.AddParameter(param);
                }
            }

            return request;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="accessTokenUrl">The access token URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <returns></returns>
        protected IRestResponse GetAccessTokenResponse(string token, Method httpMethod, params Parameter[] additionalParameters)
        {

            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            request = new RestRequest(urls.ACCESS_TOKEN_URL, httpMethod);
            request.AddParameter("code", token);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("redirect_uri", urls.REDIRECT_URL);
            if (additionalParameters != null && additionalParameters.Any())
            {
                foreach (var param in additionalParameters)
                {
                    param.Type = ParameterType.GetOrPost;
                    request.AddParameter(param);
                }
            }

            var response = client.Execute(request);

            return response;
        }

        /// <summary>
        /// Authorizes the specified access_token.
        /// </summary>
        /// <param name="access_token">The access_token.</param>
        /// <param name="userProfileUrl">The user profile URL.</param>
        /// <returns></returns>
        protected UserData Authorize(string access_token, Method httpMethod)
        {
            RestRequest request = new RestRequest(urls.USER_PROFILE_URL, httpMethod);
            request.AddParameter("access_token", access_token);
            RestClient client = new RestClient();

            var result = client.Execute(request);

            return GetUserData(result.Content);
        }

        #endregion

        #region Virtual Methods

        protected virtual RestRequest GetAuthenticationRequest()
        {
            return this.GetAuthenticationRequest(null);
        }
        protected virtual string GetAccessToken(string token)
        {
            var response = this.GetAccessTokenResponse(token, Method.GET, null);
            var data = OAuthHelper.ContentToDynamic(response.Content);
            return data.access_token;
        }
        protected virtual UserData Authorize(string access_token)
        {
            return this.Authorize(access_token, Method.GET);
        }
        /// <summary>
        /// Gets the user data.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <returns></returns>
        public virtual UserData GetUserData(string jsonObject)
        {
            dynamic obj = OAuthHelper.ContentToDynamic(jsonObject);

            return new UserData
            {
                Id = obj.id,
                Name = obj.name,
                Link = obj.link,
                Email = obj.email
            };
        }
        #endregion

        #region Public Methods
        public Uri GetClientRedirectUri()
        {
            var request = GetAuthenticationRequest();

            return new RestClient().BuildUri(request);
        }

        public UserData ValidateTokenAndGetUserInfo(string code)
        {
            string access_token = GetAccessToken(code);
            return Authorize(access_token);
        }
        #endregion

        public abstract string Scope { get; }
    }

    internal class TokenResponse
    {
        public string access_token { get; set; }
    }
}
