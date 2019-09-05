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
    public static class PlayFabProfilesAPI
    {
        static PlayFabProfilesAPI() {}


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
        /// Gets the global title access policy
        /// </summary>
        public static Task<GetGlobalPolicyResponse> GetGlobalPolicy(
            GetGlobalPolicyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetGlobalPolicyRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetGlobalPolicyResponse>("/Profile/GetGlobalPolicy", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the entity's profile.
        /// </summary>
        public static Task<GetEntityProfileResponse> GetProfile(bool? DataAsObject = default, EntityKey Entity = default, 
            GetEntityProfileRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetEntityProfileRequest();
            if(DataAsObject != default)
                request.DataAsObject = DataAsObject;
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetEntityProfileResponse>("/Profile/GetProfile", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the entity's profile.
        /// </summary>
        public static Task<GetEntityProfilesResponse> GetProfiles(List<EntityKey> Entities, bool? DataAsObject = default, 
            GetEntityProfilesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetEntityProfilesRequest();
            if(Entities != default)
                request.Entities = Entities;
            if(DataAsObject != default)
                request.DataAsObject = DataAsObject;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetEntityProfilesResponse>("/Profile/GetProfiles", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title player accounts associated with the given master player account.
        /// </summary>
        public static Task<GetTitlePlayersFromMasterPlayerAccountIdsResponse> GetTitlePlayersFromMasterPlayerAccountIds(List<string> MasterPlayerAccountIds, string TitleId = default, 
            GetTitlePlayersFromMasterPlayerAccountIdsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitlePlayersFromMasterPlayerAccountIdsRequest();
            if(MasterPlayerAccountIds != default)
                request.MasterPlayerAccountIds = MasterPlayerAccountIds;
            if(TitleId != default)
                request.TitleId = TitleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitlePlayersFromMasterPlayerAccountIdsResponse>("/Profile/GetTitlePlayersFromMasterPlayerAccountIds", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the global title access policy
        /// </summary>
        public static Task<SetGlobalPolicyResponse> SetGlobalPolicy(List<EntityPermissionStatement> Permissions = default, 
            SetGlobalPolicyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetGlobalPolicyRequest();
            if(Permissions != default)
                request.Permissions = Permissions;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetGlobalPolicyResponse>("/Profile/SetGlobalPolicy", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the entity's language. The precedence hierarchy for communication to the player is Title Player Account
        /// language, Master Player Account language, and then title default language if the first two aren't set or supported.
        /// </summary>
        public static Task<SetProfileLanguageResponse> SetProfileLanguage(EntityKey Entity = default, int? ExpectedVersion = default, string Language = default, 
            SetProfileLanguageRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetProfileLanguageRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(ExpectedVersion != default)
                request.ExpectedVersion = ExpectedVersion;
            if(Language != default)
                request.Language = Language;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetProfileLanguageResponse>("/Profile/SetProfileLanguage", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the profiles access policy
        /// </summary>
        public static Task<SetEntityProfilePolicyResponse> SetProfilePolicy(EntityKey Entity, List<EntityPermissionStatement> Statements = default, 
            SetEntityProfilePolicyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetEntityProfilePolicyRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Statements != default)
                request.Statements = Statements;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetEntityProfilePolicyResponse>("/Profile/SetProfilePolicy", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
