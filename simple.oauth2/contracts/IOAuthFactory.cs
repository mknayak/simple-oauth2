///----------------------------------------------------------------------- 
/// <copyright file="IOAuthFactory.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

namespace simple.oauth2
{
    /// <summary>
    /// IOAuthFactory
    /// </summary>
    public interface IOAuthFactory
    {
        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="instance">The instance.</param>
        void RegisterInstance(string key, object instance);
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetInstance(string key);
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetInstance<T>(string key);
    }
}
