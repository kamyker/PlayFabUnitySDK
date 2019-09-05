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
    public static class MatchmakerAPI
    {
        static MatchmakerAPI() {}


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
        /// Validates a user with the PlayFab service
        /// </summary>
        /// <param name="AuthorizationTicket">Session Ticket provided by the client. (Required)</param>
        public static Task<AuthUserResponse> AuthUser(string AuthorizationTicket, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AuthUserRequest request = new AuthUserRequest()
            {
                AuthorizationTicket = AuthorizationTicket,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AuthUserResponse>("/Matchmaker/AuthUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Informs the PlayFab game server hosting service that the indicated user has joined the Game Server Instance specified
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Server Instance the user is joining. This must be a Game Server Instance started with the Matchmaker/StartGame API. (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier for the player joining. (Required)</param>
        public static Task<PlayerJoinedResponse> PlayerJoined(string LobbyId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            PlayerJoinedRequest request = new PlayerJoinedRequest()
            {
                LobbyId = LobbyId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<PlayerJoinedResponse>("/Matchmaker/PlayerJoined", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Informs the PlayFab game server hosting service that the indicated user has left the Game Server Instance specified
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Server Instance the user is leaving. This must be a Game Server Instance started with the Matchmaker/StartGame API. (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier for the player leaving. (Required)</param>
        public static Task<PlayerLeftResponse> PlayerLeft(string LobbyId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            PlayerLeftRequest request = new PlayerLeftRequest()
            {
                LobbyId = LobbyId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<PlayerLeftResponse>("/Matchmaker/PlayerLeft", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Instructs the PlayFab game server hosting service to instantiate a new Game Server Instance
        /// </summary>
        /// <param name="Build">Unique identifier of the previously uploaded build executable which is to be started. (Required)</param>
        /// <param name="CustomCommandLineData">Custom command line argument when starting game server process. (Optional)</param>
        /// <param name="ExternalMatchmakerEventEndpoint">HTTP endpoint URL for receiving game status events, if using an external matchmaker. When the game ends, PlayFab will make a POST request to this URL with the X-SecretKey header set to the value of the game's secret and an application/json body of { "EventName": "game_ended", "GameID": "<gameid>" }. (Required)</param>
        /// <param name="GameMode">Game mode for this Game Server Instance. (Required)</param>
        /// <param name="Region">Region with which to associate the server, for filtering. (Required)</param>
        public static Task<StartGameResponse> StartGame(string Build, string ExternalMatchmakerEventEndpoint, string GameMode, Region Region, string CustomCommandLineData = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            StartGameRequest request = new StartGameRequest()
            {
                Build = Build,
                ExternalMatchmakerEventEndpoint = ExternalMatchmakerEventEndpoint,
                GameMode = GameMode,
                Region = Region,
                CustomCommandLineData = CustomCommandLineData,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<StartGameResponse>("/Matchmaker/StartGame", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user, which the external match-making service can then use to compute
        /// effective matches
        /// </summary>
        /// <param name="MinCatalogVersion">Minimum catalog version for which data is requested (filters the results to only contain inventory items which have a catalog version of this or higher). (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user whose information is being requested. (Required)</param>
        public static Task<UserInfoResponse> UserInfo(int MinCatalogVersion, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UserInfoRequest request = new UserInfoRequest()
            {
                MinCatalogVersion = MinCatalogVersion,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UserInfoResponse>("/Matchmaker/UserInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }


    }
}

#endif
