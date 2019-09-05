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
    public static class ServerAPI
    {
        static ServerAPI() {}


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
        /// Increments the character's balance of the specified virtual currency by the stated amount
        /// </summary>
        /// <param name="Amount">Amount to be added to the character balance of the specified virtual currency. Maximum VC balance is Int32 (2,147,483,647). Any increase over this value will be discarded. (Required)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user whose virtual currency balance is to be incremented. (Required)</param>
        /// <param name="VirtualCurrency">Name of the virtual currency which is to be incremented. (Required)</param>
        public static Task<ModifyCharacterVirtualCurrencyResult> AddCharacterVirtualCurrency(int Amount, string CharacterId, string PlayFabId, string VirtualCurrency, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddCharacterVirtualCurrencyRequest request = new AddCharacterVirtualCurrencyRequest()
            {
                Amount = Amount,
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                VirtualCurrency = VirtualCurrency,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyCharacterVirtualCurrencyResult>("/Server/AddCharacterVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the Friend user to the friendlist of the user with PlayFabId. At least one of
        /// FriendPlayFabId,FriendUsername,FriendEmail, or FriendTitleDisplayName should be initialized.
        /// </summary>
        /// <param name="FriendEmail">Email address of the user being added. (Optional)</param>
        /// <param name="FriendPlayFabId">The PlayFab identifier of the user being added. (Optional)</param>
        /// <param name="FriendTitleDisplayName">Title-specific display name of the user to being added. (Optional)</param>
        /// <param name="FriendUsername">The PlayFab username of the user being added (Optional)</param>
        /// <param name="PlayFabId">PlayFab identifier of the player to add a new friend. (Required)</param>
        public static Task<EmptyResponse> AddFriend(string PlayFabId, string FriendEmail = default, string FriendPlayFabId = default, string FriendTitleDisplayName = default, string FriendUsername = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddFriendRequest request = new AddFriendRequest()
            {
                PlayFabId = PlayFabId,
                FriendEmail = FriendEmail,
                FriendPlayFabId = FriendPlayFabId,
                FriendTitleDisplayName = FriendTitleDisplayName,
                FriendUsername = FriendUsername,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/AddFriend", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified generic service identifier to the player's PlayFab account. This is designed to allow for a PlayFab
        /// ID lookup of any arbitrary service identifier a title wants to add. This identifier should never be used as
        /// authentication credentials, as the intent is that it is easily accessible by other players.
        /// </summary>
        /// <param name="GenericId">Generic service identifier to add to the player account. (Required)</param>
        /// <param name="PlayFabId">PlayFabId of the user to link. (Required)</param>
        public static Task<EmptyResult> AddGenericID(GenericServiceId GenericId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddGenericIDRequest request = new AddGenericIDRequest()
            {
                GenericId = GenericId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResult>("/Server/AddGenericID", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds a given tag to a player profile. The tag's namespace is automatically generated based on the source of the tag.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="TagName">Unique tag for player profile. (Required)</param>
        public static Task<AddPlayerTagResult> AddPlayerTag(string PlayFabId, string TagName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddPlayerTagRequest request = new AddPlayerTagRequest()
            {
                PlayFabId = PlayFabId,
                TagName = TagName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AddPlayerTagResult>("/Server/AddPlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds users to the set of those able to update both the shared data, as well as the set of users in the group. Only users
        /// in the group (and the server) can add new members. Shared Groups are designed for sharing data between a very small
        /// number of players, please see our guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        /// <param name="PlayFabIds">An array of unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="SharedGroupId">Unique identifier for the shared group. (Required)</param>
        public static Task<AddSharedGroupMembersResult> AddSharedGroupMembers(List<string> PlayFabIds, string SharedGroupId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddSharedGroupMembersRequest request = new AddSharedGroupMembersRequest()
            {
                PlayFabIds = PlayFabIds,
                SharedGroupId = SharedGroupId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AddSharedGroupMembersResult>("/Server/AddSharedGroupMembers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increments the user's balance of the specified virtual currency by the stated amount
        /// </summary>
        /// <param name="Amount">Amount to be added to the user balance of the specified virtual currency. Maximum VC balance is Int32 (2,147,483,647). Any increase over this value will be discarded. (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user whose virtual currency balance is to be increased. (Required)</param>
        /// <param name="VirtualCurrency">Name of the virtual currency which is to be incremented. (Required)</param>
        public static Task<ModifyUserVirtualCurrencyResult> AddUserVirtualCurrency(int Amount, string PlayFabId, string VirtualCurrency, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest()
            {
                Amount = Amount,
                PlayFabId = PlayFabId,
                VirtualCurrency = VirtualCurrency,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Server/AddUserVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validated a client's session ticket, and if successful, returns details for that user
        /// </summary>
        /// <param name="SessionTicket">Session ticket as issued by a PlayFab client login API. (Required)</param>
        public static Task<AuthenticateSessionTicketResult> AuthenticateSessionTicket(string SessionTicket, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AuthenticateSessionTicketRequest request = new AuthenticateSessionTicketRequest()
            {
                SessionTicket = SessionTicket,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AuthenticateSessionTicketResult>("/Server/AuthenticateSessionTicket", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Awards the specified users the specified Steam achievements
        /// </summary>
        /// <param name="Achievements">Array of achievements to grant and the users to whom they are to be granted. (Required)</param>
        public static Task<AwardSteamAchievementResult> AwardSteamAchievement(List<AwardSteamAchievementItem> Achievements, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AwardSteamAchievementRequest request = new AwardSteamAchievementRequest()
            {
                Achievements = Achievements,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AwardSteamAchievementResult>("/Server/AwardSteamAchievement", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Bans users by PlayFab ID with optional IP address, or MAC address for the provided game.
        /// </summary>
        /// <param name="Bans">List of ban requests to be applied. Maximum 100. (Required)</param>
        public static Task<BanUsersResult> BanUsers(List<BanRequest> Bans, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            BanUsersRequest request = new BanUsersRequest()
            {
                Bans = Bans,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<BanUsersResult>("/Server/BanUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Consume uses of a consumable item. When all uses are consumed, it will be removed from the player's inventory.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ConsumeCount">Number of uses to consume from the item. (Required)</param>
        /// <param name="ItemInstanceId">Unique instance identifier of the item to be consumed. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<ConsumeItemResult> ConsumeItem(int ConsumeCount, string ItemInstanceId, string PlayFabId, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ConsumeItemRequest request = new ConsumeItemRequest()
            {
                ConsumeCount = ConsumeCount,
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);

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
        /// <param name="SharedGroupId">Unique identifier for the shared group (a random identifier will be assigned, if one is not specified). (Optional)</param>
        public static Task<CreateSharedGroupResult> CreateSharedGroup(string SharedGroupId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateSharedGroupRequest request = new CreateSharedGroupRequest()
            {
                SharedGroupId = SharedGroupId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateSharedGroupResult>("/Server/CreateSharedGroup", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes the specific character ID from the specified user.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="SaveCharacterInventory">If true, the character's inventory will be transferred up to the owning user; otherwise, this request will purge those items. (Required)</param>
        public static Task<DeleteCharacterFromUserResult> DeleteCharacterFromUser(string CharacterId, string PlayFabId, bool SaveCharacterInventory, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteCharacterFromUserRequest request = new DeleteCharacterFromUserRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                SaveCharacterInventory = SaveCharacterInventory,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeleteCharacterFromUserResult>("/Server/DeleteCharacterFromUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a user's player account from a title and deletes all associated data
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<DeletePlayerResult> DeletePlayer(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeletePlayerRequest request = new DeletePlayerRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeletePlayerResult>("/Server/DeletePlayer", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes push notification template for title
        /// </summary>
        /// <param name="PushNotificationTemplateId">Id of the push notification template to be deleted. (Required)</param>
        public static Task<DeletePushNotificationTemplateResult> DeletePushNotificationTemplate(string PushNotificationTemplateId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeletePushNotificationTemplateRequest request = new DeletePushNotificationTemplateRequest()
            {
                PushNotificationTemplateId = PushNotificationTemplateId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeletePushNotificationTemplateResult>("/Server/DeletePushNotificationTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a shared group, freeing up the shared group ID to be reused for a new group. Shared Groups are designed for
        /// sharing data between a very small number of players, please see our guide:
        /// https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        /// <param name="SharedGroupId">Unique identifier for the shared group. (Required)</param>
        public static Task<EmptyResponse> DeleteSharedGroup(string SharedGroupId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteSharedGroupRequest request = new DeleteSharedGroupRequest()
            {
                SharedGroupId = SharedGroupId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/DeleteSharedGroup", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Inform the matchmaker that a Game Server Instance is removed.
        /// </summary>
        /// <param name="LobbyId">Unique identifier for the Game Server Instance that is being deregistered. (Required)</param>
        public static Task<DeregisterGameResponse> DeregisterGame(string LobbyId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeregisterGameRequest request = new DeregisterGameRequest()
            {
                LobbyId = LobbyId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeregisterGameResponse>("/Server/DeregisterGame", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns the result of an evaluation of a Random Result Table - the ItemId from the game Catalog which would have been
        /// added to the player inventory, if the Random Result Table were added via a Bundle or a call to UnlockContainer.
        /// </summary>
        /// <param name="CatalogVersion">Specifies the catalog version that should be used to evaluate the Random Result Table. If unspecified, uses default/primary catalog. (Optional)</param>
        /// <param name="TableId">The unique identifier of the Random Result Table to use. (Required)</param>
        public static Task<EvaluateRandomResultTableResult> EvaluateRandomResultTable(string TableId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            EvaluateRandomResultTableRequest request = new EvaluateRandomResultTableRequest()
            {
                TableId = TableId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EvaluateRandomResultTableResult>("/Server/EvaluateRandomResultTable", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Executes a CloudScript function, with the 'currentPlayerId' variable set to the specified PlayFabId parameter value.
        /// </summary>
        /// <param name="FunctionName">The name of the CloudScript function to execute (Required)</param>
        /// <param name="FunctionParameter">Object that is passed in to the function as the first argument (Optional)</param>
        /// <param name="GeneratePlayStreamEvent">Generate a 'player_executed_cloudscript' PlayStream event containing the results of the function execution and other contextual information. This event will show up in the PlayStream debugger console for the player in Game Manager. (Optional)</param>
        /// <param name="PlayFabId">The unique user identifier for the player on whose behalf the script is being run (Required)</param>
        /// <param name="RevisionSelection">Option for which revision of the CloudScript to execute. 'Latest' executes the most recently created revision, 'Live' executes the current live, published revision, and 'Specific' executes the specified revision. The default value is 'Specific', if the SpeificRevision parameter is specified, otherwise it is 'Live'. (Optional)</param>
        /// <param name="SpecificRevision">The specivic revision to execute, when RevisionSelection is set to 'Specific' (Optional)</param>
        public static Task<ExecuteCloudScriptResult> ExecuteCloudScript(string FunctionName, string PlayFabId, object FunctionParameter = default, bool? GeneratePlayStreamEvent = default, CloudScriptRevisionOption? RevisionSelection = default, int? SpecificRevision = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ExecuteCloudScriptServerRequest request = new ExecuteCloudScriptServerRequest()
            {
                FunctionName = FunctionName,
                PlayFabId = PlayFabId,
                FunctionParameter = FunctionParameter,
                GeneratePlayStreamEvent = GeneratePlayStreamEvent,
                RevisionSelection = RevisionSelection,
                SpecificRevision = SpecificRevision,
            };

            var context = GetContext(customAuthContext);

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
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetAllSegmentsRequest request = new GetAllSegmentsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetAllSegmentsResult>("/Server/GetAllSegments", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all of the characters that belong to a specific user. CharacterIds are not globally unique; characterId must be
        /// evaluated with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<ListUsersCharactersResult> GetAllUsersCharacters(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListUsersCharactersRequest request = new ListUsersCharactersRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListUsersCharactersResult>("/Server/GetAllUsersCharacters", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified version of the title's catalog of virtual goods, including all defined properties
        /// </summary>
        /// <param name="CatalogVersion">Which catalog is being requested. If null, uses the default catalog. (Optional)</param>
        public static Task<GetCatalogItemsResult> GetCatalogItems(string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCatalogItemsRequest request = new GetCatalogItemsRequest()
            {
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCatalogItemsResult>("/Server/GetCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetCharacterDataResult> GetCharacterData(string CharacterId, string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterDataRequest request = new GetCharacterDataRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Server/GetCharacterData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user's character which cannot be accessed by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetCharacterDataResult> GetCharacterInternalData(string CharacterId, string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterDataRequest request = new GetCharacterDataRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Server/GetCharacterInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified character's current inventory of virtual goods
        /// </summary>
        /// <param name="CatalogVersion">Used to limit results to only those from a specific catalog version. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetCharacterInventoryResult> GetCharacterInventory(string CharacterId, string PlayFabId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterInventoryRequest request = new GetCharacterInventoryRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterInventoryResult>("/Server/GetCharacterInventory", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        /// <param name="CharacterType">Optional character type on which to filter the leaderboard entries. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="StartPosition">First entry in the leaderboard to be retrieved. (Required)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        public static Task<GetCharacterLeaderboardResult> GetCharacterLeaderboard(int MaxResultsCount, int StartPosition, string StatisticName, string CharacterType = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterLeaderboardRequest request = new GetCharacterLeaderboardRequest()
            {
                MaxResultsCount = MaxResultsCount,
                StartPosition = StartPosition,
                StatisticName = StatisticName,
                CharacterType = CharacterType,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterLeaderboardResult>("/Server/GetCharacterLeaderboard", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user's character which can only be read by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetCharacterDataResult> GetCharacterReadOnlyData(string CharacterId, string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterDataRequest request = new GetCharacterDataRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Server/GetCharacterReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details of all title-specific statistics for the specific character
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetCharacterStatisticsResult> GetCharacterStatistics(string CharacterId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterStatisticsRequest request = new GetCharacterStatisticsRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

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
        /// <param name="HttpMethod">HTTP method to fetch item - GET or HEAD. Use HEAD when only fetching metadata. Default is GET. (Optional)</param>
        /// <param name="Key">Key of the content item to fetch, usually formatted as a path, e.g. images/a.png (Required)</param>
        /// <param name="ThruCDN">True to download through CDN. CDN provides higher download bandwidth and lower latency. However, if you want the latest, non-cached version of the content during development, set this to false. Default is true. (Optional)</param>
        public static Task<GetContentDownloadUrlResult> GetContentDownloadUrl(string Key, string HttpMethod = default, bool? ThruCDN = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetContentDownloadUrlRequest request = new GetContentDownloadUrlRequest()
            {
                Key = Key,
                HttpMethod = HttpMethod,
                ThruCDN = ThruCDN,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetContentDownloadUrlResult>("/Server/GetContentDownloadUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked friends of the given player for the given statistic, starting from the indicated point in the
        /// leaderboard
        /// </summary>
        /// <param name="IncludeFacebookFriends">Indicates whether Facebook friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="IncludeSteamFriends">Indicates whether Steam service friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="PlayFabId">The player whose friend leaderboard to get (Required)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StartPosition">Position in the leaderboard to start this listing (defaults to the first entry). (Required)</param>
        /// <param name="StatisticName">Statistic used to rank friends for this leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        /// <param name="XboxToken">Xbox token if Xbox friends should be included. Requires Xbox be configured on PlayFab. (Optional)</param>
        public static Task<GetLeaderboardResult> GetFriendLeaderboard(int MaxResultsCount, string PlayFabId, int StartPosition, string StatisticName, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, string XboxToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetFriendLeaderboardRequest request = new GetFriendLeaderboardRequest()
            {
                MaxResultsCount = MaxResultsCount,
                PlayFabId = PlayFabId,
                StartPosition = StartPosition,
                StatisticName = StatisticName,
                IncludeFacebookFriends = IncludeFacebookFriends,
                IncludeSteamFriends = IncludeSteamFriends,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Server/GetFriendLeaderboard", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current friends for the user with PlayFabId, constrained to users who have PlayFab accounts. Friends from
        /// linked accounts (Facebook, Steam) are also included. You may optionally exclude some linked services' friends.
        /// </summary>
        /// <param name="IncludeFacebookFriends">Indicates whether Facebook friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="IncludeSteamFriends">Indicates whether Steam service friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="PlayFabId">PlayFab identifier of the player whose friend list to get. (Required)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="XboxToken">Xbox token if Xbox friends should be included. Requires Xbox be configured on PlayFab. (Optional)</param>
        public static Task<GetFriendsListResult> GetFriendsList(string PlayFabId, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, PlayerProfileViewConstraints ProfileConstraints = default, string XboxToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetFriendsListRequest request = new GetFriendsListRequest()
            {
                PlayFabId = PlayFabId,
                IncludeFacebookFriends = IncludeFacebookFriends,
                IncludeSteamFriends = IncludeSteamFriends,
                ProfileConstraints = ProfileConstraints,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetFriendsListResult>("/Server/GetFriendsList", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StartPosition">First entry in the leaderboard to be retrieved. (Required)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        public static Task<GetLeaderboardResult> GetLeaderboard(int MaxResultsCount, int StartPosition, string StatisticName, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardRequest request = new GetLeaderboardRequest()
            {
                MaxResultsCount = MaxResultsCount,
                StartPosition = StartPosition,
                StatisticName = StatisticName,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Server/GetLeaderboard", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, centered on the requested user
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="CharacterType">Optional character type on which to filter the leaderboard entries. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        public static Task<GetLeaderboardAroundCharacterResult> GetLeaderboardAroundCharacter(string CharacterId, int MaxResultsCount, string PlayFabId, string StatisticName, string CharacterType = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardAroundCharacterRequest request = new GetLeaderboardAroundCharacterRequest()
            {
                CharacterId = CharacterId,
                MaxResultsCount = MaxResultsCount,
                PlayFabId = PlayFabId,
                StatisticName = StatisticName,
                CharacterType = CharacterType,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundCharacterResult>("/Server/GetLeaderboardAroundCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, centered on the currently signed-in user
        /// </summary>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        public static Task<GetLeaderboardAroundUserResult> GetLeaderboardAroundUser(int MaxResultsCount, string PlayFabId, string StatisticName, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardAroundUserRequest request = new GetLeaderboardAroundUserRequest()
            {
                MaxResultsCount = MaxResultsCount,
                PlayFabId = PlayFabId,
                StatisticName = StatisticName,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundUserResult>("/Server/GetLeaderboardAroundUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of all of the user's characters for the given statistic.
        /// </summary>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        public static Task<GetLeaderboardForUsersCharactersResult> GetLeaderboardForUserCharacters(int MaxResultsCount, string PlayFabId, string StatisticName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardForUsersCharactersRequest request = new GetLeaderboardForUsersCharactersRequest()
            {
                MaxResultsCount = MaxResultsCount,
                PlayFabId = PlayFabId,
                StatisticName = StatisticName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardForUsersCharactersResult>("/Server/GetLeaderboardForUserCharacters", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns whatever info is requested in the response for the user. Note that PII (like email address, facebook id) may be
        /// returned. All parameters default to false.
        /// </summary>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Required)</param>
        /// <param name="PlayFabId">PlayFabId of the user whose data will be returned (Required)</param>
        public static Task<GetPlayerCombinedInfoResult> GetPlayerCombinedInfo(GetPlayerCombinedInfoRequestParams InfoRequestParameters, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
            {
                InfoRequestParameters = InfoRequestParameters,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerCombinedInfoResult>("/Server/GetPlayerCombinedInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the player's profile
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        public static Task<GetPlayerProfileResult> GetPlayerProfile(string PlayFabId, PlayerProfileViewConstraints ProfileConstraints = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerProfileRequest request = new GetPlayerProfileRequest()
            {
                PlayFabId = PlayFabId,
                ProfileConstraints = ProfileConstraints,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerProfileResult>("/Server/GetPlayerProfile", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all segments that a player currently belongs to at this moment in time.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetPlayerSegmentsResult> GetPlayerSegments(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayersSegmentsRequest request = new GetPlayersSegmentsRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

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
        /// <param name="ContinuationToken">Continuation token if retrieving subsequent pages of results. (Optional)</param>
        /// <param name="MaxBatchSize">Maximum number of profiles to load. Default is 1,000. Maximum is 10,000. (Optional)</param>
        /// <param name="SecondsToLive">Number of seconds to keep the continuation token active. After token expiration it is not possible to continue paging results. Default is 300 (5 minutes). Maximum is 1,800 (30 minutes). (Optional)</param>
        /// <param name="SegmentId">Unique identifier for this segment. (Required)</param>
        public static Task<GetPlayersInSegmentResult> GetPlayersInSegment(string SegmentId, string ContinuationToken = default, uint? MaxBatchSize = default, uint? SecondsToLive = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayersInSegmentRequest request = new GetPlayersInSegmentRequest()
            {
                SegmentId = SegmentId,
                ContinuationToken = ContinuationToken,
                MaxBatchSize = MaxBatchSize,
                SecondsToLive = SecondsToLive,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayersInSegmentResult>("/Server/GetPlayersInSegment", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current version and values for the indicated statistics, for the local player.
        /// </summary>
        /// <param name="PlayFabId">user for whom statistics are being requested (Required)</param>
        /// <param name="StatisticNames">statistics to return (Optional)</param>
        /// <param name="StatisticNameVersions">statistics to return, if StatisticNames is not set (only statistics which have a version matching that provided will be returned) (Optional)</param>
        public static Task<GetPlayerStatisticsResult> GetPlayerStatistics(string PlayFabId, List<string> StatisticNames = default, List<StatisticNameVersion> StatisticNameVersions = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest()
            {
                PlayFabId = PlayFabId,
                StatisticNames = StatisticNames,
                StatisticNameVersions = StatisticNameVersions,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticsResult>("/Server/GetPlayerStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the information on the available versions of the specified statistic.
        /// </summary>
        /// <param name="StatisticName">unique name of the statistic (Optional)</param>
        public static Task<GetPlayerStatisticVersionsResult> GetPlayerStatisticVersions(string StatisticName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerStatisticVersionsRequest request = new GetPlayerStatisticVersionsRequest()
            {
                StatisticName = StatisticName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticVersionsResult>("/Server/GetPlayerStatisticVersions", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get all tags with a given Namespace (optional) from a player profile.
        /// </summary>
        /// <param name="Namespace">Optional namespace to filter results by (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetPlayerTagsResult> GetPlayerTags(string PlayFabId, string Namespace = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerTagsRequest request = new GetPlayerTagsRequest()
            {
                PlayFabId = PlayFabId,
                Namespace = Namespace,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTagsResult>("/Server/GetPlayerTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook identifiers.
        /// </summary>
        /// <param name="FacebookIDs">Array of unique Facebook identifiers for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromFacebookIDsResult> GetPlayFabIDsFromFacebookIDs(List<string> FacebookIDs, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromFacebookIDsRequest request = new GetPlayFabIDsFromFacebookIDsRequest()
            {
                FacebookIDs = FacebookIDs,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookIDsResult>("/Server/GetPlayFabIDsFromFacebookIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook Instant Games identifiers.
        /// </summary>
        /// <param name="FacebookInstantGamesIds">Array of unique Facebook Instant Games identifiers for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromFacebookInstantGamesIdsResult> GetPlayFabIDsFromFacebookInstantGamesIds(List<string> FacebookInstantGamesIds, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromFacebookInstantGamesIdsRequest request = new GetPlayFabIDsFromFacebookInstantGamesIdsRequest()
            {
                FacebookInstantGamesIds = FacebookInstantGamesIds,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookInstantGamesIdsResult>("/Server/GetPlayFabIDsFromFacebookInstantGamesIds", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of generic service identifiers. A generic identifier is the
        /// service name plus the service-specific ID for the player, as specified by the title when the generic identifier was
        /// added to the player account.
        /// </summary>
        /// <param name="GenericIDs">Array of unique generic service identifiers for which the title needs to get PlayFab identifiers. Currently limited to a maximum of 10 in a single request. (Required)</param>
        public static Task<GetPlayFabIDsFromGenericIDsResult> GetPlayFabIDsFromGenericIDs(List<GenericServiceId> GenericIDs, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromGenericIDsRequest request = new GetPlayFabIDsFromGenericIDsRequest()
            {
                GenericIDs = GenericIDs,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGenericIDsResult>("/Server/GetPlayFabIDsFromGenericIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Nintendo Switch Device identifiers.
        /// </summary>
        /// <param name="NintendoSwitchDeviceIds">Array of unique Nintendo Switch Device identifiers for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> GetPlayFabIDsFromNintendoSwitchDeviceIds(List<string> NintendoSwitchDeviceIds, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request = new GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest()
            {
                NintendoSwitchDeviceIds = NintendoSwitchDeviceIds,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>("/Server/GetPlayFabIDsFromNintendoSwitchDeviceIds", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of PlayStation Network identifiers.
        /// </summary>
        /// <param name="IssuerId">Id of the PSN issuer environment. If null, defaults to 256 (production) (Optional)</param>
        /// <param name="PSNAccountIDs">Array of unique PlayStation Network identifiers for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromPSNAccountIDsResult> GetPlayFabIDsFromPSNAccountIDs(List<string> PSNAccountIDs, int? IssuerId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromPSNAccountIDsRequest request = new GetPlayFabIDsFromPSNAccountIDsRequest()
            {
                PSNAccountIDs = PSNAccountIDs,
                IssuerId = IssuerId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromPSNAccountIDsResult>("/Server/GetPlayFabIDsFromPSNAccountIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Steam identifiers. The Steam identifiers are the profile
        /// IDs for the user accounts, available as SteamId in the Steamworks Community API calls.
        /// </summary>
        /// <param name="SteamStringIDs">Array of unique Steam identifiers (Steam profile IDs) for which the title needs to get PlayFab identifiers. (Optional)</param>
        public static Task<GetPlayFabIDsFromSteamIDsResult> GetPlayFabIDsFromSteamIDs(List<string> SteamStringIDs = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromSteamIDsRequest request = new GetPlayFabIDsFromSteamIDsRequest()
            {
                SteamStringIDs = SteamStringIDs,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromSteamIDsResult>("/Server/GetPlayFabIDsFromSteamIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of XboxLive identifiers.
        /// </summary>
        /// <param name="Sandbox">The ID of Xbox Live sandbox. (Optional)</param>
        /// <param name="XboxLiveAccountIDs">Array of unique Xbox Live account identifiers for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromXboxLiveIDsResult> GetPlayFabIDsFromXboxLiveIDs(List<string> XboxLiveAccountIDs, string Sandbox = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromXboxLiveIDsRequest request = new GetPlayFabIDsFromXboxLiveIDsRequest()
            {
                XboxLiveAccountIDs = XboxLiveAccountIDs,
                Sandbox = Sandbox,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromXboxLiveIDsResult>("/Server/GetPlayFabIDsFromXboxLiveIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom publisher settings
        /// </summary>
        /// <param name="Keys"> array of keys to get back data from the Publisher data blob, set by the admin tools (Required)</param>
        public static Task<GetPublisherDataResult> GetPublisherData(List<string> Keys, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPublisherDataRequest request = new GetPublisherDataRequest()
            {
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPublisherDataResult>("/Server/GetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the configuration information for the specified random results tables for the title, including all ItemId
        /// values and weights
        /// </summary>
        /// <param name="CatalogVersion">Specifies the catalog version that should be used to retrieve the Random Result Tables. If unspecified, uses default/primary catalog. (Optional)</param>
        /// <param name="TableIDs">The unique identifier of the Random Result Table to use. (Required)</param>
        public static Task<GetRandomResultTablesResult> GetRandomResultTables(List<string> TableIDs, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetRandomResultTablesRequest request = new GetRandomResultTablesRequest()
            {
                TableIDs = TableIDs,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetRandomResultTablesResult>("/Server/GetRandomResultTables", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the associated PlayFab account identifiers for the given set of server custom identifiers.
        /// </summary>
        /// <param name="PlayFabIDs">Array of unique PlayFab player identifiers for which the title needs to get server custom identifiers. Cannot contain more than 25 identifiers. (Required)</param>
        public static Task<GetServerCustomIDsFromPlayFabIDsResult> GetServerCustomIDsFromPlayFabIDs(List<string> PlayFabIDs, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetServerCustomIDsFromPlayFabIDsRequest request = new GetServerCustomIDsFromPlayFabIDsRequest()
            {
                PlayFabIDs = PlayFabIDs,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetServerCustomIDsFromPlayFabIDsResult>("/Server/GetServerCustomIDsFromPlayFabIDs", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves data stored in a shared group object, as well as the list of members in the group. The server can access all
        /// public and private group data. Shared Groups are designed for sharing data between a very small number of players,
        /// please see our guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        /// <param name="GetMembers">If true, return the list of all members of the shared group. (Optional)</param>
        /// <param name="Keys">Specific keys to retrieve from the shared group (if not specified, all keys will be returned, while an empty array indicates that no keys should be returned). (Optional)</param>
        /// <param name="SharedGroupId">Unique identifier for the shared group. (Required)</param>
        public static Task<GetSharedGroupDataResult> GetSharedGroupData(string SharedGroupId, bool? GetMembers = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetSharedGroupDataRequest request = new GetSharedGroupDataRequest()
            {
                SharedGroupId = SharedGroupId,
                GetMembers = GetMembers,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetSharedGroupDataResult>("/Server/GetSharedGroupData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the set of items defined for the specified store, including all prices defined, for the specified player
        /// </summary>
        /// <param name="CatalogVersion">Catalog version to store items from. Use default catalog version if null (Optional)</param>
        /// <param name="PlayFabId">Optional identifier for the player to use in requesting the store information - if used, segment overrides will be applied (Optional)</param>
        /// <param name="StoreId">Unqiue identifier for the store which is being requested (Required)</param>
        public static Task<GetStoreItemsResult> GetStoreItems(string StoreId, string CatalogVersion = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetStoreItemsServerRequest request = new GetStoreItemsServerRequest()
            {
                StoreId = StoreId,
                CatalogVersion = CatalogVersion,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetStoreItemsResult>("/Server/GetStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current server time
        /// </summary>
        public static Task<GetTimeResult> GetTime(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTimeRequest request = new GetTimeRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTimeResult>("/Server/GetTime", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings
        /// </summary>
        /// <param name="Keys">Specific keys to search for in the title data (leave null to get all keys) (Optional)</param>
        public static Task<GetTitleDataResult> GetTitleData(List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitleDataRequest request = new GetTitleDataRequest()
            {
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Server/GetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom internal title settings
        /// </summary>
        /// <param name="Keys">Specific keys to search for in the title data (leave null to get all keys) (Optional)</param>
        public static Task<GetTitleDataResult> GetTitleInternalData(List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitleDataRequest request = new GetTitleDataRequest()
            {
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Server/GetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title news feed, as configured in the developer portal
        /// </summary>
        /// <param name="Count">Limits the results to the last n entries. Defaults to 10 if not set. (Optional)</param>
        public static Task<GetTitleNewsResult> GetTitleNews(int? Count = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitleNewsRequest request = new GetTitleNewsRequest()
            {
                Count = Count,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitleNewsResult>("/Server/GetTitleNews", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetUserAccountInfoResult> GetUserAccountInfo(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserAccountInfoRequest request = new GetUserAccountInfoRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserAccountInfoResult>("/Server/GetUserAccountInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets all bans for a user.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetUserBansResult> GetUserBans(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserBansRequest request = new GetUserBansRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserBansResult>("/Server/GetUserBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetUserDataResult> GetUserData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetUserDataResult> GetUserInternalData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified user's current inventory of virtual goods
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetUserInventoryResult> GetUserInventory(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserInventoryResult>("/Server/GetUserInventory", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetUserDataResult> GetUserPublisherData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetUserDataResult> GetUserPublisherInternalData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserPublisherInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which can only be read by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetUserDataResult> GetUserPublisherReadOnlyData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserPublisherReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which can only be read by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        public static Task<GetUserDataResult> GetUserReadOnlyData(string PlayFabId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                PlayFabId = PlayFabId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Server/GetUserReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Grants the specified character type to the user. CharacterIds are not globally unique; characterId must be evaluated
        /// with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        /// <param name="CharacterName">Non-unique display name of the character being granted (1-20 characters in length). (Required)</param>
        /// <param name="CharacterType">Type of the character being granted; statistics can be sliced based on this value. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GrantCharacterToUserResult> GrantCharacterToUser(string CharacterName, string CharacterType, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GrantCharacterToUserRequest request = new GrantCharacterToUserRequest()
            {
                CharacterName = CharacterName,
                CharacterType = CharacterType,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GrantCharacterToUserResult>("/Server/GrantCharacterToUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified items to the specified character's inventory
        /// </summary>
        /// <param name="Annotation">String detailing any additional information concerning this operation. (Optional)</param>
        /// <param name="CatalogVersion">Catalog version from which items are to be granted. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="ItemIds">Array of itemIds to grant to the user. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GrantItemsToCharacterResult> GrantItemsToCharacter(string CharacterId, string PlayFabId, string Annotation = default, string CatalogVersion = default, List<string> ItemIds = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GrantItemsToCharacterRequest request = new GrantItemsToCharacterRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                Annotation = Annotation,
                CatalogVersion = CatalogVersion,
                ItemIds = ItemIds,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToCharacterResult>("/Server/GrantItemsToCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified items to the specified user's inventory
        /// </summary>
        /// <param name="Annotation">String detailing any additional information concerning this operation. (Optional)</param>
        /// <param name="CatalogVersion">Catalog version from which items are to be granted. (Optional)</param>
        /// <param name="ItemIds">Array of itemIds to grant to the user. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GrantItemsToUserResult> GrantItemsToUser(List<string> ItemIds, string PlayFabId, string Annotation = default, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GrantItemsToUserRequest request = new GrantItemsToUserRequest()
            {
                ItemIds = ItemIds,
                PlayFabId = PlayFabId,
                Annotation = Annotation,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToUserResult>("/Server/GrantItemsToUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified items to the specified user inventories
        /// </summary>
        /// <param name="CatalogVersion">Catalog version from which items are to be granted. (Optional)</param>
        /// <param name="ItemGrants">Array of items to grant and the users to whom the items are to be granted. (Required)</param>
        public static Task<GrantItemsToUsersResult> GrantItemsToUsers(List<ItemGrant> ItemGrants, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GrantItemsToUsersRequest request = new GrantItemsToUsersRequest()
            {
                ItemGrants = ItemGrants,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToUsersResult>("/Server/GrantItemsToUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the custom server identifier, generated by the title, to the user's PlayFab account.
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the custom ID, unlink the other user and re-link. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier. (Required)</param>
        /// <param name="ServerCustomId">Unique server custom identifier for this player. (Required)</param>
        public static Task<LinkServerCustomIdResult> LinkServerCustomId(string PlayFabId, string ServerCustomId, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkServerCustomIdRequest request = new LinkServerCustomIdRequest()
            {
                PlayFabId = PlayFabId,
                ServerCustomId = ServerCustomId,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<LinkServerCustomIdResult>("/Server/LinkServerCustomId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Xbox Live account associated with the provided access code to the user's PlayFab account
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier for a user, or null if no PlayFab account is linked to the Xbox Live identifier. (Required)</param>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Required)</param>
        public static Task<LinkXboxAccountResult> LinkXboxAccount(string PlayFabId, string XboxToken, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkXboxAccountRequest request = new LinkXboxAccountRequest()
            {
                PlayFabId = PlayFabId,
                XboxToken = XboxToken,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<LinkXboxAccountResult>("/Server/LinkXboxAccount", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Securely login a game client from an external server backend using a custom identifier for that player. Server Custom ID
        /// and Client Custom ID are mutually exclusive and cannot be used to retrieve the same player account.
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="ServerCustomId">The backend server identifier for this player. (Optional)</param>
        public static Task<ServerLoginResult> LoginWithServerCustomId(bool? CreateAccount = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string ServerCustomId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithServerCustomIdRequest request = new LoginWithServerCustomIdRequest()
            {
                CreateAccount = CreateAccount,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
                ServerCustomId = ServerCustomId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ServerLoginResult>("/Server/LoginWithServerCustomId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Xbox Live Token from an external server backend, returning a session identifier that can
        /// subsequently be used for API calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Required)</param>
        public static Task<ServerLoginResult> LoginWithXbox(string XboxToken, bool? CreateAccount = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithXboxRequest request = new LoginWithXboxRequest()
            {
                XboxToken = XboxToken,
                CreateAccount = CreateAccount,
                InfoRequestParameters = InfoRequestParameters,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ServerLoginResult>("/Server/LoginWithXbox", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using an Xbox ID and Sandbox ID, returning a session identifier that can subsequently be used for API
        /// calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="Sandbox">The id of Xbox Live sandbox. (Required)</param>
        /// <param name="XboxId">Unique Xbox identifier for a user (Required)</param>
        public static Task<ServerLoginResult> LoginWithXboxId(string Sandbox, string XboxId, bool? CreateAccount = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithXboxIdRequest request = new LoginWithXboxIdRequest()
            {
                Sandbox = Sandbox,
                XboxId = XboxId,
                CreateAccount = CreateAccount,
                InfoRequestParameters = InfoRequestParameters,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ServerLoginResult>("/Server/LoginWithXboxId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Modifies the number of remaining uses of a player's inventory item
        /// </summary>
        /// <param name="ItemInstanceId">Unique instance identifier of the item to be modified. (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user whose item is being modified. (Required)</param>
        /// <param name="UsesToAdd">Number of uses to add to the item. Can be negative to remove uses. (Required)</param>
        public static Task<ModifyItemUsesResult> ModifyItemUses(string ItemInstanceId, string PlayFabId, int UsesToAdd, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ModifyItemUsesRequest request = new ModifyItemUsesRequest()
            {
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
                UsesToAdd = UsesToAdd,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyItemUsesResult>("/Server/ModifyItemUses", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Moves an item from a character's inventory into another of the users's character's inventory.
        /// </summary>
        /// <param name="GivingCharacterId">Unique identifier of the character that currently has the item. (Required)</param>
        /// <param name="ItemInstanceId">Unique PlayFab assigned instance identifier of the item (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="ReceivingCharacterId">Unique identifier of the character that will be receiving the item. (Required)</param>
        public static Task<MoveItemToCharacterFromCharacterResult> MoveItemToCharacterFromCharacter(string GivingCharacterId, string ItemInstanceId, string PlayFabId, string ReceivingCharacterId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            MoveItemToCharacterFromCharacterRequest request = new MoveItemToCharacterFromCharacterRequest()
            {
                GivingCharacterId = GivingCharacterId,
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
                ReceivingCharacterId = ReceivingCharacterId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<MoveItemToCharacterFromCharacterResult>("/Server/MoveItemToCharacterFromCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Moves an item from a user's inventory into their character's inventory.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="ItemInstanceId">Unique PlayFab assigned instance identifier of the item (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<MoveItemToCharacterFromUserResult> MoveItemToCharacterFromUser(string CharacterId, string ItemInstanceId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            MoveItemToCharacterFromUserRequest request = new MoveItemToCharacterFromUserRequest()
            {
                CharacterId = CharacterId,
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<MoveItemToCharacterFromUserResult>("/Server/MoveItemToCharacterFromUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Moves an item from a character's inventory into the owning user's inventory.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="ItemInstanceId">Unique PlayFab assigned instance identifier of the item (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<MoveItemToUserFromCharacterResult> MoveItemToUserFromCharacter(string CharacterId, string ItemInstanceId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            MoveItemToUserFromCharacterRequest request = new MoveItemToUserFromCharacterRequest()
            {
                CharacterId = CharacterId,
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<MoveItemToUserFromCharacterResult>("/Server/MoveItemToUserFromCharacter", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Informs the PlayFab match-making service that the user specified has left the Game Server Instance
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Instance the user is leaving. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<NotifyMatchmakerPlayerLeftResult> NotifyMatchmakerPlayerLeft(string LobbyId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            NotifyMatchmakerPlayerLeftRequest request = new NotifyMatchmakerPlayerLeftRequest()
            {
                LobbyId = LobbyId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<NotifyMatchmakerPlayerLeftResult>("/Server/NotifyMatchmakerPlayerLeft", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the virtual goods associated with the coupon to the user's inventory. Coupons can be generated via the
        /// Economy->Catalogs tab in the PlayFab Game Manager.
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the coupon. (Optional)</param>
        /// <param name="CharacterId">Optional identifier for the Character that should receive the item. If null, item is added to the player (Optional)</param>
        /// <param name="CouponCode">Generated coupon code to redeem. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<RedeemCouponResult> RedeemCoupon(string CouponCode, string PlayFabId, string CatalogVersion = default, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RedeemCouponRequest request = new RedeemCouponRequest()
            {
                CouponCode = CouponCode,
                PlayFabId = PlayFabId,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RedeemCouponResult>("/Server/RedeemCoupon", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates a Game Server session ticket and returns details about the user
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Server Instance that is asking for validation of the authorization ticket. (Required)</param>
        /// <param name="Ticket">Server authorization ticket passed back from a call to Matchmake or StartGame. (Required)</param>
        public static Task<RedeemMatchmakerTicketResult> RedeemMatchmakerTicket(string LobbyId, string Ticket, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RedeemMatchmakerTicketRequest request = new RedeemMatchmakerTicketRequest()
            {
                LobbyId = LobbyId,
                Ticket = Ticket,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RedeemMatchmakerTicketResult>("/Server/RedeemMatchmakerTicket", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Set the state of the indicated Game Server Instance. Also update the heartbeat for the instance.
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Server Instance for which the heartbeat is updated. (Required)</param>
        public static Task<RefreshGameServerInstanceHeartbeatResult> RefreshGameServerInstanceHeartbeat(string LobbyId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RefreshGameServerInstanceHeartbeatRequest request = new RefreshGameServerInstanceHeartbeatRequest()
            {
                LobbyId = LobbyId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RefreshGameServerInstanceHeartbeatResult>("/Server/RefreshGameServerInstanceHeartbeat", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Inform the matchmaker that a new Game Server Instance is added.
        /// </summary>
        /// <param name="Build">Unique identifier of the build running on the Game Server Instance. (Required)</param>
        /// <param name="GameMode">Game Mode the Game Server instance is running. Note that this must be defined in the Game Modes tab in the PlayFab Game Manager, along with the Build ID (the same Game Mode can be defined for multiple Build IDs). (Required)</param>
        /// <param name="LobbyId">Previous lobby id if re-registering an existing game. (Optional)</param>
        /// <param name="Region">Region in which the Game Server Instance is running. For matchmaking using non-AWS region names, set this to any AWS region and use Tags (below) to specify your custom region. (Required)</param>
        /// <param name="ServerIPV4Address">IPV4 address of the game server instance. (Optional)</param>
        /// <param name="ServerIPV6Address">IPV6 address (if any) of the game server instance. (Optional)</param>
        /// <param name="ServerPort">Port number for communication with the Game Server Instance. (Required)</param>
        /// <param name="ServerPublicDNSName">Public DNS name (if any) of the server (Optional)</param>
        /// <param name="Tags">Tags for the Game Server Instance (Optional)</param>
        public static Task<RegisterGameResponse> RegisterGame(string Build, string GameMode, Region Region, string ServerPort, string LobbyId = default, string ServerIPV4Address = default, string ServerIPV6Address = default, string ServerPublicDNSName = default, Dictionary<string,string> Tags = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RegisterGameRequest request = new RegisterGameRequest()
            {
                Build = Build,
                GameMode = GameMode,
                Region = Region,
                ServerPort = ServerPort,
                LobbyId = LobbyId,
                ServerIPV4Address = ServerIPV4Address,
                ServerIPV6Address = ServerIPV6Address,
                ServerPublicDNSName = ServerPublicDNSName,
                Tags = Tags,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RegisterGameResponse>("/Server/RegisterGame", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the specified friend from the the user's friend list
        /// </summary>
        /// <param name="FriendPlayFabId">PlayFab identifier of the friend account which is to be removed. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<EmptyResponse> RemoveFriend(string FriendPlayFabId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveFriendRequest request = new RemoveFriendRequest()
            {
                FriendPlayFabId = FriendPlayFabId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/RemoveFriend", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the specified generic service identifier from the player's PlayFab account.
        /// </summary>
        /// <param name="GenericId">Generic service identifier to be removed from the player. (Required)</param>
        /// <param name="PlayFabId">PlayFabId of the user to remove. (Required)</param>
        public static Task<EmptyResult> RemoveGenericID(GenericServiceId GenericId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveGenericIDRequest request = new RemoveGenericIDRequest()
            {
                GenericId = GenericId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResult>("/Server/RemoveGenericID", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Remove a given tag from a player profile. The tag's namespace is automatically generated based on the source of the tag.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="TagName">Unique tag for player profile. (Required)</param>
        public static Task<RemovePlayerTagResult> RemovePlayerTag(string PlayFabId, string TagName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemovePlayerTagRequest request = new RemovePlayerTagRequest()
            {
                PlayFabId = PlayFabId,
                TagName = TagName,
            };

            var context = GetContext(customAuthContext);

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
        /// <param name="PlayFabIds">An array of unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="SharedGroupId">Unique identifier for the shared group. (Required)</param>
        public static Task<RemoveSharedGroupMembersResult> RemoveSharedGroupMembers(List<string> PlayFabIds, string SharedGroupId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveSharedGroupMembersRequest request = new RemoveSharedGroupMembersRequest()
            {
                PlayFabIds = PlayFabIds,
                SharedGroupId = SharedGroupId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RemoveSharedGroupMembersResult>("/Server/RemoveSharedGroupMembers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Submit a report about a player (due to bad bahavior, etc.) on behalf of another player, so that customer service
        /// representatives for the title can take action concerning potentially toxic players.
        /// </summary>
        /// <param name="Comment">Optional additional comment by reporting player. (Optional)</param>
        /// <param name="ReporteeId">Unique PlayFab identifier of the reported player. (Required)</param>
        /// <param name="ReporterId">PlayFabId of the reporting player. (Required)</param>
        public static Task<ReportPlayerServerResult> ReportPlayer(string ReporteeId, string ReporterId, string Comment = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ReportPlayerServerRequest request = new ReportPlayerServerRequest()
            {
                ReporteeId = ReporteeId,
                ReporterId = ReporterId,
                Comment = Comment,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ReportPlayerServerResult>("/Server/ReportPlayer", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revoke all active bans for a user.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<RevokeAllBansForUserResult> RevokeAllBansForUser(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RevokeAllBansForUserRequest request = new RevokeAllBansForUserRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RevokeAllBansForUserResult>("/Server/RevokeAllBansForUser", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revoke all active bans specified with BanId.
        /// </summary>
        /// <param name="BanIds">Ids of the bans to be revoked. Maximum 100. (Required)</param>
        public static Task<RevokeBansResult> RevokeBans(List<string> BanIds, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RevokeBansRequest request = new RevokeBansRequest()
            {
                BanIds = BanIds,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RevokeBansResult>("/Server/RevokeBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revokes access to an item in a user's inventory
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ItemInstanceId">Unique PlayFab assigned instance identifier of the item (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<RevokeInventoryResult> RevokeInventoryItem(string ItemInstanceId, string PlayFabId, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RevokeInventoryItemRequest request = new RevokeInventoryItemRequest()
            {
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryResult>("/Server/RevokeInventoryItem", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Revokes access for up to 25 items across multiple users and characters.
        /// </summary>
        /// <param name="Items">Array of player items to revoke, between 1 and 25 items. (Required)</param>
        public static Task<RevokeInventoryItemsResult> RevokeInventoryItems(List<RevokeInventoryItem> Items, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RevokeInventoryItemsRequest request = new RevokeInventoryItemsRequest()
            {
                Items = Items,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryItemsResult>("/Server/RevokeInventoryItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Saves push notification template for title
        /// </summary>
        /// <param name="AndroidPayload">Android JSON for the notification template. (Optional)</param>
        /// <param name="Id">Id of the push notification template. (Optional)</param>
        /// <param name="IOSPayload">IOS JSON for the notification template. (Optional)</param>
        /// <param name="LocalizedPushNotificationTemplates">Dictionary of localized push notification templates with the language as the key. (Optional)</param>
        /// <param name="Name">Name of the push notification template. (Required)</param>
        public static Task<SavePushNotificationTemplateResult> SavePushNotificationTemplate(string Name, string AndroidPayload = default, string Id = default, string IOSPayload = default, Dictionary<string,LocalizedPushNotificationProperties> LocalizedPushNotificationTemplates = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SavePushNotificationTemplateRequest request = new SavePushNotificationTemplateRequest()
            {
                Name = Name,
                AndroidPayload = AndroidPayload,
                Id = Id,
                IOSPayload = IOSPayload,
                LocalizedPushNotificationTemplates = LocalizedPushNotificationTemplates,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SavePushNotificationTemplateResult>("/Server/SavePushNotificationTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Forces an email to be sent to the registered contact email address for the user's account based on an account recovery
        /// email template
        /// </summary>
        /// <param name="Email">User email address attached to their account (Optional)</param>
        /// <param name="EmailTemplateId">The email template id of the account recovery email template to send. (Required)</param>
        /// <param name="Username">The user's username requesting an account recovery. (Optional)</param>
        public static Task<SendCustomAccountRecoveryEmailResult> SendCustomAccountRecoveryEmail(string EmailTemplateId, string Email = default, string Username = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SendCustomAccountRecoveryEmailRequest request = new SendCustomAccountRecoveryEmailRequest()
            {
                EmailTemplateId = EmailTemplateId,
                Email = Email,
                Username = Username,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SendCustomAccountRecoveryEmailResult>("/Server/SendCustomAccountRecoveryEmail", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sends an email based on an email template to a player's contact email
        /// </summary>
        /// <param name="EmailTemplateId">The email template id of the email template to send. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<SendEmailFromTemplateResult> SendEmailFromTemplate(string EmailTemplateId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SendEmailFromTemplateRequest request = new SendEmailFromTemplateRequest()
            {
                EmailTemplateId = EmailTemplateId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SendEmailFromTemplateResult>("/Server/SendEmailFromTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sends an iOS/Android Push Notification to a specific user, if that user's device has been configured for Push
        /// Notifications in PlayFab. If a user has linked both Android and iOS devices, both will be notified.
        /// </summary>
        /// <param name="AdvancedPlatformDelivery">Allows you to provide precisely formatted json to target devices. This is an advanced feature, allowing you to deliver to custom plugin logic, fields, or functionality not natively supported by PlayFab. (Optional)</param>
        /// <param name="Message">Text of message to send. (Optional)</param>
        /// <param name="Package">Defines all possible push attributes like message, title, icon, etc. Some parameters are device specific - please see the PushNotificationPackage documentation for details. (Optional)</param>
        /// <param name="Recipient">PlayFabId of the recipient of the push notification. (Required)</param>
        /// <param name="Subject">Subject of message to send (may not be displayed in all platforms) (Optional)</param>
        /// <param name="TargetPlatforms">Target Platforms that should receive the Message or Package. If omitted, we will send to all available platforms. (Optional)</param>
        public static Task<SendPushNotificationResult> SendPushNotification(string Recipient, List<AdvancedPushPlatformMsg> AdvancedPlatformDelivery = default, string Message = default, PushNotificationPackage Package = default, string Subject = default, List<PushNotificationPlatform> TargetPlatforms = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SendPushNotificationRequest request = new SendPushNotificationRequest()
            {
                Recipient = Recipient,
                AdvancedPlatformDelivery = AdvancedPlatformDelivery,
                Message = Message,
                Package = Package,
                Subject = Subject,
                TargetPlatforms = TargetPlatforms,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SendPushNotificationResult>("/Server/SendPushNotification", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sends an iOS/Android Push Notification template to a specific user, if that user's device has been configured for Push
        /// Notifications in PlayFab. If a user has linked both Android and iOS devices, both will be notified.
        /// </summary>
        /// <param name="PushNotificationTemplateId">Id of the push notification template. (Required)</param>
        /// <param name="Recipient">PlayFabId of the push notification recipient. (Required)</param>
        public static Task<SendPushNotificationResult> SendPushNotificationFromTemplate(string PushNotificationTemplateId, string Recipient, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SendPushNotificationFromTemplateRequest request = new SendPushNotificationFromTemplateRequest()
            {
                PushNotificationTemplateId = PushNotificationTemplateId,
                Recipient = Recipient,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SendPushNotificationResult>("/Server/SendPushNotificationFromTemplate", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the tag list for a specified user in the friend list of another user
        /// </summary>
        /// <param name="FriendPlayFabId">PlayFab identifier of the friend account to which the tag(s) should be applied. (Required)</param>
        /// <param name="PlayFabId">PlayFab identifier of the player whose friend is to be updated. (Required)</param>
        /// <param name="Tags">Array of tags to set on the friend account. (Required)</param>
        public static Task<EmptyResponse> SetFriendTags(string FriendPlayFabId, string PlayFabId, List<string> Tags, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetFriendTagsRequest request = new SetFriendTagsRequest()
            {
                FriendPlayFabId = FriendPlayFabId,
                PlayFabId = PlayFabId,
                Tags = Tags,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/SetFriendTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the custom data of the indicated Game Server Instance
        /// </summary>
        /// <param name="GameServerData">Custom data to set for the specified game server instance. (Required)</param>
        /// <param name="LobbyId">Unique identifier of the Game Instance to be updated, in decimal format. (Required)</param>
        public static Task<SetGameServerInstanceDataResult> SetGameServerInstanceData(string GameServerData, string LobbyId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetGameServerInstanceDataRequest request = new SetGameServerInstanceDataRequest()
            {
                GameServerData = GameServerData,
                LobbyId = LobbyId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetGameServerInstanceDataResult>("/Server/SetGameServerInstanceData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Set the state of the indicated Game Server Instance.
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Instance to be updated, in decimal format. (Required)</param>
        /// <param name="State">State to set for the specified game server instance. (Required)</param>
        public static Task<SetGameServerInstanceStateResult> SetGameServerInstanceState(string LobbyId, GameInstanceState State, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetGameServerInstanceStateRequest request = new SetGameServerInstanceStateRequest()
            {
                LobbyId = LobbyId,
                State = State,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetGameServerInstanceStateResult>("/Server/SetGameServerInstanceState", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Set custom tags for the specified Game Server Instance
        /// </summary>
        /// <param name="LobbyId">Unique identifier of the Game Server Instance to be updated. (Required)</param>
        /// <param name="Tags">Tags to set for the specified Game Server Instance. Note that this is the complete list of tags to be associated with the Game Server Instance. (Required)</param>
        public static Task<SetGameServerInstanceTagsResult> SetGameServerInstanceTags(string LobbyId, Dictionary<string,string> Tags, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetGameServerInstanceTagsRequest request = new SetGameServerInstanceTagsRequest()
            {
                LobbyId = LobbyId,
                Tags = Tags,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetGameServerInstanceTagsResult>("/Server/SetGameServerInstanceTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the player's secret if it is not already set. Player secrets are used to sign API requests. To reset a player's
        /// secret use the Admin or Server API method SetPlayerSecret.
        /// </summary>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<SetPlayerSecretResult> SetPlayerSecret(string PlayFabId, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetPlayerSecretRequest request = new SetPlayerSecretRequest()
            {
                PlayFabId = PlayFabId,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetPlayerSecretResult>("/Server/SetPlayerSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom publisher settings
        /// </summary>
        /// <param name="Key">key we want to set a value on (note, this is additive - will only replace an existing key's value if they are the same name.) Keys are trimmed of whitespace. Keys may not begin with the '!' character. (Required)</param>
        /// <param name="Value">new value to set. Set to null to remove a value (Optional)</param>
        public static Task<SetPublisherDataResult> SetPublisherData(string Key, string Value = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetPublisherDataRequest request = new SetPublisherDataRequest()
            {
                Key = Key,
                Value = Value,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetPublisherDataResult>("/Server/SetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom title settings
        /// </summary>
        /// <param name="Key">key we want to set a value on (note, this is additive - will only replace an existing key's value if they are the same name.) Keys are trimmed of whitespace. Keys may not begin with the '!' character. (Required)</param>
        /// <param name="Value">new value to set. Set to null to remove a value (Optional)</param>
        public static Task<SetTitleDataResult> SetTitleData(string Key, string Value = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetTitleDataRequest request = new SetTitleDataRequest()
            {
                Key = Key,
                Value = Value,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Server/SetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom title settings
        /// </summary>
        /// <param name="Key">key we want to set a value on (note, this is additive - will only replace an existing key's value if they are the same name.) Keys are trimmed of whitespace. Keys may not begin with the '!' character. (Required)</param>
        /// <param name="Value">new value to set. Set to null to remove a value (Optional)</param>
        public static Task<SetTitleDataResult> SetTitleInternalData(string Key, string Value = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetTitleDataRequest request = new SetTitleDataRequest()
            {
                Key = Key,
                Value = Value,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Server/SetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the character's balance of the specified virtual currency by the stated amount. It is possible to make a VC
        /// balance negative with this API.
        /// </summary>
        /// <param name="Amount">Amount to be subtracted from the user balance of the specified virtual currency. (Required)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="VirtualCurrency">Name of the virtual currency which is to be decremented. (Required)</param>
        public static Task<ModifyCharacterVirtualCurrencyResult> SubtractCharacterVirtualCurrency(int Amount, string CharacterId, string PlayFabId, string VirtualCurrency, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SubtractCharacterVirtualCurrencyRequest request = new SubtractCharacterVirtualCurrencyRequest()
            {
                Amount = Amount,
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                VirtualCurrency = VirtualCurrency,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyCharacterVirtualCurrencyResult>("/Server/SubtractCharacterVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the user's balance of the specified virtual currency by the stated amount. It is possible to make a VC
        /// balance negative with this API.
        /// </summary>
        /// <param name="Amount">Amount to be subtracted from the user balance of the specified virtual currency. (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user whose virtual currency balance is to be decreased. (Required)</param>
        /// <param name="VirtualCurrency">Name of the virtual currency which is to be decremented. (Required)</param>
        public static Task<ModifyUserVirtualCurrencyResult> SubtractUserVirtualCurrency(int Amount, string PlayFabId, string VirtualCurrency, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SubtractUserVirtualCurrencyRequest request = new SubtractUserVirtualCurrencyRequest()
            {
                Amount = Amount,
                PlayFabId = PlayFabId,
                VirtualCurrency = VirtualCurrency,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Server/SubtractUserVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the custom server identifier from the user's PlayFab account.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab identifier. (Required)</param>
        /// <param name="ServerCustomId">Unique server custom identifier for this player. (Required)</param>
        public static Task<UnlinkServerCustomIdResult> UnlinkServerCustomId(string PlayFabId, string ServerCustomId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkServerCustomIdRequest request = new UnlinkServerCustomIdRequest()
            {
                PlayFabId = PlayFabId,
                ServerCustomId = ServerCustomId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UnlinkServerCustomIdResult>("/Server/UnlinkServerCustomId", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Xbox Live account from the user's PlayFab account
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab identifier for a user, or null if no PlayFab account is linked to the Xbox Live identifier. (Required)</param>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Required)</param>
        public static Task<UnlinkXboxAccountResult> UnlinkXboxAccount(string PlayFabId, string XboxToken, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkXboxAccountRequest request = new UnlinkXboxAccountRequest()
            {
                PlayFabId = PlayFabId,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UnlinkXboxAccountResult>("/Server/UnlinkXboxAccount", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Opens a specific container (ContainerItemInstanceId), with a specific key (KeyItemInstanceId, when required), and
        /// returns the contents of the opened container. If the container (and key when relevant) are consumable (RemainingUses >
        /// 0), their RemainingUses will be decremented, consistent with the operation of ConsumeItem.
        /// </summary>
        /// <param name="CatalogVersion">Specifies the catalog version that should be used to determine container contents. If unspecified, uses catalog associated with the item instance. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ContainerItemInstanceId">ItemInstanceId of the container to unlock. (Required)</param>
        /// <param name="KeyItemInstanceId">ItemInstanceId of the key that will be consumed by unlocking this container. If the container requires a key, this parameter is required. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<UnlockContainerItemResult> UnlockContainerInstance(string ContainerItemInstanceId, string PlayFabId, string CatalogVersion = default, string CharacterId = default, string KeyItemInstanceId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlockContainerInstanceRequest request = new UnlockContainerInstanceRequest()
            {
                ContainerItemInstanceId = ContainerItemInstanceId,
                PlayFabId = PlayFabId,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
                KeyItemInstanceId = KeyItemInstanceId,
            };

            var context = GetContext(customAuthContext);

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
        /// <param name="CatalogVersion">Specifies the catalog version that should be used to determine container contents. If unspecified, uses default/primary catalog. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ContainerItemId">Catalog ItemId of the container type to unlock. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<UnlockContainerItemResult> UnlockContainerItem(string ContainerItemId, string PlayFabId, string CatalogVersion = default, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlockContainerItemRequest request = new UnlockContainerItemRequest()
            {
                ContainerItemId = ContainerItemId,
                PlayFabId = PlayFabId,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Server/UnlockContainerItem", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update the avatar URL of the specified player
        /// </summary>
        /// <param name="ImageUrl">URL of the avatar image. If empty, it removes the existing avatar URL. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<EmptyResponse> UpdateAvatarUrl(string ImageUrl, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateAvatarUrlRequest request = new UpdateAvatarUrlRequest()
            {
                ImageUrl = ImageUrl,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/UpdateAvatarUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates information of a list of existing bans specified with Ban Ids.
        /// </summary>
        /// <param name="Bans">List of bans to be updated. Maximum 100. (Required)</param>
        public static Task<UpdateBansResult> UpdateBans(List<UpdateBanRequest> Bans, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateBansRequest request = new UpdateBansRequest()
            {
                Bans = Bans,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateBansResult>("/Server/UpdateBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user's character which is readable and writable by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<UpdateCharacterDataResult> UpdateCharacterData(string CharacterId, string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCharacterDataRequest request = new UpdateCharacterDataRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Server/UpdateCharacterData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user's character which cannot be accessed by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        public static Task<UpdateCharacterDataResult> UpdateCharacterInternalData(string CharacterId, string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCharacterDataRequest request = new UpdateCharacterDataRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Server/UpdateCharacterInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user's character which can only be read by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        public static Task<UpdateCharacterDataResult> UpdateCharacterReadOnlyData(string CharacterId, string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCharacterDataRequest request = new UpdateCharacterDataRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Server/UpdateCharacterReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the specific character
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="CharacterStatistics">Statistics to be updated with the provided values. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<UpdateCharacterStatisticsResult> UpdateCharacterStatistics(string CharacterId, string PlayFabId, Dictionary<string,int> CharacterStatistics = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCharacterStatisticsRequest request = new UpdateCharacterStatisticsRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
                CharacterStatistics = CharacterStatistics,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterStatisticsResult>("/Server/UpdateCharacterStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the user
        /// </summary>
        /// <param name="ForceUpdate">Indicates whether the statistics provided should be set, regardless of the aggregation method set on the statistic. Default is false. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Statistics">Statistics to be updated with the provided values (Required)</param>
        public static Task<UpdatePlayerStatisticsResult> UpdatePlayerStatistics(string PlayFabId, List<StatisticUpdate> Statistics, bool? ForceUpdate = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
            {
                PlayFabId = PlayFabId,
                Statistics = Statistics,
                ForceUpdate = ForceUpdate,
            };

            var context = GetContext(customAuthContext);

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
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys in this request. (Optional)</param>
        /// <param name="SharedGroupId">Unique identifier for the shared group. (Required)</param>
        public static Task<UpdateSharedGroupDataResult> UpdateSharedGroupData(string SharedGroupId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateSharedGroupDataRequest request = new UpdateSharedGroupDataRequest()
            {
                SharedGroupId = SharedGroupId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateSharedGroupDataResult>("/Server/UpdateSharedGroupData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<UpdateUserDataResult> UpdateUserData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserDataRequest request = new UpdateUserDataRequest()
            {
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<UpdateUserDataResult> UpdateUserInternalData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserInternalDataRequest request = new UpdateUserInternalDataRequest()
            {
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value pair data tagged to the specified item, which is read-only from the client.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="ItemInstanceId">Unique PlayFab assigned instance identifier of the item (Required)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<EmptyResponse> UpdateUserInventoryItemCustomData(string ItemInstanceId, string PlayFabId, string CharacterId = default, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserInventoryItemDataRequest request = new UpdateUserInventoryItemDataRequest()
            {
                ItemInstanceId = ItemInstanceId,
                PlayFabId = PlayFabId,
                CharacterId = CharacterId,
                Data = Data,
                KeysToRemove = KeysToRemove,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Server/UpdateUserInventoryItemCustomData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        public static Task<UpdateUserDataResult> UpdateUserPublisherData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserDataRequest request = new UpdateUserDataRequest()
            {
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the publisher-specific custom data for the user which cannot be accessed by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        public static Task<UpdateUserDataResult> UpdateUserPublisherInternalData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserInternalDataRequest request = new UpdateUserInternalDataRequest()
            {
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserPublisherInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the publisher-specific custom data for the user which can only be read by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        public static Task<UpdateUserDataResult> UpdateUserPublisherReadOnlyData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserDataRequest request = new UpdateUserDataRequest()
            {
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserPublisherReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title-specific custom data for the user which can only be read by the client
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        public static Task<UpdateUserDataResult> UpdateUserReadOnlyData(string PlayFabId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserDataRequest request = new UpdateUserDataRequest()
            {
                PlayFabId = PlayFabId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Server/UpdateUserReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a character-based event into PlayStream.
        /// </summary>
        /// <param name="Body">Custom event properties. Each property consists of a name (string) and a value (JSON object). (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="EventName">The name of the event, within the namespace scoped to the title. The naming convention is up to the caller, but it commonly follows the subject_verb_object pattern (e.g. player_logged_in). (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Timestamp">The time (in UTC) associated with this event. The value dafaults to the current time. (Optional)</param>
        public static Task<WriteEventResponse> WriteCharacterEvent(string CharacterId, string EventName, string PlayFabId, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteServerCharacterEventRequest request = new WriteServerCharacterEventRequest()
            {
                CharacterId = CharacterId,
                EventName = EventName,
                PlayFabId = PlayFabId,
                Body = Body,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Server/WriteCharacterEvent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a player-based event into PlayStream.
        /// </summary>
        /// <param name="Body">Custom data properties associated with the event. Each property consists of a name (string) and a value (JSON object). (Optional)</param>
        /// <param name="EventName">The name of the event, within the namespace scoped to the title. The naming convention is up to the caller, but it commonly follows the subject_verb_object pattern (e.g. player_logged_in). (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Timestamp">The time (in UTC) associated with this event. The value dafaults to the current time. (Optional)</param>
        public static Task<WriteEventResponse> WritePlayerEvent(string EventName, string PlayFabId, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteServerPlayerEventRequest request = new WriteServerPlayerEventRequest()
            {
                EventName = EventName,
                PlayFabId = PlayFabId,
                Body = Body,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Server/WritePlayerEvent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a title-based event into PlayStream.
        /// </summary>
        /// <param name="Body">Custom event properties. Each property consists of a name (string) and a value (JSON object). (Optional)</param>
        /// <param name="EventName">The name of the event, within the namespace scoped to the title. The naming convention is up to the caller, but it commonly follows the subject_verb_object pattern (e.g. player_logged_in). (Required)</param>
        /// <param name="Timestamp">The time (in UTC) associated with this event. The value dafaults to the current time. (Optional)</param>
        public static Task<WriteEventResponse> WriteTitleEvent(string EventName, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteTitleEventRequest request = new WriteTitleEventRequest()
            {
                EventName = EventName,
                Body = Body,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Server/WriteTitleEvent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }


    }
}

#endif
