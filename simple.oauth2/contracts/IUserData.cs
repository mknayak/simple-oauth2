///----------------------------------------------------------------------- 
/// <copyright file="IUserData.cs" company="CreekWorm">
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
    public class UserData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Link { get; set; }
        public string Provider { get; set; }
    }
}

