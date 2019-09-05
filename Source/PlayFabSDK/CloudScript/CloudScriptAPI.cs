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
    public static class CloudScriptAPI
    {
        static CloudScriptAPI() {}


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
        /// Cloud Script is one of PlayFab's most versatile features. It allows client code to request execution of any kind of
        /// custom server-side functionality you can implement, and it can be used in conjunction with virtually anything.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        /// <param name="FunctionName">The name of the CloudScript function to execute (Required)</param>
        /// <param name="FunctionParameter">Object that is passed in to the function as the first argument (Optional)</param>
        /// <param name="GeneratePlayStreamEvent">Generate a 'entity_executed_cloudscript' PlayStream event containing the results of the function execution and other contextual information. This event will show up in the PlayStream debugger console for the player in Game Manager. (Optional)</param>
        /// <param name="RevisionSelection">Option for which revision of the CloudScript to execute. 'Latest' executes the most recently created revision, 'Live' executes the current live, published revision, and 'Specific' executes the specified revision. The default value is 'Specific', if the SpecificRevision parameter is specified, otherwise it is 'Live'. (Optional)</param>
        /// <param name="SpecificRevision">The specific revision to execute, when RevisionSelection is set to 'Specific' (Optional)</param>
        public static Task<ExecuteCloudScriptResult> ExecuteEntityCloudScript(string FunctionName, EntityKey Entity = default, object FunctionParameter = default, bool? GeneratePlayStreamEvent = default, CloudScriptRevisionOption? RevisionSelection = default, int? SpecificRevision = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ExecuteEntityCloudScriptRequest request = new ExecuteEntityCloudScriptRequest()
            {
                FunctionName = FunctionName,
                Entity = Entity,
                FunctionParameter = FunctionParameter,
                GeneratePlayStreamEvent = GeneratePlayStreamEvent,
                RevisionSelection = RevisionSelection,
                SpecificRevision = SpecificRevision,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ExecuteCloudScriptResult>("/CloudScript/ExecuteEntityCloudScript", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
