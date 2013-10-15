///----------------------------------------------------------------------- 
/// <copyright file="YammerConstants.cs" company="CreekWorm">
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

namespace simple.oauth.provider.yammer
{
    public class YammerConstants
    {
        public static string REQUEST_TOKEN_URI;
        public static string GET_USER_INFO;
        public static string ACCESS_TOKEN_URL;
        public static string BASE_URL;
        static YammerConstants()
        {
            GET_USER_INFO = "https://www.yammer.com/api/v1/users/current.json";
            REQUEST_TOKEN_URI = "https://www.yammer.com/dialog/oauth";
            ACCESS_TOKEN_URL = "https://www.yammer.com/oauth2/access_token.json";
            BASE_URL = "https://accounts.google.com";
        }
    }
}
