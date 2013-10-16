///----------------------------------------------------------------------- 
/// <copyright file="CaseInsensativeCompararer.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 09:35:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simple.oauth2.configuration
{
    class CaseInsensativeCompararer:IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.Length;
        }
    }
}
