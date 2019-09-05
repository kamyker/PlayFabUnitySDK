#if ENABLE_PLAYFABSERVER_API

using System;
using System.Collections.Generic;
using PlayFab.MatchmakerModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// Enables the use of an external match-making service in conjunction with PlayFab hosted Game Server instances
    /// </summary>
    public static class PlayFabMatchmakerAPI
    {
        static PlayFabMatchmakerAPI() {}


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
        /// Validates a user with the PlayFab service
        /// </summary>
        public static Task<AuthUserResponse> AuthUser(string AuthorizationTicket, 
            AuthUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AuthUserRequest();
            if(AuthorizationTicket != default)
                request.AuthorizationTicket = AuthorizationTicket;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AuthUserResponse>("/Matchmaker/AuthUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Informs the PlayFab game server hosting service that the indicated user has joined the Game Server Instance specified
        /// </summary>
        public static Task<PlayerJoinedResponse> PlayerJoined(string LobbyId, string PlayFabId, 
            PlayerJoinedRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new PlayerJoinedRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<PlayerJoinedResponse>("/Matchmaker/PlayerJoined", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Informs the PlayFab game server hosting service that the indicated user has left the Game Server Instance specified
        /// </summary>
        public static Task<PlayerLeftResponse> PlayerLeft(string LobbyId, string PlayFabId, 
            PlayerLeftRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new PlayerLeftRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<PlayerLeftResponse>("/Matchmaker/PlayerLeft", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Instructs the PlayFab game server hosting service to instantiate a new Game Server Instance
        /// </summary>
        public static Task<StartGameResponse> StartGame(string Build, string ExternalMatchmakerEventEndpoint, string GameMode, Region Region, string CustomCommandLineData = default, 
            StartGameRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new StartGameRequest();
            if(Build != default)
                request.Build = Build;
            if(ExternalMatchmakerEventEndpoint != default)
                request.ExternalMatchmakerEventEndpoint = ExternalMatchmakerEventEndpoint;
            if(GameMode != default)
                request.GameMode = GameMode;
            if(Region != default)
                request.Region = Region;
            if(CustomCommandLineData != default)
                request.CustomCommandLineData = CustomCommandLineData;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<StartGameResponse>("/Matchmaker/StartGame", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user, which the external match-making service can then use to compute
        /// effective matches
        /// </summary>
        public static Task<UserInfoResponse> UserInfo(int MinCatalogVersion, string PlayFabId, 
            UserInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UserInfoRequest();
            if(MinCatalogVersion != default)
                request.MinCatalogVersion = MinCatalogVersion;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UserInfoResponse>("/Matchmaker/UserInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }


    }
}

#endif
