///----------------------------------------------------------------------- 
/// <copyright file="IOAuthClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------
using RestSharp;
using System;
using System.Collections.Generic;

namespace simple.oauth2
{
    /// <summary>
    /// IOAuthClient
    /// </summary>
    public interface IOAuthClient
    {
        /// <summary>
        /// Tries to get the authentication URL.
        /// </summary>
        /// <returns></returns>
        Uri GetAuthenticationUrl(IEnumerable<Parameter> additionalParameters);

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="accessTokenUrl">The access token URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        /// <returns></returns>
        string GetAccessToken(string token, Method httpMethod, IEnumerable<Parameter> additionalParameters);

        /// <summary>
        /// Authorizes with the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        IUserData Authorize(string access_token, Method httpMethod);
    }
}

