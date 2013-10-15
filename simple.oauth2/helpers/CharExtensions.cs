///----------------------------------------------------------------------- 
/// <copyright file="CharExtensions.cs" company="CreekWorm">
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

namespace simple.oauth2
{
    public static class CharExtension
    {
        static string validUrlChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
        /// <summary>
        /// Determines whether [is valid URL char] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        ///   <c>true</c> if [is valid URL char] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidUrlChar(this char c)
        {
            return validUrlChars.Contains(c);
        }
    }
}
