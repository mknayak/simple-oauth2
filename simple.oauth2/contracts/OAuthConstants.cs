using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth2
{
    public class OAuthConstants
    {
        public const string VERIFY_CODE = "VerifyCode";
        public const string STATE = "state";
    }

    public enum OAuthErrors
    {
        StateMissing= 010,
        VerifyCodeMissing=011,
        UnableToVerify=012
    }
}
