///----------------------------------------------------------------------- 
/// <copyright file="OAuthHelper.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
///-----------------------------------------------------------------------

using RestSharp.Contrib;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace simple.oauth2
{
    public static class OAuthHelper
    {
        /// <summary>
        /// Jsons to dynamic.
        /// </summary>
        /// <param name="content">The json string.</param>
        /// <returns></returns>
        public static dynamic ContentToDynamic(string content)
        {
            if (!content.StartsWith("{"))
            {
                return QSToDynamic(content);
            }
            else
            {
                return JsonToDynamic(content);
            }
        }

        /// <summary>
        /// Jsons to dynamic.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static dynamic JsonToDynamic(string content)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });

            return jss.Deserialize(content, typeof(object)) as dynamic;
        }

        /// <summary>
        /// QueryString to dynamic.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static dynamic QSToDynamic(string content)
        {
            var nvc = HttpUtility.ParseQueryString(content);
            dynamic returnObj = new ExpandoObject();
            IDictionary<string, object> dict = returnObj;

            foreach (var key in nvc.AllKeys)
            {
                dict.Add(key, nvc[key]);
            }
            return returnObj;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
