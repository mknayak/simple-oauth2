///----------------------------------------------------------------------- 
/// <copyright file="DefaultOAuthFactory.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

namespace simple.oauth2
{
    /// <summary>
    /// Default OAuthFactory
    /// </summary>
    internal class DefaultOAuthFactory : IOAuthFactory<IOAuthClient>
    {
        /// <summary>
        /// The _cache
        /// </summary>
        DictionaryConfiguration<IOAuthClient> _cache = new DictionaryConfiguration<IOAuthClient>();

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="instance">The instance.</param>
        public void RegisterInstance(string key, IOAuthClient instance)
        {
            _cache.AddOrUpdate(key, instance);
        }
        
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IOAuthClient GetInstance(string key)
        {
            return _cache.Get(key);
        }


        public System.Collections.Generic.IEnumerable<string> GetRegisteredProviders()
        {
            return _cache.Keys();
        }
    }
}
