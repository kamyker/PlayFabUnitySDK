#if !DISABLE_PLAYFABCLIENT_API

using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// APIs which provide the full range of PlayFab features available to the client - authentication, account and data
    /// management, inventory, friends, matchmaking, reporting, and platform-specific functionality
    /// </summary>
    public static class PlayFabClientAPI
    {
        static PlayFabClientAPI() { }

        /// <summary>
        /// Verify client login.
        /// </summary>
        public static bool IsClientLoggedIn()
        {
            return PlayFabSettings.staticPlayer.IsClientLoggedIn();
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
        /// Accepts an open trade (one that has not yet been accepted or cancelled), if the locally signed-in player is in the
        /// allowed player list for the trade, or it is open to all players. If the call is successful, the offered and accepted
        /// items will be swapped between the two players' inventories.
        /// </summary>
        public static Task<AcceptTradeResponse> AcceptTrade(string OfferingPlayerId, string TradeId, List<string> AcceptedInventoryInstanceIds = default,
            AcceptTradeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AcceptTradeRequest();
            if (OfferingPlayerId != default)
                request.OfferingPlayerId = OfferingPlayerId;
            if (TradeId != default)
                request.TradeId = TradeId;
            if (AcceptedInventoryInstanceIds != default)
                request.AcceptedInventoryInstanceIds = AcceptedInventoryInstanceIds;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AcceptTradeResponse>("/Client/AcceptTrade", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the PlayFab user, based upon a match against a supplied unique identifier, to the friend list of the local user. At
        /// least one of FriendPlayFabId,FriendUsername,FriendEmail, or FriendTitleDisplayName should be initialized.
        /// </summary>
        public static Task<AddFriendResult> AddFriend(string FriendEmail = default, string FriendPlayFabId = default, string FriendTitleDisplayName = default, string FriendUsername = default,
            AddFriendRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AddFriendRequest();
            if (FriendEmail != default)
                request.FriendEmail = FriendEmail;
            if (FriendPlayFabId != default)
                request.FriendPlayFabId = FriendPlayFabId;
            if (FriendTitleDisplayName != default)
                request.FriendTitleDisplayName = FriendTitleDisplayName;
            if (FriendUsername != default)
                request.FriendUsername = FriendUsername;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddFriendResult>("/Client/AddFriend", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified generic service identifier to the player's PlayFab account. This is designed to allow for a PlayFab
        /// ID lookup of any arbitrary service identifier a title wants to add. This identifier should never be used as
        /// authentication credentials, as the intent is that it is easily accessible by other players.
        /// </summary>
        public static Task<AddGenericIDResult> AddGenericID(GenericServiceId GenericId,
            AddGenericIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AddGenericIDRequest();
            if (GenericId != default)
                request.GenericId = GenericId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddGenericIDResult>("/Client/AddGenericID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds or updates a contact email to the player's profile.
        /// </summary>
        public static Task<AddOrUpdateContactEmailResult> AddOrUpdateContactEmail(string EmailAddress,
            AddOrUpdateContactEmailRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AddOrUpdateContactEmailRequest();
            if (EmailAddress != default)
                request.EmailAddress = EmailAddress;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddOrUpdateContactEmailResult>("/Client/AddOrUpdateContactEmail", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds users to the set of those able to update both the shared data, as well as the set of users in the group. Only users
        /// in the group can add new members. Shared Groups are designed for sharing data between a very small number of players,
        /// please see our guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<AddSharedGroupMembersResult> AddSharedGroupMembers(List<string> PlayFabIds, string SharedGroupId,
            AddSharedGroupMembersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AddSharedGroupMembersRequest();
            if (PlayFabIds != default)
                request.PlayFabIds = PlayFabIds;
            if (SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddSharedGroupMembersResult>("/Client/AddSharedGroupMembers", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds playfab username/password auth to an existing account created via an anonymous auth method, e.g. automatic device
        /// ID login.
        /// </summary>
        public static Task<AddUsernamePasswordResult> AddUsernamePassword(string Email, string Password, string Username,
            AddUsernamePasswordRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AddUsernamePasswordRequest();
            if (Email != default)
                request.Email = Email;
            if (Password != default)
                request.Password = Password;
            if (Username != default)
                request.Username = Username;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddUsernamePasswordResult>("/Client/AddUsernamePassword", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Increments the user's balance of the specified virtual currency by the stated amount
        /// </summary>
        public static Task<ModifyUserVirtualCurrencyResult> AddUserVirtualCurrency(int Amount, string VirtualCurrency,
            AddUserVirtualCurrencyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AddUserVirtualCurrencyRequest();
            if (Amount != default)
                request.Amount = Amount;
            if (VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Client/AddUserVirtualCurrency", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers the Android device to receive push notifications
        /// </summary>
        public static Task<AndroidDevicePushNotificationRegistrationResult> AndroidDevicePushNotificationRegistration(string DeviceToken, string ConfirmationMessage = default, bool? SendPushNotificationConfirmation = default,
            AndroidDevicePushNotificationRegistrationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AndroidDevicePushNotificationRegistrationRequest();
            if (DeviceToken != default)
                request.DeviceToken = DeviceToken;
            if (ConfirmationMessage != default)
                request.ConfirmationMessage = ConfirmationMessage;
            if (SendPushNotificationConfirmation != default)
                request.SendPushNotificationConfirmation = SendPushNotificationConfirmation;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AndroidDevicePushNotificationRegistrationResult>("/Client/AndroidDevicePushNotificationRegistration", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Attributes an install for advertisment.
        /// </summary>
        public static Task<AttributeInstallResult> AttributeInstall(string Adid = default, string Idfa = default,
            AttributeInstallRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new AttributeInstallRequest();
            if (Adid != default)
                request.Adid = Adid;
            if (Idfa != default)
                request.Idfa = Idfa;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AttributeInstallResult>("/Client/AttributeInstall", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Cancels an open trade (one that has not yet been accepted or cancelled). Note that only the player who created the trade
        /// can cancel it via this API call, to prevent griefing of the trade system (cancelling trades in order to prevent other
        /// players from accepting them, for trades that can be claimed by more than one player).
        /// </summary>
        public static Task<CancelTradeResponse> CancelTrade(string TradeId,
            CancelTradeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new CancelTradeRequest();
            if (TradeId != default)
                request.TradeId = TradeId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<CancelTradeResponse>("/Client/CancelTrade", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Confirms with the payment provider that the purchase was approved (if applicable) and adjusts inventory and virtual
        /// currency balances as appropriate
        /// </summary>
        public static Task<ConfirmPurchaseResult> ConfirmPurchase(string OrderId,
            ConfirmPurchaseRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ConfirmPurchaseRequest();
            if (OrderId != default)
                request.OrderId = OrderId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConfirmPurchaseResult>("/Client/ConfirmPurchase", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Consume uses of a consumable item. When all uses are consumed, it will be removed from the player's inventory.
        /// </summary>
        public static Task<ConsumeItemResult> ConsumeItem(int ConsumeCount, string ItemInstanceId, string CharacterId = default,
            ConsumeItemRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ConsumeItemRequest();
            if (ConsumeCount != default)
                request.ConsumeCount = ConsumeCount;
            if (ItemInstanceId != default)
                request.ItemInstanceId = ItemInstanceId;
            if (CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConsumeItemResult>("/Client/ConsumeItem", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Checks for any new consumable entitlements. If any are found, they are consumed and added as PlayFab items
        /// </summary>
        public static Task<ConsumePSNEntitlementsResult> ConsumePSNEntitlements(int ServiceLabel, string CatalogVersion = default,
            ConsumePSNEntitlementsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ConsumePSNEntitlementsRequest();
            if (ServiceLabel != default)
                request.ServiceLabel = ServiceLabel;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConsumePSNEntitlementsResult>("/Client/ConsumePSNEntitlements", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Grants the player's current entitlements from Xbox Live, consuming all availble items in Xbox and granting them to the
        /// player's PlayFab inventory. This call is idempotent and will not grant previously granted items to the player.
        /// </summary>
        public static Task<ConsumeXboxEntitlementsResult> ConsumeXboxEntitlements(string XboxToken, string CatalogVersion = default,
            ConsumeXboxEntitlementsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ConsumeXboxEntitlementsRequest();
            if (XboxToken != default)
                request.XboxToken = XboxToken;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConsumeXboxEntitlementsResult>("/Client/ConsumeXboxEntitlements", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Requests the creation of a shared group object, containing key/value pairs which may be updated by all members of the
        /// group. Upon creation, the current user will be the only member of the group. Shared Groups are designed for sharing data
        /// between a very small number of players, please see our guide:
        /// https://docs.microsoft.com/en-us/gaming/playfab/features/social/groups/using-shared-group-data
        /// </summary>
        public static Task<CreateSharedGroupResult> CreateSharedGroup(string SharedGroupId = default,
            CreateSharedGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new CreateSharedGroupRequest();
            if (SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<CreateSharedGroupResult>("/Client/CreateSharedGroup", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Executes a CloudScript function, with the 'currentPlayerId' set to the PlayFab ID of the authenticated player.
        /// </summary>
        public static Task<ExecuteCloudScriptResult> ExecuteCloudScript(string FunctionName, object FunctionParameter = default, bool? GeneratePlayStreamEvent = default, CloudScriptRevisionOption? RevisionSelection = default, int? SpecificRevision = default,
            ExecuteCloudScriptRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ExecuteCloudScriptRequest();
            if (FunctionName != default)
                request.FunctionName = FunctionName;
            if (FunctionParameter != default)
                request.FunctionParameter = FunctionParameter;
            if (GeneratePlayStreamEvent != default)
                request.GeneratePlayStreamEvent = GeneratePlayStreamEvent;
            if (RevisionSelection != default)
                request.RevisionSelection = RevisionSelection;
            if (SpecificRevision != default)
                request.SpecificRevision = SpecificRevision;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ExecuteCloudScriptResult>("/Client/ExecuteCloudScript", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        public static void ExecuteCloudScript<TOut>(ExecuteCloudScriptRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
            var context = (request == null ? null : request.AuthenticationContext) ?? PlayFabSettings.staticPlayer;
            Action<ExecuteCloudScriptResult> wrappedResultCallback = (wrappedResult) =>
            {
                var serializer = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer);
                var wrappedJson = serializer.SerializeObject(wrappedResult.FunctionResult);
                try
                {
                    wrappedResult.FunctionResult = serializer.DeserializeObject<TOut>(wrappedJson);
                }
                catch (Exception)
                {
                    wrappedResult.FunctionResult = wrappedJson;
                    wrappedResult.Logs.Add(new LogStatement { Level = "Warning", Data = wrappedJson, Message = "Sdk Message: Could not deserialize result as: " + typeof(TOut).Name });
                }
                resultCallback(wrappedResult);
            };
            PlayFabHttp.MakeApiCall("/Client/ExecuteCloudScript", request, AuthType.LoginSession, wrappedResultCallback, errorCallback, customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the user's PlayFab account details
        /// </summary>
        public static Task<GetAccountInfoResult> GetAccountInfo(string Email = default, string PlayFabId = default, string TitleDisplayName = default, string Username = default,
            GetAccountInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetAccountInfoRequest();
            if (Email != default)
                request.Email = Email;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if (TitleDisplayName != default)
                request.TitleDisplayName = TitleDisplayName;
            if (Username != default)
                request.Username = Username;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetAccountInfoResult>("/Client/GetAccountInfo", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all of the characters that belong to a specific user. CharacterIds are not globally unique; characterId must be
        /// evaluated with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        public static Task<ListUsersCharactersResult> GetAllUsersCharacters(string PlayFabId = default,
            ListUsersCharactersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ListUsersCharactersRequest();
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ListUsersCharactersResult>("/Client/GetAllUsersCharacters", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified version of the title's catalog of virtual goods, including all defined properties
        /// </summary>
        public static Task<GetCatalogItemsResult> GetCatalogItems(string CatalogVersion = default,
            GetCatalogItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetCatalogItemsRequest();
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCatalogItemsResult>("/Client/GetCatalogItems", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the character which is readable and writable by the client
        /// </summary>
        public static Task<GetCharacterDataResult> GetCharacterData(string CharacterId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default,
            GetCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetCharacterDataRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if (Keys != default)
                request.Keys = Keys;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Client/GetCharacterData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified character's current inventory of virtual goods
        /// </summary>
        public static Task<GetCharacterInventoryResult> GetCharacterInventory(string CharacterId, string CatalogVersion = default,
            GetCharacterInventoryRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetCharacterInventoryRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterInventoryResult>("/Client/GetCharacterInventory", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        public static Task<GetCharacterLeaderboardResult> GetCharacterLeaderboard(int StartPosition, string StatisticName, string CharacterType = default, int? MaxResultsCount = default,
            GetCharacterLeaderboardRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetCharacterLeaderboardRequest();
            if (StartPosition != default)
                request.StartPosition = StartPosition;
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (CharacterType != default)
                request.CharacterType = CharacterType;
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterLeaderboardResult>("/Client/GetCharacterLeaderboard", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the character which can only be read by the client
        /// </summary>
        public static Task<GetCharacterDataResult> GetCharacterReadOnlyData(string CharacterId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default,
            GetCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetCharacterDataRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if (Keys != default)
                request.Keys = Keys;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Client/GetCharacterReadOnlyData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details of all title-specific statistics for the user
        /// </summary>
        public static Task<GetCharacterStatisticsResult> GetCharacterStatistics(string CharacterId,
            GetCharacterStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetCharacterStatisticsRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterStatisticsResult>("/Client/GetCharacterStatistics", request,
                AuthType.LoginSession,
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
            if (request == null)
                request = new GetContentDownloadUrlRequest();
            if (Key != default)
                request.Key = Key;
            if (HttpMethod != default)
                request.HttpMethod = HttpMethod;
            if (ThruCDN != default)
                request.ThruCDN = ThruCDN;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetContentDownloadUrlResult>("/Client/GetContentDownloadUrl", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Get details about all current running game servers matching the given parameters.
        /// </summary>
        public static Task<CurrentGamesResult> GetCurrentGames(string BuildVersion = default, string GameMode = default, Region? Region = default, string StatisticName = default, CollectionFilter TagFilter = default,
            CurrentGamesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new CurrentGamesRequest();
            if (BuildVersion != default)
                request.BuildVersion = BuildVersion;
            if (GameMode != default)
                request.GameMode = GameMode;
            if (Region != default)
                request.Region = Region;
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (TagFilter != default)
                request.TagFilter = TagFilter;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<CurrentGamesResult>("/Client/GetCurrentGames", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked friends of the current player for the given statistic, starting from the indicated point in
        /// the leaderboard
        /// </summary>
        public static Task<GetLeaderboardResult> GetFriendLeaderboard(int StartPosition, string StatisticName, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, int? MaxResultsCount = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, string XboxToken = default,
            GetFriendLeaderboardRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetFriendLeaderboardRequest();
            if (StartPosition != default)
                request.StartPosition = StartPosition;
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (IncludeFacebookFriends != default)
                request.IncludeFacebookFriends = IncludeFacebookFriends;
            if (IncludeSteamFriends != default)
                request.IncludeSteamFriends = IncludeSteamFriends;
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if (ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if (Version != default)
                request.Version = Version;
            if (XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Client/GetFriendLeaderboard", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked friends of the current player for the given statistic, centered on the requested PlayFab
        /// user. If PlayFabId is empty or null will return currently logged in user.
        /// </summary>
        public static Task<GetFriendLeaderboardAroundPlayerResult> GetFriendLeaderboardAroundPlayer(string StatisticName, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, int? MaxResultsCount = default, string PlayFabId = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, string XboxToken = default,
            GetFriendLeaderboardAroundPlayerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetFriendLeaderboardAroundPlayerRequest();
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (IncludeFacebookFriends != default)
                request.IncludeFacebookFriends = IncludeFacebookFriends;
            if (IncludeSteamFriends != default)
                request.IncludeSteamFriends = IncludeSteamFriends;
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if (ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if (Version != default)
                request.Version = Version;
            if (XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetFriendLeaderboardAroundPlayerResult>("/Client/GetFriendLeaderboardAroundPlayer", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current friend list for the local user, constrained to users who have PlayFab accounts. Friends from
        /// linked accounts (Facebook, Steam) are also included. You may optionally exclude some linked services' friends.
        /// </summary>
        public static Task<GetFriendsListResult> GetFriendsList(bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, PlayerProfileViewConstraints ProfileConstraints = default, string XboxToken = default,
            GetFriendsListRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetFriendsListRequest();
            if (IncludeFacebookFriends != default)
                request.IncludeFacebookFriends = IncludeFacebookFriends;
            if (IncludeSteamFriends != default)
                request.IncludeSteamFriends = IncludeSteamFriends;
            if (ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if (XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetFriendsListResult>("/Client/GetFriendsList", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Get details about the regions hosting game servers matching the given parameters.
        /// </summary>
        public static Task<GameServerRegionsResult> GetGameServerRegions(string BuildVersion, string TitleId = default,
            GameServerRegionsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GameServerRegionsRequest();
            if (BuildVersion != default)
                request.BuildVersion = BuildVersion;
            if (TitleId != default)
                request.TitleId = TitleId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GameServerRegionsResult>("/Client/GetGameServerRegions", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        public static Task<GetLeaderboardResult> GetLeaderboard(int StartPosition, string StatisticName, int? MaxResultsCount = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default,
            GetLeaderboardRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetLeaderboardRequest();
            if (StartPosition != default)
                request.StartPosition = StartPosition;
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if (ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if (Version != default)
                request.Version = Version;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Client/GetLeaderboard", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, centered on the requested Character ID
        /// </summary>
        public static Task<GetLeaderboardAroundCharacterResult> GetLeaderboardAroundCharacter(string CharacterId, string StatisticName, string CharacterType = default, int? MaxResultsCount = default,
            GetLeaderboardAroundCharacterRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetLeaderboardAroundCharacterRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (CharacterType != default)
                request.CharacterType = CharacterType;
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundCharacterResult>("/Client/GetLeaderboardAroundCharacter", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, centered on the requested player. If PlayFabId is empty or
        /// null will return currently logged in user.
        /// </summary>
        public static Task<GetLeaderboardAroundPlayerResult> GetLeaderboardAroundPlayer(string StatisticName, int? MaxResultsCount = default, string PlayFabId = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default,
            GetLeaderboardAroundPlayerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetLeaderboardAroundPlayerRequest();
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if (ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;
            if (Version != default)
                request.Version = Version;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundPlayerResult>("/Client/GetLeaderboardAroundPlayer", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of all of the user's characters for the given statistic.
        /// </summary>
        public static Task<GetLeaderboardForUsersCharactersResult> GetLeaderboardForUserCharacters(int MaxResultsCount, string StatisticName,
            GetLeaderboardForUsersCharactersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetLeaderboardForUsersCharactersRequest();
            if (MaxResultsCount != default)
                request.MaxResultsCount = MaxResultsCount;
            if (StatisticName != default)
                request.StatisticName = StatisticName;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardForUsersCharactersResult>("/Client/GetLeaderboardForUserCharacters", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// For payments flows where the provider requires playfab (the fulfiller) to initiate the transaction, but the client
        /// completes the rest of the flow. In the Xsolla case, the token returned here will be passed to Xsolla by the client to
        /// create a cart. Poll GetPurchase using the returned OrderId once you've completed the payment.
        /// </summary>
        public static Task<GetPaymentTokenResult> GetPaymentToken(string TokenProvider,
            GetPaymentTokenRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPaymentTokenRequest();
            if (TokenProvider != default)
                request.TokenProvider = TokenProvider;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPaymentTokenResult>("/Client/GetPaymentToken", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a Photon custom authentication token that can be used to securely join the player into a Photon room. See
        /// https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/photon/quickstart for more details.
        /// </summary>
        public static Task<GetPhotonAuthenticationTokenResult> GetPhotonAuthenticationToken(string PhotonApplicationId,
            GetPhotonAuthenticationTokenRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPhotonAuthenticationTokenRequest();
            if (PhotonApplicationId != default)
                request.PhotonApplicationId = PhotonApplicationId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPhotonAuthenticationTokenResult>("/Client/GetPhotonAuthenticationToken", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves all of the user's different kinds of info.
        /// </summary>
        public static Task<GetPlayerCombinedInfoResult> GetPlayerCombinedInfo(GetPlayerCombinedInfoRequestParams InfoRequestParameters, string PlayFabId = default,
            GetPlayerCombinedInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerCombinedInfoRequest();
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerCombinedInfoResult>("/Client/GetPlayerCombinedInfo", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the player's profile
        /// </summary>
        public static Task<GetPlayerProfileResult> GetPlayerProfile(string PlayFabId = default, PlayerProfileViewConstraints ProfileConstraints = default,
            GetPlayerProfileRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerProfileRequest();
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if (ProfileConstraints != default)
                request.ProfileConstraints = ProfileConstraints;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerProfileResult>("/Client/GetPlayerProfile", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// List all segments that a player currently belongs to at this moment in time.
        /// </summary>
        public static Task<GetPlayerSegmentsResult> GetPlayerSegments(
            GetPlayerSegmentsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerSegmentsRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerSegmentsResult>("/Client/GetPlayerSegments", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the indicated statistics (current version and values for all statistics, if none are specified), for the local
        /// player.
        /// </summary>
        public static Task<GetPlayerStatisticsResult> GetPlayerStatistics(List<string> StatisticNames = default, List<StatisticNameVersion> StatisticNameVersions = default,
            GetPlayerStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerStatisticsRequest();
            if (StatisticNames != default)
                request.StatisticNames = StatisticNames;
            if (StatisticNameVersions != default)
                request.StatisticNameVersions = StatisticNameVersions;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticsResult>("/Client/GetPlayerStatistics", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the information on the available versions of the specified statistic.
        /// </summary>
        public static Task<GetPlayerStatisticVersionsResult> GetPlayerStatisticVersions(string StatisticName = default,
            GetPlayerStatisticVersionsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerStatisticVersionsRequest();
            if (StatisticName != default)
                request.StatisticName = StatisticName;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticVersionsResult>("/Client/GetPlayerStatisticVersions", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Get all tags with a given Namespace (optional) from a player profile.
        /// </summary>
        public static Task<GetPlayerTagsResult> GetPlayerTags(string PlayFabId, string Namespace = default,
            GetPlayerTagsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerTagsRequest();
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if (Namespace != default)
                request.Namespace = Namespace;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTagsResult>("/Client/GetPlayerTags", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets all trades the player has either opened or accepted, optionally filtered by trade status.
        /// </summary>
        public static Task<GetPlayerTradesResponse> GetPlayerTrades(TradeStatus? StatusFilter = default,
            GetPlayerTradesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayerTradesRequest();
            if (StatusFilter != default)
                request.StatusFilter = StatusFilter;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTradesResponse>("/Client/GetPlayerTrades", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromFacebookIDsResult> GetPlayFabIDsFromFacebookIDs(List<string> FacebookIDs,
            GetPlayFabIDsFromFacebookIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromFacebookIDsRequest();
            if (FacebookIDs != default)
                request.FacebookIDs = FacebookIDs;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookIDsResult>("/Client/GetPlayFabIDsFromFacebookIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook Instant Game identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromFacebookInstantGamesIdsResult> GetPlayFabIDsFromFacebookInstantGamesIds(List<string> FacebookInstantGamesIds,
            GetPlayFabIDsFromFacebookInstantGamesIdsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromFacebookInstantGamesIdsRequest();
            if (FacebookInstantGamesIds != default)
                request.FacebookInstantGamesIds = FacebookInstantGamesIds;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookInstantGamesIdsResult>("/Client/GetPlayFabIDsFromFacebookInstantGamesIds", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Game Center identifiers (referenced in the Game Center
        /// Programming Guide as the Player Identifier).
        /// </summary>
        public static Task<GetPlayFabIDsFromGameCenterIDsResult> GetPlayFabIDsFromGameCenterIDs(List<string> GameCenterIDs,
            GetPlayFabIDsFromGameCenterIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromGameCenterIDsRequest();
            if (GameCenterIDs != default)
                request.GameCenterIDs = GameCenterIDs;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGameCenterIDsResult>("/Client/GetPlayFabIDsFromGameCenterIDs", request,
                AuthType.LoginSession,
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
            if (request == null)
                request = new GetPlayFabIDsFromGenericIDsRequest();
            if (GenericIDs != default)
                request.GenericIDs = GenericIDs;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGenericIDsResult>("/Client/GetPlayFabIDsFromGenericIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Google identifiers. The Google identifiers are the IDs for
        /// the user accounts, available as "id" in the Google+ People API calls.
        /// </summary>
        public static Task<GetPlayFabIDsFromGoogleIDsResult> GetPlayFabIDsFromGoogleIDs(List<string> GoogleIDs,
            GetPlayFabIDsFromGoogleIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromGoogleIDsRequest();
            if (GoogleIDs != default)
                request.GoogleIDs = GoogleIDs;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGoogleIDsResult>("/Client/GetPlayFabIDsFromGoogleIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Kongregate identifiers. The Kongregate identifiers are the
        /// IDs for the user accounts, available as "user_id" from the Kongregate API methods(ex:
        /// http://developers.kongregate.com/docs/client/getUserId).
        /// </summary>
        public static Task<GetPlayFabIDsFromKongregateIDsResult> GetPlayFabIDsFromKongregateIDs(List<string> KongregateIDs,
            GetPlayFabIDsFromKongregateIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromKongregateIDsRequest();
            if (KongregateIDs != default)
                request.KongregateIDs = KongregateIDs;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromKongregateIDsResult>("/Client/GetPlayFabIDsFromKongregateIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Nintendo Switch identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> GetPlayFabIDsFromNintendoSwitchDeviceIds(List<string> NintendoSwitchDeviceIds,
            GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest();
            if (NintendoSwitchDeviceIds != default)
                request.NintendoSwitchDeviceIds = NintendoSwitchDeviceIds;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>("/Client/GetPlayFabIDsFromNintendoSwitchDeviceIds", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of PlayStation Network identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromPSNAccountIDsResult> GetPlayFabIDsFromPSNAccountIDs(List<string> PSNAccountIDs, int? IssuerId = default,
            GetPlayFabIDsFromPSNAccountIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromPSNAccountIDsRequest();
            if (PSNAccountIDs != default)
                request.PSNAccountIDs = PSNAccountIDs;
            if (IssuerId != default)
                request.IssuerId = IssuerId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromPSNAccountIDsResult>("/Client/GetPlayFabIDsFromPSNAccountIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Steam identifiers. The Steam identifiers are the profile
        /// IDs for the user accounts, available as SteamId in the Steamworks Community API calls.
        /// </summary>
        public static Task<GetPlayFabIDsFromSteamIDsResult> GetPlayFabIDsFromSteamIDs(List<string> SteamStringIDs = default,
            GetPlayFabIDsFromSteamIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromSteamIDsRequest();
            if (SteamStringIDs != default)
                request.SteamStringIDs = SteamStringIDs;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromSteamIDsResult>("/Client/GetPlayFabIDsFromSteamIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Twitch identifiers. The Twitch identifiers are the IDs for
        /// the user accounts, available as "_id" from the Twitch API methods (ex:
        /// https://github.com/justintv/Twitch-API/blob/master/v3_resources/users.md#get-usersuser).
        /// </summary>
        public static Task<GetPlayFabIDsFromTwitchIDsResult> GetPlayFabIDsFromTwitchIDs(List<string> TwitchIds,
            GetPlayFabIDsFromTwitchIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromTwitchIDsRequest();
            if (TwitchIds != default)
                request.TwitchIds = TwitchIds;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromTwitchIDsResult>("/Client/GetPlayFabIDsFromTwitchIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of XboxLive identifiers.
        /// </summary>
        public static Task<GetPlayFabIDsFromXboxLiveIDsResult> GetPlayFabIDsFromXboxLiveIDs(List<string> XboxLiveAccountIDs, string Sandbox = default,
            GetPlayFabIDsFromXboxLiveIDsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPlayFabIDsFromXboxLiveIDsRequest();
            if (XboxLiveAccountIDs != default)
                request.XboxLiveAccountIDs = XboxLiveAccountIDs;
            if (Sandbox != default)
                request.Sandbox = Sandbox;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromXboxLiveIDsResult>("/Client/GetPlayFabIDsFromXboxLiveIDs", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom publisher settings
        /// </summary>
        public static Task<GetPublisherDataResult> GetPublisherData(List<string> Keys,
            GetPublisherDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPublisherDataRequest();
            if (Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPublisherDataResult>("/Client/GetPublisherData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a purchase along with its current PlayFab status. Returns inventory items from the purchase that are still
        /// active.
        /// </summary>
        public static Task<GetPurchaseResult> GetPurchase(string OrderId,
            GetPurchaseRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetPurchaseRequest();
            if (OrderId != default)
                request.OrderId = OrderId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPurchaseResult>("/Client/GetPurchase", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves data stored in a shared group object, as well as the list of members in the group. Non-members of the group
        /// may use this to retrieve group data, including membership, but they will not receive data for keys marked as private.
        /// Shared Groups are designed for sharing data between a very small number of players, please see our guide:
        /// https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<GetSharedGroupDataResult> GetSharedGroupData(string SharedGroupId, bool? GetMembers = default, List<string> Keys = default,
            GetSharedGroupDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetSharedGroupDataRequest();
            if (SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;
            if (GetMembers != default)
                request.GetMembers = GetMembers;
            if (Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetSharedGroupDataResult>("/Client/GetSharedGroupData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the set of items defined for the specified store, including all prices defined
        /// </summary>
        public static Task<GetStoreItemsResult> GetStoreItems(string StoreId, string CatalogVersion = default,
            GetStoreItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetStoreItemsRequest();
            if (StoreId != default)
                request.StoreId = StoreId;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetStoreItemsResult>("/Client/GetStoreItems", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current server time
        /// </summary>
        public static Task<GetTimeResult> GetTime(
            GetTimeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetTimeRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTimeResult>("/Client/GetTime", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings
        /// </summary>
        public static Task<GetTitleDataResult> GetTitleData(List<string> Keys = default,
            GetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetTitleDataRequest();
            if (Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Client/GetTitleData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title news feed, as configured in the developer portal
        /// </summary>
        public static Task<GetTitleNewsResult> GetTitleNews(int? Count = default,
            GetTitleNewsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetTitleNewsRequest();
            if (Count != default)
                request.Count = Count;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTitleNewsResult>("/Client/GetTitleNews", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns the title's base 64 encoded RSA CSP blob.
        /// </summary>
        public static Task<GetTitlePublicKeyResult> GetTitlePublicKey(string TitleId, string TitleSharedSecret,
            GetTitlePublicKeyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetTitlePublicKeyRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (TitleSharedSecret != default)
                request.TitleSharedSecret = TitleSharedSecret;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitlePublicKeyResult>("/Client/GetTitlePublicKey", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the current status of an existing trade.
        /// </summary>
        public static Task<GetTradeStatusResponse> GetTradeStatus(string OfferingPlayerId, string TradeId,
            GetTradeStatusRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetTradeStatusRequest();
            if (OfferingPlayerId != default)
                request.OfferingPlayerId = OfferingPlayerId;
            if (TradeId != default)
                request.TradeId = TradeId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTradeStatusResponse>("/Client/GetTradeStatus", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default,
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetUserDataRequest();
            if (IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if (Keys != default)
                request.Keys = Keys;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the user's current inventory of virtual goods
        /// </summary>
        public static Task<GetUserInventoryResult> GetUserInventory(
            GetUserInventoryRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetUserInventoryRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserInventoryResult>("/Client/GetUserInventory", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserPublisherData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default,
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetUserDataRequest();
            if (IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if (Keys != default)
                request.Keys = Keys;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserPublisherData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which can only be read by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserPublisherReadOnlyData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default,
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetUserDataRequest();
            if (IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if (Keys != default)
                request.Keys = Keys;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserPublisherReadOnlyData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which can only be read by the client
        /// </summary>
        public static Task<GetUserDataResult> GetUserReadOnlyData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default,
            GetUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetUserDataRequest();
            if (IfChangedFromDataVersion != default)
                request.IfChangedFromDataVersion = IfChangedFromDataVersion;
            if (Keys != default)
                request.Keys = Keys;
            if (PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserReadOnlyData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Requests a challenge from the server to be signed by Windows Hello Passport service to authenticate.
        /// </summary>
        public static Task<GetWindowsHelloChallengeResponse> GetWindowsHelloChallenge(string PublicKeyHint, string TitleId,
            GetWindowsHelloChallengeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GetWindowsHelloChallengeRequest();
            if (PublicKeyHint != default)
                request.PublicKeyHint = PublicKeyHint;
            if (TitleId != default)
                request.TitleId = TitleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetWindowsHelloChallengeResponse>("/Client/GetWindowsHelloChallenge", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Grants the specified character type to the user. CharacterIds are not globally unique; characterId must be evaluated
        /// with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        public static Task<GrantCharacterToUserResult> GrantCharacterToUser(string CharacterName, string ItemId, string CatalogVersion = default,
            GrantCharacterToUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new GrantCharacterToUserRequest();
            if (CharacterName != default)
                request.CharacterName = CharacterName;
            if (ItemId != default)
                request.ItemId = ItemId;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GrantCharacterToUserResult>("/Client/GrantCharacterToUser", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Android device identifier to the user's PlayFab account
        /// </summary>
        public static Task<LinkAndroidDeviceIDResult> LinkAndroidDeviceID(string AndroidDeviceId, string AndroidDevice = default, bool? ForceLink = default, string OS = default,
            LinkAndroidDeviceIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkAndroidDeviceIDRequest();
            if (AndroidDeviceId != default)
                request.AndroidDeviceId = AndroidDeviceId;
            if (AndroidDevice != default)
                request.AndroidDevice = AndroidDevice;
            if (ForceLink != default)
                request.ForceLink = ForceLink;
            if (OS != default)
                request.OS = OS;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkAndroidDeviceIDResult>("/Client/LinkAndroidDeviceID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the custom identifier, generated by the title, to the user's PlayFab account
        /// </summary>
        public static Task<LinkCustomIDResult> LinkCustomID(string CustomId, bool? ForceLink = default,
            LinkCustomIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkCustomIDRequest();
            if (CustomId != default)
                request.CustomId = CustomId;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkCustomIDResult>("/Client/LinkCustomID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Facebook account associated with the provided Facebook access token to the user's PlayFab account
        /// </summary>
        public static Task<LinkFacebookAccountResult> LinkFacebookAccount(string AccessToken, bool? ForceLink = default,
            LinkFacebookAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkFacebookAccountRequest();
            if (AccessToken != default)
                request.AccessToken = AccessToken;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkFacebookAccountResult>("/Client/LinkFacebookAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Facebook Instant Games Id to the user's PlayFab account
        /// </summary>
        public static Task<LinkFacebookInstantGamesIdResult> LinkFacebookInstantGamesId(string FacebookInstantGamesSignature, bool? ForceLink = default,
            LinkFacebookInstantGamesIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkFacebookInstantGamesIdRequest();
            if (FacebookInstantGamesSignature != default)
                request.FacebookInstantGamesSignature = FacebookInstantGamesSignature;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkFacebookInstantGamesIdResult>("/Client/LinkFacebookInstantGamesId", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Game Center account associated with the provided Game Center ID to the user's PlayFab account
        /// </summary>
        public static Task<LinkGameCenterAccountResult> LinkGameCenterAccount(string GameCenterId, bool? ForceLink = default, string PublicKeyUrl = default, string Salt = default, string Signature = default, string Timestamp = default,
            LinkGameCenterAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkGameCenterAccountRequest();
            if (GameCenterId != default)
                request.GameCenterId = GameCenterId;
            if (ForceLink != default)
                request.ForceLink = ForceLink;
            if (PublicKeyUrl != default)
                request.PublicKeyUrl = PublicKeyUrl;
            if (Salt != default)
                request.Salt = Salt;
            if (Signature != default)
                request.Signature = Signature;
            if (Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkGameCenterAccountResult>("/Client/LinkGameCenterAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the currently signed-in user account to their Google account, using their Google account credentials
        /// </summary>
        public static Task<LinkGoogleAccountResult> LinkGoogleAccount(bool? ForceLink = default, string ServerAuthCode = default,
            LinkGoogleAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkGoogleAccountRequest();
            if (ForceLink != default)
                request.ForceLink = ForceLink;
            if (ServerAuthCode != default)
                request.ServerAuthCode = ServerAuthCode;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkGoogleAccountResult>("/Client/LinkGoogleAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the vendor-specific iOS device identifier to the user's PlayFab account
        /// </summary>
        public static Task<LinkIOSDeviceIDResult> LinkIOSDeviceID(string DeviceId, string DeviceModel = default, bool? ForceLink = default, string OS = default,
            LinkIOSDeviceIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkIOSDeviceIDRequest();
            if (DeviceId != default)
                request.DeviceId = DeviceId;
            if (DeviceModel != default)
                request.DeviceModel = DeviceModel;
            if (ForceLink != default)
                request.ForceLink = ForceLink;
            if (OS != default)
                request.OS = OS;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkIOSDeviceIDResult>("/Client/LinkIOSDeviceID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Kongregate identifier to the user's PlayFab account
        /// </summary>
        public static Task<LinkKongregateAccountResult> LinkKongregate(string AuthTicket, string KongregateId, bool? ForceLink = default,
            LinkKongregateAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkKongregateAccountRequest();
            if (AuthTicket != default)
                request.AuthTicket = AuthTicket;
            if (KongregateId != default)
                request.KongregateId = KongregateId;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkKongregateAccountResult>("/Client/LinkKongregate", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the NintendoSwitchDeviceId to the user's PlayFab account
        /// </summary>
        public static Task<LinkNintendoSwitchDeviceIdResult> LinkNintendoSwitchDeviceId(string NintendoSwitchDeviceId, bool? ForceLink = default,
            LinkNintendoSwitchDeviceIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkNintendoSwitchDeviceIdRequest();
            if (NintendoSwitchDeviceId != default)
                request.NintendoSwitchDeviceId = NintendoSwitchDeviceId;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkNintendoSwitchDeviceIdResult>("/Client/LinkNintendoSwitchDeviceId", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links an OpenID Connect account to a user's PlayFab account, based on an existing relationship between a title and an
        /// Open ID Connect provider and the OpenId Connect JWT from that provider.
        /// </summary>
        public static Task<EmptyResult> LinkOpenIdConnect(string ConnectionId, string IdToken, bool? ForceLink = default,
            LinkOpenIdConnectRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkOpenIdConnectRequest();
            if (ConnectionId != default)
                request.ConnectionId = ConnectionId;
            if (IdToken != default)
                request.IdToken = IdToken;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResult>("/Client/LinkOpenIdConnect", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the PlayStation Network account associated with the provided access code to the user's PlayFab account
        /// </summary>
        public static Task<LinkPSNAccountResult> LinkPSNAccount(string AuthCode, string RedirectUri, bool? ForceLink = default, int? IssuerId = default,
            LinkPSNAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkPSNAccountRequest();
            if (AuthCode != default)
                request.AuthCode = AuthCode;
            if (RedirectUri != default)
                request.RedirectUri = RedirectUri;
            if (ForceLink != default)
                request.ForceLink = ForceLink;
            if (IssuerId != default)
                request.IssuerId = IssuerId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkPSNAccountResult>("/Client/LinkPSNAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Steam account associated with the provided Steam authentication ticket to the user's PlayFab account
        /// </summary>
        public static Task<LinkSteamAccountResult> LinkSteamAccount(string SteamTicket, bool? ForceLink = default,
            LinkSteamAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkSteamAccountRequest();
            if (SteamTicket != default)
                request.SteamTicket = SteamTicket;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkSteamAccountResult>("/Client/LinkSteamAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Twitch account associated with the token to the user's PlayFab account.
        /// </summary>
        public static Task<LinkTwitchAccountResult> LinkTwitch(string AccessToken, bool? ForceLink = default,
            LinkTwitchAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkTwitchAccountRequest();
            if (AccessToken != default)
                request.AccessToken = AccessToken;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkTwitchAccountResult>("/Client/LinkTwitch", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Link Windows Hello authentication to the current PlayFab Account
        /// </summary>
        public static Task<LinkWindowsHelloAccountResponse> LinkWindowsHello(string PublicKey, string UserName, string DeviceName = default, bool? ForceLink = default,
            LinkWindowsHelloAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkWindowsHelloAccountRequest();
            if (PublicKey != default)
                request.PublicKey = PublicKey;
            if (UserName != default)
                request.UserName = UserName;
            if (DeviceName != default)
                request.DeviceName = DeviceName;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkWindowsHelloAccountResponse>("/Client/LinkWindowsHello", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Xbox Live account associated with the provided access code to the user's PlayFab account
        /// </summary>
        public static Task<LinkXboxAccountResult> LinkXboxAccount(string XboxToken, bool? ForceLink = default,
            LinkXboxAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LinkXboxAccountRequest();
            if (XboxToken != default)
                request.XboxToken = XboxToken;
            if (ForceLink != default)
                request.ForceLink = ForceLink;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkXboxAccountResult>("/Client/LinkXboxAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using the Android device identifier, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithAndroidDeviceID(string TitleId, string AndroidDevice = default, string AndroidDeviceId = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string OS = default, string PlayerSecret = default,
            LoginWithAndroidDeviceIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithAndroidDeviceIDRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (AndroidDevice != default)
                request.AndroidDevice = AndroidDevice;
            if (AndroidDeviceId != default)
                request.AndroidDeviceId = AndroidDeviceId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (OS != default)
                request.OS = OS;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithAndroidDeviceID", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a custom unique identifier generated by the title, returning a session identifier that can
        /// subsequently be used for API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithCustomID(string TitleId, bool? CreateAccount = default, string CustomId = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default,
            LoginWithCustomIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithCustomIDRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (CustomId != default)
                request.CustomId = CustomId;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithCustomID", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user into the PlayFab account, returning a session identifier that can subsequently be used for API calls
        /// which require an authenticated user. Unlike most other login API calls, LoginWithEmailAddress does not permit the
        /// creation of new accounts via the CreateAccountFlag. Email addresses may be used to create accounts via
        /// RegisterPlayFabUser.
        /// </summary>
        public static Task<LoginResult> LoginWithEmailAddress(string Email, string Password, string TitleId, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default,
            LoginWithEmailAddressRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithEmailAddressRequest();
            if (Email != default)
                request.Email = Email;
            if (Password != default)
                request.Password = Password;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithEmailAddress", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Facebook access token, returning a session identifier that can subsequently be used for API
        /// calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithFacebook(string AccessToken, string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default,
            LoginWithFacebookRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithFacebookRequest();
            if (AccessToken != default)
                request.AccessToken = AccessToken;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithFacebook", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Facebook Instant Games ID, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user. Requires Facebook Instant Games to be configured.
        /// </summary>
        public static Task<LoginResult> LoginWithFacebookInstantGamesId(string FacebookInstantGamesSignature, string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default,
            LoginWithFacebookInstantGamesIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithFacebookInstantGamesIdRequest();
            if (FacebookInstantGamesSignature != default)
                request.FacebookInstantGamesSignature = FacebookInstantGamesSignature;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithFacebookInstantGamesId", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using an iOS Game Center player identifier, returning a session identifier that can subsequently be
        /// used for API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithGameCenter(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerId = default, string PlayerSecret = default, string PublicKeyUrl = default, string Salt = default, string Signature = default, string Timestamp = default,
            LoginWithGameCenterRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithGameCenterRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerId != default)
                request.PlayerId = PlayerId;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (PublicKeyUrl != default)
                request.PublicKeyUrl = PublicKeyUrl;
            if (Salt != default)
                request.Salt = Salt;
            if (Signature != default)
                request.Signature = Signature;
            if (Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithGameCenter", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using their Google account credentials
        /// </summary>
        public static Task<LoginResult> LoginWithGoogleAccount(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string ServerAuthCode = default,
            LoginWithGoogleAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithGoogleAccountRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (ServerAuthCode != default)
                request.ServerAuthCode = ServerAuthCode;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithGoogleAccount", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using the vendor-specific iOS device identifier, returning a session identifier that can subsequently
        /// be used for API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithIOSDeviceID(string TitleId, bool? CreateAccount = default, string DeviceId = default, string DeviceModel = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string OS = default, string PlayerSecret = default,
            LoginWithIOSDeviceIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithIOSDeviceIDRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (DeviceId != default)
                request.DeviceId = DeviceId;
            if (DeviceModel != default)
                request.DeviceModel = DeviceModel;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (OS != default)
                request.OS = OS;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithIOSDeviceID", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Kongregate player account.
        /// </summary>
        public static Task<LoginResult> LoginWithKongregate(string TitleId, string AuthTicket = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string KongregateId = default, string PlayerSecret = default,
            LoginWithKongregateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithKongregateRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (AuthTicket != default)
                request.AuthTicket = AuthTicket;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (KongregateId != default)
                request.KongregateId = KongregateId;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithKongregate", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Nintendo Switch Device ID, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithNintendoSwitchDeviceId(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string NintendoSwitchDeviceId = default, string PlayerSecret = default,
            LoginWithNintendoSwitchDeviceIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithNintendoSwitchDeviceIdRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (NintendoSwitchDeviceId != default)
                request.NintendoSwitchDeviceId = NintendoSwitchDeviceId;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithNintendoSwitchDeviceId", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Logs in a user with an Open ID Connect JWT created by an existing relationship between a title and an Open ID Connect
        /// provider.
        /// </summary>
        public static Task<LoginResult> LoginWithOpenIdConnect(string ConnectionId, string IdToken, string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default,
            LoginWithOpenIdConnectRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithOpenIdConnectRequest();
            if (ConnectionId != default)
                request.ConnectionId = ConnectionId;
            if (IdToken != default)
                request.IdToken = IdToken;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithOpenIdConnect", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user into the PlayFab account, returning a session identifier that can subsequently be used for API calls
        /// which require an authenticated user. Unlike most other login API calls, LoginWithPlayFab does not permit the creation of
        /// new accounts via the CreateAccountFlag. Username/Password credentials may be used to create accounts via
        /// RegisterPlayFabUser, or added to existing accounts using AddUsernamePassword.
        /// </summary>
        public static Task<LoginResult> LoginWithPlayFab(string Password, string TitleId, string Username, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default,
            LoginWithPlayFabRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithPlayFabRequest();
            if (Password != default)
                request.Password = Password;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (Username != default)
                request.Username = Username;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithPlayFab", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a PlayStation Network authentication code, returning a session identifier that can subsequently
        /// be used for API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithPSN(string TitleId, string AuthCode = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, int? IssuerId = default, string PlayerSecret = default, string RedirectUri = default,
            LoginWithPSNRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithPSNRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (AuthCode != default)
                request.AuthCode = AuthCode;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (IssuerId != default)
                request.IssuerId = IssuerId;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (RedirectUri != default)
                request.RedirectUri = RedirectUri;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithPSN", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Steam authentication ticket, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithSteam(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string SteamTicket = default,
            LoginWithSteamRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithSteamRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (SteamTicket != default)
                request.SteamTicket = SteamTicket;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithSteam", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Twitch access token.
        /// </summary>
        public static Task<LoginResult> LoginWithTwitch(string TitleId, string AccessToken = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default,
            LoginWithTwitchRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithTwitchRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (AccessToken != default)
                request.AccessToken = AccessToken;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithTwitch", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Completes the Windows Hello login flow by returning the signed value of the challange from GetWindowsHelloChallenge.
        /// Windows Hello has a 2 step client to server authentication scheme. Step one is to request from the server a challenge
        /// string. Step two is to request the user sign the string via Windows Hello and then send the signed value back to the
        /// server.
        /// </summary>
        public static Task<LoginResult> LoginWithWindowsHello(string ChallengeSignature, string PublicKeyHint, string TitleId, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default,
            LoginWithWindowsHelloRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithWindowsHelloRequest();
            if (ChallengeSignature != default)
                request.ChallengeSignature = ChallengeSignature;
            if (PublicKeyHint != default)
                request.PublicKeyHint = PublicKeyHint;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithWindowsHello", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Xbox Live Token, returning a session identifier that can subsequently be used for API calls
        /// which require an authenticated user
        /// </summary>
        public static Task<LoginResult> LoginWithXbox(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string XboxToken = default,
            LoginWithXboxRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new LoginWithXboxRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (CreateAccount != default)
                request.CreateAccount = CreateAccount;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithXbox", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Attempts to locate a game session matching the given parameters. If the goal is to match the player into a specific
        /// active session, only the LobbyId is required. Otherwise, the BuildVersion, GameMode, and Region are all required
        /// parameters. Note that parameters specified in the search are required (they are not weighting factors). If a slot is
        /// found in a server instance matching the parameters, the slot will be assigned to that player, removing it from the
        /// availabe set. In that case, the information on the game session will be returned, otherwise the Status returned will be
        /// GameNotFound.
        /// </summary>
        public static Task<MatchmakeResult> Matchmake(string BuildVersion = default, string CharacterId = default, string GameMode = default, string LobbyId = default, Region? Region = default, bool? StartNewIfNoneFound = default, string StatisticName = default, CollectionFilter TagFilter = default,
            MatchmakeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new MatchmakeRequest();
            if (BuildVersion != default)
                request.BuildVersion = BuildVersion;
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (GameMode != default)
                request.GameMode = GameMode;
            if (LobbyId != default)
                request.LobbyId = LobbyId;
            if (Region != default)
                request.Region = Region;
            if (StartNewIfNoneFound != default)
                request.StartNewIfNoneFound = StartNewIfNoneFound;
            if (StatisticName != default)
                request.StatisticName = StatisticName;
            if (TagFilter != default)
                request.TagFilter = TagFilter;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<MatchmakeResult>("/Client/Matchmake", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Opens a new outstanding trade. Note that a given item instance may only be in one open trade at a time.
        /// </summary>
        public static Task<OpenTradeResponse> OpenTrade(List<string> AllowedPlayerIds = default, List<string> OfferedInventoryInstanceIds = default, List<string> RequestedCatalogItemIds = default,
            OpenTradeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new OpenTradeRequest();
            if (AllowedPlayerIds != default)
                request.AllowedPlayerIds = AllowedPlayerIds;
            if (OfferedInventoryInstanceIds != default)
                request.OfferedInventoryInstanceIds = OfferedInventoryInstanceIds;
            if (RequestedCatalogItemIds != default)
                request.RequestedCatalogItemIds = RequestedCatalogItemIds;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<OpenTradeResponse>("/Client/OpenTrade", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Selects a payment option for purchase order created via StartPurchase
        /// </summary>
        public static Task<PayForPurchaseResult> PayForPurchase(string Currency, string OrderId, string ProviderName, string ProviderTransactionId = default,
            PayForPurchaseRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new PayForPurchaseRequest();
            if (Currency != default)
                request.Currency = Currency;
            if (OrderId != default)
                request.OrderId = OrderId;
            if (ProviderName != default)
                request.ProviderName = ProviderName;
            if (ProviderTransactionId != default)
                request.ProviderTransactionId = ProviderTransactionId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<PayForPurchaseResult>("/Client/PayForPurchase", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Buys a single item with virtual currency. You must specify both the virtual currency to use to purchase, as well as what
        /// the client believes the price to be. This lets the server fail the purchase if the price has changed.
        /// </summary>
        public static Task<PurchaseItemResult> PurchaseItem(string ItemId, int Price, string VirtualCurrency, string CatalogVersion = default, string CharacterId = default, string StoreId = default,
            PurchaseItemRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new PurchaseItemRequest();
            if (ItemId != default)
                request.ItemId = ItemId;
            if (Price != default)
                request.Price = Price;
            if (VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (StoreId != default)
                request.StoreId = StoreId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<PurchaseItemResult>("/Client/PurchaseItem", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the virtual goods associated with the coupon to the user's inventory. Coupons can be generated via the
        /// Economy->Catalogs tab in the PlayFab Game Manager.
        /// </summary>
        public static Task<RedeemCouponResult> RedeemCoupon(string CouponCode, string CatalogVersion = default, string CharacterId = default,
            RedeemCouponRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RedeemCouponRequest();
            if (CouponCode != default)
                request.CouponCode = CouponCode;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if (CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RedeemCouponResult>("/Client/RedeemCoupon", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Uses the supplied OAuth code to refresh the internally cached player PSN auth token
        /// </summary>
        public static Task<EmptyResponse> RefreshPSNAuthToken(string AuthCode, string RedirectUri, int? IssuerId = default,
            RefreshPSNAuthTokenRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RefreshPSNAuthTokenRequest();
            if (AuthCode != default)
                request.AuthCode = AuthCode;
            if (RedirectUri != default)
                request.RedirectUri = RedirectUri;
            if (IssuerId != default)
                request.IssuerId = IssuerId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/RefreshPSNAuthToken", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers the iOS device to receive push notifications
        /// </summary>
        public static Task<RegisterForIOSPushNotificationResult> RegisterForIOSPushNotification(string DeviceToken, string ConfirmationMessage = default, bool? SendPushNotificationConfirmation = default,
            RegisterForIOSPushNotificationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RegisterForIOSPushNotificationRequest();
            if (DeviceToken != default)
                request.DeviceToken = DeviceToken;
            if (ConfirmationMessage != default)
                request.ConfirmationMessage = ConfirmationMessage;
            if (SendPushNotificationConfirmation != default)
                request.SendPushNotificationConfirmation = SendPushNotificationConfirmation;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RegisterForIOSPushNotificationResult>("/Client/RegisterForIOSPushNotification", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers a new Playfab user account, returning a session identifier that can subsequently be used for API calls which
        /// require an authenticated user. You must supply either a username or an email address.
        /// </summary>
        public static Task<RegisterPlayFabUserResult> RegisterPlayFabUser(string TitleId, string DisplayName = default, string Email = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string Password = default, string PlayerSecret = default, bool? RequireBothUsernameAndEmail = default, string Username = default,
            RegisterPlayFabUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RegisterPlayFabUserRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (DisplayName != default)
                request.DisplayName = DisplayName;
            if (Email != default)
                request.Email = Email;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (Password != default)
                request.Password = Password;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (RequireBothUsernameAndEmail != default)
                request.RequireBothUsernameAndEmail = RequireBothUsernameAndEmail;
            if (Username != default)
                request.Username = Username;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<RegisterPlayFabUserResult>("/Client/RegisterPlayFabUser", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers a new PlayFab user account using Windows Hello authentication, returning a session ticket that can
        /// subsequently be used for API calls which require an authenticated user
        /// </summary>
        public static Task<LoginResult> RegisterWithWindowsHello(string TitleId, string DeviceName = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string PublicKey = default, string UserName = default,
            RegisterWithWindowsHelloRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RegisterWithWindowsHelloRequest();
            if (TitleId != default)
                request.TitleId = TitleId;
            if (DeviceName != default)
                request.DeviceName = DeviceName;
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (InfoRequestParameters != default)
                request.InfoRequestParameters = InfoRequestParameters;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;
            if (PublicKey != default)
                request.PublicKey = PublicKey;
            if (UserName != default)
                request.UserName = UserName;

            var context = GetContext(request);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/RegisterWithWindowsHello", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a contact email from the player's profile.
        /// </summary>
        public static Task<RemoveContactEmailResult> RemoveContactEmail(
            RemoveContactEmailRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RemoveContactEmailRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveContactEmailResult>("/Client/RemoveContactEmail", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a specified user from the friend list of the local user
        /// </summary>
        public static Task<RemoveFriendResult> RemoveFriend(string FriendPlayFabId,
            RemoveFriendRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RemoveFriendRequest();
            if (FriendPlayFabId != default)
                request.FriendPlayFabId = FriendPlayFabId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveFriendResult>("/Client/RemoveFriend", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the specified generic service identifier from the player's PlayFab account.
        /// </summary>
        public static Task<RemoveGenericIDResult> RemoveGenericID(GenericServiceId GenericId,
            RemoveGenericIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RemoveGenericIDRequest();
            if (GenericId != default)
                request.GenericId = GenericId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveGenericIDResult>("/Client/RemoveGenericID", request,
                AuthType.LoginSession,
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
            if (request == null)
                request = new RemoveSharedGroupMembersRequest();
            if (PlayFabIds != default)
                request.PlayFabIds = PlayFabIds;
            if (SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveSharedGroupMembersResult>("/Client/RemoveSharedGroupMembers", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Write a PlayStream event to describe the provided player device information. This API method is not designed to be
        /// called directly by developers. Each PlayFab client SDK will eventually report this information automatically.
        /// </summary>
        public static Task<EmptyResponse> ReportDeviceInfo(Dictionary<string, object> Info = default,
            DeviceInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new DeviceInfoRequest();
            if (Info != default)
                request.Info = Info;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/ReportDeviceInfo", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Submit a report for another player (due to bad bahavior, etc.), so that customer service representatives for the title
        /// can take action concerning potentially toxic players.
        /// </summary>
        public static Task<ReportPlayerClientResult> ReportPlayer(string ReporteeId, string Comment = default,
            ReportPlayerClientRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ReportPlayerClientRequest();
            if (ReporteeId != default)
                request.ReporteeId = ReporteeId;
            if (Comment != default)
                request.Comment = Comment;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ReportPlayerClientResult>("/Client/ReportPlayer", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Restores all in-app purchases based on the given restore receipt
        /// </summary>
        public static Task<RestoreIOSPurchasesResult> RestoreIOSPurchases(string ReceiptData, string CatalogVersion = default,
            RestoreIOSPurchasesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new RestoreIOSPurchasesRequest();
            if (ReceiptData != default)
                request.ReceiptData = ReceiptData;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RestoreIOSPurchasesResult>("/Client/RestoreIOSPurchases", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Forces an email to be sent to the registered email address for the user's account, with a link allowing the user to
        /// change the password.If an account recovery email template ID is provided, an email using the custom email template will
        /// be used.
        /// </summary>
        public static Task<SendAccountRecoveryEmailResult> SendAccountRecoveryEmail(string Email, string TitleId, string EmailTemplateId = default,
            SendAccountRecoveryEmailRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new SendAccountRecoveryEmailRequest();
            if (Email != default)
                request.Email = Email;
            if (TitleId != default)
                request.TitleId = TitleId;
            if (EmailTemplateId != default)
                request.EmailTemplateId = EmailTemplateId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SendAccountRecoveryEmailResult>("/Client/SendAccountRecoveryEmail", request,
                AuthType.None,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the tag list for a specified user in the friend list of the local user
        /// </summary>
        public static Task<SetFriendTagsResult> SetFriendTags(string FriendPlayFabId, List<string> Tags,
            SetFriendTagsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new SetFriendTagsRequest();
            if (FriendPlayFabId != default)
                request.FriendPlayFabId = FriendPlayFabId;
            if (Tags != default)
                request.Tags = Tags;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<SetFriendTagsResult>("/Client/SetFriendTags", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the player's secret if it is not already set. Player secrets are used to sign API requests. To reset a player's
        /// secret use the Admin or Server API method SetPlayerSecret.
        /// </summary>
        public static Task<SetPlayerSecretResult> SetPlayerSecret(string EncryptedRequest = default, string PlayerSecret = default,
            SetPlayerSecretRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new SetPlayerSecretRequest();
            if (EncryptedRequest != default)
                request.EncryptedRequest = EncryptedRequest;
            if (PlayerSecret != default)
                request.PlayerSecret = PlayerSecret;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<SetPlayerSecretResult>("/Client/SetPlayerSecret", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Start a new game server with a given configuration, add the current player and return the connection information.
        /// </summary>
        public static Task<StartGameResult> StartGame(string BuildVersion, string GameMode, Region Region, string CharacterId = default, string CustomCommandLineData = default, string StatisticName = default,
            StartGameRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new StartGameRequest();
            if (BuildVersion != default)
                request.BuildVersion = BuildVersion;
            if (GameMode != default)
                request.GameMode = GameMode;
            if (Region != default)
                request.Region = Region;
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (CustomCommandLineData != default)
                request.CustomCommandLineData = CustomCommandLineData;
            if (StatisticName != default)
                request.StatisticName = StatisticName;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<StartGameResult>("/Client/StartGame", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates an order for a list of items from the title catalog
        /// </summary>
        public static Task<StartPurchaseResult> StartPurchase(List<ItemPurchaseRequest> Items, string CatalogVersion = default, string StoreId = default,
            StartPurchaseRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new StartPurchaseRequest();
            if (Items != default)
                request.Items = Items;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if (StoreId != default)
                request.StoreId = StoreId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<StartPurchaseResult>("/Client/StartPurchase", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the user's balance of the specified virtual currency by the stated amount. It is possible to make a VC
        /// balance negative with this API.
        /// </summary>
        public static Task<ModifyUserVirtualCurrencyResult> SubtractUserVirtualCurrency(int Amount, string VirtualCurrency,
            SubtractUserVirtualCurrencyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new SubtractUserVirtualCurrencyRequest();
            if (Amount != default)
                request.Amount = Amount;
            if (VirtualCurrency != default)
                request.VirtualCurrency = VirtualCurrency;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Client/SubtractUserVirtualCurrency", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Android device identifier from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkAndroidDeviceIDResult> UnlinkAndroidDeviceID(string AndroidDeviceId = default,
            UnlinkAndroidDeviceIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkAndroidDeviceIDRequest();
            if (AndroidDeviceId != default)
                request.AndroidDeviceId = AndroidDeviceId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkAndroidDeviceIDResult>("/Client/UnlinkAndroidDeviceID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related custom identifier from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkCustomIDResult> UnlinkCustomID(string CustomId = default,
            UnlinkCustomIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkCustomIDRequest();
            if (CustomId != default)
                request.CustomId = CustomId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkCustomIDResult>("/Client/UnlinkCustomID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Facebook account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkFacebookAccountResult> UnlinkFacebookAccount(
            UnlinkFacebookAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkFacebookAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkFacebookAccountResult>("/Client/UnlinkFacebookAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Facebook Instant Game Ids from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkFacebookInstantGamesIdResult> UnlinkFacebookInstantGamesId(string FacebookInstantGamesId = default,
            UnlinkFacebookInstantGamesIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkFacebookInstantGamesIdRequest();
            if (FacebookInstantGamesId != default)
                request.FacebookInstantGamesId = FacebookInstantGamesId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkFacebookInstantGamesIdResult>("/Client/UnlinkFacebookInstantGamesId", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Game Center account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkGameCenterAccountResult> UnlinkGameCenterAccount(
            UnlinkGameCenterAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkGameCenterAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkGameCenterAccountResult>("/Client/UnlinkGameCenterAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Google account from the user's PlayFab account
        /// (https://developers.google.com/android/reference/com/google/android/gms/auth/GoogleAuthUtil#public-methods).
        /// </summary>
        public static Task<UnlinkGoogleAccountResult> UnlinkGoogleAccount(
            UnlinkGoogleAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkGoogleAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkGoogleAccountResult>("/Client/UnlinkGoogleAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related iOS device identifier from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkIOSDeviceIDResult> UnlinkIOSDeviceID(string DeviceId = default,
            UnlinkIOSDeviceIDRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkIOSDeviceIDRequest();
            if (DeviceId != default)
                request.DeviceId = DeviceId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkIOSDeviceIDResult>("/Client/UnlinkIOSDeviceID", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Kongregate identifier from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkKongregateAccountResult> UnlinkKongregate(
            UnlinkKongregateAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkKongregateAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkKongregateAccountResult>("/Client/UnlinkKongregate", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related NintendoSwitchDeviceId from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkNintendoSwitchDeviceIdResult> UnlinkNintendoSwitchDeviceId(string NintendoSwitchDeviceId = default,
            UnlinkNintendoSwitchDeviceIdRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkNintendoSwitchDeviceIdRequest();
            if (NintendoSwitchDeviceId != default)
                request.NintendoSwitchDeviceId = NintendoSwitchDeviceId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkNintendoSwitchDeviceIdResult>("/Client/UnlinkNintendoSwitchDeviceId", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks an OpenID Connect account from a user's PlayFab account, based on the connection ID of an existing relationship
        /// between a title and an Open ID Connect provider.
        /// </summary>
        public static Task<EmptyResponse> UnlinkOpenIdConnect(string ConnectionId,
            UninkOpenIdConnectRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UninkOpenIdConnectRequest();
            if (ConnectionId != default)
                request.ConnectionId = ConnectionId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/UnlinkOpenIdConnect", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related PSN account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkPSNAccountResult> UnlinkPSNAccount(
            UnlinkPSNAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkPSNAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkPSNAccountResult>("/Client/UnlinkPSNAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Steam account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkSteamAccountResult> UnlinkSteamAccount(
            UnlinkSteamAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkSteamAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkSteamAccountResult>("/Client/UnlinkSteamAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Twitch account from the user's PlayFab account.
        /// </summary>
        public static Task<UnlinkTwitchAccountResult> UnlinkTwitch(
            UnlinkTwitchAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkTwitchAccountRequest();

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkTwitchAccountResult>("/Client/UnlinkTwitch", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlink Windows Hello authentication from the current PlayFab Account
        /// </summary>
        public static Task<UnlinkWindowsHelloAccountResponse> UnlinkWindowsHello(string PublicKeyHint,
            UnlinkWindowsHelloAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkWindowsHelloAccountRequest();
            if (PublicKeyHint != default)
                request.PublicKeyHint = PublicKeyHint;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkWindowsHelloAccountResponse>("/Client/UnlinkWindowsHello", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Xbox Live account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkXboxAccountResult> UnlinkXboxAccount(string XboxToken,
            UnlinkXboxAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlinkXboxAccountRequest();
            if (XboxToken != default)
                request.XboxToken = XboxToken;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkXboxAccountResult>("/Client/UnlinkXboxAccount", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Opens the specified container, with the specified key (when required), and returns the contents of the opened container.
        /// If the container (and key when relevant) are consumable (RemainingUses > 0), their RemainingUses will be decremented,
        /// consistent with the operation of ConsumeItem.
        /// </summary>
        public static Task<UnlockContainerItemResult> UnlockContainerInstance(string ContainerItemInstanceId, string CatalogVersion = default, string CharacterId = default, string KeyItemInstanceId = default,
            UnlockContainerInstanceRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlockContainerInstanceRequest();
            if (ContainerItemInstanceId != default)
                request.ContainerItemInstanceId = ContainerItemInstanceId;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (KeyItemInstanceId != default)
                request.KeyItemInstanceId = KeyItemInstanceId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Client/UnlockContainerInstance", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Searches target inventory for an ItemInstance matching the given CatalogItemId, if necessary unlocks it using an
        /// appropriate key, and returns the contents of the opened container. If the container (and key when relevant) are
        /// consumable (RemainingUses > 0), their RemainingUses will be decremented, consistent with the operation of ConsumeItem.
        /// </summary>
        public static Task<UnlockContainerItemResult> UnlockContainerItem(string ContainerItemId, string CatalogVersion = default, string CharacterId = default,
            UnlockContainerItemRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UnlockContainerItemRequest();
            if (ContainerItemId != default)
                request.ContainerItemId = ContainerItemId;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if (CharacterId != default)
                request.CharacterId = CharacterId;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Client/UnlockContainerItem", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Update the avatar URL of the player
        /// </summary>
        public static Task<EmptyResponse> UpdateAvatarUrl(string ImageUrl,
            UpdateAvatarUrlRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateAvatarUrlRequest();
            if (ImageUrl != default)
                request.ImageUrl = ImageUrl;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/UpdateAvatarUrl", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the title-specific custom data for the user's character which is readable and writable by the client
        /// </summary>
        public static Task<UpdateCharacterDataResult> UpdateCharacterData(string CharacterId, Dictionary<string, string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default,
            UpdateCharacterDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateCharacterDataRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (Data != default)
                request.Data = Data;
            if (KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if (Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Client/UpdateCharacterData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the specific character. By default, clients are not
        /// permitted to update statistics. Developers may override this setting in the Game Manager > Settings > API Features.
        /// </summary>
        public static Task<UpdateCharacterStatisticsResult> UpdateCharacterStatistics(string CharacterId, Dictionary<string, int> CharacterStatistics = default,
            UpdateCharacterStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateCharacterStatisticsRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (CharacterStatistics != default)
                request.CharacterStatistics = CharacterStatistics;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterStatisticsResult>("/Client/UpdateCharacterStatistics", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the user. By default, clients are not permitted to
        /// update statistics. Developers may override this setting in the Game Manager > Settings > API Features.
        /// </summary>
        public static Task<UpdatePlayerStatisticsResult> UpdatePlayerStatistics(List<StatisticUpdate> Statistics,
            UpdatePlayerStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdatePlayerStatisticsRequest();
            if (Statistics != default)
                request.Statistics = Statistics;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdatePlayerStatisticsResult>("/Client/UpdatePlayerStatistics", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds, updates, and removes data keys for a shared group object. If the permission is set to Public, all fields updated
        /// or added in this call will be readable by users not in the group. By default, data permissions are set to Private.
        /// Regardless of the permission setting, only members of the group can update the data. Shared Groups are designed for
        /// sharing data between a very small number of players, please see our guide:
        /// https://api.playfab.com/docs/tutorials/landing-players/shared-groups
        /// </summary>
        public static Task<UpdateSharedGroupDataResult> UpdateSharedGroupData(string SharedGroupId, Dictionary<string, string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default,
            UpdateSharedGroupDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateSharedGroupDataRequest();
            if (SharedGroupId != default)
                request.SharedGroupId = SharedGroupId;
            if (Data != default)
                request.Data = Data;
            if (KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if (Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateSharedGroupDataResult>("/Client/UpdateSharedGroupData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserData(Dictionary<string, string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default,
            UpdateUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateUserDataRequest();
            if (Data != default)
                request.Data = Data;
            if (KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if (Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Client/UpdateUserData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        public static Task<UpdateUserDataResult> UpdateUserPublisherData(Dictionary<string, string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default,
            UpdateUserDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateUserDataRequest();
            if (Data != default)
                request.Data = Data;
            if (KeysToRemove != default)
                request.KeysToRemove = KeysToRemove;
            if (Permission != default)
                request.Permission = Permission;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Client/UpdateUserPublisherData", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title specific display name for the user
        /// </summary>
        public static Task<UpdateUserTitleDisplayNameResult> UpdateUserTitleDisplayName(string DisplayName,
            UpdateUserTitleDisplayNameRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new UpdateUserTitleDisplayNameRequest();
            if (DisplayName != default)
                request.DisplayName = DisplayName;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateUserTitleDisplayNameResult>("/Client/UpdateUserTitleDisplayName", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates with Amazon that the receipt for an Amazon App Store in-app purchase is valid and that it matches the
        /// purchased catalog item
        /// </summary>
        public static Task<ValidateAmazonReceiptResult> ValidateAmazonIAPReceipt(string CurrencyCode, int PurchasePrice, string ReceiptId, string UserId, string CatalogVersion = default,
            ValidateAmazonReceiptRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ValidateAmazonReceiptRequest();
            if (CurrencyCode != default)
                request.CurrencyCode = CurrencyCode;
            if (PurchasePrice != default)
                request.PurchasePrice = PurchasePrice;
            if (ReceiptId != default)
                request.ReceiptId = ReceiptId;
            if (UserId != default)
                request.UserId = UserId;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateAmazonReceiptResult>("/Client/ValidateAmazonIAPReceipt", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates a Google Play purchase and gives the corresponding item to the player.
        /// </summary>
        public static Task<ValidateGooglePlayPurchaseResult> ValidateGooglePlayPurchase(string ReceiptJson, string Signature, string CatalogVersion = default, string CurrencyCode = default, uint? PurchasePrice = default,
            ValidateGooglePlayPurchaseRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ValidateGooglePlayPurchaseRequest();
            if (ReceiptJson != default)
                request.ReceiptJson = ReceiptJson;
            if (Signature != default)
                request.Signature = Signature;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if (CurrencyCode != default)
                request.CurrencyCode = CurrencyCode;
            if (PurchasePrice != default)
                request.PurchasePrice = PurchasePrice;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateGooglePlayPurchaseResult>("/Client/ValidateGooglePlayPurchase", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates with the Apple store that the receipt for an iOS in-app purchase is valid and that it matches the purchased
        /// catalog item
        /// </summary>
        public static Task<ValidateIOSReceiptResult> ValidateIOSReceipt(string CurrencyCode, int PurchasePrice, string ReceiptData, string CatalogVersion = default,
            ValidateIOSReceiptRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ValidateIOSReceiptRequest();
            if (CurrencyCode != default)
                request.CurrencyCode = CurrencyCode;
            if (PurchasePrice != default)
                request.PurchasePrice = PurchasePrice;
            if (ReceiptData != default)
                request.ReceiptData = ReceiptData;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateIOSReceiptResult>("/Client/ValidateIOSReceipt", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates with Windows that the receipt for an Windows App Store in-app purchase is valid and that it matches the
        /// purchased catalog item
        /// </summary>
        public static Task<ValidateWindowsReceiptResult> ValidateWindowsStoreReceipt(string CurrencyCode, uint PurchasePrice, string Receipt, string CatalogVersion = default,
            ValidateWindowsReceiptRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new ValidateWindowsReceiptRequest();
            if (CurrencyCode != default)
                request.CurrencyCode = CurrencyCode;
            if (PurchasePrice != default)
                request.PurchasePrice = PurchasePrice;
            if (Receipt != default)
                request.Receipt = Receipt;
            if (CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateWindowsReceiptResult>("/Client/ValidateWindowsStoreReceipt", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a character-based event into PlayStream.
        /// </summary>
        public static Task<WriteEventResponse> WriteCharacterEvent(string CharacterId, string EventName, Dictionary<string, object> Body = default, DateTime? Timestamp = default,
            WriteClientCharacterEventRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new WriteClientCharacterEventRequest();
            if (CharacterId != default)
                request.CharacterId = CharacterId;
            if (EventName != default)
                request.EventName = EventName;
            if (Body != default)
                request.Body = Body;
            if (Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Client/WriteCharacterEvent", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a player-based event into PlayStream.
        /// </summary>
        public static Task<WriteEventResponse> WritePlayerEvent(string EventName, Dictionary<string, object> Body = default, DateTime? Timestamp = default,
            WriteClientPlayerEventRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new WriteClientPlayerEventRequest();
            if (EventName != default)
                request.EventName = EventName;
            if (Body != default)
                request.Body = Body;
            if (Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Client/WritePlayerEvent", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a title-based event into PlayStream.
        /// </summary>
        public static Task<WriteEventResponse> WriteTitleEvent(string EventName, Dictionary<string, object> Body = default, DateTime? Timestamp = default,
            WriteTitleEventRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if (request == null)
                request = new WriteTitleEventRequest();
            if (EventName != default)
                request.EventName = EventName;
            if (Body != default)
                request.Body = Body;
            if (Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Client/WriteTitleEvent", request,
                AuthType.LoginSession,
                customData, extraHeaders, context);
        }


    }
}

#endif
