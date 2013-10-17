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
        public static void Register()
        {
            var factory = OAuthStore.Current.Factory;

            factory.RegisterInstance("GooglePlus", new GooglePlusClient(
                "93975902289.apps.googleusercontent.com",
                "CqdSp9ssOe6Z0BPTt6QrQPzo",
                "http://oauthtest.manas.com/home/oauthcallback?provider=googleplus"));

            factory.RegisterInstance("FaceBook", new FacebookClient(
                "544743042263211",
                "28bfa573917062a012d7697c9417cd00",
                "http://oauthtest.manas.com/home/oauthcallback?provider=facebook"));

            factory.RegisterInstance("LinkedIn", new LinkedInClient(
                "ien56lc81q6b",
                "ur3jLRxsivEcXZLc",
                "http://oauthtest.manas.com/home/oauthcallback?provider=linkedin"));

            factory.RegisterInstance("Yammer", new YammerClient(
                "cJZFagfrLYF1f2RCDMkYNg",
                "PaOA1HYq1OwUKzyRorIZqd9FRKDp17nhnQetBF2Y",
                "http://oauthtest.manas.com/home/oauthcallback?provider=yammer"));
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