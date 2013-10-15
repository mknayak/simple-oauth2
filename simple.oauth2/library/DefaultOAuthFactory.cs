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
    internal class DefaultOAuthFactory : IOAuthFactory
    {
        /// <summary>
        /// The _cache
        /// </summary>
        DictionaryConfiguration<object> _cache = new DictionaryConfiguration<object>();

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="instance">The instance.</param>
        public void RegisterInstance(string key, object instance)
        {
            _cache.AddOrUpdate(key, instance);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetInstance(string key)
        {
            return _cache.Get(key); ;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetInstance<T>(string key)
        {
            return (T)GetInstance(key);
        }
    }
}
