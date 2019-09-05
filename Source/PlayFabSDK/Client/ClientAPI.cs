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
    public static class ClientAPI
    {
        static ClientAPI() {}

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

        private static PlayFabAuthenticationContext GetContext(PlayFabAuthenticationContext context) => context ?? PlayFabSettings.staticPlayer;

        /// <summary>
        /// Accepts an open trade (one that has not yet been accepted or cancelled), if the locally signed-in player is in the
        /// allowed player list for the trade, or it is open to all players. If the call is successful, the offered and accepted
        /// items will be swapped between the two players' inventories.
        /// </summary>
        /// <param name="AcceptedInventoryInstanceIds">Items from the accepting player's inventory in exchange for the offered items in the trade. In the case of a gift, this will be null. (Optional)</param>
        /// <param name="OfferingPlayerId">Player who opened the trade. (Required)</param>
        /// <param name="TradeId">Trade identifier. (Required)</param>
        public static Task<AcceptTradeResponse> AcceptTrade(string OfferingPlayerId, string TradeId, List<string> AcceptedInventoryInstanceIds = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AcceptTradeRequest request = new AcceptTradeRequest()
            {
                OfferingPlayerId = OfferingPlayerId,
                TradeId = TradeId,
                AcceptedInventoryInstanceIds = AcceptedInventoryInstanceIds,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AcceptTradeResponse>("/Client/AcceptTrade", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the PlayFab user, based upon a match against a supplied unique identifier, to the friend list of the local user. At
        /// least one of FriendPlayFabId,FriendUsername,FriendEmail, or FriendTitleDisplayName should be initialized.
        /// </summary>
        /// <param name="FriendEmail">Email address of the user to attempt to add to the local user's friend list. (Optional)</param>
        /// <param name="FriendPlayFabId">PlayFab identifier of the user to attempt to add to the local user's friend list. (Optional)</param>
        /// <param name="FriendTitleDisplayName">Title-specific display name of the user to attempt to add to the local user's friend list. (Optional)</param>
        /// <param name="FriendUsername">PlayFab username of the user to attempt to add to the local user's friend list. (Optional)</param>
        public static Task<AddFriendResult> AddFriend(string FriendEmail = default, string FriendPlayFabId = default, string FriendTitleDisplayName = default, string FriendUsername = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddFriendRequest request = new AddFriendRequest()
            {
                FriendEmail = FriendEmail,
                FriendPlayFabId = FriendPlayFabId,
                FriendTitleDisplayName = FriendTitleDisplayName,
                FriendUsername = FriendUsername,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddFriendResult>("/Client/AddFriend", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the specified generic service identifier to the player's PlayFab account. This is designed to allow for a PlayFab
        /// ID lookup of any arbitrary service identifier a title wants to add. This identifier should never be used as
        /// authentication credentials, as the intent is that it is easily accessible by other players.
        /// </summary>
        /// <param name="GenericId">Generic service identifier to add to the player account. (Required)</param>
        public static Task<AddGenericIDResult> AddGenericID(GenericServiceId GenericId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddGenericIDRequest request = new AddGenericIDRequest()
            {
                GenericId = GenericId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddGenericIDResult>("/Client/AddGenericID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds or updates a contact email to the player's profile.
        /// </summary>
        /// <param name="EmailAddress">The new contact email to associate with the player. (Required)</param>
        public static Task<AddOrUpdateContactEmailResult> AddOrUpdateContactEmail(string EmailAddress, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddOrUpdateContactEmailRequest request = new AddOrUpdateContactEmailRequest()
            {
                EmailAddress = EmailAddress,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddOrUpdateContactEmailResult>("/Client/AddOrUpdateContactEmail", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds users to the set of those able to update both the shared data, as well as the set of users in the group. Only users
        /// in the group can add new members. Shared Groups are designed for sharing data between a very small number of players,
        /// please see our guide: https://api.playfab.com/docs/tutorials/landing-players/shared-groups
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddSharedGroupMembersResult>("/Client/AddSharedGroupMembers", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds playfab username/password auth to an existing account created via an anonymous auth method, e.g. automatic device
        /// ID login.
        /// </summary>
        /// <param name="Email">User email address attached to their account (Required)</param>
        /// <param name="Password">Password for the PlayFab account (6-100 characters) (Required)</param>
        /// <param name="Username">PlayFab username for the account (3-20 characters) (Required)</param>
        public static Task<AddUsernamePasswordResult> AddUsernamePassword(string Email, string Password, string Username, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddUsernamePasswordRequest request = new AddUsernamePasswordRequest()
            {
                Email = Email,
                Password = Password,
                Username = Username,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AddUsernamePasswordResult>("/Client/AddUsernamePassword", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increments the user's balance of the specified virtual currency by the stated amount
        /// </summary>
        /// <param name="Amount">Amount to be added to the user balance of the specified virtual currency. (Required)</param>
        /// <param name="VirtualCurrency">Name of the virtual currency which is to be incremented. (Required)</param>
        public static Task<ModifyUserVirtualCurrencyResult> AddUserVirtualCurrency(int Amount, string VirtualCurrency, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest()
            {
                Amount = Amount,
                VirtualCurrency = VirtualCurrency,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Client/AddUserVirtualCurrency", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers the Android device to receive push notifications
        /// </summary>
        /// <param name="ConfirmationMessage">Message to display when confirming push notification. (Optional)</param>
        /// <param name="DeviceToken">Registration ID provided by the Google Cloud Messaging service when the title registered to receive push notifications (see the GCM documentation, here: http://developer.android.com/google/gcm/client.html). (Required)</param>
        /// <param name="SendPushNotificationConfirmation">If true, send a test push message immediately after sucessful registration. Defaults to false. (Optional)</param>
        public static Task<AndroidDevicePushNotificationRegistrationResult> AndroidDevicePushNotificationRegistration(string DeviceToken, string ConfirmationMessage = default, bool? SendPushNotificationConfirmation = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AndroidDevicePushNotificationRegistrationRequest request = new AndroidDevicePushNotificationRegistrationRequest()
            {
                DeviceToken = DeviceToken,
                ConfirmationMessage = ConfirmationMessage,
                SendPushNotificationConfirmation = SendPushNotificationConfirmation,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AndroidDevicePushNotificationRegistrationResult>("/Client/AndroidDevicePushNotificationRegistration", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Attributes an install for advertisment.
        /// </summary>
        /// <param name="Adid">The adid for this device. (Optional)</param>
        /// <param name="Idfa">The IdentifierForAdvertisers for iOS Devices. (Optional)</param>
        public static Task<AttributeInstallResult> AttributeInstall(string Adid = default, string Idfa = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AttributeInstallRequest request = new AttributeInstallRequest()
            {
                Adid = Adid,
                Idfa = Idfa,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<AttributeInstallResult>("/Client/AttributeInstall", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Cancels an open trade (one that has not yet been accepted or cancelled). Note that only the player who created the trade
        /// can cancel it via this API call, to prevent griefing of the trade system (cancelling trades in order to prevent other
        /// players from accepting them, for trades that can be claimed by more than one player).
        /// </summary>
        /// <param name="TradeId">Trade identifier. (Required)</param>
        public static Task<CancelTradeResponse> CancelTrade(string TradeId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CancelTradeRequest request = new CancelTradeRequest()
            {
                TradeId = TradeId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<CancelTradeResponse>("/Client/CancelTrade", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Confirms with the payment provider that the purchase was approved (if applicable) and adjusts inventory and virtual
        /// currency balances as appropriate
        /// </summary>
        /// <param name="OrderId">Purchase order identifier returned from StartPurchase. (Required)</param>
        public static Task<ConfirmPurchaseResult> ConfirmPurchase(string OrderId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ConfirmPurchaseRequest request = new ConfirmPurchaseRequest()
            {
                OrderId = OrderId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConfirmPurchaseResult>("/Client/ConfirmPurchase", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Consume uses of a consumable item. When all uses are consumed, it will be removed from the player's inventory.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ConsumeCount">Number of uses to consume from the item. (Required)</param>
        /// <param name="ItemInstanceId">Unique instance identifier of the item to be consumed. (Required)</param>
        public static Task<ConsumeItemResult> ConsumeItem(int ConsumeCount, string ItemInstanceId, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ConsumeItemRequest request = new ConsumeItemRequest()
            {
                ConsumeCount = ConsumeCount,
                ItemInstanceId = ItemInstanceId,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConsumeItemResult>("/Client/ConsumeItem", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Checks for any new consumable entitlements. If any are found, they are consumed and added as PlayFab items
        /// </summary>
        /// <param name="CatalogVersion">Which catalog to match granted entitlements against. If null, defaults to title default catalog (Optional)</param>
        /// <param name="ServiceLabel">Id of the PSN service label to consume entitlements from (Required)</param>
        public static Task<ConsumePSNEntitlementsResult> ConsumePSNEntitlements(int ServiceLabel, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ConsumePSNEntitlementsRequest request = new ConsumePSNEntitlementsRequest()
            {
                ServiceLabel = ServiceLabel,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ConsumePSNEntitlementsResult>("/Client/ConsumePSNEntitlements", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Grants the player's current entitlements from Xbox Live, consuming all availble items in Xbox and granting them to the
        /// player's PlayFab inventory. This call is idempotent and will not grant previously granted items to the player.
        /// </summary>
        /// <param name="CatalogVersion">Catalog version to use (Optional)</param>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Required)</param>
        public static Task<ConsumeXboxEntitlementsResult> ConsumeXboxEntitlements(string XboxToken, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ConsumeXboxEntitlementsRequest request = new ConsumeXboxEntitlementsRequest()
            {
                XboxToken = XboxToken,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

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
        /// <param name="SharedGroupId">Unique identifier for the shared group (a random identifier will be assigned, if one is not specified). (Optional)</param>
        public static Task<CreateSharedGroupResult> CreateSharedGroup(string SharedGroupId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateSharedGroupRequest request = new CreateSharedGroupRequest()
            {
                SharedGroupId = SharedGroupId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<CreateSharedGroupResult>("/Client/CreateSharedGroup", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Executes a CloudScript function, with the 'currentPlayerId' set to the PlayFab ID of the authenticated player.
        /// </summary>
        /// <param name="FunctionName">The name of the CloudScript function to execute (Required)</param>
        /// <param name="FunctionParameter">Object that is passed in to the function as the first argument (Optional)</param>
        /// <param name="GeneratePlayStreamEvent">Generate a 'player_executed_cloudscript' PlayStream event containing the results of the function execution and other contextual information. This event will show up in the PlayStream debugger console for the player in Game Manager. (Optional)</param>
        /// <param name="RevisionSelection">Option for which revision of the CloudScript to execute. 'Latest' executes the most recently created revision, 'Live' executes the current live, published revision, and 'Specific' executes the specified revision. The default value is 'Specific', if the SpeificRevision parameter is specified, otherwise it is 'Live'. (Optional)</param>
        /// <param name="SpecificRevision">The specivic revision to execute, when RevisionSelection is set to 'Specific' (Optional)</param>
        public static Task<ExecuteCloudScriptResult> ExecuteCloudScript(string FunctionName, object FunctionParameter = default, bool? GeneratePlayStreamEvent = default, CloudScriptRevisionOption? RevisionSelection = default, int? SpecificRevision = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest()
            {
                FunctionName = FunctionName,
                FunctionParameter = FunctionParameter,
                GeneratePlayStreamEvent = GeneratePlayStreamEvent,
                RevisionSelection = RevisionSelection,
                SpecificRevision = SpecificRevision,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

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
                try {
                    wrappedResult.FunctionResult = serializer.DeserializeObject<TOut>(wrappedJson);
                } catch (Exception) {
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
        /// <param name="Email">User email address for the account to find (if no Username is specified). (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user whose info is being requested. Optional, defaults to the authenticated user if no other lookup identifier set. (Optional)</param>
        /// <param name="TitleDisplayName">Title-specific username for the account to find (if no Email is set). Note that if the non-unique Title Display Names option is enabled for the title, attempts to look up users by Title Display Name will always return AccountNotFound. (Optional)</param>
        /// <param name="Username">PlayFab Username for the account to find (if no PlayFabId is specified). (Optional)</param>
        public static Task<GetAccountInfoResult> GetAccountInfo(string Email = default, string PlayFabId = default, string TitleDisplayName = default, string Username = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetAccountInfoRequest request = new GetAccountInfoRequest()
            {
                Email = Email,
                PlayFabId = PlayFabId,
                TitleDisplayName = TitleDisplayName,
                Username = Username,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetAccountInfoResult>("/Client/GetAccountInfo", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all of the characters that belong to a specific user. CharacterIds are not globally unique; characterId must be
        /// evaluated with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Optional)</param>
        public static Task<ListUsersCharactersResult> GetAllUsersCharacters(string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListUsersCharactersRequest request = new ListUsersCharactersRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ListUsersCharactersResult>("/Client/GetAllUsersCharacters", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCatalogItemsResult>("/Client/GetCatalogItems", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the character which is readable and writable by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user to load data for. Optional, defaults to yourself if not set. (Optional)</param>
        public static Task<GetCharacterDataResult> GetCharacterData(string CharacterId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterDataRequest request = new GetCharacterDataRequest()
            {
                CharacterId = CharacterId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Client/GetCharacterData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the specified character's current inventory of virtual goods
        /// </summary>
        /// <param name="CatalogVersion">Used to limit results to only those from a specific catalog version. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        public static Task<GetCharacterInventoryResult> GetCharacterInventory(string CharacterId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterInventoryRequest request = new GetCharacterInventoryRequest()
            {
                CharacterId = CharacterId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterInventoryResult>("/Client/GetCharacterInventory", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        /// <param name="CharacterType">Optional character type on which to filter the leaderboard entries. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. Default 10, maximum 100. (Optional)</param>
        /// <param name="StartPosition">First entry in the leaderboard to be retrieved. (Required)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        public static Task<GetCharacterLeaderboardResult> GetCharacterLeaderboard(int StartPosition, string StatisticName, string CharacterType = default, int? MaxResultsCount = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterLeaderboardRequest request = new GetCharacterLeaderboardRequest()
            {
                StartPosition = StartPosition,
                StatisticName = StatisticName,
                CharacterType = CharacterType,
                MaxResultsCount = MaxResultsCount,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterLeaderboardResult>("/Client/GetCharacterLeaderboard", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the character which can only be read by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">Specific keys to search for in the custom user data. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user to load data for. Optional, defaults to yourself if not set. (Optional)</param>
        public static Task<GetCharacterDataResult> GetCharacterReadOnlyData(string CharacterId, uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterDataRequest request = new GetCharacterDataRequest()
            {
                CharacterId = CharacterId,
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetCharacterDataResult>("/Client/GetCharacterReadOnlyData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details of all title-specific statistics for the user
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        public static Task<GetCharacterStatisticsResult> GetCharacterStatistics(string CharacterId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCharacterStatisticsRequest request = new GetCharacterStatisticsRequest()
            {
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetContentDownloadUrlResult>("/Client/GetContentDownloadUrl", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get details about all current running game servers matching the given parameters.
        /// </summary>
        /// <param name="BuildVersion">Build to match against. (Optional)</param>
        /// <param name="GameMode">Game mode to look for. (Optional)</param>
        /// <param name="Region">Region to check for Game Server Instances. (Optional)</param>
        /// <param name="StatisticName">Statistic name to find statistic-based matches. (Optional)</param>
        /// <param name="TagFilter">Filter to include and/or exclude Game Server Instances associated with certain tags. (Optional)</param>
        public static Task<CurrentGamesResult> GetCurrentGames(string BuildVersion = default, string GameMode = default, Region? Region = default, string StatisticName = default, CollectionFilter TagFilter = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CurrentGamesRequest request = new CurrentGamesRequest()
            {
                BuildVersion = BuildVersion,
                GameMode = GameMode,
                Region = Region,
                StatisticName = StatisticName,
                TagFilter = TagFilter,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<CurrentGamesResult>("/Client/GetCurrentGames", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked friends of the current player for the given statistic, starting from the indicated point in
        /// the leaderboard
        /// </summary>
        /// <param name="IncludeFacebookFriends">Indicates whether Facebook friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="IncludeSteamFriends">Indicates whether Steam service friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. Default 10, maximum 100. (Optional)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StartPosition">Position in the leaderboard to start this listing (defaults to the first entry). (Required)</param>
        /// <param name="StatisticName">Statistic used to rank friends for this leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        /// <param name="XboxToken">Xbox token if Xbox friends should be included. Requires Xbox be configured on PlayFab. (Optional)</param>
        public static Task<GetLeaderboardResult> GetFriendLeaderboard(int StartPosition, string StatisticName, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, int? MaxResultsCount = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, string XboxToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetFriendLeaderboardRequest request = new GetFriendLeaderboardRequest()
            {
                StartPosition = StartPosition,
                StatisticName = StatisticName,
                IncludeFacebookFriends = IncludeFacebookFriends,
                IncludeSteamFriends = IncludeSteamFriends,
                MaxResultsCount = MaxResultsCount,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Client/GetFriendLeaderboard", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked friends of the current player for the given statistic, centered on the requested PlayFab
        /// user. If PlayFabId is empty or null will return currently logged in user.
        /// </summary>
        /// <param name="IncludeFacebookFriends">Indicates whether Facebook friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="IncludeSteamFriends">Indicates whether Steam service friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. Default 10, maximum 100. (Optional)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user to center the leaderboard around. If null will center on the logged in user. (Optional)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StatisticName">Statistic used to rank players for this leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        /// <param name="XboxToken">Xbox token if Xbox friends should be included. Requires Xbox be configured on PlayFab. (Optional)</param>
        public static Task<GetFriendLeaderboardAroundPlayerResult> GetFriendLeaderboardAroundPlayer(string StatisticName, bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, int? MaxResultsCount = default, string PlayFabId = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, string XboxToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetFriendLeaderboardAroundPlayerRequest request = new GetFriendLeaderboardAroundPlayerRequest()
            {
                StatisticName = StatisticName,
                IncludeFacebookFriends = IncludeFacebookFriends,
                IncludeSteamFriends = IncludeSteamFriends,
                MaxResultsCount = MaxResultsCount,
                PlayFabId = PlayFabId,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetFriendLeaderboardAroundPlayerResult>("/Client/GetFriendLeaderboardAroundPlayer", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the current friend list for the local user, constrained to users who have PlayFab accounts. Friends from
        /// linked accounts (Facebook, Steam) are also included. You may optionally exclude some linked services' friends.
        /// </summary>
        /// <param name="IncludeFacebookFriends">Indicates whether Facebook friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="IncludeSteamFriends">Indicates whether Steam service friends should be included in the response. Default is true. (Optional)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="XboxToken">Xbox token if Xbox friends should be included. Requires Xbox be configured on PlayFab. (Optional)</param>
        public static Task<GetFriendsListResult> GetFriendsList(bool? IncludeFacebookFriends = default, bool? IncludeSteamFriends = default, PlayerProfileViewConstraints ProfileConstraints = default, string XboxToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetFriendsListRequest request = new GetFriendsListRequest()
            {
                IncludeFacebookFriends = IncludeFacebookFriends,
                IncludeSteamFriends = IncludeSteamFriends,
                ProfileConstraints = ProfileConstraints,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetFriendsListResult>("/Client/GetFriendsList", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get details about the regions hosting game servers matching the given parameters.
        /// </summary>
        /// <param name="BuildVersion">version of game server for which stats are being requested (Required)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Optional)</param>
        public static Task<GameServerRegionsResult> GetGameServerRegions(string BuildVersion, string TitleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GameServerRegionsRequest request = new GameServerRegionsRequest()
            {
                BuildVersion = BuildVersion,
                TitleId = TitleId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GameServerRegionsResult>("/Client/GetGameServerRegions", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, starting from the indicated point in the leaderboard
        /// </summary>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. Default 10, maximum 100. (Optional)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StartPosition">Position in the leaderboard to start this listing (defaults to the first entry). (Required)</param>
        /// <param name="StatisticName">Statistic used to rank players for this leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        public static Task<GetLeaderboardResult> GetLeaderboard(int StartPosition, string StatisticName, int? MaxResultsCount = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardRequest request = new GetLeaderboardRequest()
            {
                StartPosition = StartPosition,
                StatisticName = StatisticName,
                MaxResultsCount = MaxResultsCount,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardResult>("/Client/GetLeaderboard", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked characters for the given statistic, centered on the requested Character ID
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character on which to center the leaderboard. (Required)</param>
        /// <param name="CharacterType">Optional character type on which to filter the leaderboard entries. (Optional)</param>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. Default 10, maximum 100. (Optional)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        public static Task<GetLeaderboardAroundCharacterResult> GetLeaderboardAroundCharacter(string CharacterId, string StatisticName, string CharacterType = default, int? MaxResultsCount = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardAroundCharacterRequest request = new GetLeaderboardAroundCharacterRequest()
            {
                CharacterId = CharacterId,
                StatisticName = StatisticName,
                CharacterType = CharacterType,
                MaxResultsCount = MaxResultsCount,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundCharacterResult>("/Client/GetLeaderboardAroundCharacter", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of ranked users for the given statistic, centered on the requested player. If PlayFabId is empty or
        /// null will return currently logged in user.
        /// </summary>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. Default 10, maximum 100. (Optional)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user to center the leaderboard around. If null will center on the logged in user. (Optional)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        /// <param name="StatisticName">Statistic used to rank players for this leaderboard. (Required)</param>
        /// <param name="Version">The version of the leaderboard to get. (Optional)</param>
        public static Task<GetLeaderboardAroundPlayerResult> GetLeaderboardAroundPlayer(string StatisticName, int? MaxResultsCount = default, string PlayFabId = default, PlayerProfileViewConstraints ProfileConstraints = default, int? Version = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardAroundPlayerRequest request = new GetLeaderboardAroundPlayerRequest()
            {
                StatisticName = StatisticName,
                MaxResultsCount = MaxResultsCount,
                PlayFabId = PlayFabId,
                ProfileConstraints = ProfileConstraints,
                Version = Version,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardAroundPlayerResult>("/Client/GetLeaderboardAroundPlayer", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of all of the user's characters for the given statistic.
        /// </summary>
        /// <param name="MaxResultsCount">Maximum number of entries to retrieve. (Required)</param>
        /// <param name="StatisticName">Unique identifier for the title-specific statistic for the leaderboard. (Required)</param>
        public static Task<GetLeaderboardForUsersCharactersResult> GetLeaderboardForUserCharacters(int MaxResultsCount, string StatisticName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetLeaderboardForUsersCharactersRequest request = new GetLeaderboardForUsersCharactersRequest()
            {
                MaxResultsCount = MaxResultsCount,
                StatisticName = StatisticName,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetLeaderboardForUsersCharactersResult>("/Client/GetLeaderboardForUserCharacters", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// For payments flows where the provider requires playfab (the fulfiller) to initiate the transaction, but the client
        /// completes the rest of the flow. In the Xsolla case, the token returned here will be passed to Xsolla by the client to
        /// create a cart. Poll GetPurchase using the returned OrderId once you've completed the payment.
        /// </summary>
        /// <param name="TokenProvider">The name of service to provide the payment token. Allowed Values are: xsolla (Required)</param>
        public static Task<GetPaymentTokenResult> GetPaymentToken(string TokenProvider, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPaymentTokenRequest request = new GetPaymentTokenRequest()
            {
                TokenProvider = TokenProvider,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPaymentTokenResult>("/Client/GetPaymentToken", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a Photon custom authentication token that can be used to securely join the player into a Photon room. See
        /// https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/photon/quickstart for more details.
        /// </summary>
        /// <param name="PhotonApplicationId">The Photon applicationId for the game you wish to log into. (Required)</param>
        public static Task<GetPhotonAuthenticationTokenResult> GetPhotonAuthenticationToken(string PhotonApplicationId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest()
            {
                PhotonApplicationId = PhotonApplicationId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPhotonAuthenticationTokenResult>("/Client/GetPhotonAuthenticationToken", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves all of the user's different kinds of info.
        /// </summary>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Required)</param>
        /// <param name="PlayFabId">PlayFabId of the user whose data will be returned. If not filled included, we return the data for the calling player.  (Optional)</param>
        public static Task<GetPlayerCombinedInfoResult> GetPlayerCombinedInfo(GetPlayerCombinedInfoRequestParams InfoRequestParameters, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
            {
                InfoRequestParameters = InfoRequestParameters,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerCombinedInfoResult>("/Client/GetPlayerCombinedInfo", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the player's profile
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Optional)</param>
        /// <param name="ProfileConstraints">If non-null, this determines which properties of the resulting player profiles to return. For API calls from the client, only the allowed client profile properties for the title may be requested. These allowed properties are configured in the Game Manager "Client Profile Options" tab in the "Settings" section. (Optional)</param>
        public static Task<GetPlayerProfileResult> GetPlayerProfile(string PlayFabId = default, PlayerProfileViewConstraints ProfileConstraints = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerProfileRequest request = new GetPlayerProfileRequest()
            {
                PlayFabId = PlayFabId,
                ProfileConstraints = ProfileConstraints,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerProfileResult>("/Client/GetPlayerProfile", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all segments that a player currently belongs to at this moment in time.
        /// </summary>
        public static Task<GetPlayerSegmentsResult> GetPlayerSegments(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerSegmentsRequest request = new GetPlayerSegmentsRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerSegmentsResult>("/Client/GetPlayerSegments", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the indicated statistics (current version and values for all statistics, if none are specified), for the local
        /// player.
        /// </summary>
        /// <param name="StatisticNames">statistics to return (current version will be returned for each) (Optional)</param>
        /// <param name="StatisticNameVersions">statistics to return, if StatisticNames is not set (only statistics which have a version matching that provided will be returned) (Optional)</param>
        public static Task<GetPlayerStatisticsResult> GetPlayerStatistics(List<string> StatisticNames = default, List<StatisticNameVersion> StatisticNameVersions = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest()
            {
                StatisticNames = StatisticNames,
                StatisticNameVersions = StatisticNameVersions,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticsResult>("/Client/GetPlayerStatistics", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticVersionsResult>("/Client/GetPlayerStatisticVersions", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTagsResult>("/Client/GetPlayerTags", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets all trades the player has either opened or accepted, optionally filtered by trade status.
        /// </summary>
        /// <param name="StatusFilter">Returns only trades with the given status. If null, returns all trades. (Optional)</param>
        public static Task<GetPlayerTradesResponse> GetPlayerTrades(TradeStatus? StatusFilter = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerTradesRequest request = new GetPlayerTradesRequest()
            {
                StatusFilter = StatusFilter,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTradesResponse>("/Client/GetPlayerTrades", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookIDsResult>("/Client/GetPlayFabIDsFromFacebookIDs", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Facebook Instant Game identifiers.
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromFacebookInstantGamesIdsResult>("/Client/GetPlayFabIDsFromFacebookInstantGamesIds", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Game Center identifiers (referenced in the Game Center
        /// Programming Guide as the Player Identifier).
        /// </summary>
        /// <param name="GameCenterIDs">Array of unique Game Center identifiers (the Player Identifier) for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromGameCenterIDsResult> GetPlayFabIDsFromGameCenterIDs(List<string> GameCenterIDs, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromGameCenterIDsRequest request = new GetPlayFabIDsFromGameCenterIDsRequest()
            {
                GameCenterIDs = GameCenterIDs,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGameCenterIDsResult>("/Client/GetPlayFabIDsFromGameCenterIDs", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGenericIDsResult>("/Client/GetPlayFabIDsFromGenericIDs", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Google identifiers. The Google identifiers are the IDs for
        /// the user accounts, available as "id" in the Google+ People API calls.
        /// </summary>
        /// <param name="GoogleIDs">Array of unique Google identifiers (Google+ user IDs) for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromGoogleIDsResult> GetPlayFabIDsFromGoogleIDs(List<string> GoogleIDs, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromGoogleIDsRequest request = new GetPlayFabIDsFromGoogleIDsRequest()
            {
                GoogleIDs = GoogleIDs,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromGoogleIDsResult>("/Client/GetPlayFabIDsFromGoogleIDs", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Kongregate identifiers. The Kongregate identifiers are the
        /// IDs for the user accounts, available as "user_id" from the Kongregate API methods(ex:
        /// http://developers.kongregate.com/docs/client/getUserId).
        /// </summary>
        /// <param name="KongregateIDs">Array of unique Kongregate identifiers (Kongregate's user_id) for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromKongregateIDsResult> GetPlayFabIDsFromKongregateIDs(List<string> KongregateIDs, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromKongregateIDsRequest request = new GetPlayFabIDsFromKongregateIDsRequest()
            {
                KongregateIDs = KongregateIDs,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromKongregateIDsResult>("/Client/GetPlayFabIDsFromKongregateIDs", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Nintendo Switch identifiers.
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>("/Client/GetPlayFabIDsFromNintendoSwitchDeviceIds", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromPSNAccountIDsResult>("/Client/GetPlayFabIDsFromPSNAccountIDs", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromSteamIDsResult>("/Client/GetPlayFabIDsFromSteamIDs", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the unique PlayFab identifiers for the given set of Twitch identifiers. The Twitch identifiers are the IDs for
        /// the user accounts, available as "_id" from the Twitch API methods (ex:
        /// https://github.com/justintv/Twitch-API/blob/master/v3_resources/users.md#get-usersuser).
        /// </summary>
        /// <param name="TwitchIds">Array of unique Twitch identifiers (Twitch's _id) for which the title needs to get PlayFab identifiers. (Required)</param>
        public static Task<GetPlayFabIDsFromTwitchIDsResult> GetPlayFabIDsFromTwitchIDs(List<string> TwitchIds, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayFabIDsFromTwitchIDsRequest request = new GetPlayFabIDsFromTwitchIDsRequest()
            {
                TwitchIds = TwitchIds,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromTwitchIDsResult>("/Client/GetPlayFabIDsFromTwitchIDs", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPlayFabIDsFromXboxLiveIDsResult>("/Client/GetPlayFabIDsFromXboxLiveIDs", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetPublisherDataResult>("/Client/GetPublisherData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a purchase along with its current PlayFab status. Returns inventory items from the purchase that are still
        /// active.
        /// </summary>
        /// <param name="OrderId">Purchase order identifier. (Required)</param>
        public static Task<GetPurchaseResult> GetPurchase(string OrderId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPurchaseRequest request = new GetPurchaseRequest()
            {
                OrderId = OrderId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetSharedGroupDataResult>("/Client/GetSharedGroupData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the set of items defined for the specified store, including all prices defined
        /// </summary>
        /// <param name="CatalogVersion">Catalog version to store items from. Use default catalog version if null (Optional)</param>
        /// <param name="StoreId">Unqiue identifier for the store which is being requested. (Required)</param>
        public static Task<GetStoreItemsResult> GetStoreItems(string StoreId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetStoreItemsRequest request = new GetStoreItemsRequest()
            {
                StoreId = StoreId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetStoreItemsResult>("/Client/GetStoreItems", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTimeResult>("/Client/GetTime", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Client/GetTitleData", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTitleNewsResult>("/Client/GetTitleNews", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns the title's base 64 encoded RSA CSP blob.
        /// </summary>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        /// <param name="TitleSharedSecret">The shared secret key for this title (Required)</param>
        public static Task<GetTitlePublicKeyResult> GetTitlePublicKey(string TitleId, string TitleSharedSecret, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitlePublicKeyRequest request = new GetTitlePublicKeyRequest()
            {
                TitleId = TitleId,
                TitleSharedSecret = TitleSharedSecret,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitlePublicKeyResult>("/Client/GetTitlePublicKey", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the current status of an existing trade.
        /// </summary>
        /// <param name="OfferingPlayerId">Player who opened trade. (Required)</param>
        /// <param name="TradeId">Trade identifier as returned by OpenTradeOffer. (Required)</param>
        public static Task<GetTradeStatusResponse> GetTradeStatus(string OfferingPlayerId, string TradeId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTradeStatusRequest request = new GetTradeStatusRequest()
            {
                OfferingPlayerId = OfferingPlayerId,
                TradeId = TradeId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetTradeStatusResponse>("/Client/GetTradeStatus", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">List of unique keys to load from. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user to load data for. Optional, defaults to yourself if not set. When specified to a PlayFab id of another player, then this will only return public keys for that account. (Optional)</param>
        public static Task<GetUserDataResult> GetUserData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the user's current inventory of virtual goods
        /// </summary>
        public static Task<GetUserInventoryResult> GetUserInventory(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserInventoryResult>("/Client/GetUserInventory", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">List of unique keys to load from. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user to load data for. Optional, defaults to yourself if not set. When specified to a PlayFab id of another player, then this will only return public keys for that account. (Optional)</param>
        public static Task<GetUserDataResult> GetUserPublisherData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserPublisherData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the publisher-specific custom data for the user which can only be read by the client
        /// </summary>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">List of unique keys to load from. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user to load data for. Optional, defaults to yourself if not set. When specified to a PlayFab id of another player, then this will only return public keys for that account. (Optional)</param>
        public static Task<GetUserDataResult> GetUserPublisherReadOnlyData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserPublisherReadOnlyData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the title-specific custom data for the user which can only be read by the client
        /// </summary>
        /// <param name="IfChangedFromDataVersion">The version that currently exists according to the caller. The call will return the data for all of the keys if the version in the system is greater than this. (Optional)</param>
        /// <param name="Keys">List of unique keys to load from. (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab identifier of the user to load data for. Optional, defaults to yourself if not set. When specified to a PlayFab id of another player, then this will only return public keys for that account. (Optional)</param>
        public static Task<GetUserDataResult> GetUserReadOnlyData(uint? IfChangedFromDataVersion = default, List<string> Keys = default, string PlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetUserDataRequest request = new GetUserDataRequest()
            {
                IfChangedFromDataVersion = IfChangedFromDataVersion,
                Keys = Keys,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Client/GetUserReadOnlyData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Requests a challenge from the server to be signed by Windows Hello Passport service to authenticate.
        /// </summary>
        /// <param name="PublicKeyHint">SHA256 hash of the PublicKey generated by Windows Hello. (Required)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<GetWindowsHelloChallengeResponse> GetWindowsHelloChallenge(string PublicKeyHint, string TitleId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetWindowsHelloChallengeRequest request = new GetWindowsHelloChallengeRequest()
            {
                PublicKeyHint = PublicKeyHint,
                TitleId = TitleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetWindowsHelloChallengeResponse>("/Client/GetWindowsHelloChallenge", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Grants the specified character type to the user. CharacterIds are not globally unique; characterId must be evaluated
        /// with the parent PlayFabId to guarantee uniqueness.
        /// </summary>
        /// <param name="CatalogVersion">Catalog version from which items are to be granted. (Optional)</param>
        /// <param name="CharacterName">Non-unique display name of the character being granted (1-20 characters in length). (Required)</param>
        /// <param name="ItemId">Catalog item identifier of the item in the user's inventory that corresponds to the character in the catalog to be created. (Required)</param>
        public static Task<GrantCharacterToUserResult> GrantCharacterToUser(string CharacterName, string ItemId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GrantCharacterToUserRequest request = new GrantCharacterToUserRequest()
            {
                CharacterName = CharacterName,
                ItemId = ItemId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<GrantCharacterToUserResult>("/Client/GrantCharacterToUser", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Android device identifier to the user's PlayFab account
        /// </summary>
        /// <param name="AndroidDevice">Specific model of the user's device. (Optional)</param>
        /// <param name="AndroidDeviceId">Android device identifier for the user's device. (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the device, unlink the other user and re-link. (Optional)</param>
        /// <param name="OS">Specific Operating System version for the user's device. (Optional)</param>
        public static Task<LinkAndroidDeviceIDResult> LinkAndroidDeviceID(string AndroidDeviceId, string AndroidDevice = default, bool? ForceLink = default, string OS = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkAndroidDeviceIDRequest request = new LinkAndroidDeviceIDRequest()
            {
                AndroidDeviceId = AndroidDeviceId,
                AndroidDevice = AndroidDevice,
                ForceLink = ForceLink,
                OS = OS,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkAndroidDeviceIDResult>("/Client/LinkAndroidDeviceID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the custom identifier, generated by the title, to the user's PlayFab account
        /// </summary>
        /// <param name="CustomId">Custom unique identifier for the user, generated by the title. (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the custom ID, unlink the other user and re-link. (Optional)</param>
        public static Task<LinkCustomIDResult> LinkCustomID(string CustomId, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkCustomIDRequest request = new LinkCustomIDRequest()
            {
                CustomId = CustomId,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkCustomIDResult>("/Client/LinkCustomID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Facebook account associated with the provided Facebook access token to the user's PlayFab account
        /// </summary>
        /// <param name="AccessToken">Unique identifier from Facebook for the user. (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        public static Task<LinkFacebookAccountResult> LinkFacebookAccount(string AccessToken, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkFacebookAccountRequest request = new LinkFacebookAccountRequest()
            {
                AccessToken = AccessToken,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkFacebookAccountResult>("/Client/LinkFacebookAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Facebook Instant Games Id to the user's PlayFab account
        /// </summary>
        /// <param name="FacebookInstantGamesSignature">Facebook Instant Games signature for the user. (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the Facebook Instant Games ID, unlink the other user and re-link. (Optional)</param>
        public static Task<LinkFacebookInstantGamesIdResult> LinkFacebookInstantGamesId(string FacebookInstantGamesSignature, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkFacebookInstantGamesIdRequest request = new LinkFacebookInstantGamesIdRequest()
            {
                FacebookInstantGamesSignature = FacebookInstantGamesSignature,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkFacebookInstantGamesIdResult>("/Client/LinkFacebookInstantGamesId", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Game Center account associated with the provided Game Center ID to the user's PlayFab account
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="GameCenterId">Game Center identifier for the player account to be linked. (Required)</param>
        /// <param name="PublicKeyUrl">The URL for the public encryption key that will be used to verify the signature. (Optional)</param>
        /// <param name="Salt">A random value used to compute the hash and keep it randomized. (Optional)</param>
        /// <param name="Signature">The verification signature of the authentication payload. (Optional)</param>
        /// <param name="Timestamp">The integer representation of date and time that the signature was created on. PlayFab will reject authentication signatures not within 10 minutes of the server's current time. (Optional)</param>
        public static Task<LinkGameCenterAccountResult> LinkGameCenterAccount(string GameCenterId, bool? ForceLink = default, string PublicKeyUrl = default, string Salt = default, string Signature = default, string Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkGameCenterAccountRequest request = new LinkGameCenterAccountRequest()
            {
                GameCenterId = GameCenterId,
                ForceLink = ForceLink,
                PublicKeyUrl = PublicKeyUrl,
                Salt = Salt,
                Signature = Signature,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkGameCenterAccountResult>("/Client/LinkGameCenterAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the currently signed-in user account to their Google account, using their Google account credentials
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="ServerAuthCode">Server authentication code obtained on the client by calling getServerAuthCode() (https://developers.google.com/identity/sign-in/android/offline-access) from Google Play for the user. (Optional)</param>
        public static Task<LinkGoogleAccountResult> LinkGoogleAccount(bool? ForceLink = default, string ServerAuthCode = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkGoogleAccountRequest request = new LinkGoogleAccountRequest()
            {
                ForceLink = ForceLink,
                ServerAuthCode = ServerAuthCode,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkGoogleAccountResult>("/Client/LinkGoogleAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the vendor-specific iOS device identifier to the user's PlayFab account
        /// </summary>
        /// <param name="DeviceId">Vendor-specific iOS identifier for the user's device. (Required)</param>
        /// <param name="DeviceModel">Specific model of the user's device. (Optional)</param>
        /// <param name="ForceLink">If another user is already linked to the device, unlink the other user and re-link. (Optional)</param>
        /// <param name="OS">Specific Operating System version for the user's device. (Optional)</param>
        public static Task<LinkIOSDeviceIDResult> LinkIOSDeviceID(string DeviceId, string DeviceModel = default, bool? ForceLink = default, string OS = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkIOSDeviceIDRequest request = new LinkIOSDeviceIDRequest()
            {
                DeviceId = DeviceId,
                DeviceModel = DeviceModel,
                ForceLink = ForceLink,
                OS = OS,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkIOSDeviceIDResult>("/Client/LinkIOSDeviceID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Kongregate identifier to the user's PlayFab account
        /// </summary>
        /// <param name="AuthTicket">Valid session auth ticket issued by Kongregate (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="KongregateId">Numeric user ID assigned by Kongregate (Required)</param>
        public static Task<LinkKongregateAccountResult> LinkKongregate(string AuthTicket, string KongregateId, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkKongregateAccountRequest request = new LinkKongregateAccountRequest()
            {
                AuthTicket = AuthTicket,
                KongregateId = KongregateId,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkKongregateAccountResult>("/Client/LinkKongregate", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the NintendoSwitchDeviceId to the user's PlayFab account
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the Nintendo Switch Device ID, unlink the other user and re-link. (Optional)</param>
        /// <param name="NintendoSwitchDeviceId">Nintendo Switch unique identifier for the user's device. (Required)</param>
        public static Task<LinkNintendoSwitchDeviceIdResult> LinkNintendoSwitchDeviceId(string NintendoSwitchDeviceId, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkNintendoSwitchDeviceIdRequest request = new LinkNintendoSwitchDeviceIdRequest()
            {
                NintendoSwitchDeviceId = NintendoSwitchDeviceId,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkNintendoSwitchDeviceIdResult>("/Client/LinkNintendoSwitchDeviceId", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links an OpenID Connect account to a user's PlayFab account, based on an existing relationship between a title and an
        /// Open ID Connect provider and the OpenId Connect JWT from that provider.
        /// </summary>
        /// <param name="ConnectionId">A name that identifies which configured OpenID Connect provider relationship to use. Maximum 100 characters. (Required)</param>
        /// <param name="ForceLink">If another user is already linked to a specific OpenId Connect user, unlink the other user and re-link. (Optional)</param>
        /// <param name="IdToken">The JSON Web token (JWT) returned by the identity provider after login. Represented as the id_token field in the identity provider's response. Used to validate the request and find the user ID (OpenID Connect subject) to link with. (Required)</param>
        public static Task<EmptyResult> LinkOpenIdConnect(string ConnectionId, string IdToken, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkOpenIdConnectRequest request = new LinkOpenIdConnectRequest()
            {
                ConnectionId = ConnectionId,
                IdToken = IdToken,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResult>("/Client/LinkOpenIdConnect", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the PlayStation Network account associated with the provided access code to the user's PlayFab account
        /// </summary>
        /// <param name="AuthCode">Authentication code provided by the PlayStation Network. (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="IssuerId">Id of the PSN issuer environment. If null, defaults to 256 (production) (Optional)</param>
        /// <param name="RedirectUri">Redirect URI supplied to PSN when requesting an auth code (Required)</param>
        public static Task<LinkPSNAccountResult> LinkPSNAccount(string AuthCode, string RedirectUri, bool? ForceLink = default, int? IssuerId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkPSNAccountRequest request = new LinkPSNAccountRequest()
            {
                AuthCode = AuthCode,
                RedirectUri = RedirectUri,
                ForceLink = ForceLink,
                IssuerId = IssuerId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkPSNAccountResult>("/Client/LinkPSNAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Steam account associated with the provided Steam authentication ticket to the user's PlayFab account
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="SteamTicket">Authentication token for the user, returned as a byte array from Steam, and converted to a string (for example, the byte 0x08 should become "08"). (Required)</param>
        public static Task<LinkSteamAccountResult> LinkSteamAccount(string SteamTicket, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkSteamAccountRequest request = new LinkSteamAccountRequest()
            {
                SteamTicket = SteamTicket,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkSteamAccountResult>("/Client/LinkSteamAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Twitch account associated with the token to the user's PlayFab account.
        /// </summary>
        /// <param name="AccessToken">Valid token issued by Twitch (Required)</param>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        public static Task<LinkTwitchAccountResult> LinkTwitch(string AccessToken, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkTwitchAccountRequest request = new LinkTwitchAccountRequest()
            {
                AccessToken = AccessToken,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkTwitchAccountResult>("/Client/LinkTwitch", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Link Windows Hello authentication to the current PlayFab Account
        /// </summary>
        /// <param name="DeviceName">Device name. (Optional)</param>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="PublicKey">PublicKey generated by Windows Hello. (Required)</param>
        /// <param name="UserName">Player's user named used by Windows Hello. (Required)</param>
        public static Task<LinkWindowsHelloAccountResponse> LinkWindowsHello(string PublicKey, string UserName, string DeviceName = default, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkWindowsHelloAccountRequest request = new LinkWindowsHelloAccountRequest()
            {
                PublicKey = PublicKey,
                UserName = UserName,
                DeviceName = DeviceName,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkWindowsHelloAccountResponse>("/Client/LinkWindowsHello", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Links the Xbox Live account associated with the provided access code to the user's PlayFab account
        /// </summary>
        /// <param name="ForceLink">If another user is already linked to the account, unlink the other user and re-link. (Optional)</param>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Required)</param>
        public static Task<LinkXboxAccountResult> LinkXboxAccount(string XboxToken, bool? ForceLink = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LinkXboxAccountRequest request = new LinkXboxAccountRequest()
            {
                XboxToken = XboxToken,
                ForceLink = ForceLink,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<LinkXboxAccountResult>("/Client/LinkXboxAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using the Android device identifier, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user
        /// </summary>
        /// <param name="AndroidDevice">Specific model of the user's device. (Optional)</param>
        /// <param name="AndroidDeviceId">Android device identifier for the user's device. (Optional)</param>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="OS">Specific Operating System version for the user's device. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithAndroidDeviceID(string TitleId, string AndroidDevice = default, string AndroidDeviceId = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string OS = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithAndroidDeviceIDRequest request = new LoginWithAndroidDeviceIDRequest()
            {
                TitleId = TitleId,
                AndroidDevice = AndroidDevice,
                AndroidDeviceId = AndroidDeviceId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                OS = OS,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithAndroidDeviceID", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a custom unique identifier generated by the title, returning a session identifier that can
        /// subsequently be used for API calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="CustomId">Custom unique identifier for the user, generated by the title. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithCustomID(string TitleId, bool? CreateAccount = default, string CustomId = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                CustomId = CustomId,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
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
        /// <param name="Email">Email address for the account. (Required)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="Password">Password for the PlayFab account (6-100 characters) (Required)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithEmailAddress(string Email, string Password, string TitleId, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest()
            {
                Email = Email,
                Password = Password,
                TitleId = TitleId,
                InfoRequestParameters = InfoRequestParameters,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithEmailAddress", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Facebook access token, returning a session identifier that can subsequently be used for API
        /// calls which require an authenticated user
        /// </summary>
        /// <param name="AccessToken">Unique identifier from Facebook for the user. (Required)</param>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithFacebook(string AccessToken, string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithFacebookRequest request = new LoginWithFacebookRequest()
            {
                AccessToken = AccessToken,
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithFacebook", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Facebook Instant Games ID, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user. Requires Facebook Instant Games to be configured.
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="FacebookInstantGamesSignature">Facebook Instant Games signature for the user. (Required)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithFacebookInstantGamesId(string FacebookInstantGamesSignature, string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithFacebookInstantGamesIdRequest request = new LoginWithFacebookInstantGamesIdRequest()
            {
                FacebookInstantGamesSignature = FacebookInstantGamesSignature,
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithFacebookInstantGamesId", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using an iOS Game Center player identifier, returning a session identifier that can subsequently be
        /// used for API calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerId">Unique Game Center player id. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="PublicKeyUrl">The URL for the public encryption key that will be used to verify the signature. (Optional)</param>
        /// <param name="Salt">A random value used to compute the hash and keep it randomized. (Optional)</param>
        /// <param name="Signature">The verification signature of the authentication payload. (Optional)</param>
        /// <param name="Timestamp">The integer representation of date and time that the signature was created on. PlayFab will reject authentication signatures not within 10 minutes of the server's current time. (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithGameCenter(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerId = default, string PlayerSecret = default, string PublicKeyUrl = default, string Salt = default, string Signature = default, string Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithGameCenterRequest request = new LoginWithGameCenterRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerId = PlayerId,
                PlayerSecret = PlayerSecret,
                PublicKeyUrl = PublicKeyUrl,
                Salt = Salt,
                Signature = Signature,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithGameCenter", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using their Google account credentials
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="ServerAuthCode">OAuth 2.0 server authentication code obtained on the client by calling the getServerAuthCode() (https://developers.google.com/identity/sign-in/android/offline-access) Google client API. (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithGoogleAccount(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string ServerAuthCode = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
                ServerAuthCode = ServerAuthCode,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithGoogleAccount", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using the vendor-specific iOS device identifier, returning a session identifier that can subsequently
        /// be used for API calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="DeviceId">Vendor-specific iOS identifier for the user's device. (Optional)</param>
        /// <param name="DeviceModel">Specific model of the user's device. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="OS">Specific Operating System version for the user's device. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithIOSDeviceID(string TitleId, bool? CreateAccount = default, string DeviceId = default, string DeviceModel = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string OS = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithIOSDeviceIDRequest request = new LoginWithIOSDeviceIDRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                DeviceId = DeviceId,
                DeviceModel = DeviceModel,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                OS = OS,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithIOSDeviceID", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Kongregate player account.
        /// </summary>
        /// <param name="AuthTicket">Token issued by Kongregate's client API for the user. (Optional)</param>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="KongregateId">Numeric user ID assigned by Kongregate (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithKongregate(string TitleId, string AuthTicket = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string KongregateId = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithKongregateRequest request = new LoginWithKongregateRequest()
            {
                TitleId = TitleId,
                AuthTicket = AuthTicket,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                KongregateId = KongregateId,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithKongregate", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Nintendo Switch Device ID, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="NintendoSwitchDeviceId">Nintendo Switch unique identifier for the user's device. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithNintendoSwitchDeviceId(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string NintendoSwitchDeviceId = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithNintendoSwitchDeviceIdRequest request = new LoginWithNintendoSwitchDeviceIdRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                NintendoSwitchDeviceId = NintendoSwitchDeviceId,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithNintendoSwitchDeviceId", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Logs in a user with an Open ID Connect JWT created by an existing relationship between a title and an Open ID Connect
        /// provider.
        /// </summary>
        /// <param name="ConnectionId">A name that identifies which configured OpenID Connect provider relationship to use. Maximum 100 characters. (Required)</param>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="IdToken">The JSON Web token (JWT) returned by the identity provider after login. Represented as the id_token field in the identity provider's response. (Required)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithOpenIdConnect(string ConnectionId, string IdToken, string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithOpenIdConnectRequest request = new LoginWithOpenIdConnectRequest()
            {
                ConnectionId = ConnectionId,
                IdToken = IdToken,
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
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
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="Password">Password for the PlayFab account (6-100 characters) (Required)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        /// <param name="Username">PlayFab username for the account. (Required)</param>
        public static Task<LoginResult> LoginWithPlayFab(string Password, string TitleId, string Username, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
            {
                Password = Password,
                TitleId = TitleId,
                Username = Username,
                InfoRequestParameters = InfoRequestParameters,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithPlayFab", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a PlayStation Network authentication code, returning a session identifier that can subsequently
        /// be used for API calls which require an authenticated user
        /// </summary>
        /// <param name="AuthCode">Auth code provided by the PSN OAuth provider. (Optional)</param>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="IssuerId">Id of the PSN issuer environment. If null, defaults to 256 (production) (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="RedirectUri">Redirect URI supplied to PSN when requesting an auth code (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithPSN(string TitleId, string AuthCode = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, int? IssuerId = default, string PlayerSecret = default, string RedirectUri = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithPSNRequest request = new LoginWithPSNRequest()
            {
                TitleId = TitleId,
                AuthCode = AuthCode,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                IssuerId = IssuerId,
                PlayerSecret = PlayerSecret,
                RedirectUri = RedirectUri,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithPSN", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Steam authentication ticket, returning a session identifier that can subsequently be used for
        /// API calls which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="SteamTicket">Authentication token for the user, returned as a byte array from Steam, and converted to a string (for example, the byte 0x08 should become "08"). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithSteam(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string SteamTicket = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithSteamRequest request = new LoginWithSteamRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
                SteamTicket = SteamTicket,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithSteam", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Twitch access token.
        /// </summary>
        /// <param name="AccessToken">Token issued by Twitch's API for the user. (Optional)</param>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithTwitch(string TitleId, string AccessToken = default, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithTwitchRequest request = new LoginWithTwitchRequest()
            {
                TitleId = TitleId,
                AccessToken = AccessToken,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
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
        /// <param name="ChallengeSignature">The signed response from the user for the Challenge. (Required)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PublicKeyHint">SHA256 hash of the PublicKey generated by Windows Hello. (Required)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<LoginResult> LoginWithWindowsHello(string ChallengeSignature, string PublicKeyHint, string TitleId, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithWindowsHelloRequest request = new LoginWithWindowsHelloRequest()
            {
                ChallengeSignature = ChallengeSignature,
                PublicKeyHint = PublicKeyHint,
                TitleId = TitleId,
                InfoRequestParameters = InfoRequestParameters,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/LoginWithWindowsHello", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Signs the user in using a Xbox Live Token, returning a session identifier that can subsequently be used for API calls
        /// which require an authenticated user
        /// </summary>
        /// <param name="CreateAccount">Automatically create a PlayFab account if one is not currently linked to this ID. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Optional)</param>
        public static Task<LoginResult> LoginWithXbox(string TitleId, bool? CreateAccount = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string XboxToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LoginWithXboxRequest request = new LoginWithXboxRequest()
            {
                TitleId = TitleId,
                CreateAccount = CreateAccount,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);
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
        /// <param name="BuildVersion">Build version to match against. [Note: Required if LobbyId is not specified] (Optional)</param>
        /// <param name="CharacterId">Character to use for stats based matching. Leave null to use account stats. (Optional)</param>
        /// <param name="GameMode">Game mode to match make against. [Note: Required if LobbyId is not specified] (Optional)</param>
        /// <param name="LobbyId">Lobby identifier to match make against. This is used to select a specific Game Server Instance. (Optional)</param>
        /// <param name="Region">Region to match make against. [Note: Required if LobbyId is not specified] (Optional)</param>
        /// <param name="StartNewIfNoneFound">Start a game session if one with an open slot is not found. Defaults to true. (Optional)</param>
        /// <param name="StatisticName">Player statistic to use in finding a match. May be null for no stat-based matching. (Optional)</param>
        /// <param name="TagFilter">Filter to include and/or exclude Game Server Instances associated with certain Tags (Optional)</param>
        public static Task<MatchmakeResult> Matchmake(string BuildVersion = default, string CharacterId = default, string GameMode = default, string LobbyId = default, Region? Region = default, bool? StartNewIfNoneFound = default, string StatisticName = default, CollectionFilter TagFilter = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            MatchmakeRequest request = new MatchmakeRequest()
            {
                BuildVersion = BuildVersion,
                CharacterId = CharacterId,
                GameMode = GameMode,
                LobbyId = LobbyId,
                Region = Region,
                StartNewIfNoneFound = StartNewIfNoneFound,
                StatisticName = StatisticName,
                TagFilter = TagFilter,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<MatchmakeResult>("/Client/Matchmake", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Opens a new outstanding trade. Note that a given item instance may only be in one open trade at a time.
        /// </summary>
        /// <param name="AllowedPlayerIds">Players who are allowed to accept the trade. If null, the trade may be accepted by any player. If empty, the trade may not be accepted by any player. (Optional)</param>
        /// <param name="OfferedInventoryInstanceIds">Player inventory items offered for trade. If not set, the trade is effectively a gift request (Optional)</param>
        /// <param name="RequestedCatalogItemIds">Catalog items accepted for the trade. If not set, the trade is effectively a gift. (Optional)</param>
        public static Task<OpenTradeResponse> OpenTrade(List<string> AllowedPlayerIds = default, List<string> OfferedInventoryInstanceIds = default, List<string> RequestedCatalogItemIds = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            OpenTradeRequest request = new OpenTradeRequest()
            {
                AllowedPlayerIds = AllowedPlayerIds,
                OfferedInventoryInstanceIds = OfferedInventoryInstanceIds,
                RequestedCatalogItemIds = RequestedCatalogItemIds,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<OpenTradeResponse>("/Client/OpenTrade", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Selects a payment option for purchase order created via StartPurchase
        /// </summary>
        /// <param name="Currency">Currency to use to fund the purchase. (Required)</param>
        /// <param name="OrderId">Purchase order identifier returned from StartPurchase. (Required)</param>
        /// <param name="ProviderName">Payment provider to use to fund the purchase. (Required)</param>
        /// <param name="ProviderTransactionId">Payment provider transaction identifier. Required for Facebook Payments. (Optional)</param>
        public static Task<PayForPurchaseResult> PayForPurchase(string Currency, string OrderId, string ProviderName, string ProviderTransactionId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            PayForPurchaseRequest request = new PayForPurchaseRequest()
            {
                Currency = Currency,
                OrderId = OrderId,
                ProviderName = ProviderName,
                ProviderTransactionId = ProviderTransactionId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<PayForPurchaseResult>("/Client/PayForPurchase", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Buys a single item with virtual currency. You must specify both the virtual currency to use to purchase, as well as what
        /// the client believes the price to be. This lets the server fail the purchase if the price has changed.
        /// </summary>
        /// <param name="CatalogVersion">Catalog version for the items to be purchased (defaults to most recent version. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ItemId">Unique identifier of the item to purchase. (Required)</param>
        /// <param name="Price">Price the client expects to pay for the item (in case a new catalog or store was uploaded, with new prices). (Required)</param>
        /// <param name="StoreId">Store to buy this item through. If not set, prices default to those in the catalog. (Optional)</param>
        /// <param name="VirtualCurrency">Virtual currency to use to purchase the item. (Required)</param>
        public static Task<PurchaseItemResult> PurchaseItem(string ItemId, int Price, string VirtualCurrency, string CatalogVersion = default, string CharacterId = default, string StoreId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            PurchaseItemRequest request = new PurchaseItemRequest()
            {
                ItemId = ItemId,
                Price = Price,
                VirtualCurrency = VirtualCurrency,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
                StoreId = StoreId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<PurchaseItemResult>("/Client/PurchaseItem", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the virtual goods associated with the coupon to the user's inventory. Coupons can be generated via the
        /// Economy->Catalogs tab in the PlayFab Game Manager.
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the coupon. If null, uses the default catalog (Optional)</param>
        /// <param name="CharacterId">Optional identifier for the Character that should receive the item. If null, item is added to the player (Optional)</param>
        /// <param name="CouponCode">Generated coupon code to redeem. (Required)</param>
        public static Task<RedeemCouponResult> RedeemCoupon(string CouponCode, string CatalogVersion = default, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RedeemCouponRequest request = new RedeemCouponRequest()
            {
                CouponCode = CouponCode,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RedeemCouponResult>("/Client/RedeemCoupon", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Uses the supplied OAuth code to refresh the internally cached player PSN auth token
        /// </summary>
        /// <param name="AuthCode">Auth code returned by PSN OAuth system. (Required)</param>
        /// <param name="IssuerId">Id of the PSN issuer environment. If null, defaults to 256 (production) (Optional)</param>
        /// <param name="RedirectUri">Redirect URI supplied to PSN when requesting an auth code (Required)</param>
        public static Task<EmptyResponse> RefreshPSNAuthToken(string AuthCode, string RedirectUri, int? IssuerId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RefreshPSNAuthTokenRequest request = new RefreshPSNAuthTokenRequest()
            {
                AuthCode = AuthCode,
                RedirectUri = RedirectUri,
                IssuerId = IssuerId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/RefreshPSNAuthToken", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers the iOS device to receive push notifications
        /// </summary>
        /// <param name="ConfirmationMessage">Message to display when confirming push notification. (Optional)</param>
        /// <param name="DeviceToken">Unique token generated by the Apple Push Notification service when the title registered to receive push notifications. (Required)</param>
        /// <param name="SendPushNotificationConfirmation">If true, send a test push message immediately after sucessful registration. Defaults to false. (Optional)</param>
        public static Task<RegisterForIOSPushNotificationResult> RegisterForIOSPushNotification(string DeviceToken, string ConfirmationMessage = default, bool? SendPushNotificationConfirmation = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RegisterForIOSPushNotificationRequest request = new RegisterForIOSPushNotificationRequest()
            {
                DeviceToken = DeviceToken,
                ConfirmationMessage = ConfirmationMessage,
                SendPushNotificationConfirmation = SendPushNotificationConfirmation,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RegisterForIOSPushNotificationResult>("/Client/RegisterForIOSPushNotification", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers a new Playfab user account, returning a session identifier that can subsequently be used for API calls which
        /// require an authenticated user. You must supply either a username or an email address.
        /// </summary>
        /// <param name="DisplayName">An optional parameter for setting the display name for this title (3-25 characters). (Optional)</param>
        /// <param name="Email">User email address attached to their account (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="Password">Password for the PlayFab account (6-100 characters) (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="RequireBothUsernameAndEmail">An optional parameter that specifies whether both the username and email parameters are required. If true, both parameters are required; if false, the user must supply either the username or email parameter. The default value is true. (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        /// <param name="Username">PlayFab username for the account (3-20 characters) (Optional)</param>
        public static Task<RegisterPlayFabUserResult> RegisterPlayFabUser(string TitleId, string DisplayName = default, string Email = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string Password = default, string PlayerSecret = default, bool? RequireBothUsernameAndEmail = default, string Username = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
            {
                TitleId = TitleId,
                DisplayName = DisplayName,
                Email = Email,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                Password = Password,
                PlayerSecret = PlayerSecret,
                RequireBothUsernameAndEmail = RequireBothUsernameAndEmail,
                Username = Username,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<RegisterPlayFabUserResult>("/Client/RegisterPlayFabUser", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers a new PlayFab user account using Windows Hello authentication, returning a session ticket that can
        /// subsequently be used for API calls which require an authenticated user
        /// </summary>
        /// <param name="DeviceName">Device name. (Optional)</param>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="InfoRequestParameters">Flags for which pieces of info to return for the user. (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        /// <param name="PublicKey">PublicKey generated by Windows Hello. (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        /// <param name="UserName">Player's user name used by Windows Hello. (Optional)</param>
        public static Task<LoginResult> RegisterWithWindowsHello(string TitleId, string DeviceName = default, string EncryptedRequest = default, GetPlayerCombinedInfoRequestParams InfoRequestParameters = default, string PlayerSecret = default, string PublicKey = default, string UserName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RegisterWithWindowsHelloRequest request = new RegisterWithWindowsHelloRequest()
            {
                TitleId = TitleId,
                DeviceName = DeviceName,
                EncryptedRequest = EncryptedRequest,
                InfoRequestParameters = InfoRequestParameters,
                PlayerSecret = PlayerSecret,
                PublicKey = PublicKey,
                UserName = UserName,
            };

            var context = GetContext(customAuthContext);
            request.TitleId = request.TitleId ?? PlayFabSettings.TitleId;

            return PlayFabHttp.MakeApiCallAsync<LoginResult>("/Client/RegisterWithWindowsHello", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a contact email from the player's profile.
        /// </summary>
        public static Task<RemoveContactEmailResult> RemoveContactEmail(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveContactEmailRequest request = new RemoveContactEmailRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveContactEmailResult>("/Client/RemoveContactEmail", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a specified user from the friend list of the local user
        /// </summary>
        /// <param name="FriendPlayFabId">PlayFab identifier of the friend account which is to be removed. (Required)</param>
        public static Task<RemoveFriendResult> RemoveFriend(string FriendPlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveFriendRequest request = new RemoveFriendRequest()
            {
                FriendPlayFabId = FriendPlayFabId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveFriendResult>("/Client/RemoveFriend", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the specified generic service identifier from the player's PlayFab account.
        /// </summary>
        /// <param name="GenericId">Generic service identifier to be removed from the player. (Required)</param>
        public static Task<RemoveGenericIDResult> RemoveGenericID(GenericServiceId GenericId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveGenericIDRequest request = new RemoveGenericIDRequest()
            {
                GenericId = GenericId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RemoveSharedGroupMembersResult>("/Client/RemoveSharedGroupMembers", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Write a PlayStream event to describe the provided player device information. This API method is not designed to be
        /// called directly by developers. Each PlayFab client SDK will eventually report this information automatically.
        /// </summary>
        /// <param name="Info">Information posted to the PlayStream Event. Currently arbitrary, and specific to the environment sending it. (Optional)</param>
        public static Task<EmptyResponse> ReportDeviceInfo(Dictionary<string,object> Info = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeviceInfoRequest request = new DeviceInfoRequest()
            {
                Info = Info,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/ReportDeviceInfo", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Submit a report for another player (due to bad bahavior, etc.), so that customer service representatives for the title
        /// can take action concerning potentially toxic players.
        /// </summary>
        /// <param name="Comment">Optional additional comment by reporting player. (Optional)</param>
        /// <param name="ReporteeId">Unique PlayFab identifier of the reported player. (Required)</param>
        public static Task<ReportPlayerClientResult> ReportPlayer(string ReporteeId, string Comment = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ReportPlayerClientRequest request = new ReportPlayerClientRequest()
            {
                ReporteeId = ReporteeId,
                Comment = Comment,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ReportPlayerClientResult>("/Client/ReportPlayer", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Restores all in-app purchases based on the given restore receipt
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the restored items. If null, defaults to primary catalog. (Optional)</param>
        /// <param name="ReceiptData">Base64 encoded receipt data, passed back by the App Store as a result of a successful purchase. (Required)</param>
        public static Task<RestoreIOSPurchasesResult> RestoreIOSPurchases(string ReceiptData, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RestoreIOSPurchasesRequest request = new RestoreIOSPurchasesRequest()
            {
                ReceiptData = ReceiptData,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<RestoreIOSPurchasesResult>("/Client/RestoreIOSPurchases", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Forces an email to be sent to the registered email address for the user's account, with a link allowing the user to
        /// change the password.If an account recovery email template ID is provided, an email using the custom email template will
        /// be used.
        /// </summary>
        /// <param name="Email">User email address attached to their account (Required)</param>
        /// <param name="EmailTemplateId">The email template id of the account recovery email template to send. (Optional)</param>
        /// <param name="TitleId">Unique identifier for the title, found in the Settings > Game Properties section of the PlayFab developer site when a title has been selected. (Required)</param>
        public static Task<SendAccountRecoveryEmailResult> SendAccountRecoveryEmail(string Email, string TitleId, string EmailTemplateId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SendAccountRecoveryEmailRequest request = new SendAccountRecoveryEmailRequest()
            {
                Email = Email,
                TitleId = TitleId,
                EmailTemplateId = EmailTemplateId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SendAccountRecoveryEmailResult>("/Client/SendAccountRecoveryEmail", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the tag list for a specified user in the friend list of the local user
        /// </summary>
        /// <param name="FriendPlayFabId">PlayFab identifier of the friend account to which the tag(s) should be applied. (Required)</param>
        /// <param name="Tags">Array of tags to set on the friend account. (Required)</param>
        public static Task<SetFriendTagsResult> SetFriendTags(string FriendPlayFabId, List<string> Tags, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetFriendTagsRequest request = new SetFriendTagsRequest()
            {
                FriendPlayFabId = FriendPlayFabId,
                Tags = Tags,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<SetFriendTagsResult>("/Client/SetFriendTags", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the player's secret if it is not already set. Player secrets are used to sign API requests. To reset a player's
        /// secret use the Admin or Server API method SetPlayerSecret.
        /// </summary>
        /// <param name="EncryptedRequest">Base64 encoded body that is encrypted with the Title's public RSA key (Enterprise Only). (Optional)</param>
        /// <param name="PlayerSecret">Player secret that is used to verify API request signatures (Enterprise Only). (Optional)</param>
        public static Task<SetPlayerSecretResult> SetPlayerSecret(string EncryptedRequest = default, string PlayerSecret = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetPlayerSecretRequest request = new SetPlayerSecretRequest()
            {
                EncryptedRequest = EncryptedRequest,
                PlayerSecret = PlayerSecret,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<SetPlayerSecretResult>("/Client/SetPlayerSecret", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Start a new game server with a given configuration, add the current player and return the connection information.
        /// </summary>
        /// <param name="BuildVersion">version information for the build of the game server which is to be started (Required)</param>
        /// <param name="CharacterId">character to use for stats based matching. Leave null to use account stats (Optional)</param>
        /// <param name="CustomCommandLineData">custom command line argument when starting game server process (Optional)</param>
        /// <param name="GameMode">the title-defined game mode this server is to be running (defaults to 0 if there is only one mode) (Required)</param>
        /// <param name="Region">the region to associate this server with for match filtering (Required)</param>
        /// <param name="StatisticName">player statistic for others to use in finding this game. May be null for no stat-based matching (Optional)</param>
        public static Task<StartGameResult> StartGame(string BuildVersion, string GameMode, Region Region, string CharacterId = default, string CustomCommandLineData = default, string StatisticName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            StartGameRequest request = new StartGameRequest()
            {
                BuildVersion = BuildVersion,
                GameMode = GameMode,
                Region = Region,
                CharacterId = CharacterId,
                CustomCommandLineData = CustomCommandLineData,
                StatisticName = StatisticName,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<StartGameResult>("/Client/StartGame", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates an order for a list of items from the title catalog
        /// </summary>
        /// <param name="CatalogVersion">Catalog version for the items to be purchased. Defaults to most recent catalog. (Optional)</param>
        /// <param name="Items">Array of items to purchase. (Required)</param>
        /// <param name="StoreId">Store through which to purchase items. If not set, prices will be pulled from the catalog itself. (Optional)</param>
        public static Task<StartPurchaseResult> StartPurchase(List<ItemPurchaseRequest> Items, string CatalogVersion = default, string StoreId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            StartPurchaseRequest request = new StartPurchaseRequest()
            {
                Items = Items,
                CatalogVersion = CatalogVersion,
                StoreId = StoreId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<StartPurchaseResult>("/Client/StartPurchase", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the user's balance of the specified virtual currency by the stated amount. It is possible to make a VC
        /// balance negative with this API.
        /// </summary>
        /// <param name="Amount">Amount to be subtracted from the user balance of the specified virtual currency. (Required)</param>
        /// <param name="VirtualCurrency">Name of the virtual currency which is to be decremented. (Required)</param>
        public static Task<ModifyUserVirtualCurrencyResult> SubtractUserVirtualCurrency(int Amount, string VirtualCurrency, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SubtractUserVirtualCurrencyRequest request = new SubtractUserVirtualCurrencyRequest()
            {
                Amount = Amount,
                VirtualCurrency = VirtualCurrency,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Client/SubtractUserVirtualCurrency", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Android device identifier from the user's PlayFab account
        /// </summary>
        /// <param name="AndroidDeviceId">Android device identifier for the user's device. If not specified, the most recently signed in Android Device ID will be used. (Optional)</param>
        public static Task<UnlinkAndroidDeviceIDResult> UnlinkAndroidDeviceID(string AndroidDeviceId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkAndroidDeviceIDRequest request = new UnlinkAndroidDeviceIDRequest()
            {
                AndroidDeviceId = AndroidDeviceId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkAndroidDeviceIDResult>("/Client/UnlinkAndroidDeviceID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related custom identifier from the user's PlayFab account
        /// </summary>
        /// <param name="CustomId">Custom unique identifier for the user, generated by the title. If not specified, the most recently signed in Custom ID will be used. (Optional)</param>
        public static Task<UnlinkCustomIDResult> UnlinkCustomID(string CustomId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkCustomIDRequest request = new UnlinkCustomIDRequest()
            {
                CustomId = CustomId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkCustomIDResult>("/Client/UnlinkCustomID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Facebook account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkFacebookAccountResult> UnlinkFacebookAccount(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkFacebookAccountRequest request = new UnlinkFacebookAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkFacebookAccountResult>("/Client/UnlinkFacebookAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Facebook Instant Game Ids from the user's PlayFab account
        /// </summary>
        /// <param name="FacebookInstantGamesId">Facebook Instant Games identifier for the user. If not specified, the most recently signed in ID will be used. (Optional)</param>
        public static Task<UnlinkFacebookInstantGamesIdResult> UnlinkFacebookInstantGamesId(string FacebookInstantGamesId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkFacebookInstantGamesIdRequest request = new UnlinkFacebookInstantGamesIdRequest()
            {
                FacebookInstantGamesId = FacebookInstantGamesId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkFacebookInstantGamesIdResult>("/Client/UnlinkFacebookInstantGamesId", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Game Center account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkGameCenterAccountResult> UnlinkGameCenterAccount(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkGameCenterAccountRequest request = new UnlinkGameCenterAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkGameCenterAccountResult>("/Client/UnlinkGameCenterAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Google account from the user's PlayFab account
        /// (https://developers.google.com/android/reference/com/google/android/gms/auth/GoogleAuthUtil#public-methods).
        /// </summary>
        public static Task<UnlinkGoogleAccountResult> UnlinkGoogleAccount(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkGoogleAccountRequest request = new UnlinkGoogleAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkGoogleAccountResult>("/Client/UnlinkGoogleAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related iOS device identifier from the user's PlayFab account
        /// </summary>
        /// <param name="DeviceId">Vendor-specific iOS identifier for the user's device. If not specified, the most recently signed in iOS Device ID will be used. (Optional)</param>
        public static Task<UnlinkIOSDeviceIDResult> UnlinkIOSDeviceID(string DeviceId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkIOSDeviceIDRequest request = new UnlinkIOSDeviceIDRequest()
            {
                DeviceId = DeviceId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkIOSDeviceIDResult>("/Client/UnlinkIOSDeviceID", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Kongregate identifier from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkKongregateAccountResult> UnlinkKongregate(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkKongregateAccountRequest request = new UnlinkKongregateAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkKongregateAccountResult>("/Client/UnlinkKongregate", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related NintendoSwitchDeviceId from the user's PlayFab account
        /// </summary>
        /// <param name="NintendoSwitchDeviceId">Nintendo Switch Device identifier for the user. If not specified, the most recently signed in device ID will be used. (Optional)</param>
        public static Task<UnlinkNintendoSwitchDeviceIdResult> UnlinkNintendoSwitchDeviceId(string NintendoSwitchDeviceId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkNintendoSwitchDeviceIdRequest request = new UnlinkNintendoSwitchDeviceIdRequest()
            {
                NintendoSwitchDeviceId = NintendoSwitchDeviceId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkNintendoSwitchDeviceIdResult>("/Client/UnlinkNintendoSwitchDeviceId", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks an OpenID Connect account from a user's PlayFab account, based on the connection ID of an existing relationship
        /// between a title and an Open ID Connect provider.
        /// </summary>
        /// <param name="ConnectionId">A name that identifies which configured OpenID Connect provider relationship to use. Maximum 100 characters. (Required)</param>
        public static Task<EmptyResponse> UnlinkOpenIdConnect(string ConnectionId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UninkOpenIdConnectRequest request = new UninkOpenIdConnectRequest()
            {
                ConnectionId = ConnectionId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/UnlinkOpenIdConnect", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related PSN account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkPSNAccountResult> UnlinkPSNAccount(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkPSNAccountRequest request = new UnlinkPSNAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkPSNAccountResult>("/Client/UnlinkPSNAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Steam account from the user's PlayFab account
        /// </summary>
        public static Task<UnlinkSteamAccountResult> UnlinkSteamAccount(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkSteamAccountRequest request = new UnlinkSteamAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkSteamAccountResult>("/Client/UnlinkSteamAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Twitch account from the user's PlayFab account.
        /// </summary>
        public static Task<UnlinkTwitchAccountResult> UnlinkTwitch(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkTwitchAccountRequest request = new UnlinkTwitchAccountRequest()
            {
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkTwitchAccountResult>("/Client/UnlinkTwitch", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlink Windows Hello authentication from the current PlayFab Account
        /// </summary>
        /// <param name="PublicKeyHint">SHA256 hash of the PublicKey generated by Windows Hello. (Required)</param>
        public static Task<UnlinkWindowsHelloAccountResponse> UnlinkWindowsHello(string PublicKeyHint, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkWindowsHelloAccountRequest request = new UnlinkWindowsHelloAccountRequest()
            {
                PublicKeyHint = PublicKeyHint,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkWindowsHelloAccountResponse>("/Client/UnlinkWindowsHello", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unlinks the related Xbox Live account from the user's PlayFab account
        /// </summary>
        /// <param name="XboxToken">Token provided by the Xbox Live SDK/XDK method GetTokenAndSignatureAsync("POST", "https://playfabapi.com/", ""). (Required)</param>
        public static Task<UnlinkXboxAccountResult> UnlinkXboxAccount(string XboxToken, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlinkXboxAccountRequest request = new UnlinkXboxAccountRequest()
            {
                XboxToken = XboxToken,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlinkXboxAccountResult>("/Client/UnlinkXboxAccount", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Opens the specified container, with the specified key (when required), and returns the contents of the opened container.
        /// If the container (and key when relevant) are consumable (RemainingUses > 0), their RemainingUses will be decremented,
        /// consistent with the operation of ConsumeItem.
        /// </summary>
        /// <param name="CatalogVersion">Specifies the catalog version that should be used to determine container contents. If unspecified, uses catalog associated with the item instance. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ContainerItemInstanceId">ItemInstanceId of the container to unlock. (Required)</param>
        /// <param name="KeyItemInstanceId">ItemInstanceId of the key that will be consumed by unlocking this container. If the container requires a key, this parameter is required. (Optional)</param>
        public static Task<UnlockContainerItemResult> UnlockContainerInstance(string ContainerItemInstanceId, string CatalogVersion = default, string CharacterId = default, string KeyItemInstanceId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlockContainerInstanceRequest request = new UnlockContainerInstanceRequest()
            {
                ContainerItemInstanceId = ContainerItemInstanceId,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
                KeyItemInstanceId = KeyItemInstanceId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Client/UnlockContainerInstance", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Searches target inventory for an ItemInstance matching the given CatalogItemId, if necessary unlocks it using an
        /// appropriate key, and returns the contents of the opened container. If the container (and key when relevant) are
        /// consumable (RemainingUses > 0), their RemainingUses will be decremented, consistent with the operation of ConsumeItem.
        /// </summary>
        /// <param name="CatalogVersion">Specifies the catalog version that should be used to determine container contents. If unspecified, uses default/primary catalog. (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Optional)</param>
        /// <param name="ContainerItemId">Catalog ItemId of the container type to unlock. (Required)</param>
        public static Task<UnlockContainerItemResult> UnlockContainerItem(string ContainerItemId, string CatalogVersion = default, string CharacterId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnlockContainerItemRequest request = new UnlockContainerItemRequest()
            {
                ContainerItemId = ContainerItemId,
                CatalogVersion = CatalogVersion,
                CharacterId = CharacterId,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UnlockContainerItemResult>("/Client/UnlockContainerItem", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update the avatar URL of the player
        /// </summary>
        /// <param name="ImageUrl">URL of the avatar image. If empty, it removes the existing avatar URL. (Required)</param>
        public static Task<EmptyResponse> UpdateAvatarUrl(string ImageUrl, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateAvatarUrlRequest request = new UpdateAvatarUrlRequest()
            {
                ImageUrl = ImageUrl,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Client/UpdateAvatarUrl", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the title-specific custom data for the user's character which is readable and writable by the client
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. (Optional)</param>
        public static Task<UpdateCharacterDataResult> UpdateCharacterData(string CharacterId, Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCharacterDataRequest request = new UpdateCharacterDataRequest()
            {
                CharacterId = CharacterId,
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterDataResult>("/Client/UpdateCharacterData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the specific character. By default, clients are not
        /// permitted to update statistics. Developers may override this setting in the Game Manager > Settings > API Features.
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="CharacterStatistics">Statistics to be updated with the provided values, in the Key(string), Value(int) pattern. (Optional)</param>
        public static Task<UpdateCharacterStatisticsResult> UpdateCharacterStatistics(string CharacterId, Dictionary<string,int> CharacterStatistics = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCharacterStatisticsRequest request = new UpdateCharacterStatisticsRequest()
            {
                CharacterId = CharacterId,
                CharacterStatistics = CharacterStatistics,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateCharacterStatisticsResult>("/Client/UpdateCharacterStatistics", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the values of the specified title-specific statistics for the user. By default, clients are not permitted to
        /// update statistics. Developers may override this setting in the Game Manager > Settings > API Features.
        /// </summary>
        /// <param name="Statistics">Statistics to be updated with the provided values (Required)</param>
        public static Task<UpdatePlayerStatisticsResult> UpdatePlayerStatistics(List<StatisticUpdate> Statistics, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
            {
                Statistics = Statistics,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateSharedGroupDataResult>("/Client/UpdateSharedGroupData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the title-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. This is used for requests by one player for information about another player; those requests will only return Public keys. (Optional)</param>
        public static Task<UpdateUserDataResult> UpdateUserData(Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserDataRequest request = new UpdateUserDataRequest()
            {
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Client/UpdateUserData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the publisher-specific custom data for the user which is readable and writable by the client
        /// </summary>
        /// <param name="Data">Key-value pairs to be written to the custom data. Note that keys are trimmed of whitespace, are limited in size, and may not begin with a '!' character or be null. (Optional)</param>
        /// <param name="KeysToRemove">Optional list of Data-keys to remove from UserData. Some SDKs cannot insert null-values into Data due to language constraints. Use this to delete the keys directly. (Optional)</param>
        /// <param name="Permission">Permission to be applied to all user data keys written in this request. Defaults to "private" if not set. This is used for requests by one player for information about another player; those requests will only return Public keys. (Optional)</param>
        public static Task<UpdateUserDataResult> UpdateUserPublisherData(Dictionary<string,string> Data = default, List<string> KeysToRemove = default, UserDataPermission? Permission = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserDataRequest request = new UpdateUserDataRequest()
            {
                Data = Data,
                KeysToRemove = KeysToRemove,
                Permission = Permission,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Client/UpdateUserPublisherData", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title specific display name for the user
        /// </summary>
        /// <param name="DisplayName">New title display name for the user - must be between 3 and 25 characters. (Required)</param>
        public static Task<UpdateUserTitleDisplayNameResult> UpdateUserTitleDisplayName(string DisplayName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest()
            {
                DisplayName = DisplayName,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<UpdateUserTitleDisplayNameResult>("/Client/UpdateUserTitleDisplayName", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates with Amazon that the receipt for an Amazon App Store in-app purchase is valid and that it matches the
        /// purchased catalog item
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the fulfilled items. If null, defaults to the primary catalog. (Optional)</param>
        /// <param name="CurrencyCode">Currency used to pay for the purchase (ISO 4217 currency code). (Required)</param>
        /// <param name="PurchasePrice">Amount of the stated currency paid, in centesimal units. (Required)</param>
        /// <param name="ReceiptId">ReceiptId returned by the Amazon App Store in-app purchase API (Required)</param>
        /// <param name="UserId">AmazonId of the user making the purchase as returned by the Amazon App Store in-app purchase API (Required)</param>
        public static Task<ValidateAmazonReceiptResult> ValidateAmazonIAPReceipt(string CurrencyCode, int PurchasePrice, string ReceiptId, string UserId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ValidateAmazonReceiptRequest request = new ValidateAmazonReceiptRequest()
            {
                CurrencyCode = CurrencyCode,
                PurchasePrice = PurchasePrice,
                ReceiptId = ReceiptId,
                UserId = UserId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateAmazonReceiptResult>("/Client/ValidateAmazonIAPReceipt", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates a Google Play purchase and gives the corresponding item to the player.
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the fulfilled items. If null, defaults to the primary catalog. (Optional)</param>
        /// <param name="CurrencyCode">Currency used to pay for the purchase (ISO 4217 currency code). (Optional)</param>
        /// <param name="PurchasePrice">Amount of the stated currency paid, in centesimal units. (Optional)</param>
        /// <param name="ReceiptJson">Original JSON string returned by the Google Play IAB API. (Required)</param>
        /// <param name="Signature">Signature returned by the Google Play IAB API. (Required)</param>
        public static Task<ValidateGooglePlayPurchaseResult> ValidateGooglePlayPurchase(string ReceiptJson, string Signature, string CatalogVersion = default, string CurrencyCode = default, uint? PurchasePrice = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ValidateGooglePlayPurchaseRequest request = new ValidateGooglePlayPurchaseRequest()
            {
                ReceiptJson = ReceiptJson,
                Signature = Signature,
                CatalogVersion = CatalogVersion,
                CurrencyCode = CurrencyCode,
                PurchasePrice = PurchasePrice,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateGooglePlayPurchaseResult>("/Client/ValidateGooglePlayPurchase", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates with the Apple store that the receipt for an iOS in-app purchase is valid and that it matches the purchased
        /// catalog item
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the fulfilled items. If null, defaults to the primary catalog. (Optional)</param>
        /// <param name="CurrencyCode">Currency used to pay for the purchase (ISO 4217 currency code). (Required)</param>
        /// <param name="PurchasePrice">Amount of the stated currency paid, in centesimal units. (Required)</param>
        /// <param name="ReceiptData">Base64 encoded receipt data, passed back by the App Store as a result of a successful purchase. (Required)</param>
        public static Task<ValidateIOSReceiptResult> ValidateIOSReceipt(string CurrencyCode, int PurchasePrice, string ReceiptData, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ValidateIOSReceiptRequest request = new ValidateIOSReceiptRequest()
            {
                CurrencyCode = CurrencyCode,
                PurchasePrice = PurchasePrice,
                ReceiptData = ReceiptData,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateIOSReceiptResult>("/Client/ValidateIOSReceipt", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Validates with Windows that the receipt for an Windows App Store in-app purchase is valid and that it matches the
        /// purchased catalog item
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the fulfilled items. If null, defaults to the primary catalog. (Optional)</param>
        /// <param name="CurrencyCode">Currency used to pay for the purchase (ISO 4217 currency code). (Required)</param>
        /// <param name="PurchasePrice">Amount of the stated currency paid, in centesimal units. (Required)</param>
        /// <param name="Receipt">XML Receipt returned by the Windows App Store in-app purchase API (Required)</param>
        public static Task<ValidateWindowsReceiptResult> ValidateWindowsStoreReceipt(string CurrencyCode, uint PurchasePrice, string Receipt, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ValidateWindowsReceiptRequest request = new ValidateWindowsReceiptRequest()
            {
                CurrencyCode = CurrencyCode,
                PurchasePrice = PurchasePrice,
                Receipt = Receipt,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<ValidateWindowsReceiptResult>("/Client/ValidateWindowsStoreReceipt", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a character-based event into PlayStream.
        /// </summary>
        /// <param name="Body">Custom event properties. Each property consists of a name (string) and a value (JSON object). (Optional)</param>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="EventName">The name of the event, within the namespace scoped to the title. The naming convention is up to the caller, but it commonly follows the subject_verb_object pattern (e.g. player_logged_in). (Required)</param>
        /// <param name="Timestamp">The time (in UTC) associated with this event. The value dafaults to the current time. (Optional)</param>
        public static Task<WriteEventResponse> WriteCharacterEvent(string CharacterId, string EventName, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteClientCharacterEventRequest request = new WriteClientCharacterEventRequest()
            {
                CharacterId = CharacterId,
                EventName = EventName,
                Body = Body,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Client/WriteCharacterEvent", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Writes a player-based event into PlayStream.
        /// </summary>
        /// <param name="Body">Custom data properties associated with the event. Each property consists of a name (string) and a value (JSON object). (Optional)</param>
        /// <param name="EventName">The name of the event, within the namespace scoped to the title. The naming convention is up to the caller, but it commonly follows the subject_verb_object pattern (e.g. player_logged_in). (Required)</param>
        /// <param name="Timestamp">The time (in UTC) associated with this event. The value dafaults to the current time. (Optional)</param>
        public static Task<WriteEventResponse> WritePlayerEvent(string EventName, Dictionary<string,object> Body = default, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest()
            {
                EventName = EventName,
                Body = Body,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Client/WritePlayerEvent", request,
				AuthType.LoginSession,
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
            if (!IsClientLoggedIn()) throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn,"Must be logged in to call this method");

            return PlayFabHttp.MakeApiCallAsync<WriteEventResponse>("/Client/WriteTitleEvent", request,
				AuthType.LoginSession,
				customData, extraHeaders, context);
        }


    }
}

#endif
