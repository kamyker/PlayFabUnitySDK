#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.AuthenticationModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// The Authentication APIs provide a convenient way to convert classic authentication responses into entity authentication
    /// models. These APIs will provide you with the entity authentication token needed for subsequent Entity API calls. Manage
    /// API keys for authenticating any entity.
    /// </summary>
    public static class AuthenticationAPI
    {
        static AuthenticationAPI() {}


        /// <summary>
        /// Verify entity login.
        /// </summary>
        public static bool IsEntityLoggedIn()
        {
            return PlayFabSettings.staticPlayer.IsEntityLoggedIn();
        }

        /// <summary>
        /// Clear the Client SessionToken which allows this Client to call API calls requiring login.
        /// A new/fresh login will be required after calling this.
        /// </summary>
        public static void ForgetAllCredentials()
        {
            PlayFabSettings.staticPlayer.ForgetAllCredentials();
        }

        private static PlayFabAuthenticationContext GetContext(PlayFabAuthenticationContext context) => context ?? PlayFabSettings.staticPlayer;

        /// <summary>
        /// Method to exchange a legacy AuthenticationTicket or title SecretKey for an Entity Token or to refresh a still valid
        /// Entity Token.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        public static Task<GetEntityTokenResponse> GetEntityToken(EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetEntityTokenRequest request = new GetEntityTokenRequest()
            {
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);
            AuthType authType = AuthType.None;
#if !DISABLE_PLAYFABCLIENT_API
            if (context.ClientSessionTicket != null) { authType = AuthType.LoginSession; }
#endif
#if ENABLE_PLAYFABSERVER_API || ENABLE_PLAYFABADMIN_API
            if (PlayFabSettings.staticSettings.DeveloperSecretKey != null) { authType = AuthType.DevSecretKey; } // TODO: Need to get the correct settings first
#endif
#if !DISABLE_PLAYFABENTITY_API
            if (context.EntityToken != null) { authType = AuthType.EntityToken; }
#endif

            return PlayFabHttp.MakeApiCallAsync<GetEntityTokenResponse>("/Authentication/GetEntityToken", request,
				authType,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Method for a server to validate a client provided EntityToken. Only callable by the title entity.
        /// </summary>
        /// <param name="EntityToken">Client EntityToken (Required)</param>
        public static Task<ValidateEntityTokenResponse> ValidateEntityToken(string EntityToken, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ValidateEntityTokenRequest request = new ValidateEntityTokenRequest()
            {
                EntityToken = EntityToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ValidateEntityTokenResponse>("/Authentication/ValidateEntityToken", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
