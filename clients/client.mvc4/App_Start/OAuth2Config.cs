using simple.oauth2;
using simple.oauth2.providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace client.mvc4
{
    public class OAuth2Config
    {
        public static void Register(){
            var factory = OAuthStore.Current.Factory;

            factory.RegisterInstance("GooglePlus", new GooglePlusClient("93975902289.apps.googleusercontent.com", "CqdSp9ssOe6Z0BPTt6QrQPzo"));
            factory.RegisterInstance("FaceBook", new FacebookClient("544743042263211", "28bfa573917062a012d7697c9417cd00"));
        }

        public static IEnumerable<string> RegisteredProviders
        {
            get
            {
                return OAuthStore.Current.Factory.GetRegisteredProviders();
            }
        }
    }
}