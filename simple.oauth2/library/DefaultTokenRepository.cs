///----------------------------------------------------------------------- 
/// <copyright file="DefaultTokenRepository.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

namespace simple.oauth2
{
    internal class DefaultTokenRepository : ITokenRepository
    {
        /// <summary>
        /// The _cache
        /// </summary>
        DictionaryConfiguration<string> _cache = new DictionaryConfiguration<string>();

        /// <summary>
        /// Tries to add token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool TryAddToken(string token, string provider)
        {
            _cache.AddOrUpdate(GetKey(token, provider), string.Empty);
            return true;
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ValidateToken(string token, string provider)
        {
            return _cache.Contains(GetKey(token, provider));
        }

        /// <summary>
        /// Validates and remove the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ValidateAndRemove(string token, string provider)
        {
            if (ValidateToken(token, provider))
            {
                _cache.Remove(GetKey(token, provider));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        private string GetKey(string token, string provider)
        {
            return string.Format("{0}_{1}", provider, token);
        }
    }
}
