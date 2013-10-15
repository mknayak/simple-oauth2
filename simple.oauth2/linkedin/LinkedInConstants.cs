///----------------------------------------------------------------------- 
/// <copyright file="LinkedInConstants.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth.provider.linkedin
{
    public class LinkedInConstants
    {
        public static string REQUEST_TOKEN_URI;
        public static string GET_USER_INFO;
        public static string ACCESS_TOKEN_URL;
        public static string BASE_URL;
        public static string SCOPES;
        public static string PUBLIC_PROFILE_URL;
        static LinkedInConstants()
        {
            PUBLIC_PROFILE_URL = "https://api.linkedin.com/v1/people/id={id}";
            SCOPES = "r_basicprofile r_emailaddress r_contactinfo";
            GET_USER_INFO = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,public-profile-url,picture-url,email-address)";
            REQUEST_TOKEN_URI = "https://www.linkedin.com/uas/oauth2/authorization";
            ACCESS_TOKEN_URL = "https://www.linkedin.com/uas/oauth2/accessToken";
            BASE_URL = "https://accounts.google.com";
        }
    }
}
