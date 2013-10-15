///----------------------------------------------------------------------- 
/// <copyright file="GooglePlusConstants.cs" company="CreekWorm">
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

namespace simple.oauth.provider.gplus
{
    public class GooglePlusConstants
    {
        public static string REQUEST_TOKEN_URI;
        public static string GET_USER_INFO;
        public static string ACCESS_TOKEN_URL;
        public static string BASE_URL;
        static GooglePlusConstants()
        {
            GET_USER_INFO = "https://www.googleapis.com/oauth2/v2/userinfo";
            REQUEST_TOKEN_URI = "https://accounts.google.com/o/oauth2/auth";
            ACCESS_TOKEN_URL = "https://accounts.google.com/o/oauth2/token";
            BASE_URL = "https://accounts.google.com";
        }
    }
}
