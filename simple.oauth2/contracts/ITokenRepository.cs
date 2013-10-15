///----------------------------------------------------------------------- 
/// <copyright file="ITokenRepository.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

namespace simple.oauth2
{
    public interface ITokenRepository
    {
        /// <summary>
        /// Tries to add token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        bool TryAddToken(string token, string provider);
        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        bool ValidateToken(string token, string provider);
        /// <summary>
        /// Validates and remove the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        bool ValidateAndRemove(string token, string provider);
    }
}
