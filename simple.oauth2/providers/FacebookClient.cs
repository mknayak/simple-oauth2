///----------------------------------------------------------------------- 
/// <copyright file="FacebookClient.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 17, 2013 12:37:50 PM</date>
/// </copyright>
///-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth2.providers
{
    public class FacebookClient : OAuthClientBase
    {
        /// <summary>
        /// The urls
        /// </summary>
        static OAuth2Urls urls = new OAuth2Urls("https://www.facebook.com/dialog/oauth",
            "https://graph.facebook.com/oauth/access_token",
            "https://graph.facebook.com/me"            
            );
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookClient" /> class.
        /// </summary>
        /// <param name="appId">The app id.</param>
        /// <param name="appSecret">The app secret.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        public FacebookClient(string appId, string appSecret,string redirectUrl)
            : base(appId, appSecret, urls)
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
            get { return "email"; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name
        {
            get { return "FaceBook"; }
        }
    }
}
