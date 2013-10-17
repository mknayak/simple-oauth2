///----------------------------------------------------------------------- 
/// <copyright file="OAuth2Urls.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

namespace simple.oauth2
{
    /// <summary>
    /// OAuth2Urls
    /// </summary>
    public class OAuth2Urls
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2Urls" /> class.
        /// </summary>
        /// <param name="requestAccessUrl">The request access URL.</param>
        /// <param name="accessTokenUrl">The access token URL.</param>
        /// <param name="userProfileUrl">The user profile URL.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        public OAuth2Urls(string requestAccessUrl, string accessTokenUrl, string userProfileUrl)
        {
            this.REQUEST_ACCESS_URL = requestAccessUrl;
            this.ACCESS_TOKEN_URL = accessTokenUrl;
            this.USER_PROFILE_URL = userProfileUrl;
        }
        /// <summary>
        /// Gets or sets the REQUEST ACCESS URL.
        /// </summary>
        /// <value>
        /// The REQUEST ACCESS URL.
        /// </value>
        public string REQUEST_ACCESS_URL { get; set; }
        /// <summary>
        /// Gets or sets the ACCESS TOKEN URL.
        /// </summary>
        /// <value>
        /// The ACCESS TOKEN URL.
        /// </value>
        public string ACCESS_TOKEN_URL { get; set; }
        /// <summary>
        /// Gets or sets the USER PROFILE URL.
        /// </summary>
        /// <value>
        /// The USER PROFILE URL.
        /// </value>
        public string USER_PROFILE_URL { get; set; }
    }
}
