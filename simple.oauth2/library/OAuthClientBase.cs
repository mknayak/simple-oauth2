///----------------------------------------------------------------------- 
/// <copyright file="OAuthClientBase.cs" company="CreekWorm">
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

namespace simple.oauth2
{
    /// <summary>
    /// OAuth Client Base
    /// </summary>
    public abstract class OAuthClientBase : IOAuthClient
    {
        private string clientId;
        private string clientSecret;
        private OAuth2Urls urls;
        private string providerName;
        private ITokenRepository repository;
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
            this.repository = OAuthStore.Current.TokenRepository;
        }

        #region Implemented Methods

        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <returns></returns>
        protected RestRequest GetAuthenticationRequest(params Parameter[] additionalParameters)
        {
            if (!Uri.IsWellFormedUriString(REDIRECT_URL, UriKind.Absolute))
            {
                throw new ArgumentException("Redirect uri should be an absolute url");
            }
            RestRequest request = new RestRequest(urls.REQUEST_ACCESS_URL, Method.GET);
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", REDIRECT_URL);
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
        /// Gets the access token request.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <returns></returns>
        protected RestRequest GetAccessTokenRequest(string token, Method httpMethod, params Parameter[] additionalParameters)
        {
            RestRequest request = new RestRequest();
            request = new RestRequest(urls.ACCESS_TOKEN_URL, httpMethod);
            request.AddParameter("code", token);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("redirect_uri", REDIRECT_URL);
            if (additionalParameters != null && additionalParameters.Any())
            {
                foreach (var param in additionalParameters)
                {
                    param.Type = ParameterType.GetOrPost;
                    request.AddParameter(param);
                }
            }

            return request;
        }

        /// <summary>
        /// Gets the user info request.
        /// </summary>
        /// <param name="access_token">The access_token.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <returns></returns>
        protected RestRequest GetUserInfoRequest(string access_token, Method httpMethod)
        {
            RestRequest request = new RestRequest(urls.USER_PROFILE_URL, httpMethod);
            request.AddParameter("access_token", access_token);

            return request;
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Gets the authentication request.
        /// </summary>
        /// <returns></returns>
        protected virtual RestRequest GetAuthenticationRequest()
        {
            return this.GetAuthenticationRequest(null);
        }
        /// <summary>
        /// Gets the access token request.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        protected virtual RestRequest GetAccessTokenRequest(string token)
        {
            var request = this.GetAccessTokenRequest(token, Method.GET, null);
            return request;
        }
        /// <summary>
        /// Gets the user info request.
        /// </summary>
        /// <param name="access_token">The access_token.</param>
        /// <returns></returns>
        protected virtual RestRequest GetUserInfoRequest(string access_token)
        {
            return this.GetUserInfoRequest(access_token, Method.GET);
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
        /// <summary>
        /// Tries to get the authentication URL.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public Uri GetClientRedirectUri(IDictionary<string, string> state)
        {
            var request = GetAuthenticationRequest();
            string verifyCode = Guid.NewGuid().ToString("N");

            repository.TryAddToken(this.providerName, verifyCode);

            if (state == null)
                state = new Dictionary<string, string>();

            state[OAuthConstants.VERIFY_CODE] = verifyCode;

            var stateValue = state.Select(k => string.Format("{0}={1}", k.Key, k.Value)).Aggregate((f, s) => f + "&" + s);

            request.AddParameter(OAuthConstants.STATE, OAuthHelper.Base64Encode(stateValue));

            return new RestClient().BuildUri(request);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="stateString">The state string.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public UserData ValidateTokenAndGetUserInfo(string code, string stateString, out IDictionary<string, string> state)
        {
            state = Validate(stateString);

            RestClient client = new RestClient();

            var accessTokenRequest = GetAccessTokenRequest(code);
            var response = client.Execute(accessTokenRequest);

            var data = OAuthHelper.ContentToDynamic(response.Content);
            string access_token = Convert.ToString(data.access_token);

            var authorizeRequest = this.GetUserInfoRequest(access_token);
            // Some providers sends user info along with access_token
            if (authorizeRequest != null)
            {
                response = client.Execute(authorizeRequest);
            }
            return GetUserData(response.Content);
        }

        /// <summary>
        /// Validates the specified state string.
        /// </summary>
        /// <param name="stateString">The state string.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        private IDictionary<string, string> Validate(string stateString)
        {
            IDictionary<string, string> state;
            if (string.IsNullOrWhiteSpace(stateString))
                throw new ArgumentException(string.Format("ERRORCODE:{0},ERROR:{1}", (int)OAuthErrors.StateMissing, "State is required."));

            var stateValue = OAuthHelper.Base64Decode(stateString);
            var nvc = HttpUtility.ParseQueryString(stateValue);

            state = nvc.AllKeys.Select(n => new { Key = n, Value = nvc[n] }).ToDictionary(c => c.Key, c => c.Value);
            var verifyCode = state[OAuthConstants.VERIFY_CODE];
            if (string.IsNullOrWhiteSpace(verifyCode))
                throw new ArgumentException(string.Format("ERRORCODE:{0},ERROR:{1}", (int)OAuthErrors.VerifyCodeMissing, "VerifyCode is missing."));

            if (state.Remove(OAuthConstants.VERIFY_CODE))
            {
                if (!this.repository.ValidateAndRemove(this.providerName, verifyCode))
                {
                    throw new ArgumentException(string.Format("ERRORCODE:{0},ERROR:{1}", (int)OAuthErrors.UnableToVerify, "unable to verify the request origin."));
                }
            }
            return state;
        }
        #endregion

        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public abstract string Scope { get; }

        /// <summary>
        /// Gets or sets the REDIRECT URL.
        /// </summary>
        /// <value>
        /// The REDIRECT URL.
        /// </value>
        protected string REDIRECT_URL { get; set; }
    }
}
