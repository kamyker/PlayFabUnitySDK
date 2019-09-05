#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.ProfilesModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// All PlayFab entities have profiles, which hold top-level properties about the entity. These APIs give you the tools
    /// needed to manage entity profiles.
    /// </summary>
    public static class ProfilesAPI
    {
        static ProfilesAPI() {}


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
        /// Gets the global title access policy
        /// </summary>
        public static Task<GetGlobalPolicyResponse> GetGlobalPolicy(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetGlobalPolicyRequest request = new GetGlobalPolicyRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetGlobalPolicyResponse>("/Profile/GetGlobalPolicy", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the entity's profile.
        /// </summary>
        /// <param name="DataAsObject">Determines whether the objects will be returned as an escaped JSON string or as a un-escaped JSON object. Default is JSON string. (Optional)</param>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        public static Task<GetEntityProfileResponse> GetProfile(bool? DataAsObject = default, EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetEntityProfileRequest request = new GetEntityProfileRequest()
            {
                DataAsObject = DataAsObject,
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetEntityProfileResponse>("/Profile/GetProfile", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the entity's profile.
        /// </summary>
        /// <param name="DataAsObject">Determines whether the objects will be returned as an escaped JSON string or as a un-escaped JSON object. Default is JSON string. (Optional)</param>
        /// <param name="Entities">Entity keys of the profiles to load. Must be between 1 and 25 (Required)</param>
        public static Task<GetEntityProfilesResponse> GetProfiles(List<EntityKey> Entities, bool? DataAsObject = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetEntityProfilesRequest request = new GetEntityProfilesRequest()
            {
                Entities = Entities,
                DataAsObject = DataAsObject,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetEntityProfilesResponse>("/Profile/GetProfiles", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title player accounts associated with the given master player account.
        /// </summary>
        /// <param name="MasterPlayerAccountIds">Master player account ids. (Required)</param>
        /// <param name="TitleId">Id of title to get players from. (Optional)</param>
        public static Task<GetTitlePlayersFromMasterPlayerAccountIdsResponse> GetTitlePlayersFromMasterPlayerAccountIds(List<string> MasterPlayerAccountIds, string TitleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitlePlayersFromMasterPlayerAccountIdsRequest request = new GetTitlePlayersFromMasterPlayerAccountIdsRequest()
            {
                MasterPlayerAccountIds = MasterPlayerAccountIds,
                TitleId = TitleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitlePlayersFromMasterPlayerAccountIdsResponse>("/Profile/GetTitlePlayersFromMasterPlayerAccountIds", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the global title access policy
        /// </summary>
        /// <param name="Permissions">The permissions that govern access to all entities under this title or namespace. (Optional)</param>
        public static Task<SetGlobalPolicyResponse> SetGlobalPolicy(List<EntityPermissionStatement> Permissions = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetGlobalPolicyRequest request = new SetGlobalPolicyRequest()
            {
                Permissions = Permissions,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetGlobalPolicyResponse>("/Profile/SetGlobalPolicy", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the entity's language. The precedence hierarchy for communication to the player is Title Player Account
        /// language, Master Player Account language, and then title default language if the first two aren't set or supported.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        /// <param name="ExpectedVersion">The expected version of a profile to perform this update on (Optional)</param>
        /// <param name="Language">The language to set on the given entity. Deletes the profile's language if passed in a null string. (Optional)</param>
        public static Task<SetProfileLanguageResponse> SetProfileLanguage(EntityKey Entity = default, int? ExpectedVersion = default, string Language = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetProfileLanguageRequest request = new SetProfileLanguageRequest()
            {
                Entity = Entity,
                ExpectedVersion = ExpectedVersion,
                Language = Language,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetProfileLanguageResponse>("/Profile/SetProfileLanguage", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the profiles access policy
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Statements">The statements to include in the access policy. (Optional)</param>
        public static Task<SetEntityProfilePolicyResponse> SetProfilePolicy(EntityKey Entity, List<EntityPermissionStatement> Statements = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetEntityProfilePolicyRequest request = new SetEntityProfilePolicyRequest()
            {
                Entity = Entity,
                Statements = Statements,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetEntityProfilePolicyResponse>("/Profile/SetProfilePolicy", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
