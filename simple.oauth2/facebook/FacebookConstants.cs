///----------------------------------------------------------------------- 
/// <copyright file="FacebookConstants.cs" company="CreekWorm">
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

namespace simple.oauth.provider.facebook
{
    public class FacebookConstants
    {
        public static string REQUEST_TOKEN_URI;
        public static string REQUEST_AUTHENTICATE_URI;
        public static string REQUEST_AUTHORIZE_URI;
        public static string ACCESS_TOKEN_URL;
        public static string BASE_URL;

        static FacebookConstants()
        {
            REQUEST_TOKEN_URI = "https://www.facebook.com/dialog/oauth";
            REQUEST_AUTHORIZE_URI = "https://graph.facebook.com/me";
            ACCESS_TOKEN_URL = "https://graph.facebook.com/oauth/access_token";
            BASE_URL = "https://www.facebook.com";
        }
    }
}
