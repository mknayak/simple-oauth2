///----------------------------------------------------------------------- 
/// <copyright file="OAuthHelper.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace simple.oauth2
{
    public static class OAuthHelper
    {

        /// <summary>
        /// Generates the time stamp.
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generates the nonce.
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonce()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Percent the encode the url
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string PercentEncode(string value)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in value)
            {
                if (c.IsValidUrlChar())
                {
                    result.Append(c);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)c));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Jsons to dynamic.
        /// </summary>
        /// <param name="content">The json string.</param>
        /// <returns></returns>
        public static dynamic ContentToDynamic(string content)
        {
            if (!content.StartsWith("{"))
            {
                var nvc= HttpUtility.ParseQueryString(content);
                dynamic returnObj = new ExpandoObject();
                IDictionary<string, object> dict = returnObj;

                foreach (var key in nvc.AllKeys)
                {
                    dict.Add(key, nvc[key]);
                }
                return returnObj;
            }
            else
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });

                return jss.Deserialize(content, typeof(object)) as dynamic;
            }
        }
    }
}
