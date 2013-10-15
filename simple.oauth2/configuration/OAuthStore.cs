///----------------------------------------------------------------------- 
/// <copyright file="OAuthStore.cs" company="CreekWorm">
/// Copyright (c) CreekWorm. All rights reserved. 
/// <author>Manas Kumar Nayak</author>
/// <date>Wednesday, October 16, 2013 12:37:50 AM</date>
/// </copyright>
//-----------------------------------------------------------------------

namespace simple.oauth2
{
    public class OAuthStore
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public static DictionaryConfiguration<string> Configuration
        {
            get;
            private set;
        }

        private IOAuthFactory _factory;
        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        public IOAuthFactory Factory
        {
            get
            {
                if (_factory == null) { _factory = new DefaultOAuthFactory(); }
                return _factory;
            }
        }

        private ITokenRepository _tRepo;
        /// <summary>
        /// Gets the token repository.
        /// </summary>
        /// <value>
        /// The token repository.
        /// </value>
        public ITokenRepository TokenRepository
        {
            get
            {
                if (_tRepo == null) { _tRepo = new DefaultTokenRepository(); }
                return _tRepo;
            }
        }

        #region Methods
        /// <summary>
        /// Sets the OAuth factory.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void SetOAuthFactory(IOAuthFactory factory)
        {
            this._factory = factory;
        }

        /// <summary>
        /// Sets the token repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void SetTokenRepository(ITokenRepository repository)
        {
            this._tRepo = repository;
        }
        #endregion

        #region singleton instance
        /// <summary>
        /// Prevents a default instance of the <see cref="OAuthStore"/> class from being created.
        /// </summary>
        private OAuthStore() { }
        static OAuthStore _store;
        /// <summary>
        /// Gets the current instance.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static OAuthStore Current
        {
            get
            {
                if (_store == null)
                    _store = new OAuthStore();
                return _store;
            }
        }
        #endregion
    }
}

