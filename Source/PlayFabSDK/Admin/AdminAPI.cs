#if ENABLE_PLAYFABADMIN_API

using System;
using System.Collections.Generic;
using PlayFab.AdminModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// APIs for managing title configurations, uploaded Game Server code executables, and user data
    /// </summary>
    public static class AdminAPI
    {
        static AdminAPI() {}


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
        /// Abort an ongoing task instance.
        /// </summary>
        /// <param name="TaskInstanceId">ID of a task instance that is being aborted. (Required)</param>
        public static Task<EmptyResponse> AbortTaskInstance(string TaskInstanceId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AbortTaskInstanceRequest request = new AbortTaskInstanceRequest()
            {
                TaskInstanceId = TaskInstanceId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/AbortTaskInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update news item to include localized version
        /// </summary>
        /// <param name="Body">Localized body text of the news. (Required)</param>
        /// <param name="Language">Language of the news item. (Required)</param>
        /// <param name="NewsId">Unique id of the updated news item. (Required)</param>
        /// <param name="Title">Localized title (headline) of the news item. (Required)</param>
        public static Task<AddLocalizedNewsResult> AddLocalizedNews(string Body, string Language, string NewsId, string Title, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddLocalizedNewsRequest request = new AddLocalizedNewsRequest()
            {
                Body = Body,
                Language = Language,
                NewsId = NewsId,
                Title = Title,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AddLocalizedNewsResult>("/Admin/AddLocalizedNews", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds a new news item to the title's news feed
        /// </summary>
        /// <param name="Body">Default body text of the news. (Required)</param>
        /// <param name="Timestamp">Time this news was published. If not set, defaults to now. (Optional)</param>
        /// <param name="Title">Default title (headline) of the news item. (Required)</param>
        public static Task<AddNewsResult> AddNews(string Body, string Title, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddNewsRequest request = new AddNewsRequest()
            {
                Body = Body,
                Title = Title,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AddNewsResult>("/Admin/AddNews", request,
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

            return PlayFabHttp.MakeApiCallAsync<AddPlayerTagResult>("/Admin/AddPlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the game server executable specified (previously uploaded - see GetServerBuildUploadUrl) to the set of those a
        /// client is permitted to request in a call to StartGame
        /// </summary>
        /// <param name="ActiveRegions">server host regions in which this build should be running and available (Optional)</param>
        /// <param name="BuildId">unique identifier for the build executable (Required)</param>
        /// <param name="CommandLineTemplate">appended to the end of the command line when starting game servers (Optional)</param>
        /// <param name="Comment">developer comment(s) for this build (Optional)</param>
        /// <param name="ExecutablePath">path to the game server executable. Defaults to gameserver.exe (Optional)</param>
        /// <param name="MaxGamesPerHost">maximum number of game server instances that can run on a single host machine (Required)</param>
        /// <param name="MinFreeGameSlots">minimum capacity of additional game server instances that can be started before the autoscaling service starts new host machines (given the number of current running host machines and game server instances) (Required)</param>
        public static Task<AddServerBuildResult> AddServerBuild(string BuildId, int MaxGamesPerHost, int MinFreeGameSlots, List<Region> ActiveRegions = default, string CommandLineTemplate = default, string Comment = default, string ExecutablePath = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddServerBuildRequest request = new AddServerBuildRequest()
            {
                BuildId = BuildId,
                MaxGamesPerHost = MaxGamesPerHost,
                MinFreeGameSlots = MinFreeGameSlots,
                ActiveRegions = ActiveRegions,
                CommandLineTemplate = CommandLineTemplate,
                Comment = Comment,
                ExecutablePath = ExecutablePath,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AddServerBuildResult>("/Admin/AddServerBuild", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increments the specified virtual currency by the stated amount
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

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Admin/AddUserVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds one or more virtual currencies to the set defined for the title. Virtual Currencies have a maximum value of
        /// 2,147,483,647 when granted to a player. Any value over that will be discarded.
        /// </summary>
        /// <param name="VirtualCurrencies">List of virtual currencies and their initial deposits (the amount a user is granted when signing in for the first time) to the title (Required)</param>
        public static Task<BlankResult> AddVirtualCurrencyTypes(List<VirtualCurrencyData> VirtualCurrencies, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddVirtualCurrencyTypesRequest request = new AddVirtualCurrencyTypesRequest()
            {
                VirtualCurrencies = VirtualCurrencies,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<BlankResult>("/Admin/AddVirtualCurrencyTypes", request,
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

            return PlayFabHttp.MakeApiCallAsync<BanUsersResult>("/Admin/BanUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Checks the global count for the limited edition item.
        /// </summary>
        /// <param name="CatalogVersion">Which catalog is being updated. If null, uses the default catalog. (Optional)</param>
        /// <param name="ItemId">The item to check for. (Required)</param>
        public static Task<CheckLimitedEditionItemAvailabilityResult> CheckLimitedEditionItemAvailability(string ItemId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CheckLimitedEditionItemAvailabilityRequest request = new CheckLimitedEditionItemAvailabilityRequest()
            {
                ItemId = ItemId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CheckLimitedEditionItemAvailabilityResult>("/Admin/CheckLimitedEditionItemAvailability", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create an ActionsOnPlayersInSegment task, which iterates through all players in a segment to execute action.
        /// </summary>
        /// <param name="Description">Description the task (Optional)</param>
        /// <param name="IsActive">Whether the schedule is active. Inactive schedule will not trigger task execution. (Required)</param>
        /// <param name="Name">Name of the task. This is a unique identifier for tasks in the title. (Required)</param>
        /// <param name="Parameter">Task details related to segment and action (Required)</param>
        /// <param name="Schedule">Cron expression for the run schedule of the task. The expression should be in UTC. (Optional)</param>
        public static Task<CreateTaskResult> CreateActionsOnPlayersInSegmentTask(bool IsActive, string Name, ActionsOnPlayersInSegmentTaskParameter Parameter, string Description = default, string Schedule = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateActionsOnPlayerSegmentTaskRequest request = new CreateActionsOnPlayerSegmentTaskRequest()
            {
                IsActive = IsActive,
                Name = Name,
                Parameter = Parameter,
                Description = Description,
                Schedule = Schedule,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateTaskResult>("/Admin/CreateActionsOnPlayersInSegmentTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create a CloudScript task, which can run a CloudScript on a schedule.
        /// </summary>
        /// <param name="Description">Description the task (Optional)</param>
        /// <param name="IsActive">Whether the schedule is active. Inactive schedule will not trigger task execution. (Required)</param>
        /// <param name="Name">Name of the task. This is a unique identifier for tasks in the title. (Required)</param>
        /// <param name="Parameter">Task details related to CloudScript (Required)</param>
        /// <param name="Schedule">Cron expression for the run schedule of the task. The expression should be in UTC. (Optional)</param>
        public static Task<CreateTaskResult> CreateCloudScriptTask(bool IsActive, string Name, CloudScriptTaskParameter Parameter, string Description = default, string Schedule = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateCloudScriptTaskRequest request = new CreateCloudScriptTaskRequest()
            {
                IsActive = IsActive,
                Name = Name,
                Parameter = Parameter,
                Description = Description,
                Schedule = Schedule,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateTaskResult>("/Admin/CreateCloudScriptTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers a relationship between a title and an Open ID Connect provider.
        /// </summary>
        /// <param name="ClientId">The client ID given by the ID provider. (Required)</param>
        /// <param name="ClientSecret">The client secret given by the ID provider. (Required)</param>
        /// <param name="ConnectionId">A name for the connection that identifies it within the title. (Required)</param>
        /// <param name="IssuerDiscoveryUrl">The discovery document URL to read issuer information from. This must be the absolute URL to the JSON OpenId Configuration document and must be accessible from the internet. If you don't know it, try your issuer URL followed by "/.well-known/openid-configuration". For example, if the issuer is https://example.com, try https://example.com/.well-known/openid-configuration (Optional)</param>
        /// <param name="IssuerInformation">Manually specified information for an OpenID Connect issuer. (Optional)</param>
        public static Task<EmptyResponse> CreateOpenIdConnection(string ClientId, string ClientSecret, string ConnectionId, string IssuerDiscoveryUrl = default, OpenIdIssuerInformation IssuerInformation = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateOpenIdConnectionRequest request = new CreateOpenIdConnectionRequest()
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                ConnectionId = ConnectionId,
                IssuerDiscoveryUrl = IssuerDiscoveryUrl,
                IssuerInformation = IssuerInformation,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/CreateOpenIdConnection", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new Player Shared Secret Key. It may take up to 5 minutes for this key to become generally available after
        /// this API returns.
        /// </summary>
        /// <param name="FriendlyName">Friendly name for this key (Optional)</param>
        public static Task<CreatePlayerSharedSecretResult> CreatePlayerSharedSecret(string FriendlyName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreatePlayerSharedSecretRequest request = new CreatePlayerSharedSecretRequest()
            {
                FriendlyName = FriendlyName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreatePlayerSharedSecretResult>("/Admin/CreatePlayerSharedSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds a new player statistic configuration to the title, optionally allowing the developer to specify a reset interval
        /// and an aggregation method.
        /// </summary>
        /// <param name="AggregationMethod">the aggregation method to use in updating the statistic (defaults to last) (Optional)</param>
        /// <param name="StatisticName">unique name of the statistic (Required)</param>
        /// <param name="VersionChangeInterval">interval at which the values of the statistic for all players are reset (resets begin at the next interval boundary) (Optional)</param>
        public static Task<CreatePlayerStatisticDefinitionResult> CreatePlayerStatisticDefinition(string StatisticName, StatisticAggregationMethod? AggregationMethod = default, StatisticResetIntervalOption? VersionChangeInterval = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreatePlayerStatisticDefinitionRequest request = new CreatePlayerStatisticDefinitionRequest()
            {
                StatisticName = StatisticName,
                AggregationMethod = AggregationMethod,
                VersionChangeInterval = VersionChangeInterval,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreatePlayerStatisticDefinitionResult>("/Admin/CreatePlayerStatisticDefinition", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Delete a content file from the title. When deleting a file that does not exist, it returns success.
        /// </summary>
        /// <param name="Key">Key of the content item to be deleted (Required)</param>
        public static Task<BlankResult> DeleteContent(string Key, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteContentRequest request = new DeleteContentRequest()
            {
                Key = Key,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<BlankResult>("/Admin/DeleteContent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a master player account entirely from all titles and deletes all associated data
        /// </summary>
        /// <param name="MetaData">Developer created string to identify a user without PlayFab ID (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<DeleteMasterPlayerAccountResult> DeleteMasterPlayerAccount(string PlayFabId, string MetaData = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteMasterPlayerAccountRequest request = new DeleteMasterPlayerAccountRequest()
            {
                PlayFabId = PlayFabId,
                MetaData = MetaData,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeleteMasterPlayerAccountResult>("/Admin/DeleteMasterPlayerAccount", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a relationship between a title and an OpenID Connect provider.
        /// </summary>
        /// <param name="ConnectionId">unique name of the connection (Required)</param>
        public static Task<EmptyResponse> DeleteOpenIdConnection(string ConnectionId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteOpenIdConnectionRequest request = new DeleteOpenIdConnectionRequest()
            {
                ConnectionId = ConnectionId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/DeleteOpenIdConnection", request,
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

            return PlayFabHttp.MakeApiCallAsync<DeletePlayerResult>("/Admin/DeletePlayer", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes an existing Player Shared Secret Key. It may take up to 5 minutes for this delete to be reflected after this API
        /// returns.
        /// </summary>
        /// <param name="SecretKey">The shared secret key to delete (Optional)</param>
        public static Task<DeletePlayerSharedSecretResult> DeletePlayerSharedSecret(string SecretKey = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeletePlayerSharedSecretRequest request = new DeletePlayerSharedSecretRequest()
            {
                SecretKey = SecretKey,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeletePlayerSharedSecretResult>("/Admin/DeletePlayerSharedSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes an existing virtual item store
        /// </summary>
        /// <param name="CatalogVersion">catalog version of the store to delete. If null, uses the default catalog. (Optional)</param>
        /// <param name="StoreId">unqiue identifier for the store which is to be deleted (Required)</param>
        public static Task<DeleteStoreResult> DeleteStore(string StoreId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteStoreRequest request = new DeleteStoreRequest()
            {
                StoreId = StoreId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeleteStoreResult>("/Admin/DeleteStore", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Delete a task.
        /// </summary>
        /// <param name="Identifier">Specify either the task ID or the name of task to be deleted. (Optional)</param>
        public static Task<EmptyResponse> DeleteTask(NameIdentifier Identifier = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteTaskRequest request = new DeleteTaskRequest()
            {
                Identifier = Identifier,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/DeleteTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Permanently deletes a title and all associated configuration
        /// </summary>
        public static Task<DeleteTitleResult> DeleteTitle(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteTitleRequest request = new DeleteTitleRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeleteTitleResult>("/Admin/DeleteTitle", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Exports all associated data of a master player account
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<ExportMasterPlayerDataResult> ExportMasterPlayerData(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ExportMasterPlayerDataRequest request = new ExportMasterPlayerDataRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ExportMasterPlayerDataResult>("/Admin/ExportMasterPlayerData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get information about a ActionsOnPlayersInSegment task instance.
        /// </summary>
        /// <param name="TaskInstanceId">ID of the requested task instance. (Required)</param>
        public static Task<GetActionsOnPlayersInSegmentTaskInstanceResult> GetActionsOnPlayersInSegmentTaskInstance(string TaskInstanceId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTaskInstanceRequest request = new GetTaskInstanceRequest()
            {
                TaskInstanceId = TaskInstanceId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetActionsOnPlayersInSegmentTaskInstanceResult>("/Admin/GetActionsOnPlayersInSegmentTaskInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
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

            return PlayFabHttp.MakeApiCallAsync<GetAllSegmentsResult>("/Admin/GetAllSegments", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetCatalogItemsResult>("/Admin/GetCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the contents and information of a specific Cloud Script revision.
        /// </summary>
        /// <param name="Revision">Revision number. If left null, defaults to the latest revision (Optional)</param>
        /// <param name="Version">Version number. If left null, defaults to the latest version (Optional)</param>
        public static Task<GetCloudScriptRevisionResult> GetCloudScriptRevision(int? Revision = default, int? Version = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCloudScriptRevisionRequest request = new GetCloudScriptRevisionRequest()
            {
                Revision = Revision,
                Version = Version,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCloudScriptRevisionResult>("/Admin/GetCloudScriptRevision", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get detail information about a CloudScript task instance.
        /// </summary>
        /// <param name="TaskInstanceId">ID of the requested task instance. (Required)</param>
        public static Task<GetCloudScriptTaskInstanceResult> GetCloudScriptTaskInstance(string TaskInstanceId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTaskInstanceRequest request = new GetTaskInstanceRequest()
            {
                TaskInstanceId = TaskInstanceId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCloudScriptTaskInstanceResult>("/Admin/GetCloudScriptTaskInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all the current cloud script versions. For each version, information about the current published and latest
        /// revisions is also listed.
        /// </summary>
        public static Task<GetCloudScriptVersionsResult> GetCloudScriptVersions(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetCloudScriptVersionsRequest request = new GetCloudScriptVersionsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetCloudScriptVersionsResult>("/Admin/GetCloudScriptVersions", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all contents of the title and get statistics such as size
        /// </summary>
        /// <param name="Prefix">Limits the response to keys that begin with the specified prefix. You can use prefixes to list contents under a folder, or for a specified version, etc. (Optional)</param>
        public static Task<GetContentListResult> GetContentList(string Prefix = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetContentListRequest request = new GetContentListRequest()
            {
                Prefix = Prefix,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetContentListResult>("/Admin/GetContentList", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the pre-signed URL for uploading a content file. A subsequent HTTP PUT to the returned URL uploads the
        /// content. Also, please be aware that the Content service is specifically PlayFab's CDN offering, for which standard CDN
        /// rates apply.
        /// </summary>
        /// <param name="ContentType">A standard MIME type describing the format of the contents. The same MIME type has to be set in the header when uploading the content. If not specified, the MIME type is 'binary/octet-stream' by default. (Optional)</param>
        /// <param name="Key">Key of the content item to upload, usually formatted as a path, e.g. images/a.png (Required)</param>
        public static Task<GetContentUploadUrlResult> GetContentUploadUrl(string Key, string ContentType = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetContentUploadUrlRequest request = new GetContentUploadUrlRequest()
            {
                Key = Key,
                ContentType = ContentType,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetContentUploadUrlResult>("/Admin/GetContentUploadUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a download URL for the requested report
        /// </summary>
        /// <param name="Day">Reporting year (UTC) (Required)</param>
        /// <param name="Month">Reporting month (UTC) (Required)</param>
        /// <param name="ReportName">Report name (Required)</param>
        /// <param name="Year">Reporting year (UTC) (Required)</param>
        public static Task<GetDataReportResult> GetDataReport(int Day, int Month, string ReportName, int Year, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetDataReportRequest request = new GetDataReportRequest()
            {
                Day = Day,
                Month = Month,
                ReportName = ReportName,
                Year = Year,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetDataReportResult>("/Admin/GetDataReport", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details for a specific completed session, including links to standard out and standard error logs
        /// </summary>
        /// <param name="LobbyId">unique identifier of the lobby for which info is being requested (Required)</param>
        public static Task<GetMatchmakerGameInfoResult> GetMatchmakerGameInfo(string LobbyId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetMatchmakerGameInfoRequest request = new GetMatchmakerGameInfoRequest()
            {
                LobbyId = LobbyId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetMatchmakerGameInfoResult>("/Admin/GetMatchmakerGameInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details of defined game modes for the specified game server executable
        /// </summary>
        /// <param name="BuildVersion">previously uploaded build version for which game modes are being requested (Required)</param>
        public static Task<GetMatchmakerGameModesResult> GetMatchmakerGameModes(string BuildVersion, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetMatchmakerGameModesRequest request = new GetMatchmakerGameModesRequest()
            {
                BuildVersion = BuildVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetMatchmakerGameModesResult>("/Admin/GetMatchmakerGameModes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get the list of titles that the player has played
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<GetPlayedTitleListResult> GetPlayedTitleList(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayedTitleListRequest request = new GetPlayedTitleListRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayedTitleListResult>("/Admin/GetPlayedTitleList", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a player's ID from an auth token.
        /// </summary>
        /// <param name="Token">The auth token of the player requesting the password reset. (Required)</param>
        /// <param name="TokenType">The type of auth token of the player requesting the password reset. (Required)</param>
        public static Task<GetPlayerIdFromAuthTokenResult> GetPlayerIdFromAuthToken(string Token, AuthTokenType TokenType, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerIdFromAuthTokenRequest request = new GetPlayerIdFromAuthTokenRequest()
            {
                Token = Token,
                TokenType = TokenType,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerIdFromAuthTokenResult>("/Admin/GetPlayerIdFromAuthToken", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerProfileResult>("/Admin/GetPlayerProfile", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerSegmentsResult>("/Admin/GetPlayerSegments", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns all Player Shared Secret Keys including disabled and expired.
        /// </summary>
        public static Task<GetPlayerSharedSecretsResult> GetPlayerSharedSecrets(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerSharedSecretsRequest request = new GetPlayerSharedSecretsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerSharedSecretsResult>("/Admin/GetPlayerSharedSecrets", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayersInSegmentResult>("/Admin/GetPlayersInSegment", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the configuration information for all player statistics defined in the title, regardless of whether they have
        /// a reset interval.
        /// </summary>
        public static Task<GetPlayerStatisticDefinitionsResult> GetPlayerStatisticDefinitions(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPlayerStatisticDefinitionsRequest request = new GetPlayerStatisticDefinitionsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticDefinitionsResult>("/Admin/GetPlayerStatisticDefinitions", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticVersionsResult>("/Admin/GetPlayerStatisticVersions", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTagsResult>("/Admin/GetPlayerTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the requested policy.
        /// </summary>
        /// <param name="PolicyName">The name of the policy to read. Only supported name is 'ApiPolicy'. (Optional)</param>
        public static Task<GetPolicyResponse> GetPolicy(string PolicyName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetPolicyRequest request = new GetPolicyRequest()
            {
                PolicyName = PolicyName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetPolicyResponse>("/Admin/GetPolicy", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPublisherDataResult>("/Admin/GetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the random drop table configuration for the title
        /// </summary>
        /// <param name="CatalogVersion">catalog version to fetch tables from. Use default catalog version if null (Optional)</param>
        public static Task<GetRandomResultTablesResult> GetRandomResultTables(string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetRandomResultTablesRequest request = new GetRandomResultTablesRequest()
            {
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetRandomResultTablesResult>("/Admin/GetRandomResultTables", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the build details for the specified game server executable
        /// </summary>
        /// <param name="BuildId">unique identifier of the previously uploaded build executable for which information is being requested (Required)</param>
        public static Task<GetServerBuildInfoResult> GetServerBuildInfo(string BuildId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetServerBuildInfoRequest request = new GetServerBuildInfoRequest()
            {
                BuildId = BuildId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetServerBuildInfoResult>("/Admin/GetServerBuildInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the pre-authorized URL for uploading a game server package containing a build (does not enable the build for
        /// use - see AddServerBuild)
        /// </summary>
        /// <param name="BuildId">unique identifier of the game server build to upload (Required)</param>
        public static Task<GetServerBuildUploadURLResult> GetServerBuildUploadUrl(string BuildId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetServerBuildUploadURLRequest request = new GetServerBuildUploadURLRequest()
            {
                BuildId = BuildId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetServerBuildUploadURLResult>("/Admin/GetServerBuildUploadUrl", request,
				AuthType.DevSecretKey,
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

            return PlayFabHttp.MakeApiCallAsync<GetStoreItemsResult>("/Admin/GetStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Query for task instances by task, status, or time range.
        /// </summary>
        /// <param name="StartedAtRangeFrom">Optional range-from filter for task instances' StartedAt timestamp. (Optional)</param>
        /// <param name="StartedAtRangeTo">Optional range-to filter for task instances' StartedAt timestamp. (Optional)</param>
        /// <param name="StatusFilter">Optional filter for task instances that are of a specific status. (Optional)</param>
        /// <param name="TaskIdentifier">Name or ID of the task whose instances are being queried. If not specified, return all task instances that satisfy conditions set by other filters. (Optional)</param>
        public static Task<GetTaskInstancesResult> GetTaskInstances(DateTime? StartedAtRangeFrom = default, DateTime? StartedAtRangeTo = default, TaskInstanceStatus? StatusFilter = default, NameIdentifier TaskIdentifier = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTaskInstancesRequest request = new GetTaskInstancesRequest()
            {
                StartedAtRangeFrom = StartedAtRangeFrom,
                StartedAtRangeTo = StartedAtRangeTo,
                StatusFilter = StatusFilter,
                TaskIdentifier = TaskIdentifier,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTaskInstancesResult>("/Admin/GetTaskInstances", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get definition information on a specified task or all tasks within a title.
        /// </summary>
        /// <param name="Identifier">Provide either the task ID or the task name to get a specific task. If not specified, return all defined tasks. (Optional)</param>
        public static Task<GetTasksResult> GetTasks(NameIdentifier Identifier = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTasksRequest request = new GetTasksRequest()
            {
                Identifier = Identifier,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTasksResult>("/Admin/GetTasks", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings which can be read by the client
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

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Admin/GetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings which cannot be read by the client
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

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Admin/GetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user, based upon a match against a supplied unique identifier
        /// </summary>
        /// <param name="Email">User email address attached to their account (Optional)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Optional)</param>
        /// <param name="TitleDisplayName">Title specific username to match against existing user accounts (Optional)</param>
        /// <param name="Username">PlayFab username for the account (3-20 characters) (Optional)</param>
        public static Task<LookupUserAccountInfoResult> GetUserAccountInfo(string Email = default, string PlayFabId = default, string TitleDisplayName = default, string Username = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            LookupUserAccountInfoRequest request = new LookupUserAccountInfoRequest()
            {
                Email = Email,
                PlayFabId = PlayFabId,
                TitleDisplayName = TitleDisplayName,
                Username = Username,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<LookupUserAccountInfoResult>("/Admin/GetUserAccountInfo", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserBansResult>("/Admin/GetUserBans", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserInventoryResult>("/Admin/GetUserInventory", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserPublisherData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserPublisherInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserPublisherReadOnlyData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserReadOnlyData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToUsersResult>("/Admin/GrantItemsToUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increases the global count for the given scarce resource.
        /// </summary>
        /// <param name="Amount">Amount to increase availability by. (Required)</param>
        /// <param name="CatalogVersion">Which catalog is being updated. If null, uses the default catalog. (Optional)</param>
        /// <param name="ItemId">The item which needs more availability. (Required)</param>
        public static Task<IncrementLimitedEditionItemAvailabilityResult> IncrementLimitedEditionItemAvailability(int Amount, string ItemId, string CatalogVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            IncrementLimitedEditionItemAvailabilityRequest request = new IncrementLimitedEditionItemAvailabilityRequest()
            {
                Amount = Amount,
                ItemId = ItemId,
                CatalogVersion = CatalogVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<IncrementLimitedEditionItemAvailabilityResult>("/Admin/IncrementLimitedEditionItemAvailability", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Resets the indicated statistic, removing all player entries for it and backing up the old values.
        /// </summary>
        /// <param name="StatisticName">unique name of the statistic (Optional)</param>
        public static Task<IncrementPlayerStatisticVersionResult> IncrementPlayerStatisticVersion(string StatisticName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            IncrementPlayerStatisticVersionRequest request = new IncrementPlayerStatisticVersionRequest()
            {
                StatisticName = StatisticName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<IncrementPlayerStatisticVersionResult>("/Admin/IncrementPlayerStatisticVersion", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of all Open ID Connect providers registered to a title.
        /// </summary>
        public static Task<ListOpenIdConnectionResponse> ListOpenIdConnection(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListOpenIdConnectionRequest request = new ListOpenIdConnectionRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListOpenIdConnectionResponse>("/Admin/ListOpenIdConnection", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the build details for all game server executables which are currently defined for the title
        /// </summary>
        public static Task<ListBuildsResult> ListServerBuilds(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListBuildsRequest request = new ListBuildsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListBuildsResult>("/Admin/ListServerBuilds", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retuns the list of all defined virtual currencies for the title
        /// </summary>
        public static Task<ListVirtualCurrencyTypesResult> ListVirtualCurrencyTypes(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListVirtualCurrencyTypesRequest request = new ListVirtualCurrencyTypesRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListVirtualCurrencyTypesResult>("/Admin/ListVirtualCurrencyTypes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the game server mode details for the specified game server executable
        /// </summary>
        /// <param name="BuildVersion">previously uploaded build version for which game modes are being specified (Required)</param>
        /// <param name="GameModes">array of game modes (Note: this will replace all game modes for the indicated build version) (Required)</param>
        public static Task<ModifyMatchmakerGameModesResult> ModifyMatchmakerGameModes(string BuildVersion, List<GameModeInfo> GameModes, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ModifyMatchmakerGameModesRequest request = new ModifyMatchmakerGameModesRequest()
            {
                BuildVersion = BuildVersion,
                GameModes = GameModes,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyMatchmakerGameModesResult>("/Admin/ModifyMatchmakerGameModes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the build details for the specified game server executable
        /// </summary>
        /// <param name="ActiveRegions">array of regions where this build can used, when it is active (Optional)</param>
        /// <param name="BuildId">unique identifier of the previously uploaded build executable to be updated (Required)</param>
        /// <param name="CommandLineTemplate">appended to the end of the command line when starting game servers (Optional)</param>
        /// <param name="Comment">developer comment(s) for this build (Optional)</param>
        /// <param name="ExecutablePath">path to the game server executable. Defaults to gameserver.exe (Optional)</param>
        /// <param name="MaxGamesPerHost">maximum number of game server instances that can run on a single host machine (Required)</param>
        /// <param name="MinFreeGameSlots">minimum capacity of additional game server instances that can be started before the autoscaling service starts new host machines (given the number of current running host machines and game server instances) (Required)</param>
        /// <param name="Timestamp">new timestamp (Optional)</param>
        public static Task<ModifyServerBuildResult> ModifyServerBuild(string BuildId, int MaxGamesPerHost, int MinFreeGameSlots, List<Region> ActiveRegions = default, string CommandLineTemplate = default, string Comment = default, string ExecutablePath = default, DateTime? Timestamp = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ModifyServerBuildRequest request = new ModifyServerBuildRequest()
            {
                BuildId = BuildId,
                MaxGamesPerHost = MaxGamesPerHost,
                MinFreeGameSlots = MinFreeGameSlots,
                ActiveRegions = ActiveRegions,
                CommandLineTemplate = CommandLineTemplate,
                Comment = Comment,
                ExecutablePath = ExecutablePath,
                Timestamp = Timestamp,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ModifyServerBuildResult>("/Admin/ModifyServerBuild", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Attempts to process an order refund through the original real money payment provider.
        /// </summary>
        /// <param name="OrderId">Unique order ID for the purchase in question. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Reason">The Reason parameter should correspond with the payment providers reason field, if they require one such as Facebook. In the case of Facebook this must match one of their refund or dispute resolution enums (See: https://developers.facebook.com/docs/payments/implementation-guide/handling-disputes-refunds) (Optional)</param>
        public static Task<RefundPurchaseResponse> RefundPurchase(string OrderId, string PlayFabId, string Reason = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RefundPurchaseRequest request = new RefundPurchaseRequest()
            {
                OrderId = OrderId,
                PlayFabId = PlayFabId,
                Reason = Reason,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RefundPurchaseResponse>("/Admin/RefundPurchase", request,
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

            return PlayFabHttp.MakeApiCallAsync<RemovePlayerTagResult>("/Admin/RemovePlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the game server executable specified from the set of those a client is permitted to request in a call to
        /// StartGame
        /// </summary>
        /// <param name="BuildId">unique identifier of the previously uploaded build executable to be removed (Required)</param>
        public static Task<RemoveServerBuildResult> RemoveServerBuild(string BuildId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveServerBuildRequest request = new RemoveServerBuildRequest()
            {
                BuildId = BuildId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RemoveServerBuildResult>("/Admin/RemoveServerBuild", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes one or more virtual currencies from the set defined for the title.
        /// </summary>
        /// <param name="VirtualCurrencies">List of virtual currencies to delete (Required)</param>
        public static Task<BlankResult> RemoveVirtualCurrencyTypes(List<VirtualCurrencyData> VirtualCurrencies, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveVirtualCurrencyTypesRequest request = new RemoveVirtualCurrencyTypesRequest()
            {
                VirtualCurrencies = VirtualCurrencies,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<BlankResult>("/Admin/RemoveVirtualCurrencyTypes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Completely removes all statistics for the specified character, for the current game
        /// </summary>
        /// <param name="CharacterId">Unique PlayFab assigned ID for a specific character owned by a user (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<ResetCharacterStatisticsResult> ResetCharacterStatistics(string CharacterId, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ResetCharacterStatisticsRequest request = new ResetCharacterStatisticsRequest()
            {
                CharacterId = CharacterId,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ResetCharacterStatisticsResult>("/Admin/ResetCharacterStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Reset a player's password for a given title.
        /// </summary>
        /// <param name="Password">The new password for the player. (Required)</param>
        /// <param name="Token">The token of the player requesting the password reset. (Required)</param>
        public static Task<ResetPasswordResult> ResetPassword(string Password, string Token, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ResetPasswordRequest request = new ResetPasswordRequest()
            {
                Password = Password,
                Token = Token,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ResetPasswordResult>("/Admin/ResetPassword", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Completely removes all statistics for the specified user, for the current game
        /// </summary>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        public static Task<ResetUserStatisticsResult> ResetUserStatistics(string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ResetUserStatisticsRequest request = new ResetUserStatisticsRequest()
            {
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ResetUserStatisticsResult>("/Admin/ResetUserStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Attempts to resolve a dispute with the original order's payment provider.
        /// </summary>
        /// <param name="OrderId">Unique order ID for the purchase in question. (Required)</param>
        /// <param name="Outcome">Enum for the desired purchase result state after notifying the payment provider. Valid values are Revoke, Reinstate and Manual. Manual will cause no change to the order state. (Required)</param>
        /// <param name="PlayFabId">Unique PlayFab assigned ID of the user on whom the operation will be performed. (Required)</param>
        /// <param name="Reason">The Reason parameter should correspond with the payment providers reason field, if they require one such as Facebook. In the case of Facebook this must match one of their refund or dispute resolution enums (See: https://developers.facebook.com/docs/payments/implementation-guide/handling-disputes-refunds) (Optional)</param>
        public static Task<ResolvePurchaseDisputeResponse> ResolvePurchaseDispute(string OrderId, ResolutionOutcome Outcome, string PlayFabId, string Reason = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ResolvePurchaseDisputeRequest request = new ResolvePurchaseDisputeRequest()
            {
                OrderId = OrderId,
                Outcome = Outcome,
                PlayFabId = PlayFabId,
                Reason = Reason,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ResolvePurchaseDisputeResponse>("/Admin/ResolvePurchaseDispute", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeAllBansForUserResult>("/Admin/RevokeAllBansForUser", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeBansResult>("/Admin/RevokeBans", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryResult>("/Admin/RevokeInventoryItem", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryItemsResult>("/Admin/RevokeInventoryItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Run a task immediately regardless of its schedule.
        /// </summary>
        /// <param name="Identifier">Provide either the task ID or the task name to run a task. (Optional)</param>
        public static Task<RunTaskResult> RunTask(NameIdentifier Identifier = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RunTaskRequest request = new RunTaskRequest()
            {
                Identifier = Identifier,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RunTaskResult>("/Admin/RunTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Forces an email to be sent to the registered email address for the user's account, with a link allowing the user to
        /// change the password.If an account recovery email template ID is provided, an email using the custom email template will
        /// be used.
        /// </summary>
        /// <param name="Email">User email address attached to their account (Required)</param>
        /// <param name="EmailTemplateId">The email template id of the account recovery email template to send. (Optional)</param>
        public static Task<SendAccountRecoveryEmailResult> SendAccountRecoveryEmail(string Email, string EmailTemplateId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SendAccountRecoveryEmailRequest request = new SendAccountRecoveryEmailRequest()
            {
                Email = Email,
                EmailTemplateId = EmailTemplateId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SendAccountRecoveryEmailResult>("/Admin/SendAccountRecoveryEmail", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates the catalog configuration of all virtual goods for the specified catalog version
        /// </summary>
        /// <param name="Catalog">Array of catalog items to be submitted. Note that while CatalogItem has a parameter for CatalogVersion, it is not required and ignored in this call. (Optional)</param>
        /// <param name="CatalogVersion">Which catalog is being updated. If null, uses the default catalog. (Optional)</param>
        /// <param name="SetAsDefaultCatalog">Should this catalog be set as the default catalog. Defaults to true. If there is currently no default catalog, this will always set it. (Optional)</param>
        public static Task<UpdateCatalogItemsResult> SetCatalogItems(List<CatalogItem> Catalog = default, string CatalogVersion = default, bool? SetAsDefaultCatalog = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCatalogItemsRequest request = new UpdateCatalogItemsRequest()
            {
                Catalog = Catalog,
                CatalogVersion = CatalogVersion,
                SetAsDefaultCatalog = SetAsDefaultCatalog,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCatalogItemsResult>("/Admin/SetCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets or resets the player's secret. Player secrets are used to sign API requests.
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

            return PlayFabHttp.MakeApiCallAsync<SetPlayerSecretResult>("/Admin/SetPlayerSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the currently published revision of a title Cloud Script
        /// </summary>
        /// <param name="Revision">Revision to make the current published revision (Required)</param>
        /// <param name="Version">Version number (Required)</param>
        public static Task<SetPublishedRevisionResult> SetPublishedRevision(int Revision, int Version, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetPublishedRevisionRequest request = new SetPublishedRevisionRequest()
            {
                Revision = Revision,
                Version = Version,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetPublishedRevisionResult>("/Admin/SetPublishedRevision", request,
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

            return PlayFabHttp.MakeApiCallAsync<SetPublisherDataResult>("/Admin/SetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets all the items in one virtual store
        /// </summary>
        /// <param name="CatalogVersion">Catalog version of the store to update. If null, uses the default catalog. (Optional)</param>
        /// <param name="MarketingData">Additional data about the store (Optional)</param>
        /// <param name="Store">Array of store items - references to catalog items, with specific pricing - to be added (Optional)</param>
        /// <param name="StoreId">Unique identifier for the store which is to be updated (Required)</param>
        public static Task<UpdateStoreItemsResult> SetStoreItems(string StoreId, string CatalogVersion = default, StoreMarketingModel MarketingData = default, List<StoreItem> Store = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateStoreItemsRequest request = new UpdateStoreItemsRequest()
            {
                StoreId = StoreId,
                CatalogVersion = CatalogVersion,
                MarketingData = MarketingData,
                Store = Store,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateStoreItemsResult>("/Admin/SetStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the key-value store of custom title settings which can be read by the client
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

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Admin/SetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom title settings which cannot be read by the client
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

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Admin/SetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the Amazon Resource Name (ARN) for iOS and Android push notifications. Documentation on the exact restrictions can
        /// be found at: http://docs.aws.amazon.com/sns/latest/api/API_CreatePlatformApplication.html. Currently, Amazon device
        /// Messaging is not supported.
        /// </summary>
        /// <param name="Credential">Credential is the Private Key for APNS/APNS_SANDBOX, and the API Key for GCM (Required)</param>
        /// <param name="Key">for APNS, this is the PlatformPrincipal (SSL Certificate) (Optional)</param>
        /// <param name="Name">name of the application sending the message (application names must be made up of only uppercase and lowercase ASCII letters, numbers, underscores, hyphens, and periods, and must be between 1 and 256 characters long) (Required)</param>
        /// <param name="OverwriteOldARN">replace any existing ARN with the newly generated one. If this is set to false, an error will be returned if notifications have already setup for this platform. (Required)</param>
        /// <param name="Platform">supported notification platforms are Apple Push Notification Service (APNS and APNS_SANDBOX) for iOS and Google Cloud Messaging (GCM) for Android (Required)</param>
        public static Task<SetupPushNotificationResult> SetupPushNotification(string Credential, string Name, bool OverwriteOldARN, PushSetupPlatform Platform, string Key = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetupPushNotificationRequest request = new SetupPushNotificationRequest()
            {
                Credential = Credential,
                Name = Name,
                OverwriteOldARN = OverwriteOldARN,
                Platform = Platform,
                Key = Key,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetupPushNotificationResult>("/Admin/SetupPushNotification", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the specified virtual currency by the stated amount
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

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Admin/SubtractUserVirtualCurrency", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateBansResult>("/Admin/UpdateBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the catalog configuration for virtual goods in the specified catalog version
        /// </summary>
        /// <param name="Catalog">Array of catalog items to be submitted. Note that while CatalogItem has a parameter for CatalogVersion, it is not required and ignored in this call. (Optional)</param>
        /// <param name="CatalogVersion">Which catalog is being updated. If null, uses the default catalog. (Optional)</param>
        /// <param name="SetAsDefaultCatalog">Should this catalog be set as the default catalog. Defaults to true. If there is currently no default catalog, this will always set it. (Optional)</param>
        public static Task<UpdateCatalogItemsResult> UpdateCatalogItems(List<CatalogItem> Catalog = default, string CatalogVersion = default, bool? SetAsDefaultCatalog = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCatalogItemsRequest request = new UpdateCatalogItemsRequest()
            {
                Catalog = Catalog,
                CatalogVersion = CatalogVersion,
                SetAsDefaultCatalog = SetAsDefaultCatalog,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCatalogItemsResult>("/Admin/UpdateCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new Cloud Script revision and uploads source code to it. Note that at this time, only one file should be
        /// submitted in the revision.
        /// </summary>
        /// <param name="DeveloperPlayFabId">PlayFab user ID of the developer initiating the request. (Optional)</param>
        /// <param name="Files">List of Cloud Script files to upload to create the new revision. Must have at least one file. (Required)</param>
        /// <param name="Publish">Immediately publish the new revision (Required)</param>
        public static Task<UpdateCloudScriptResult> UpdateCloudScript(List<CloudScriptFile> Files, bool Publish, string DeveloperPlayFabId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateCloudScriptRequest request = new UpdateCloudScriptRequest()
            {
                Files = Files,
                Publish = Publish,
                DeveloperPlayFabId = DeveloperPlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateCloudScriptResult>("/Admin/UpdateCloudScript", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Modifies data and credentials for an existing relationship between a title and an Open ID Connect provider
        /// </summary>
        /// <param name="ClientId">The client ID given by the ID provider. (Optional)</param>
        /// <param name="ClientSecret">The client secret given by the ID provider. (Optional)</param>
        /// <param name="ConnectionId">A name for the connection that identifies it within the title. (Required)</param>
        /// <param name="IssuerDiscoveryUrl">The issuer URL or discovery document URL to read issuer information from (Optional)</param>
        /// <param name="IssuerInformation">Manually specified information for an OpenID Connect issuer. (Optional)</param>
        public static Task<EmptyResponse> UpdateOpenIdConnection(string ConnectionId, string ClientId = default, string ClientSecret = default, string IssuerDiscoveryUrl = default, OpenIdIssuerInformation IssuerInformation = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateOpenIdConnectionRequest request = new UpdateOpenIdConnectionRequest()
            {
                ConnectionId = ConnectionId,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                IssuerDiscoveryUrl = IssuerDiscoveryUrl,
                IssuerInformation = IssuerInformation,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/UpdateOpenIdConnection", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates a existing Player Shared Secret Key. It may take up to 5 minutes for this update to become generally available
        /// after this API returns.
        /// </summary>
        /// <param name="Disabled">Disable or Enable this key (Required)</param>
        /// <param name="FriendlyName">Friendly name for this key (Optional)</param>
        /// <param name="SecretKey">The shared secret key to update (Optional)</param>
        public static Task<UpdatePlayerSharedSecretResult> UpdatePlayerSharedSecret(bool Disabled, string FriendlyName = default, string SecretKey = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdatePlayerSharedSecretRequest request = new UpdatePlayerSharedSecretRequest()
            {
                Disabled = Disabled,
                FriendlyName = FriendlyName,
                SecretKey = SecretKey,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdatePlayerSharedSecretResult>("/Admin/UpdatePlayerSharedSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates a player statistic configuration for the title, optionally allowing the developer to specify a reset interval.
        /// </summary>
        /// <param name="AggregationMethod">the aggregation method to use in updating the statistic (defaults to last) (Optional)</param>
        /// <param name="StatisticName">unique name of the statistic (Required)</param>
        /// <param name="VersionChangeInterval">interval at which the values of the statistic for all players are reset (changes are effective at the next occurance of the new interval boundary) (Optional)</param>
        public static Task<UpdatePlayerStatisticDefinitionResult> UpdatePlayerStatisticDefinition(string StatisticName, StatisticAggregationMethod? AggregationMethod = default, StatisticResetIntervalOption? VersionChangeInterval = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdatePlayerStatisticDefinitionRequest request = new UpdatePlayerStatisticDefinitionRequest()
            {
                StatisticName = StatisticName,
                AggregationMethod = AggregationMethod,
                VersionChangeInterval = VersionChangeInterval,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdatePlayerStatisticDefinitionResult>("/Admin/UpdatePlayerStatisticDefinition", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Changes a policy for a title
        /// </summary>
        /// <param name="OverwritePolicy">Whether to overwrite or append to the existing policy. (Required)</param>
        /// <param name="PolicyName">The name of the policy being updated. Only supported name is 'ApiPolicy' (Required)</param>
        /// <param name="Statements">The new statements to include in the policy. (Required)</param>
        public static Task<UpdatePolicyResponse> UpdatePolicy(bool OverwritePolicy, string PolicyName, List<PermissionStatement> Statements, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdatePolicyRequest request = new UpdatePolicyRequest()
            {
                OverwritePolicy = OverwritePolicy,
                PolicyName = PolicyName,
                Statements = Statements,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdatePolicyResponse>("/Admin/UpdatePolicy", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the random drop table configuration for the title
        /// </summary>
        /// <param name="CatalogVersion">which catalog is being updated. If null, update the current default catalog version (Optional)</param>
        /// <param name="Tables">array of random result tables to make available (Note: specifying an existing TableId will result in overwriting that table, while any others will be added to the available set) (Optional)</param>
        public static Task<UpdateRandomResultTablesResult> UpdateRandomResultTables(string CatalogVersion = default, List<RandomResultTable> Tables = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateRandomResultTablesRequest request = new UpdateRandomResultTablesRequest()
            {
                CatalogVersion = CatalogVersion,
                Tables = Tables,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateRandomResultTablesResult>("/Admin/UpdateRandomResultTables", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates an existing virtual item store with new or modified items
        /// </summary>
        /// <param name="StoreId">Unique identifier for the store which is to be updated (Required)</param>
        /// <param name="CatalogVersion">Catalog version of the store to update. If null, uses the default catalog. (Optional)</param>
        /// <param name="MarketingData">Additional data about the store (Optional)</param>
        /// <param name="Store">Array of store items - references to catalog items, with specific pricing - to be added (Optional)</param>
        public static Task<UpdateStoreItemsResult> UpdateStoreItems(string StoreId, string CatalogVersion = default, StoreMarketingModel MarketingData = default, List<StoreItem> Store = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateStoreItemsRequest request = new UpdateStoreItemsRequest()
            {
                StoreId = StoreId,
                CatalogVersion = CatalogVersion,
                MarketingData = MarketingData,
                Store = Store,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateStoreItemsResult>("/Admin/UpdateStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update an existing task.
        /// </summary>
        /// <param name="Description">Description the task (Optional)</param>
        /// <param name="Identifier">Specify either the task ID or the name of the task to be updated. (Optional)</param>
        /// <param name="IsActive">Whether the schedule is active. Inactive schedule will not trigger task execution. (Required)</param>
        /// <param name="Name">Name of the task. This is a unique identifier for tasks in the title. (Required)</param>
        /// <param name="Parameter">Parameter object specific to the task type. See each task type's create API documentation for details. (Optional)</param>
        /// <param name="Schedule">Cron expression for the run schedule of the task. The expression should be in UTC. (Optional)</param>
        /// <param name="Type">Task type. (Required)</param>
        public static Task<EmptyResponse> UpdateTask(bool IsActive, string Name, ScheduledTaskType Type, string Description = default, NameIdentifier Identifier = default, object Parameter = default, string Schedule = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateTaskRequest request = new UpdateTaskRequest()
            {
                IsActive = IsActive,
                Name = Name,
                Type = Type,
                Description = Description,
                Identifier = Identifier,
                Parameter = Parameter,
                Schedule = Schedule,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/UpdateTask", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserPublisherData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserPublisherInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserPublisherReadOnlyData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title specific display name for a user
        /// </summary>
        /// <param name="DisplayName">New title display name for the user - must be between 3 and 25 characters (Required)</param>
        /// <param name="PlayFabId">PlayFab unique identifier of the user whose title specific display name is to be changed (Required)</param>
        public static Task<UpdateUserTitleDisplayNameResult> UpdateUserTitleDisplayName(string DisplayName, string PlayFabId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest()
            {
                DisplayName = DisplayName,
                PlayFabId = PlayFabId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserTitleDisplayNameResult>("/Admin/UpdateUserTitleDisplayName", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }


    }
}

#endif
