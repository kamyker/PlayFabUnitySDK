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
    public static class PlayFabAdminAPI
    {
        static PlayFabAdminAPI() {}


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
        /// Abort an ongoing task instance.
        /// </summary>
        public static Task<EmptyResponse> AbortTaskInstance(string TaskInstanceId, 
            AbortTaskInstanceRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AbortTaskInstanceRequest();
            if(TaskInstanceId != default)
                request.TaskInstanceId = TaskInstanceId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/AbortTaskInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update news item to include localized version
        /// </summary>
        public static Task<AddLocalizedNewsResult> AddLocalizedNews(string Body, string Language, string NewsId, string Title, 
            AddLocalizedNewsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddLocalizedNewsRequest();
            if(Body != default)
                request.Body = Body;
            if(Language != default)
                request.Language = Language;
            if(NewsId != default)
                request.NewsId = NewsId;
            if(Title != default)
                request.Title = Title;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AddLocalizedNewsResult>("/Admin/AddLocalizedNews", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds a new news item to the title's news feed
        /// </summary>
        public static Task<AddNewsResult> AddNews(string Body, string Title, DateTime? Timestamp = default, 
            AddNewsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddNewsRequest();
            if(Body != default)
                request.Body = Body;
            if(Title != default)
                request.Title = Title;
            if(Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AddNewsResult>("/Admin/AddNews", request,
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

            return PlayFabHttp.MakeApiCallAsync<AddPlayerTagResult>("/Admin/AddPlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds the game server executable specified (previously uploaded - see GetServerBuildUploadUrl) to the set of those a
        /// client is permitted to request in a call to StartGame
        /// </summary>
        public static Task<AddServerBuildResult> AddServerBuild(string BuildId, int MaxGamesPerHost, int MinFreeGameSlots, List<Region> ActiveRegions = default, string CommandLineTemplate = default, string Comment = default, string ExecutablePath = default, 
            AddServerBuildRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddServerBuildRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(MaxGamesPerHost != default)
                request.MaxGamesPerHost = MaxGamesPerHost;
            if(MinFreeGameSlots != default)
                request.MinFreeGameSlots = MinFreeGameSlots;
            if(ActiveRegions != default)
                request.ActiveRegions = ActiveRegions;
            if(CommandLineTemplate != default)
                request.CommandLineTemplate = CommandLineTemplate;
            if(Comment != default)
                request.Comment = Comment;
            if(ExecutablePath != default)
                request.ExecutablePath = ExecutablePath;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AddServerBuildResult>("/Admin/AddServerBuild", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increments the specified virtual currency by the stated amount
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

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Admin/AddUserVirtualCurrency", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds one or more virtual currencies to the set defined for the title. Virtual Currencies have a maximum value of
        /// 2,147,483,647 when granted to a player. Any value over that will be discarded.
        /// </summary>
        public static Task<BlankResult> AddVirtualCurrencyTypes(List<VirtualCurrencyData> VirtualCurrencies, 
            AddVirtualCurrencyTypesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddVirtualCurrencyTypesRequest();
            if(VirtualCurrencies != default)
                request.VirtualCurrencies = VirtualCurrencies;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<BlankResult>("/Admin/AddVirtualCurrencyTypes", request,
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

            return PlayFabHttp.MakeApiCallAsync<BanUsersResult>("/Admin/BanUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Checks the global count for the limited edition item.
        /// </summary>
        public static Task<CheckLimitedEditionItemAvailabilityResult> CheckLimitedEditionItemAvailability(string ItemId, string CatalogVersion = default, 
            CheckLimitedEditionItemAvailabilityRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CheckLimitedEditionItemAvailabilityRequest();
            if(ItemId != default)
                request.ItemId = ItemId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CheckLimitedEditionItemAvailabilityResult>("/Admin/CheckLimitedEditionItemAvailability", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create an ActionsOnPlayersInSegment task, which iterates through all players in a segment to execute action.
        /// </summary>
        public static Task<CreateTaskResult> CreateActionsOnPlayersInSegmentTask(bool IsActive, string Name, ActionsOnPlayersInSegmentTaskParameter Parameter, string Description = default, string Schedule = default, 
            CreateActionsOnPlayerSegmentTaskRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateActionsOnPlayerSegmentTaskRequest();
            if(IsActive != default)
                request.IsActive = IsActive;
            if(Name != default)
                request.Name = Name;
            if(Parameter != default)
                request.Parameter = Parameter;
            if(Description != default)
                request.Description = Description;
            if(Schedule != default)
                request.Schedule = Schedule;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateTaskResult>("/Admin/CreateActionsOnPlayersInSegmentTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create a CloudScript task, which can run a CloudScript on a schedule.
        /// </summary>
        public static Task<CreateTaskResult> CreateCloudScriptTask(bool IsActive, string Name, CloudScriptTaskParameter Parameter, string Description = default, string Schedule = default, 
            CreateCloudScriptTaskRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateCloudScriptTaskRequest();
            if(IsActive != default)
                request.IsActive = IsActive;
            if(Name != default)
                request.Name = Name;
            if(Parameter != default)
                request.Parameter = Parameter;
            if(Description != default)
                request.Description = Description;
            if(Schedule != default)
                request.Schedule = Schedule;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateTaskResult>("/Admin/CreateCloudScriptTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Registers a relationship between a title and an Open ID Connect provider.
        /// </summary>
        public static Task<EmptyResponse> CreateOpenIdConnection(string ClientId, string ClientSecret, string ConnectionId, string IssuerDiscoveryUrl = default, OpenIdIssuerInformation IssuerInformation = default, 
            CreateOpenIdConnectionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateOpenIdConnectionRequest();
            if(ClientId != default)
                request.ClientId = ClientId;
            if(ClientSecret != default)
                request.ClientSecret = ClientSecret;
            if(ConnectionId != default)
                request.ConnectionId = ConnectionId;
            if(IssuerDiscoveryUrl != default)
                request.IssuerDiscoveryUrl = IssuerDiscoveryUrl;
            if(IssuerInformation != default)
                request.IssuerInformation = IssuerInformation;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/CreateOpenIdConnection", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new Player Shared Secret Key. It may take up to 5 minutes for this key to become generally available after
        /// this API returns.
        /// </summary>
        public static Task<CreatePlayerSharedSecretResult> CreatePlayerSharedSecret(string FriendlyName = default, 
            CreatePlayerSharedSecretRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreatePlayerSharedSecretRequest();
            if(FriendlyName != default)
                request.FriendlyName = FriendlyName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreatePlayerSharedSecretResult>("/Admin/CreatePlayerSharedSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds a new player statistic configuration to the title, optionally allowing the developer to specify a reset interval
        /// and an aggregation method.
        /// </summary>
        public static Task<CreatePlayerStatisticDefinitionResult> CreatePlayerStatisticDefinition(string StatisticName, StatisticAggregationMethod? AggregationMethod = default, StatisticResetIntervalOption? VersionChangeInterval = default, 
            CreatePlayerStatisticDefinitionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreatePlayerStatisticDefinitionRequest();
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(AggregationMethod != default)
                request.AggregationMethod = AggregationMethod;
            if(VersionChangeInterval != default)
                request.VersionChangeInterval = VersionChangeInterval;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreatePlayerStatisticDefinitionResult>("/Admin/CreatePlayerStatisticDefinition", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Delete a content file from the title. When deleting a file that does not exist, it returns success.
        /// </summary>
        public static Task<BlankResult> DeleteContent(string Key, 
            DeleteContentRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteContentRequest();
            if(Key != default)
                request.Key = Key;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<BlankResult>("/Admin/DeleteContent", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a master player account entirely from all titles and deletes all associated data
        /// </summary>
        public static Task<DeleteMasterPlayerAccountResult> DeleteMasterPlayerAccount(string PlayFabId, string MetaData = default, 
            DeleteMasterPlayerAccountRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteMasterPlayerAccountRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(MetaData != default)
                request.MetaData = MetaData;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeleteMasterPlayerAccountResult>("/Admin/DeleteMasterPlayerAccount", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes a relationship between a title and an OpenID Connect provider.
        /// </summary>
        public static Task<EmptyResponse> DeleteOpenIdConnection(string ConnectionId, 
            DeleteOpenIdConnectionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteOpenIdConnectionRequest();
            if(ConnectionId != default)
                request.ConnectionId = ConnectionId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/DeleteOpenIdConnection", request,
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

            return PlayFabHttp.MakeApiCallAsync<DeletePlayerResult>("/Admin/DeletePlayer", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes an existing Player Shared Secret Key. It may take up to 5 minutes for this delete to be reflected after this API
        /// returns.
        /// </summary>
        public static Task<DeletePlayerSharedSecretResult> DeletePlayerSharedSecret(string SecretKey = default, 
            DeletePlayerSharedSecretRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeletePlayerSharedSecretRequest();
            if(SecretKey != default)
                request.SecretKey = SecretKey;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeletePlayerSharedSecretResult>("/Admin/DeletePlayerSharedSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes an existing virtual item store
        /// </summary>
        public static Task<DeleteStoreResult> DeleteStore(string StoreId, string CatalogVersion = default, 
            DeleteStoreRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteStoreRequest();
            if(StoreId != default)
                request.StoreId = StoreId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeleteStoreResult>("/Admin/DeleteStore", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Delete a task.
        /// </summary>
        public static Task<EmptyResponse> DeleteTask(NameIdentifier Identifier = default, 
            DeleteTaskRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteTaskRequest();
            if(Identifier != default)
                request.Identifier = Identifier;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/DeleteTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Permanently deletes a title and all associated configuration
        /// </summary>
        public static Task<DeleteTitleResult> DeleteTitle(
            DeleteTitleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteTitleRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeleteTitleResult>("/Admin/DeleteTitle", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Exports all associated data of a master player account
        /// </summary>
        public static Task<ExportMasterPlayerDataResult> ExportMasterPlayerData(string PlayFabId, 
            ExportMasterPlayerDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ExportMasterPlayerDataRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ExportMasterPlayerDataResult>("/Admin/ExportMasterPlayerData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get information about a ActionsOnPlayersInSegment task instance.
        /// </summary>
        public static Task<GetActionsOnPlayersInSegmentTaskInstanceResult> GetActionsOnPlayersInSegmentTaskInstance(string TaskInstanceId, 
            GetTaskInstanceRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTaskInstanceRequest();
            if(TaskInstanceId != default)
                request.TaskInstanceId = TaskInstanceId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetActionsOnPlayersInSegmentTaskInstanceResult>("/Admin/GetActionsOnPlayersInSegmentTaskInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
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

            return PlayFabHttp.MakeApiCallAsync<GetAllSegmentsResult>("/Admin/GetAllSegments", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetCatalogItemsResult>("/Admin/GetCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the contents and information of a specific Cloud Script revision.
        /// </summary>
        public static Task<GetCloudScriptRevisionResult> GetCloudScriptRevision(int? Revision = default, int? Version = default, 
            GetCloudScriptRevisionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCloudScriptRevisionRequest();
            if(Revision != default)
                request.Revision = Revision;
            if(Version != default)
                request.Version = Version;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCloudScriptRevisionResult>("/Admin/GetCloudScriptRevision", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get detail information about a CloudScript task instance.
        /// </summary>
        public static Task<GetCloudScriptTaskInstanceResult> GetCloudScriptTaskInstance(string TaskInstanceId, 
            GetTaskInstanceRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTaskInstanceRequest();
            if(TaskInstanceId != default)
                request.TaskInstanceId = TaskInstanceId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCloudScriptTaskInstanceResult>("/Admin/GetCloudScriptTaskInstance", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all the current cloud script versions. For each version, information about the current published and latest
        /// revisions is also listed.
        /// </summary>
        public static Task<GetCloudScriptVersionsResult> GetCloudScriptVersions(
            GetCloudScriptVersionsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetCloudScriptVersionsRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetCloudScriptVersionsResult>("/Admin/GetCloudScriptVersions", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all contents of the title and get statistics such as size
        /// </summary>
        public static Task<GetContentListResult> GetContentList(string Prefix = default, 
            GetContentListRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetContentListRequest();
            if(Prefix != default)
                request.Prefix = Prefix;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetContentListResult>("/Admin/GetContentList", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the pre-signed URL for uploading a content file. A subsequent HTTP PUT to the returned URL uploads the
        /// content. Also, please be aware that the Content service is specifically PlayFab's CDN offering, for which standard CDN
        /// rates apply.
        /// </summary>
        public static Task<GetContentUploadUrlResult> GetContentUploadUrl(string Key, string ContentType = default, 
            GetContentUploadUrlRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetContentUploadUrlRequest();
            if(Key != default)
                request.Key = Key;
            if(ContentType != default)
                request.ContentType = ContentType;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetContentUploadUrlResult>("/Admin/GetContentUploadUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a download URL for the requested report
        /// </summary>
        public static Task<GetDataReportResult> GetDataReport(int Day, int Month, string ReportName, int Year, 
            GetDataReportRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetDataReportRequest();
            if(Day != default)
                request.Day = Day;
            if(Month != default)
                request.Month = Month;
            if(ReportName != default)
                request.ReportName = ReportName;
            if(Year != default)
                request.Year = Year;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetDataReportResult>("/Admin/GetDataReport", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details for a specific completed session, including links to standard out and standard error logs
        /// </summary>
        public static Task<GetMatchmakerGameInfoResult> GetMatchmakerGameInfo(string LobbyId, 
            GetMatchmakerGameInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetMatchmakerGameInfoRequest();
            if(LobbyId != default)
                request.LobbyId = LobbyId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetMatchmakerGameInfoResult>("/Admin/GetMatchmakerGameInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the details of defined game modes for the specified game server executable
        /// </summary>
        public static Task<GetMatchmakerGameModesResult> GetMatchmakerGameModes(string BuildVersion, 
            GetMatchmakerGameModesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetMatchmakerGameModesRequest();
            if(BuildVersion != default)
                request.BuildVersion = BuildVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetMatchmakerGameModesResult>("/Admin/GetMatchmakerGameModes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get the list of titles that the player has played
        /// </summary>
        public static Task<GetPlayedTitleListResult> GetPlayedTitleList(string PlayFabId, 
            GetPlayedTitleListRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayedTitleListRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayedTitleListResult>("/Admin/GetPlayedTitleList", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a player's ID from an auth token.
        /// </summary>
        public static Task<GetPlayerIdFromAuthTokenResult> GetPlayerIdFromAuthToken(string Token, AuthTokenType TokenType, 
            GetPlayerIdFromAuthTokenRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerIdFromAuthTokenRequest();
            if(Token != default)
                request.Token = Token;
            if(TokenType != default)
                request.TokenType = TokenType;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerIdFromAuthTokenResult>("/Admin/GetPlayerIdFromAuthToken", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerProfileResult>("/Admin/GetPlayerProfile", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerSegmentsResult>("/Admin/GetPlayerSegments", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Returns all Player Shared Secret Keys including disabled and expired.
        /// </summary>
        public static Task<GetPlayerSharedSecretsResult> GetPlayerSharedSecrets(
            GetPlayerSharedSecretsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerSharedSecretsRequest();

            var context = GetContext(request);

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

            return PlayFabHttp.MakeApiCallAsync<GetPlayersInSegmentResult>("/Admin/GetPlayersInSegment", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the configuration information for all player statistics defined in the title, regardless of whether they have
        /// a reset interval.
        /// </summary>
        public static Task<GetPlayerStatisticDefinitionsResult> GetPlayerStatisticDefinitions(
            GetPlayerStatisticDefinitionsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPlayerStatisticDefinitionsRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticDefinitionsResult>("/Admin/GetPlayerStatisticDefinitions", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerStatisticVersionsResult>("/Admin/GetPlayerStatisticVersions", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPlayerTagsResult>("/Admin/GetPlayerTags", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the requested policy.
        /// </summary>
        public static Task<GetPolicyResponse> GetPolicy(string PolicyName = default, 
            GetPolicyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetPolicyRequest();
            if(PolicyName != default)
                request.PolicyName = PolicyName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetPolicyResponse>("/Admin/GetPolicy", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetPublisherDataResult>("/Admin/GetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the random drop table configuration for the title
        /// </summary>
        public static Task<GetRandomResultTablesResult> GetRandomResultTables(string CatalogVersion = default, 
            GetRandomResultTablesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetRandomResultTablesRequest();
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetRandomResultTablesResult>("/Admin/GetRandomResultTables", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the build details for the specified game server executable
        /// </summary>
        public static Task<GetServerBuildInfoResult> GetServerBuildInfo(string BuildId, 
            GetServerBuildInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetServerBuildInfoRequest();
            if(BuildId != default)
                request.BuildId = BuildId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetServerBuildInfoResult>("/Admin/GetServerBuildInfo", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the pre-authorized URL for uploading a game server package containing a build (does not enable the build for
        /// use - see AddServerBuild)
        /// </summary>
        public static Task<GetServerBuildUploadURLResult> GetServerBuildUploadUrl(string BuildId, 
            GetServerBuildUploadURLRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetServerBuildUploadURLRequest();
            if(BuildId != default)
                request.BuildId = BuildId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetServerBuildUploadURLResult>("/Admin/GetServerBuildUploadUrl", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the set of items defined for the specified store, including all prices defined
        /// </summary>
        public static Task<GetStoreItemsResult> GetStoreItems(string StoreId, string CatalogVersion = default, 
            GetStoreItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetStoreItemsRequest();
            if(StoreId != default)
                request.StoreId = StoreId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetStoreItemsResult>("/Admin/GetStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Query for task instances by task, status, or time range.
        /// </summary>
        public static Task<GetTaskInstancesResult> GetTaskInstances(DateTime? StartedAtRangeFrom = default, DateTime? StartedAtRangeTo = default, TaskInstanceStatus? StatusFilter = default, NameIdentifier TaskIdentifier = default, 
            GetTaskInstancesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTaskInstancesRequest();
            if(StartedAtRangeFrom != default)
                request.StartedAtRangeFrom = StartedAtRangeFrom;
            if(StartedAtRangeTo != default)
                request.StartedAtRangeTo = StartedAtRangeTo;
            if(StatusFilter != default)
                request.StatusFilter = StatusFilter;
            if(TaskIdentifier != default)
                request.TaskIdentifier = TaskIdentifier;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTaskInstancesResult>("/Admin/GetTaskInstances", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get definition information on a specified task or all tasks within a title.
        /// </summary>
        public static Task<GetTasksResult> GetTasks(NameIdentifier Identifier = default, 
            GetTasksRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTasksRequest();
            if(Identifier != default)
                request.Identifier = Identifier;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTasksResult>("/Admin/GetTasks", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings which can be read by the client
        /// </summary>
        public static Task<GetTitleDataResult> GetTitleData(List<string> Keys = default, 
            GetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleDataRequest();
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Admin/GetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the key-value store of custom title settings which cannot be read by the client
        /// </summary>
        public static Task<GetTitleDataResult> GetTitleInternalData(List<string> Keys = default, 
            GetTitleDataRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleDataRequest();
            if(Keys != default)
                request.Keys = Keys;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleDataResult>("/Admin/GetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the relevant details for a specified user, based upon a match against a supplied unique identifier
        /// </summary>
        public static Task<LookupUserAccountInfoResult> GetUserAccountInfo(string Email = default, string PlayFabId = default, string TitleDisplayName = default, string Username = default, 
            LookupUserAccountInfoRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new LookupUserAccountInfoRequest();
            if(Email != default)
                request.Email = Email;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(TitleDisplayName != default)
                request.TitleDisplayName = TitleDisplayName;
            if(Username != default)
                request.Username = Username;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<LookupUserAccountInfoResult>("/Admin/GetUserAccountInfo", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserBansResult>("/Admin/GetUserBans", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserInventoryResult>("/Admin/GetUserInventory", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserPublisherData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserPublisherInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserPublisherReadOnlyData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GetUserDataResult>("/Admin/GetUserReadOnlyData", request,
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

            return PlayFabHttp.MakeApiCallAsync<GrantItemsToUsersResult>("/Admin/GrantItemsToUsers", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Increases the global count for the given scarce resource.
        /// </summary>
        public static Task<IncrementLimitedEditionItemAvailabilityResult> IncrementLimitedEditionItemAvailability(int Amount, string ItemId, string CatalogVersion = default, 
            IncrementLimitedEditionItemAvailabilityRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new IncrementLimitedEditionItemAvailabilityRequest();
            if(Amount != default)
                request.Amount = Amount;
            if(ItemId != default)
                request.ItemId = ItemId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<IncrementLimitedEditionItemAvailabilityResult>("/Admin/IncrementLimitedEditionItemAvailability", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Resets the indicated statistic, removing all player entries for it and backing up the old values.
        /// </summary>
        public static Task<IncrementPlayerStatisticVersionResult> IncrementPlayerStatisticVersion(string StatisticName = default, 
            IncrementPlayerStatisticVersionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new IncrementPlayerStatisticVersionRequest();
            if(StatisticName != default)
                request.StatisticName = StatisticName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<IncrementPlayerStatisticVersionResult>("/Admin/IncrementPlayerStatisticVersion", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves a list of all Open ID Connect providers registered to a title.
        /// </summary>
        public static Task<ListOpenIdConnectionResponse> ListOpenIdConnection(
            ListOpenIdConnectionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListOpenIdConnectionRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListOpenIdConnectionResponse>("/Admin/ListOpenIdConnection", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves the build details for all game server executables which are currently defined for the title
        /// </summary>
        public static Task<ListBuildsResult> ListServerBuilds(
            ListBuildsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListBuildsRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListBuildsResult>("/Admin/ListServerBuilds", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retuns the list of all defined virtual currencies for the title
        /// </summary>
        public static Task<ListVirtualCurrencyTypesResult> ListVirtualCurrencyTypes(
            ListVirtualCurrencyTypesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListVirtualCurrencyTypesRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListVirtualCurrencyTypesResult>("/Admin/ListVirtualCurrencyTypes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the game server mode details for the specified game server executable
        /// </summary>
        public static Task<ModifyMatchmakerGameModesResult> ModifyMatchmakerGameModes(string BuildVersion, List<GameModeInfo> GameModes, 
            ModifyMatchmakerGameModesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ModifyMatchmakerGameModesRequest();
            if(BuildVersion != default)
                request.BuildVersion = BuildVersion;
            if(GameModes != default)
                request.GameModes = GameModes;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyMatchmakerGameModesResult>("/Admin/ModifyMatchmakerGameModes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the build details for the specified game server executable
        /// </summary>
        public static Task<ModifyServerBuildResult> ModifyServerBuild(string BuildId, int MaxGamesPerHost, int MinFreeGameSlots, List<Region> ActiveRegions = default, string CommandLineTemplate = default, string Comment = default, string ExecutablePath = default, DateTime? Timestamp = default, 
            ModifyServerBuildRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ModifyServerBuildRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(MaxGamesPerHost != default)
                request.MaxGamesPerHost = MaxGamesPerHost;
            if(MinFreeGameSlots != default)
                request.MinFreeGameSlots = MinFreeGameSlots;
            if(ActiveRegions != default)
                request.ActiveRegions = ActiveRegions;
            if(CommandLineTemplate != default)
                request.CommandLineTemplate = CommandLineTemplate;
            if(Comment != default)
                request.Comment = Comment;
            if(ExecutablePath != default)
                request.ExecutablePath = ExecutablePath;
            if(Timestamp != default)
                request.Timestamp = Timestamp;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ModifyServerBuildResult>("/Admin/ModifyServerBuild", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Attempts to process an order refund through the original real money payment provider.
        /// </summary>
        public static Task<RefundPurchaseResponse> RefundPurchase(string OrderId, string PlayFabId, string Reason = default, 
            RefundPurchaseRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RefundPurchaseRequest();
            if(OrderId != default)
                request.OrderId = OrderId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Reason != default)
                request.Reason = Reason;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RefundPurchaseResponse>("/Admin/RefundPurchase", request,
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

            return PlayFabHttp.MakeApiCallAsync<RemovePlayerTagResult>("/Admin/RemovePlayerTag", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes the game server executable specified from the set of those a client is permitted to request in a call to
        /// StartGame
        /// </summary>
        public static Task<RemoveServerBuildResult> RemoveServerBuild(string BuildId, 
            RemoveServerBuildRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveServerBuildRequest();
            if(BuildId != default)
                request.BuildId = BuildId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RemoveServerBuildResult>("/Admin/RemoveServerBuild", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes one or more virtual currencies from the set defined for the title.
        /// </summary>
        public static Task<BlankResult> RemoveVirtualCurrencyTypes(List<VirtualCurrencyData> VirtualCurrencies, 
            RemoveVirtualCurrencyTypesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveVirtualCurrencyTypesRequest();
            if(VirtualCurrencies != default)
                request.VirtualCurrencies = VirtualCurrencies;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<BlankResult>("/Admin/RemoveVirtualCurrencyTypes", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Completely removes all statistics for the specified character, for the current game
        /// </summary>
        public static Task<ResetCharacterStatisticsResult> ResetCharacterStatistics(string CharacterId, string PlayFabId, 
            ResetCharacterStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ResetCharacterStatisticsRequest();
            if(CharacterId != default)
                request.CharacterId = CharacterId;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ResetCharacterStatisticsResult>("/Admin/ResetCharacterStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Reset a player's password for a given title.
        /// </summary>
        public static Task<ResetPasswordResult> ResetPassword(string Password, string Token, 
            ResetPasswordRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ResetPasswordRequest();
            if(Password != default)
                request.Password = Password;
            if(Token != default)
                request.Token = Token;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ResetPasswordResult>("/Admin/ResetPassword", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Completely removes all statistics for the specified user, for the current game
        /// </summary>
        public static Task<ResetUserStatisticsResult> ResetUserStatistics(string PlayFabId, 
            ResetUserStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ResetUserStatisticsRequest();
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ResetUserStatisticsResult>("/Admin/ResetUserStatistics", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Attempts to resolve a dispute with the original order's payment provider.
        /// </summary>
        public static Task<ResolvePurchaseDisputeResponse> ResolvePurchaseDispute(string OrderId, ResolutionOutcome Outcome, string PlayFabId, string Reason = default, 
            ResolvePurchaseDisputeRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ResolvePurchaseDisputeRequest();
            if(OrderId != default)
                request.OrderId = OrderId;
            if(Outcome != default)
                request.Outcome = Outcome;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;
            if(Reason != default)
                request.Reason = Reason;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ResolvePurchaseDisputeResponse>("/Admin/ResolvePurchaseDispute", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeAllBansForUserResult>("/Admin/RevokeAllBansForUser", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeBansResult>("/Admin/RevokeBans", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryResult>("/Admin/RevokeInventoryItem", request,
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

            return PlayFabHttp.MakeApiCallAsync<RevokeInventoryItemsResult>("/Admin/RevokeInventoryItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Run a task immediately regardless of its schedule.
        /// </summary>
        public static Task<RunTaskResult> RunTask(NameIdentifier Identifier = default, 
            RunTaskRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RunTaskRequest();
            if(Identifier != default)
                request.Identifier = Identifier;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RunTaskResult>("/Admin/RunTask", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Forces an email to be sent to the registered email address for the user's account, with a link allowing the user to
        /// change the password.If an account recovery email template ID is provided, an email using the custom email template will
        /// be used.
        /// </summary>
        public static Task<SendAccountRecoveryEmailResult> SendAccountRecoveryEmail(string Email, string EmailTemplateId = default, 
            SendAccountRecoveryEmailRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SendAccountRecoveryEmailRequest();
            if(Email != default)
                request.Email = Email;
            if(EmailTemplateId != default)
                request.EmailTemplateId = EmailTemplateId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SendAccountRecoveryEmailResult>("/Admin/SendAccountRecoveryEmail", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates the catalog configuration of all virtual goods for the specified catalog version
        /// </summary>
        public static Task<UpdateCatalogItemsResult> SetCatalogItems(List<CatalogItem> Catalog = default, string CatalogVersion = default, bool? SetAsDefaultCatalog = default, 
            UpdateCatalogItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCatalogItemsRequest();
            if(Catalog != default)
                request.Catalog = Catalog;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(SetAsDefaultCatalog != default)
                request.SetAsDefaultCatalog = SetAsDefaultCatalog;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCatalogItemsResult>("/Admin/SetCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets or resets the player's secret. Player secrets are used to sign API requests.
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

            return PlayFabHttp.MakeApiCallAsync<SetPlayerSecretResult>("/Admin/SetPlayerSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the currently published revision of a title Cloud Script
        /// </summary>
        public static Task<SetPublishedRevisionResult> SetPublishedRevision(int Revision, int Version, 
            SetPublishedRevisionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetPublishedRevisionRequest();
            if(Revision != default)
                request.Revision = Revision;
            if(Version != default)
                request.Version = Version;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetPublishedRevisionResult>("/Admin/SetPublishedRevision", request,
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

            return PlayFabHttp.MakeApiCallAsync<SetPublisherDataResult>("/Admin/SetPublisherData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets all the items in one virtual store
        /// </summary>
        public static Task<UpdateStoreItemsResult> SetStoreItems(string StoreId, string CatalogVersion = default, StoreMarketingModel MarketingData = default, List<StoreItem> Store = default, 
            UpdateStoreItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateStoreItemsRequest();
            if(StoreId != default)
                request.StoreId = StoreId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(MarketingData != default)
                request.MarketingData = MarketingData;
            if(Store != default)
                request.Store = Store;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateStoreItemsResult>("/Admin/SetStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates and updates the key-value store of custom title settings which can be read by the client
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

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Admin/SetTitleData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the key-value store of custom title settings which cannot be read by the client
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

            return PlayFabHttp.MakeApiCallAsync<SetTitleDataResult>("/Admin/SetTitleInternalData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets the Amazon Resource Name (ARN) for iOS and Android push notifications. Documentation on the exact restrictions can
        /// be found at: http://docs.aws.amazon.com/sns/latest/api/API_CreatePlatformApplication.html. Currently, Amazon device
        /// Messaging is not supported.
        /// </summary>
        public static Task<SetupPushNotificationResult> SetupPushNotification(string Credential, string Name, bool OverwriteOldARN, PushSetupPlatform Platform, string Key = default, 
            SetupPushNotificationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetupPushNotificationRequest();
            if(Credential != default)
                request.Credential = Credential;
            if(Name != default)
                request.Name = Name;
            if(OverwriteOldARN != default)
                request.OverwriteOldARN = OverwriteOldARN;
            if(Platform != default)
                request.Platform = Platform;
            if(Key != default)
                request.Key = Key;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetupPushNotificationResult>("/Admin/SetupPushNotification", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Decrements the specified virtual currency by the stated amount
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

            return PlayFabHttp.MakeApiCallAsync<ModifyUserVirtualCurrencyResult>("/Admin/SubtractUserVirtualCurrency", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateBansResult>("/Admin/UpdateBans", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the catalog configuration for virtual goods in the specified catalog version
        /// </summary>
        public static Task<UpdateCatalogItemsResult> UpdateCatalogItems(List<CatalogItem> Catalog = default, string CatalogVersion = default, bool? SetAsDefaultCatalog = default, 
            UpdateCatalogItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCatalogItemsRequest();
            if(Catalog != default)
                request.Catalog = Catalog;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(SetAsDefaultCatalog != default)
                request.SetAsDefaultCatalog = SetAsDefaultCatalog;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCatalogItemsResult>("/Admin/UpdateCatalogItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new Cloud Script revision and uploads source code to it. Note that at this time, only one file should be
        /// submitted in the revision.
        /// </summary>
        public static Task<UpdateCloudScriptResult> UpdateCloudScript(List<CloudScriptFile> Files, bool Publish, string DeveloperPlayFabId = default, 
            UpdateCloudScriptRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateCloudScriptRequest();
            if(Files != default)
                request.Files = Files;
            if(Publish != default)
                request.Publish = Publish;
            if(DeveloperPlayFabId != default)
                request.DeveloperPlayFabId = DeveloperPlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateCloudScriptResult>("/Admin/UpdateCloudScript", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Modifies data and credentials for an existing relationship between a title and an Open ID Connect provider
        /// </summary>
        public static Task<EmptyResponse> UpdateOpenIdConnection(string ConnectionId, string ClientId = default, string ClientSecret = default, string IssuerDiscoveryUrl = default, OpenIdIssuerInformation IssuerInformation = default, 
            UpdateOpenIdConnectionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateOpenIdConnectionRequest();
            if(ConnectionId != default)
                request.ConnectionId = ConnectionId;
            if(ClientId != default)
                request.ClientId = ClientId;
            if(ClientSecret != default)
                request.ClientSecret = ClientSecret;
            if(IssuerDiscoveryUrl != default)
                request.IssuerDiscoveryUrl = IssuerDiscoveryUrl;
            if(IssuerInformation != default)
                request.IssuerInformation = IssuerInformation;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/UpdateOpenIdConnection", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates a existing Player Shared Secret Key. It may take up to 5 minutes for this update to become generally available
        /// after this API returns.
        /// </summary>
        public static Task<UpdatePlayerSharedSecretResult> UpdatePlayerSharedSecret(bool Disabled, string FriendlyName = default, string SecretKey = default, 
            UpdatePlayerSharedSecretRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdatePlayerSharedSecretRequest();
            if(Disabled != default)
                request.Disabled = Disabled;
            if(FriendlyName != default)
                request.FriendlyName = FriendlyName;
            if(SecretKey != default)
                request.SecretKey = SecretKey;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdatePlayerSharedSecretResult>("/Admin/UpdatePlayerSharedSecret", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates a player statistic configuration for the title, optionally allowing the developer to specify a reset interval.
        /// </summary>
        public static Task<UpdatePlayerStatisticDefinitionResult> UpdatePlayerStatisticDefinition(string StatisticName, StatisticAggregationMethod? AggregationMethod = default, StatisticResetIntervalOption? VersionChangeInterval = default, 
            UpdatePlayerStatisticDefinitionRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdatePlayerStatisticDefinitionRequest();
            if(StatisticName != default)
                request.StatisticName = StatisticName;
            if(AggregationMethod != default)
                request.AggregationMethod = AggregationMethod;
            if(VersionChangeInterval != default)
                request.VersionChangeInterval = VersionChangeInterval;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdatePlayerStatisticDefinitionResult>("/Admin/UpdatePlayerStatisticDefinition", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Changes a policy for a title
        /// </summary>
        public static Task<UpdatePolicyResponse> UpdatePolicy(bool OverwritePolicy, string PolicyName, List<PermissionStatement> Statements, 
            UpdatePolicyRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdatePolicyRequest();
            if(OverwritePolicy != default)
                request.OverwritePolicy = OverwritePolicy;
            if(PolicyName != default)
                request.PolicyName = PolicyName;
            if(Statements != default)
                request.Statements = Statements;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdatePolicyResponse>("/Admin/UpdatePolicy", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the random drop table configuration for the title
        /// </summary>
        public static Task<UpdateRandomResultTablesResult> UpdateRandomResultTables(string CatalogVersion = default, List<RandomResultTable> Tables = default, 
            UpdateRandomResultTablesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateRandomResultTablesRequest();
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(Tables != default)
                request.Tables = Tables;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateRandomResultTablesResult>("/Admin/UpdateRandomResultTables", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates an existing virtual item store with new or modified items
        /// </summary>
        public static Task<UpdateStoreItemsResult> UpdateStoreItems(string StoreId, string CatalogVersion = default, StoreMarketingModel MarketingData = default, List<StoreItem> Store = default, 
            UpdateStoreItemsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateStoreItemsRequest();
            if(StoreId != default)
                request.StoreId = StoreId;
            if(CatalogVersion != default)
                request.CatalogVersion = CatalogVersion;
            if(MarketingData != default)
                request.MarketingData = MarketingData;
            if(Store != default)
                request.Store = Store;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateStoreItemsResult>("/Admin/UpdateStoreItems", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Update an existing task.
        /// </summary>
        public static Task<EmptyResponse> UpdateTask(bool IsActive, string Name, ScheduledTaskType Type, string Description = default, NameIdentifier Identifier = default, object Parameter = default, string Schedule = default, 
            UpdateTaskRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateTaskRequest();
            if(IsActive != default)
                request.IsActive = IsActive;
            if(Name != default)
                request.Name = Name;
            if(Type != default)
                request.Type = Type;
            if(Description != default)
                request.Description = Description;
            if(Identifier != default)
                request.Identifier = Identifier;
            if(Parameter != default)
                request.Parameter = Parameter;
            if(Schedule != default)
                request.Schedule = Schedule;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Admin/UpdateTask", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserPublisherData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserPublisherInternalData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserPublisherReadOnlyData", request,
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

            return PlayFabHttp.MakeApiCallAsync<UpdateUserDataResult>("/Admin/UpdateUserReadOnlyData", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates the title specific display name for a user
        /// </summary>
        public static Task<UpdateUserTitleDisplayNameResult> UpdateUserTitleDisplayName(string DisplayName, string PlayFabId, 
            UpdateUserTitleDisplayNameRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateUserTitleDisplayNameRequest();
            if(DisplayName != default)
                request.DisplayName = DisplayName;
            if(PlayFabId != default)
                request.PlayFabId = PlayFabId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateUserTitleDisplayNameResult>("/Admin/UpdateUserTitleDisplayName", request,
				AuthType.DevSecretKey,
				customData, extraHeaders, context);
        }


    }
}

#endif
