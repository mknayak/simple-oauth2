using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple.oauth2
{
    /// <summary>
    /// OAuth Constants
    /// </summary>
    public class OAuthConstants
    {
        /// <summary>
        /// The verify_ code
        /// </summary>
        public const string VERIFY_CODE = "VerifyCode";
        /// <summary>
        /// The state
        /// </summary>
        public const string STATE = "state";
    }

    /// <summary>
    /// OAuth Errors
    /// </summary>
    public enum OAuthErrors
    {
        /// <summary>
        /// The state is missing
        /// </summary>
        StateMissing= 010,
        /// <summary>
        /// The verify code is missing
        /// </summary>
        VerifyCodeMissing=011,
        /// <summary>
        /// Unable to verify
        /// </summary>
        UnableToVerify=012
    }
}
