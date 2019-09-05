#if ENABLE_PLAYFABSERVER_API

using System;
using System.Collections.Generic;
using PlayFab.ServerModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// Provides functionality to allow external (developer-controlled) servers to interact with user inventories and data in a
    /// trusted manner, and to handle matchmaking and client connection orchestration
    /// </summary>
    public static class PlayFabServerAPI
    {
        static PlayFabServerAPI() {}


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
        /// Increments the character's balance of the specified virtual currency by the stated amount
        /// </summary>
        public static Task<ModifyCharacterVirtualCurrencyResult> AddCharacterVirtualCurrency(int Amount, string CharacterId, string PlayFabId, string VirtualCurrency, 
            AddCharacterVirtualCurrencyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddCharacterVirtualCurrencyRequest();
            if(Amount != default)
                request.Amount = Amount;
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyCharacterVirtualCurrencyResult>("/Server/AddCharacterVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the Friend user to the friendlist of the user with PlayFabId. At least one of
        /// FriendPlayFabId,FriendUsername,FriendEmail, or FriendTitleDisplayName should be initialized.
        /// </summary>
        public static Task<EmptyResponse> AddFriend(string PlayFabId, string FriendEmail = default, string FriendPlayFabId = default, string FriendTitleDisplayName = default, string FriendUsername = default, 
            AddFriendRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddFriendRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(FriendEmail != default)
                request.FriendEmail = FriendEmail;
            if(FriendPlayFabId != default)
                request.FriendPlayFabId = FriendPlayFabId;
            if(FriendTitleDisplayName != default)
                request.FriendTitleDisplayName = FriendTitleDisplayName;
            if(FriendUsername != default)
                request.FriendUsername = FriendUsername;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/AddFriend", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified generic service identifier to the player's PlayFab account. This is designed to allow for a PlayFab
        /// ID lookup of any arbitrary service identifier a title wants to add. This identifier should never be used as
        /// authentication credentials, as the intent is that it is easily accessible by other players.
        /// </summary>
        public static Task<EmptyResult> AddGenericID(GenericServiceId GenericId, string PlayFabId, 
            AddGenericIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddGenericIDRequest();
            if(GenericId != default)
                request.GenericId = GenericId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResult>("/Server/AddGenericID", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds a given tag to a player profile. The tag's namespace is automatically generated based on the source of the tag.
        /// </summary>
        public static Task<AddPlayerTagResult> AddPlayerTag(string PlayFabId, string TagName, 
            AddPlayerTagRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddPlayerTagRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(TagName != default)
                request.TagName = TagName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AddPlayerTagResult>("/Server/AddPlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds users to the set of those able to update both the shared data, as well as the set of users in the group. Only users
        /// in the group (and the server) can add new members. Shared Groups are designed for sharing data between a very small
        /// number of players, please see our guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<AddSharedGroupMembersResult> AddSharedGroupMembers(List<string> PlayFabIds, string SharedGroupId, 
            AddSharedGroupMembersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddSharedGroupMembersRequest();
            if(PlayFabIds != default)
                request.PlayFabIds = PlayFabIds;
            if(SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AddSharedGroupMembersResult>("/Server/AddSharedGroupMembers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increments the user's balance of the specified virtual currency by the stated amount
        /// </summary>
        public static Task<ModifyUserVirtualCurrencyResult> AddUserVirtualCurrency(int Amount, string PlayFabId, string VirtualCurrency, 
            AddUserVirtualCurrencyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddUserVirtualCurrencyRequest();
            if(Amount != default)
                request.Amount = Amount;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Server/AddUserVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validated a client's session ticket, and if successful, returns details for that user
        /// </summary>
        public static Task<AuthenticateSessionTicketResult> AuthenticateSessionTicket(string SessionTicket, 
            AuthenticateSessionTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AuthenticateSessionTicketRequest();
            if(SessionTicket != default)
                request.SessionTicket = SessionTicket;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AuthenticateSessionTicketResult>("/Server/AuthenticateSessionTicket", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Awards the specified users the specified Steam achievements
        /// </summary>
        public static Task<AwardSteamAchievementResult> AwardSteamAchievement(List<AwardSteamAchievementItem> Achievements, 
            AwardSteamAchievementRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AwardSteamAchievementRequest();
            if(Achievements != default)
                request.Achievements = Achievements;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AwardSteamAchievementResult>("/Server/AwardSteamAchievement", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Bans users by PlayFab ID with optional IP address, or MAC address for the provided game.
        /// </summary>
        public static Task<BanUsersResult> BanUsers(List<BanRequest> Bans, 
            BanUsersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new BanUsersRequest();
            if(Bans != default)
                request.Bans = Bans;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<BanUsersResult>("/Server/BanUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Consume uses of a consumable item. When all uses are consumed, it will be removed from the player's inventory.
        /// </summary>
        public static Task<ConsumeItemResult> ConsumeItem(int ConsumeCount, string ItemInstanceId, string PlayFabId, string CharacterId = default, 
            ConsumeItemRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ConsumeItemRequest();
            if(ConsumeCount != default)
                request.ConsumeCount = ConsumeCount;
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ConsumeItemResult>("/Server/ConsumeItem", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Requests the creation of a shared group object, containing key/value pairs which may be updated by all members of the
        /// group. When created by a server, the group will initially have no members. Shared Groups are designed for sharing data
        /// between a very small number of players, please see our guide:
        /// https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<CreateSharedGroupResult> CreateSharedGroup(string SharedGroupId = default, 
            CreateSharedGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateSharedGroupRequest();
            if(SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateSharedGroupResult>("/Server/CreateSharedGroup", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes the specific character ID from the specified user.
        /// </summary>
        public static Task<DeleteCharacterFromUserResult> DeleteCharacterFromUser(string CharacterId, string PlayFabId, bool SaveCharacterInventory, 
            DeleteCharacterFromUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteCharacterFromUserRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(SaveCharacterInventory != default)
                request.SaveCharacterInventory = SaveCharacterInventory;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeleteCharacterFromUserResult>("/Server/DeleteCharacterFromUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a user's player account from a title and deletes all associated data
        /// </summary>
        public static Task<DeletePlayerResult> DeletePlayer(string PlayFabId, 
            DeletePlayerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeletePlayerRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeletePlayerResult>("/Server/DeletePlayer", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes push notification template for title
        /// </summary>
        public static Task<DeletePushNotificationTemplateResult> DeletePushNotificationTemplate(string PushNotificationTemplateId, 
            DeletePushNotificationTemplateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeletePushNotificationTemplateRequest();
            if(PushNotificationTemplateId != default)
                request.PushNotificationTemplateId = PushNotificationTemplateId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeletePushNotificationTemplateResult>("/Server/DeletePushNotificationTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a shared group, freeing up the shared group ID to be reused for a new group. Shared Groups are designed for
        /// sharing data between a very small number of players, please see our guide:
        /// https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<EmptyResponse> DeleteSharedGroup(string SharedGroupId, 
            DeleteSharedGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteSharedGroupRequest();
            if(SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/DeleteSharedGroup", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Inform the matchmaker that a Game Server Instance is removed.
        /// </summary>
        public static Task<DeregisterGameResponse> DeregisterGame(string LobbyId, 
            DeregisterGameRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeregisterGameRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeregisterGameResponse>("/Server/DeregisterGame", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns the result of an evaluation of a Random Result Table - the ItemId from the game Catalog which would have been
        /// added to the player inventory, if the Random Result Table were added via a Bundle or a call to UnlockContainer.
        /// </summary>
        public static Task<EvaluateRandomResultTableResult> EvaluateRandomResultTable(string TableId, string CatalogVersion = default, 
            EvaluateRandomResultTableRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new EvaluateRandomResultTableRequest();
            if(TableId != default)
                request.TableId = TableId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EvaluateRandomResultTableResult>("/Server/EvaluateRandomResultTable", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Executes a CloudScript function, with the 'currentPlayerId' variable set to the specified PlayFabId parameter value.
        /// </summary>
        public static Task<ExecuteCloudScriptResult> ExecuteCloudScript(string FunctionName, string PlayFabId, object FunctionParameter = default, bool? GeneratePlayStreamEvent = default, CloudScriptRevisionOption? RevisionSelection = default, int? SpecificRevision = default, 
            ExecuteCloudScriptServerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ExecuteCloudScriptServerRequest();
            if(FunctionName != default)
                request.FunctionName = FunctionName;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(FunctionParameter != default)
                request.FunctionParameter = FunctionParameter;
            if(GeneratePlayStreamEvent != default)
                request.GeneratePlayStreamEvent = GeneratePlayStreamEvent;
            if(RevisionSelection != default)
                request.RevisionSelection = RevisionSelection;
            if(SpecificRevision != default)
                request.SpecificRevision = SpecificRevision;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ExecuteCloudScriptResult>("/Server/ExecuteCloudScript", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        public static void ExecuteCloudScript<TOut>(ExecuteCloudScriptServerRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.DeveloperSecretKey)) throw new PlayFabException(PlayFabExceptionCode.DeveloperKeyNotSet, "Must set PlayFabSettings.staticSettings.DeveloperSecretKey to call this method");
            var context = (request == null ? null : request.AuthenticationContext) ?? PlayFabSettings.staticPlayer;
            Action<ExecuteCloudScriptResult> wrappedResultCallback = (wrappedResult) =>
            {
                var serializer = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer);
                var wrappedJson = serializer.SerializeObject(wrappedResult.FunctionResult);
                try {
                    wrappedResult.FunctionResult = serializer.DeserializeObject<TOut>(wrappedJson);
                } catch (Exception) {
                    wrappedResult.FunctionResult = wrappedJson;
                    wrappedResult.Logs.Add(new LogStatement { Level = "Warning", Data = wrappedJson, Message = "Sdk Message: Could not deserialize result as: " + typeof(TOut).Name });
                }
                resultCallback(wrappedResult);
            };
            PlayFabHttp.MakeApiCall("/Server/ExecuteCloudScript", request, AuthType.DevSecretKey, wrappedResultCallback, errorCallback, customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves an array of player segment definitions. Results from this can be used in subsequent API calls such as
        /// GetPlayersInSegment which requires a Segment ID. While segment names can change the ID for that segment will not change.
        /// </summary>
        public static Task<GetAllSegmentsResult> GetAllSegments(
            GetAllSegmentsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetAllSegmentsRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetAllSegmentsResult>("/Server/GetAllSegments", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all of the characters that belong to a specific user. CharacterIds are not globally unique; characterId must be
        /// evaluated with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        public static Task<ListUsersCharactersResult> GetAllUsersCharacters(string PlayFabId, 
            ListUsersCharactersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListUsersCharactersRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListUsersCharactersResult>("/Server/GetAllUsersCharacters", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified version of the title's catalog of virtual goods, including all defined properties
        /// </summary>
        public static Task<GetCatalogItemsResult> GetCatalogItems(string CatalogVersion = default, 
            GetCatalogItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCatalogItemsRequest();
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCatalogItemsResult>("/Server/GetCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<GetCharacterDataResult> GetCharacterData(string CharacterId, string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCharacterDataRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Server/GetCharacterData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user's character which cannot be accessed by the client
        /// </summary>
        public static Task<GetCharacterDataResult> GetCharacterInternalData(string CharacterId, string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCharacterDataRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Server/GetCharacterInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified character's current inventory of virtual goods
        /// </summary>
        public static Task<GetCharacterInventoryResult> GetCharacterInventory(string CharacterId, string PlayFabId, string CatalogVersion = default, 
            GetCharacterInventoryRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCharacterInventoryRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterInventoryResult>("/Server/GetCharacterInventory", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        public static Task<GetCharacterLeaderboardResult> GetCharacterLeaderboard(int MaxResultsCount, int StartPosition, string StatisticName, string CharacterType = default, 
            GetCharacterLeaderboardRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCharacterLeaderboardRequest();
            if(MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if(StartPosition != default)
                request.StartPosition = StartPosition;
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(CharacterType != default)
                request.CharacterType = CharacterType;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterLeaderboardResult>("/Server/GetCharacterLeaderboard", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user's character which can only be read by the client
        /// </summary>
        public static Task<GetCharacterDataResult> GetCharacterReadOnlyData(string CharacterId, string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCharacterDataRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Server/GetCharacterReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details of all title-specific statistics for the specific character
        /// </summary>
        public static Task<GetCharacterStatisticsResult> GetCharacterStatistics(string CharacterId, string PlayFabId, 
            GetCharacterStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCharacterStatisticsRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterStatisticsResult>("/Server/GetCharacterStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// This API retrieves a pre-signed URL for accessing a content file for the title. A subsequent HTTP GET to the returned
        /// URL will attempt to download the content. A HEAD query to the returned URL will attempt to retrieve the metadata of the
        /// content. Note that a successful result does not guarantee the existence of this content - if it has not been uploaded,
        /// the query to retrieve the data will fail. See this post for more information:
        /// https://community.playfab.com/hc/en-us/community/posts/205469488-How-to-upload-files-to-PlayFab-s-Content-Service. Also,
        /// please be aware that the Content service is specifically PlayFab's CDN offering, for which standard CDN rates apply.
        /// </summary>
        public static Task<GetContentDownloadUrlResult> GetContentDownloadUrl(string Key, string HttpMethod = default, bool? ThruCDN = default, 
            GetContentDownloadUrlRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetContentDownloadUrlRequest();
            if(Key != default)
                request.Key = Key;
            if(HttpMethod != default)
                request.HttpMethod = HttpMethod;
            if(ThruCDN != default)
                request.ThruCDN = ThruCDN;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetContentDownloadUrlResult>("/Server/GetContentDownloadUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked friends of the given player for the given statistic, starting from the indicated point in the
        /// leaderboard
        /// </summary>
        public static Task<GetLeaderboardResult> GetFriendLeaderboard(int MaxResultsCount, string PlayFabId, int StartPosition, string StatisticName, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, string XboxToken = default, 
            GetFriendLeaderboardRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetFriendLeaderboardRequest();
            if(MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(StartPosition != default)
                request.StartPosition = StartPosition;
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(IncludeFacebookFriends != default)
                request.IncludeFacebookFriends = IncludeFacebookFriends;
            if(IncludeSteamFriends != default)
                request.IncludeSteamFriends = IncludeSteamFriends;
            if(ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if(Version != default)
                request.Version = Version;
            if(XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Server/GetFriendLeaderboard", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current friends for the user with PlayFabId, constrained to users who have PlayFab accounts. Friends from
        /// linked accounts (Facebook, Steam) are also included. You may optionally exclude some linked services' friends.
        /// </summary>
        public static Task<GetFriendsListResult> GetFriendsList(string PlayFabId, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, PlayerProfileViewConstraints ProfileConstraints = default, string XboxToken = default, 
            GetFriendsListRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetFriendsListRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IncludeFacebookFriends != default)
                request.IncludeFacebookFriends = IncludeFacebookFriends;
            if(IncludeSteamFriends != default)
                request.IncludeSteamFriends = IncludeSteamFriends;
            if(ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if(XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetFriendsListResult>("/Server/GetFriendsList", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        public static Task<GetLeaderboardResult> GetLeaderboard(int MaxResultsCount, int StartPosition, string StatisticName, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, 
            GetLeaderboardRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetLeaderboardRequest();
            if(MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if(StartPosition != default)
                request.StartPosition = StartPosition;
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if(Version != default)
                request.Version = Version;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Server/GetLeaderboard", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, centered on the requested user
        /// </summary>
        public static Task<GetLeaderboardAroundCharacterResult> GetLeaderboardAroundCharacter(string CharacterId, int MaxResultsCount, string PlayFabId, string StatisticName, string CharacterType = default, 
            GetLeaderboardAroundCharacterRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetLeaderboardAroundCharacterRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(CharacterType != default)
                request.CharacterType = CharacterType;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundCharacterResult>("/Server/GetLeaderboardAroundCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, centered on the currently signed-in user
        /// </summary>
        public static Task<GetLeaderboardAroundUserResult> GetLeaderboardAroundUser(int MaxResultsCount, string PlayFabId, string StatisticName, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, 
            GetLeaderboardAroundUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetLeaderboardAroundUserRequest();
            if(MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if(Version != default)
                request.Version = Version;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundUserResult>("/Server/GetLeaderboardAroundUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of all of the user's characters for the given statistic.
        /// </summary>
        public static Task<GetLeaderboardForUsersCharactersResult> GetLeaderboardForUserCharacters(int MaxResultsCount, string PlayFabId, string StatisticName, 
            GetLeaderboardForUsersCharactersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetLeaderboardForUsersCharactersRequest();
            if(MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(StatisticName != default)
                request.StatisticName = StatisticName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardForUsersCharactersResult>("/Server/GetLeaderboardForUserCharacters", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns whatever info is requested in the response for the user. Note that PII (like email address, facebook id) may be
        /// returned. All parameters default to false.
        /// </summary>
        public static Task<GetPlayerCombinedInfoResult> GetPlayerCombinedInfo(GetPlayerCombinedInfoRequestParams InfoRequestParameters, string PlayFabId, 
            GetPlayerCombinedInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerCombinedInfoRequest();
            if(InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerCombinedInfoResult>("/Server/GetPlayerCombinedInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the player's profile
        /// </summary>
        public static Task<GetPlayerProfileResult> GetPlayerProfile(string PlayFabId, PlayerProfileViewConstraints ProfileConstraints = default, 
            GetPlayerProfileRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerProfileRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerProfileResult>("/Server/GetPlayerProfile", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all segments that a player currently belongs to at this moment in time.
        /// </summary>
        public static Task<GetPlayerSegmentsResult> GetPlayerSegments(string PlayFabId, 
            GetPlayersSegmentsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayersSegmentsRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerSegmentsResult>("/Server/GetPlayerSegments", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Allows for paging through all players in a given segment. This API creates a snapshot of all player profiles that match
        /// the segment definition at the time of its creation and lives through the Total Seconds to Live, refreshing its life span
        /// on each subsequent use of the Continuation Token. Profiles that change during the course of paging will not be reflected
        /// in the results. AB Test segments are currently not supported by this operation.
        /// </summary>
        public static Task<GetPlayersInSegmentResult> GetPlayersInSegment(string SegmentId, string ContinuationToken = default, uint? MaxBatchSize = default, uint? SecondsToLive = default, 
            GetPlayersInSegmentRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayersInSegmentRequest();
            if(SegmentId != default)
                request.SegmentId = SegmentId;
            if(ContinuationToken != default)
                request.ContinuationToken = ContinuationToken;
            if(MaxBatchSize != default)
                request.MaxBatchSize = MaxBatchSize;
            if(SecondsToLive != default)
                request.SecondsToLive = SecondsToLive;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayersInSegmentResult>("/Server/GetPlayersInSegment", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current version and values for the indicated statistics, for the local player.
        /// </summary>
        public static Task<GetPlayerStatisticsResult> GetPlayerStatistics(string PlayFabId, List<string> StatisticNames = default, List<StatisticNameVersion> StatisticNameVersions = default, 
            GetPlayerStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerStatisticsRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(StatisticNames != default)
                request.StatisticNames = StatisticNames;
            if(StatisticNameVersions != default)
                request.StatisticNameVersions = StatisticNameVersions;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticsResult>("/Server/GetPlayerStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the information on the available versions of the specified statistic.
        /// </summary>
        public static Task<GetPlayerStatisticVersionsResult> GetPlayerStatisticVersions(string StatisticName = default, 
            GetPlayerStatisticVersionsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerStatisticVersionsRequest();
            if(StatisticName != default)
                request.StatisticName = StatisticName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticVersionsResult>("/Server/GetPlayerStatisticVersions", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get all tags with a given Namespace (optional) from a player profile.
        /// </summary>
        public static Task<GetPlayerTagsResult> GetPlayerTags(string PlayFabId, string Namespace = default, 
            GetPlayerTagsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerTagsRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Namespace != default)
                request.Namespace = Namespace;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTagsResult>("/Server/GetPlayerTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromFacebookIDsResult> GetPlayFabIDsFromFacebookIDs(List<string> FacebookIDs, 
            GetPlayFabIDsFromFacebookIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromFacebookIDsRequest();
            if(FacebookIDs != default)
                request.FacebookIDs = FacebookIDs;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookIDsResult>("/Server/GetPlayFabIDsFromFacebookIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook Instant Games identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromFacebookInstantGamesIdsResult> GetPlayFabIDsFromFacebookInstantGamesIds(List<string> FacebookInstantGamesIds, 
            GetPlayFabIDsFromFacebookInstantGamesIdsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromFacebookInstantGamesIdsRequest();
            if(FacebookInstantGamesIds != default)
                request.FacebookInstantGamesIds = FacebookInstantGamesIds;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookInstantGamesIdsResult>("/Server/GetPlayFabIDsFromFacebookInstantGamesIds", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of generic service identifiers. A generic identifier is the
        /// service name plus the service-specific ID for the player, as specified by the title when the generic identifier was
        /// added to the player account.
        /// </summary>
        public static Task<GetPlayFabIDsFromGenericIDsResult> GetPlayFabIDsFromGenericIDs(List<GenericServiceId> GenericIDs, 
            GetPlayFabIDsFromGenericIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromGenericIDsRequest();
            if(GenericIDs != default)
                request.GenericIDs = GenericIDs;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGenericIDsResult>("/Server/GetPlayFabIDsFromGenericIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Nintendo Switch Device identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> GetPlayFabIDsFromNintendoSwitchDeviceIds(List<string> NintendoSwitchDeviceIds, 
            GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest();
            if(NintendoSwitchDeviceIds != default)
                request.NintendoSwitchDeviceIds = NintendoSwitchDeviceIds;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>("/Server/GetPlayFabIDsFromNintendoSwitchDeviceIds", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of PlayStation Network identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromPSNAccountIDsResult> GetPlayFabIDsFromPSNAccountIDs(List<string> PSNAccountIDs, int? IssuerId = default, 
            GetPlayFabIDsFromPSNAccountIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromPSNAccountIDsRequest();
            if(PSNAccountIDs != default)
                request.PSNAccountIDs = PSNAccountIDs;
            if(IssuerId != default)
                request.IssuerId = IssuerId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromPSNAccountIDsResult>("/Server/GetPlayFabIDsFromPSNAccountIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Steam identifiers. The Steam identifiers are the profile
        /// IDs for the user accounts, available as SteamId in the Steamworks Community API calls.
        /// </summary>
        public static Task<GetPlayFabIDsFromSteamIDsResult> GetPlayFabIDsFromSteamIDs(List<string> SteamStringIDs = default, 
            GetPlayFabIDsFromSteamIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromSteamIDsRequest();
            if(SteamStringIDs != default)
                request.SteamStringIDs = SteamStringIDs;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromSteamIDsResult>("/Server/GetPlayFabIDsFromSteamIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of XboxLive identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromXboxLiveIDsResult> GetPlayFabIDsFromXboxLiveIDs(List<string> XboxLiveAccountIDs, string Sandbox = default, 
            GetPlayFabIDsFromXboxLiveIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayFabIDsFromXboxLiveIDsRequest();
            if(XboxLiveAccountIDs != default)
                request.XboxLiveAccountIDs = XboxLiveAccountIDs;
            if(Sandbox != default)
                request.Sandbox = Sandbox;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromXboxLiveIDsResult>("/Server/GetPlayFabIDsFromXboxLiveIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom publisher settings
        /// </summary>
        public static Task<GetPublisherDataResult> GetPublisherData(List<string> Keys, 
            GetPublisherDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPublisherDataRequest();
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPublisherDataResult>("/Server/GetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the configuration information for the specified random results tables for the title, including all ItemId
        /// values and weights
        /// </summary>
        public static Task<GetRandomResultTablesResult> GetRandomResultTables(List<string> TableIDs, string CatalogVersion = default, 
            GetRandomResultTablesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetRandomResultTablesRequest();
            if(TableIDs != default)
                request.TableIDs = TableIDs;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetRandomResultTablesResult>("/Server/GetRandomResultTables", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the associated PlayFab account identifiers for the given set of server custom identifiers.
        /// </summary>
        public static Task<GetServerCustomIDsFromPlayFabIDsResult> GetServerCustomIDsFromPlayFabIDs(List<string> PlayFabIDs, 
            GetServerCustomIDsFromPlayFabIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetServerCustomIDsFromPlayFabIDsRequest();
            if(PlayFabIDs != default)
                request.PlayFabIDs = PlayFabIDs;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetServerCustomIDsFromPlayFabIDsResult>("/Server/GetServerCustomIDsFromPlayFabIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves data stored in a shared group object, as well as the list of members in the group. The server can access all
        /// public and private group data. Shared Groups are designed for sharing data between a very small number of players,
        /// please see our guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<GetSharedGroupDataResult> GetSharedGroupData(string SharedGroupId, bool? GetMembers = default, List<string> Keys = default, 
            GetSharedGroupDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetSharedGroupDataRequest();
            if(SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;
            if(GetMembers != default)
                request.GetMembers = GetMembers;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetSharedGroupDataResult>("/Server/GetSharedGroupData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the set of items defined for the specified store, including all prices defined, for the specified player
        /// </summary>
        public static Task<GetStoreItemsResult> GetStoreItems(string StoreId, string CatalogVersion = default, string PlayFabId = default, 
            GetStoreItemsServerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetStoreItemsServerRequest();
            if(StoreId != default)
                request.StoreId = StoreId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetStoreItemsResult>("/Server/GetStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current server time
        /// </summary>
        public static Task<GetTimeResult> GetTime(
            GetTimeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTimeRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTimeResult>("/Server/GetTime", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings
        /// </summary>
        public static Task<GetTitleDataResult> GetTitleData(List<string> Keys = default, 
            GetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleDataRequest();
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Server/GetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom internal title settings
        /// </summary>
        public static Task<GetTitleDataResult> GetTitleInternalData(List<string> Keys = default, 
            GetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleDataRequest();
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Server/GetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title news feed, as configured in the developer portal
        /// </summary>
        public static Task<GetTitleNewsResult> GetTitleNews(int? Count = default, 
            GetTitleNewsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleNewsRequest();
            if(Count != default)
                request.Count = Count;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleNewsResult>("/Server/GetTitleNews", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user
        /// </summary>
        public static Task<GetUserAccountInfoResult> GetUserAccountInfo(string PlayFabId, 
            GetUserAccountInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserAccountInfoRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserAccountInfoResult>("/Server/GetUserAccountInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets all bans for a user.
        /// </summary>
        public static Task<GetUserBansResult> GetUserBans(string PlayFabId, 
            GetUserBansRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserBansRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserBansResult>("/Server/GetUserBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserInternalData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified user's current inventory of virtual goods
        /// </summary>
        public static Task<GetUserInventoryResult> GetUserInventory(string PlayFabId, 
            GetUserInventoryRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserInventoryRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserInventoryResult>("/Server/GetUserInventory", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserPublisherData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserPublisherInternalData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserPublisherInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which can only be read by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserPublisherReadOnlyData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserPublisherReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which can only be read by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserReadOnlyData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Grants the specified character type to the user. CharacterIds are not globally unique; characterId must be evaluated
        /// with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        public static Task<GrantCharacterToUserResult> GrantCharacterToUser(string CharacterName, string CharacterType, string PlayFabId, 
            GrantCharacterToUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GrantCharacterToUserRequest();
            if(CharacterName != default)
                request.CharacterName = CharacterName;
            if(CharacterType != default)
                request.CharacterType = CharacterType;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GrantCharacterToUserResult>("/Server/GrantCharacterToUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified items to the specified character's inventory
        /// </summary>
        public static Task<GrantItemsToCharacterResult> GrantItemsToCharacter(string CharacterId, string PlayFabId, string Annotation = default, string CatalogVersion = default, List<string> ItemIds = default, 
            GrantItemsToCharacterRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GrantItemsToCharacterRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Annotation != default)
                request.Annotation = Annotation;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(ItemIds != default)
                request.ItemIds = ItemIds;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToCharacterResult>("/Server/GrantItemsToCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified items to the specified user's inventory
        /// </summary>
        public static Task<GrantItemsToUserResult> GrantItemsToUser(List<string> ItemIds, string PlayFabId, string Annotation = default, string CatalogVersion = default, 
            GrantItemsToUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GrantItemsToUserRequest();
            if(ItemIds != default)
                request.ItemIds = ItemIds;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Annotation != default)
                request.Annotation = Annotation;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToUserResult>("/Server/GrantItemsToUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified items to the specified user inventories
        /// </summary>
        public static Task<GrantItemsToUsersResult> GrantItemsToUsers(List<ItemGrant> ItemGrants, string CatalogVersion = default, 
            GrantItemsToUsersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GrantItemsToUsersRequest();
            if(ItemGrants != default)
                request.ItemGrants = ItemGrants;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToUsersResult>("/Server/GrantItemsToUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the custom server identifier, generated by the title, to the user's PlayFab account.
        /// </summary>
        public static Task<LinkServerCustomIdResult> LinkServerCustomId(string PlayFabId, string ServerCustomId, bool? ForceLink = default, 
            LinkServerCustomIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new LinkServerCustomIdRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(ServerCustomId != default)
                request.ServerCustomId = ServerCustomId;
            if(ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<LinkServerCustomIdResult>("/Server/LinkServerCustomId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Xbox Live account associated with the provided access code to the user's PlayFab account
        /// </summary>
        public static Task<LinkXboxAccountResult> LinkXboxAccount(string PlayFabId, string XboxToken, bool? ForceLink = default, 
            LinkXboxAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new LinkXboxAccountRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(XboxToken != default)
                request.XboxToken = XboxToken;
            if(ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<LinkXboxAccountResult>("/Server/LinkXboxAccount", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Securely login a game client from an external server backend using a custom identifier for that player. Server Custom ID
        /// and Client Custom ID are mutually exclusive and cannot be used to retrieve the same player account.
        /// </summary>
        public static Task<ServerLoginResult> LoginWithServerCustomId(bool? CreateAccount = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string ServerCustomId = default, 
            LoginWithServerCustomIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new LoginWithServerCustomIdRequest();
            if(CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if(InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if(PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if(ServerCustomId != default)
                request.ServerCustomId = ServerCustomId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ServerLoginResult>("/Server/LoginWithServerCustomId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Xbox Live Token from an external server backend, returning a session identifier that can
        /// subsequently be used for API calls which require an authenticated user
        /// </summary>
        public static Task<ServerLoginResult> LoginWithXbox(string XboxToken, bool? CreateAccount = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            LoginWithXboxRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new LoginWithXboxRequest();
            if(XboxToken != default)
                request.XboxToken = XboxToken;
            if(CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if(InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ServerLoginResult>("/Server/LoginWithXbox", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using an Xbox ID and Sandbox ID, returning a session identifier that can subsequently be used for API
        /// calls which require an authenticated user
        /// </summary>
        public static Task<ServerLoginResult> LoginWithXboxId(string Sandbox, string XboxId, bool? CreateAccount = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            LoginWithXboxIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new LoginWithXboxIdRequest();
            if(Sandbox != default)
                request.Sandbox = Sandbox;
            if(XboxId != default)
                request.XboxId = XboxId;
            if(CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if(InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ServerLoginResult>("/Server/LoginWithXboxId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Modifies the number of remaining uses of a player's inventory item
        /// </summary>
        public static Task<ModifyItemUsesResult> ModifyItemUses(string ItemInstanceId, string PlayFabId, int UsesToAdd, 
            ModifyItemUsesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ModifyItemUsesRequest();
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(UsesToAdd != default)
                request.UsesToAdd = UsesToAdd;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyItemUsesResult>("/Server/ModifyItemUses", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Moves an item from a character's inventory into another of the users's character's inventory.
        /// </summary>
        public static Task<MoveItemToCharacterFromCharacterResult> MoveItemToCharacterFromCharacter(string GivingCharacterId, string ItemInstanceId, string PlayFabId, string ReceivingCharacterId, 
            MoveItemToCharacterFromCharacterRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new MoveItemToCharacterFromCharacterRequest();
            if(GivingCharacterId != default)
                request.GivingCharacterId = GivingCharacterId;
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(ReceivingCharacterId != default)
                request.ReceivingCharacterId = ReceivingCharacterId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<MoveItemToCharacterFromCharacterResult>("/Server/MoveItemToCharacterFromCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Moves an item from a user's inventory into their character's inventory.
        /// </summary>
        public static Task<MoveItemToCharacterFromUserResult> MoveItemToCharacterFromUser(string CharacterId, string ItemInstanceId, string PlayFabId, 
            MoveItemToCharacterFromUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new MoveItemToCharacterFromUserRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<MoveItemToCharacterFromUserResult>("/Server/MoveItemToCharacterFromUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Moves an item from a character's inventory into the owning user's inventory.
        /// </summary>
        public static Task<MoveItemToUserFromCharacterResult> MoveItemToUserFromCharacter(string CharacterId, string ItemInstanceId, string PlayFabId, 
            MoveItemToUserFromCharacterRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new MoveItemToUserFromCharacterRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<MoveItemToUserFromCharacterResult>("/Server/MoveItemToUserFromCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Informs the PlayFab match-making service that the user specified has left the Game Server Instance
        /// </summary>
        public static Task<NotifyMatchmakerPlayerLeftResult> NotifyMatchmakerPlayerLeft(string LobbyId, string PlayFabId, 
            NotifyMatchmakerPlayerLeftRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new NotifyMatchmakerPlayerLeftRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<NotifyMatchmakerPlayerLeftResult>("/Server/NotifyMatchmakerPlayerLeft", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the virtual goods associated with the coupon to the user's inventory. Coupons can be generated via the
        /// Economy->Catalogs tab in the PlayFab Game Manager.
        /// </summary>
        public static Task<RedeemCouponResult> RedeemCoupon(string CouponCode, string PlayFabId, string CatalogVersion = default, string CharacterId = default, 
            RedeemCouponRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RedeemCouponRequest();
            if(CouponCode != default)
                request.CouponCode = CouponCode;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RedeemCouponResult>("/Server/RedeemCoupon", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates a Game Server session ticket and returns details about the user
        /// </summary>
        public static Task<RedeemMatchmakerTicketResult> RedeemMatchmakerTicket(string LobbyId, string Ticket, 
            RedeemMatchmakerTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RedeemMatchmakerTicketRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(Ticket != default)
                request.Ticket = Ticket;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RedeemMatchmakerTicketResult>("/Server/RedeemMatchmakerTicket", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Set the state of the indicated Game Server Instance. Also update the heartbeat for the instance.
        /// </summary>
        public static Task<RefreshGameServerInstanceHeartbeatResult> RefreshGameServerInstanceHeartbeat(string LobbyId, 
            RefreshGameServerInstanceHeartbeatRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RefreshGameServerInstanceHeartbeatRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RefreshGameServerInstanceHeartbeatResult>("/Server/RefreshGameServerInstanceHeartbeat", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Inform the matchmaker that a new Game Server Instance is added.
        /// </summary>
        public static Task<RegisterGameResponse> RegisterGame(string Build, string GameMode, Region Region, string ServerPort, string LobbyId = default, string ServerIPV4Address = default, string ServerIPV6Address = default, string ServerPublicDNSName = default, Dictionary<string,string> Tags = default, 
            RegisterGameRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RegisterGameRequest();
            if(Build != default)
                request.Build = Build;
            if(GameMode != default)
                request.GameMode = GameMode;
            if(Region != default)
                request.Region = Region;
            if(ServerPort != default)
                request.ServerPort = ServerPort;
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(ServerIPV4Address != default)
                request.ServerIPV4Address = ServerIPV4Address;
            if(ServerIPV6Address != default)
                request.ServerIPV6Address = ServerIPV6Address;
            if(ServerPublicDNSName != default)
                request.ServerPublicDNSName = ServerPublicDNSName;
            if(Tags != default)
                request.Tags = Tags;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RegisterGameResponse>("/Server/RegisterGame", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the specified friend from the the user's friend list
        /// </summary>
        public static Task<EmptyResponse> RemoveFriend(string FriendPlayFabId, string PlayFabId, 
            RemoveFriendRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveFriendRequest();
            if(FriendPlayFabId != default)
                request.FriendPlayFabId = FriendPlayFabId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/RemoveFriend", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the specified generic service identifier from the player's PlayFab account.
        /// </summary>
        public static Task<EmptyResult> RemoveGenericID(GenericServiceId GenericId, string PlayFabId, 
            RemoveGenericIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveGenericIDRequest();
            if(GenericId != default)
                request.GenericId = GenericId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResult>("/Server/RemoveGenericID", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Remove a given tag from a player profile. The tag's namespace is automatically generated based on the source of the tag.
        /// </summary>
        public static Task<RemovePlayerTagResult> RemovePlayerTag(string PlayFabId, string TagName, 
            RemovePlayerTagRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemovePlayerTagRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(TagName != default)
                request.TagName = TagName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RemovePlayerTagResult>("/Server/RemovePlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes users from the set of those able to update the shared data and the set of users in the group. Only users in the
        /// group can remove members. If as a result of the call, zero users remain with access, the group and its associated data
        /// will be deleted. Shared Groups are designed for sharing data between a very small number of players, please see our
        /// guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<RemoveSharedGroupMembersResult> RemoveSharedGroupMembers(List<string> PlayFabIds, string SharedGroupId, 
            RemoveSharedGroupMembersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveSharedGroupMembersRequest();
            if(PlayFabIds != default)
                request.PlayFabIds = PlayFabIds;
            if(SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RemoveSharedGroupMembersResult>("/Server/RemoveSharedGroupMembers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Submit a report about a player (due to bad bahavior, etc.) on behalf of another player, so that customer service
        /// representatives for the title can take action concerning potentially toxic players.
        /// </summary>
        public static Task<ReportPlayerServerResult> ReportPlayer(string ReporteeId, string ReporterId, string Comment = default, 
            ReportPlayerServerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ReportPlayerServerRequest();
            if(ReporteeId != default)
                request.ReporteeId = ReporteeId;
            if(ReporterId != default)
                request.ReporterId = ReporterId;
            if(Comment != default)
                request.Comment = Comment;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ReportPlayerServerResult>("/Server/ReportPlayer", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revoke all active bans for a user.
        /// </summary>
        public static Task<RevokeAllBansForUserResult> RevokeAllBansForUser(string PlayFabId, 
            RevokeAllBansForUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RevokeAllBansForUserRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RevokeAllBansForUserResult>("/Server/RevokeAllBansForUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revoke all active bans specified with BanId.
        /// </summary>
        public static Task<RevokeBansResult> RevokeBans(List<string> BanIds, 
            RevokeBansRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RevokeBansRequest();
            if(BanIds != default)
                request.BanIds = BanIds;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RevokeBansResult>("/Server/RevokeBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revokes access to an item in a user's inventory
        /// </summary>
        public static Task<RevokeInventoryResult> RevokeInventoryItem(string ItemInstanceId, string PlayFabId, string CharacterId = default, 
            RevokeInventoryItemRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RevokeInventoryItemRequest();
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryResult>("/Server/RevokeInventoryItem", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revokes access for up to 25 items across multiple users and characters.
        /// </summary>
        public static Task<RevokeInventoryItemsResult> RevokeInventoryItems(List<RevokeInventoryItem> Items, 
            RevokeInventoryItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RevokeInventoryItemsRequest();
            if(Items != default)
                request.Items = Items;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryItemsResult>("/Server/RevokeInventoryItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Saves push notification template for title
        /// </summary>
        public static Task<SavePushNotificationTemplateResult> SavePushNotificationTemplate(string Name, string AndroidPayload = default, string Id = default, string IOSPayload = default, Dictionary<string,LocalizedPushNotificationProperties> LocalizedPushNotificationTemplates = default, 
            SavePushNotificationTemplateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SavePushNotificationTemplateRequest();
            if(Name != default)
                request.Name = Name;
            if(AndroidPayload != default)
                request.AndroidPayload = AndroidPayload;
            if(Id != default)
                request.Id = Id;
            if(IOSPayload != default)
                request.IOSPayload = IOSPayload;
            if(LocalizedPushNotificationTemplates != default)
                request.LocalizedPushNotificationTemplates = LocalizedPushNotificationTemplates;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SavePushNotificationTemplateResult>("/Server/SavePushNotificationTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Forces an email to be sent to the registered contact email address for the user's account based on an account recovery
        /// email template
        /// </summary>
        public static Task<SendCustomAccountRecoveryEmailResult> SendCustomAccountRecoveryEmail(string EmailTemplateId, string Email = default, string Username = default, 
            SendCustomAccountRecoveryEmailRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SendCustomAccountRecoveryEmailRequest();
            if(EmailTemplateId != default)
                request.EmailTemplateId = EmailTemplateId;
            if(Email != default)
                request.Email = Email;
            if(Username != default)
                request.Username = Username;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SendCustomAccountRecoveryEmailResult>("/Server/SendCustomAccountRecoveryEmail", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sends an email based on an email template to a player's contact email
        /// </summary>
        public static Task<SendEmailFromTemplateResult> SendEmailFromTemplate(string EmailTemplateId, string PlayFabId, 
            SendEmailFromTemplateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SendEmailFromTemplateRequest();
            if(EmailTemplateId != default)
                request.EmailTemplateId = EmailTemplateId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SendEmailFromTemplateResult>("/Server/SendEmailFromTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sends an iOS/Android Push Notification to a specific user, if that user's device has been configured for Push
        /// Notifications in PlayFab. If a user has linked both Android and iOS devices, both will be notified.
        /// </summary>
        public static Task<SendPushNotificationResult> SendPushNotification(string Recipient, List<AdvancedPushPlatformMsg> AdvancedPlatformDelivery = default, string Message = default, PushNotificationPackage Package = default, string Subject = default, List<PushNotificationPlatform> TargetPlatforms = default, 
            SendPushNotificationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SendPushNotificationRequest();
            if(Recipient != default)
                request.Recipient = Recipient;
            if(AdvancedPlatformDelivery != default)
                request.AdvancedPlatformDelivery = AdvancedPlatformDelivery;
            if(Message != default)
                request.Message = Message;
            if(Package != default)
                request.Package = Package;
            if(Subject != default)
                request.Subject = Subject;
            if(TargetPlatforms != default)
                request.TargetPlatforms = TargetPlatforms;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SendPushNotificationResult>("/Server/SendPushNotification", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sends an iOS/Android Push Notification template to a specific user, if that user's device has been configured for Push
        /// Notifications in PlayFab. If a user has linked both Android and iOS devices, both will be notified.
        /// </summary>
        public static Task<SendPushNotificationResult> SendPushNotificationFromTemplate(string PushNotificationTemplateId, string Recipient, 
            SendPushNotificationFromTemplateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SendPushNotificationFromTemplateRequest();
            if(PushNotificationTemplateId != default)
                request.PushNotificationTemplateId = PushNotificationTemplateId;
            if(Recipient != default)
                request.Recipient = Recipient;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SendPushNotificationResult>("/Server/SendPushNotificationFromTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the tag list for a specified user in the friend list of another user
        /// </summary>
        public static Task<EmptyResponse> SetFriendTags(string FriendPlayFabId, string PlayFabId, List<string> Tags, 
            SetFriendTagsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetFriendTagsRequest();
            if(FriendPlayFabId != default)
                request.FriendPlayFabId = FriendPlayFabId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Tags != default)
                request.Tags = Tags;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/SetFriendTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the custom data of the indicated Game Server Instance
        /// </summary>
        public static Task<SetGameServerInstanceDataResult> SetGameServerInstanceData(string GameServerData, string LobbyId, 
            SetGameServerInstanceDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetGameServerInstanceDataRequest();
            if(GameServerData != default)
                request.GameServerData = GameServerData;
            if(LobbyId != default)
                request.LobbyId = LobbyId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetGameServerInstanceDataResult>("/Server/SetGameServerInstanceData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Set the state of the indicated Game Server Instance.
        /// </summary>
        public static Task<SetGameServerInstanceStateResult> SetGameServerInstanceState(string LobbyId, GameInstanceState State, 
            SetGameServerInstanceStateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetGameServerInstanceStateRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(State != default)
                request.State = State;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetGameServerInstanceStateResult>("/Server/SetGameServerInstanceState", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Set custom tags for the specified Game Server Instance
        /// </summary>
        public static Task<SetGameServerInstanceTagsResult> SetGameServerInstanceTags(string LobbyId, Dictionary<string,string> Tags, 
            SetGameServerInstanceTagsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetGameServerInstanceTagsRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;
            if(Tags != default)
                request.Tags = Tags;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetGameServerInstanceTagsResult>("/Server/SetGameServerInstanceTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the player's secret if it is not already set. Player secrets are used to sign API requests. To reset a player's
        /// secret use the Admin or Server API method SetPlayerSecret.
        /// </summary>
        public static Task<SetPlayerSecretResult> SetPlayerSecret(string PlayFabId, string PlayerSecret = default, 
            SetPlayerSecretRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetPlayerSecretRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetPlayerSecretResult>("/Server/SetPlayerSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom publisher settings
        /// </summary>
        public static Task<SetPublisherDataResult> SetPublisherData(string Key, string Value = default, 
            SetPublisherDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetPublisherDataRequest();
            if(Key != default)
                request.Key = Key;
            if(Value != default)
                request.Value = Value;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetPublisherDataResult>("/Server/SetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom title settings
        /// </summary>
        public static Task<SetTitleDataResult> SetTitleData(string Key, string Value = default, 
            SetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetTitleDataRequest();
            if(Key != default)
                request.Key = Key;
            if(Value != default)
                request.Value = Value;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Server/SetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom title settings
        /// </summary>
        public static Task<SetTitleDataResult> SetTitleInternalData(string Key, string Value = default, 
            SetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetTitleDataRequest();
            if(Key != default)
                request.Key = Key;
            if(Value != default)
                request.Value = Value;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Server/SetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the character's balance of the specified virtual currency by the stated amount. It is possible to make a VC
        /// balance negative with this API.
        /// </summary>
        public static Task<ModifyCharacterVirtualCurrencyResult> SubtractCharacterVirtualCurrency(int Amount, string CharacterId, string PlayFabId, string VirtualCurrency, 
            SubtractCharacterVirtualCurrencyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SubtractCharacterVirtualCurrencyRequest();
            if(Amount != default)
                request.Amount = Amount;
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyCharacterVirtualCurrencyResult>("/Server/SubtractCharacterVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the user's balance of the specified virtual currency by the stated amount. It is possible to make a VC
        /// balance negative with this API.
        /// </summary>
        public static Task<ModifyUserVirtualCurrencyResult> SubtractUserVirtualCurrency(int Amount, string PlayFabId, string VirtualCurrency, 
            SubtractUserVirtualCurrencyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SubtractUserVirtualCurrencyRequest();
            if(Amount != default)
                request.Amount = Amount;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Server/SubtractUserVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the custom server identifier from the user's PlayFab account.
        /// </summary>
        public static Task<UnlinkServerCustomIdResult> UnlinkServerCustomId(string PlayFabId, string ServerCustomId, 
            UnlinkServerCustomIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UnlinkServerCustomIdRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(ServerCustomId != default)
                request.ServerCustomId = ServerCustomId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UnlinkServerCustomIdResult>("/Server/UnlinkServerCustomId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Xbox Live account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkXboxAccountResult> UnlinkXboxAccount(string PlayFabId, string XboxToken, 
            UnlinkXboxAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UnlinkXboxAccountRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UnlinkXboxAccountResult>("/Server/UnlinkXboxAccount", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Opens a specific container (ContainerItemInstanceId), with a specific key (KeyItemInstanceId, when required), and
        /// returns the contents of the opened container. If the container (and key when relevant) are consumable (RemainingUses >
        /// 0), their RemainingUses will be decremented, consistent with the operation of ConsumeItem.
        /// </summary>
        public static Task<UnlockContainerItemResult> UnlockContainerInstance(string ContainerItemInstanceId, string PlayFabId, string CatalogVersion = default, string CharacterId = default, string KeyItemInstanceId = default, 
            UnlockContainerInstanceRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UnlockContainerInstanceRequest();
            if(ContainerItemInstanceId != default)
                request.ContainerItemInstanceId = ContainerItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(KeyItemInstanceId != default)
                request.KeyItemInstanceId = KeyItemInstanceId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Server/UnlockContainerInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Searches Player or Character inventory for any ItemInstance matching the given CatalogItemId, if necessary unlocks it
        /// using any appropriate key, and returns the contents of the opened container. If the container (and key when relevant)
        /// are consumable (RemainingUses > 0), their RemainingUses will be decremented, consistent with the operation of
        /// ConsumeItem.
        /// </summary>
        public static Task<UnlockContainerItemResult> UnlockContainerItem(string ContainerItemId, string PlayFabId, string CatalogVersion = default, string CharacterId = default, 
            UnlockContainerItemRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UnlockContainerItemRequest();
            if(ContainerItemId != default)
                request.ContainerItemId = ContainerItemId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Server/UnlockContainerItem", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update the avatar URL of the specified player
        /// </summary>
        public static Task<EmptyResponse> UpdateAvatarUrl(string ImageUrl, string PlayFabId, 
            UpdateAvatarUrlRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateAvatarUrlRequest();
            if(ImageUrl != default)
                request.ImageUrl = ImageUrl;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/UpdateAvatarUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates information of a list of existing bans specified with Ban Ids.
        /// </summary>
        public static Task<UpdateBansResult> UpdateBans(List<UpdateBanRequest> Bans, 
            UpdateBansRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateBansRequest();
            if(Bans != default)
                request.Bans = Bans;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateBansResult>("/Server/UpdateBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user's character which is readable and writable by the client
        /// </summary>
        public static Task<UpdateCharacterDataResult> UpdateCharacterData(string CharacterId, string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCharacterDataRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Server/UpdateCharacterData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user's character which cannot be accessed by the client
        /// </summary>
        public static Task<UpdateCharacterDataResult> UpdateCharacterInternalData(string CharacterId, string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCharacterDataRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Server/UpdateCharacterInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user's character which can only be read by the client
        /// </summary>
        public static Task<UpdateCharacterDataResult> UpdateCharacterReadOnlyData(string CharacterId, string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCharacterDataRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Server/UpdateCharacterReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the specific character
        /// </summary>
        public static Task<UpdateCharacterStatisticsResult> UpdateCharacterStatistics(string CharacterId, string PlayFabId, Dictionary<string,int> CharacterStatistics = default, 
            UpdateCharacterStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCharacterStatisticsRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CharacterStatistics != default)
                request.CharacterStatistics = CharacterStatistics;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterStatisticsResult>("/Server/UpdateCharacterStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the user
        /// </summary>
        public static Task<UpdatePlayerStatisticsResult> UpdatePlayerStatistics(string PlayFabId, List<StatisticUpdate> Statistics, bool? ForceUpdate = default, 
            UpdatePlayerStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdatePlayerStatisticsRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Statistics != default)
                request.Statistics = Statistics;
            if(ForceUpdate != default)
                request.ForceUpdate = ForceUpdate;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdatePlayerStatisticsResult>("/Server/UpdatePlayerStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds, updates, and removes data keys for a shared group object. If the permission is set to Public, all fields updated
        /// or added in this call will be readable by users not in the group. By default, data permissions are set to Private.
        /// Regardless of the permission setting, only members of the group (and the server) can update the data. Shared Groups are
        /// designed for sharing data between a very small number of players, please see our guide:
        /// https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<UpdateSharedGroupDataResult> UpdateSharedGroupData(string SharedGroupId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateSharedGroupDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateSharedGroupDataRequest();
            if(SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateSharedGroupDataResult>("/Server/UpdateSharedGroupData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserInternalData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, 
            UpdateUserInternalDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserInternalDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value pair data tagged to the specified item, which is read-only from the client.
        /// </summary>
        public static Task<EmptyResponse> UpdateUserInventoryItemCustomData(string ItemInstanceId, string PlayFabId, string CharacterId = default, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, 
            UpdateUserInventoryItemDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserInventoryItemDataRequest();
            if(ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/UpdateUserInventoryItemCustomData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserPublisherData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the publisher-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserPublisherInternalData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, 
            UpdateUserInternalDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserInternalDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserPublisherInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the publisher-specific custom data for the user which can only be read by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserPublisherReadOnlyData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserPublisherReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user which can only be read by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserReadOnlyData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            UpdateUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Data != default)
                request.Data = Data;
            if(KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if(Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a character-based event into PlayStream.
        /// </summary>
        public static Task<WriteEventResponse> WriteCharacterEvent(string CharacterId, string EventName, string PlayFabId, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            WriteServerCharacterEventRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new WriteServerCharacterEventRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(EventName != default)
                request.EventName = EventName;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Body != default)
                request.Body = Body;
            if(Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Server/WriteCharacterEvent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a player-based event into PlayStream.
        /// </summary>
        public static Task<WriteEventResponse> WritePlayerEvent(string EventName, string PlayFabId, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            WriteServerPlayerEventRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new WriteServerPlayerEventRequest();
            if(EventName != default)
                request.EventName = EventName;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Body != default)
                request.Body = Body;
            if(Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Server/WritePlayerEvent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a title-based event into PlayStream.
        /// </summary>
        public static Task<WriteEventResponse> WriteTitleEvent(string EventName, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            WriteTitleEventRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new WriteTitleEventRequest();
            if(EventName != default)
                request.EventName = EventName;
            if(Body != default)
                request.Body = Body;
            if(Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Server/WriteTitleEvent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }


    }
}

#endif
