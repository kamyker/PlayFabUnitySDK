#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.LocalizationModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// The Localization APIs give you the tools needed to manage language setup in your title.
    /// </summary>
    public static class PlayFabLocalizationAPI
    {
        static PlayFabLocalizationAPI() {}


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

        private static PlayFabAuthenticationContext GetContext(SharedModels.PlayFabRequestCommon request) => (request == null ? null : request.AuthenticationContext) ?? PlayFabSettings.staticPlayer;

        /// <summary>
        /// Retrieves the list of allowed languages, only accessible by title entities
        /// </summary>
        public static Task<GetLanguageListResponse> GetLanguageList(
            GetLanguageListRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetLanguageListRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetLanguageListResponse>("/Locale/GetLanguageList", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
