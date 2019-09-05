#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.CloudScriptModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// API methods for executing CloudScript using an Entity Profile
    /// </summary>
    public static class PlayFabCloudScriptAPI
    {
        static PlayFabCloudScriptAPI() {}


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
        /// Cloud Script is one of PlayFab's most versatile features. It allows client code to request execution of any kind of
        /// custom server-side functionality you can implement, and it can be used in conjunction with virtually anything.
        /// </summary>
        public static Task<ExecuteCloudScriptResult> ExecuteEntityCloudScript(string FunctionName, EntityKey Entity = default, object FunctionParameter = default, bool? GeneratePlayStreamEvent = default, CloudScriptRevisionOption? RevisionSelection = default, int? SpecificRevision = default, 
            ExecuteEntityCloudScriptRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ExecuteEntityCloudScriptRequest();
            if(FunctionName != default)
                request.FunctionName = FunctionName;
            if(Entity != default)
                request.Entity = Entity;
            if(FunctionParameter != default)
                request.FunctionParameter = FunctionParameter;
            if(GeneratePlayStreamEvent != default)
                request.GeneratePlayStreamEvent = GeneratePlayStreamEvent;
            if(RevisionSelection != default)
                request.RevisionSelection = RevisionSelection;
            if(SpecificRevision != default)
                request.SpecificRevision = SpecificRevision;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ExecuteCloudScriptResult>("/CloudScript/ExecuteEntityCloudScript", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
