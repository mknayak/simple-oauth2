using simple.oauth2;
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