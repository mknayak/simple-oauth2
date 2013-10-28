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
        /// <param name="state">The state.</param>
        /// <returns></returns>
        Uri GetClientRedirectUri(IDictionary<string, string> state);

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="stateString">The state string.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        UserData ValidateTokenAndGetUserInfo(string code, string stateString, out IDictionary<string, string> state);
    }
}

