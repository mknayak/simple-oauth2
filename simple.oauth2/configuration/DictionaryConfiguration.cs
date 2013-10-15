///----------------------------------------------------------------------- 
/// <copyright file="DictionaryConfiguration.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth2
{
    /// <summary>
    /// DictionaryConfiguration
    /// </summary>
    public class DictionaryConfiguration<T>
    {
        ConcurrentDictionary<string, T> dict = new ConcurrentDictionary<string, T>();

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T this[string key]
        {
            get
            {
                T value;
                dict.TryGetValue(key, out value);
                return value;
            }
            set
            {
                if (dict.ContainsKey(key))
                {
                    T originalValue;
                    dict.TryGetValue(key, out originalValue);
                    dict.TryUpdate(key, value, originalValue);
                }
                else
                    dict.TryAdd(key, value);

            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get(string key)
        {
            return this[key];
        }

        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddOrUpdate(string key, T value)
        {
            this[key] = value;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Remove(string key)
        {
            T value;
            dict.TryRemove(key, out value);
            return value;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            dict.Clear();
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="range">The range.</param>
        public void AddRange(IDictionary<string,T> range)
        {
            foreach (var item in range)
            {
                this[item.Key] = item.Value;
            }
        }
        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            return dict.ContainsKey(key);
        }
    }
}
