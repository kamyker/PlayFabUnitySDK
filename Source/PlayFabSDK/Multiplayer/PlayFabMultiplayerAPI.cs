#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.MultiplayerModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// API methods for managing multiplayer servers. API methods for managing parties.
    /// </summary>
    public static partial class PlayFabMultiplayerAPI
    {
        static PlayFabMultiplayerAPI() {}


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
        /// Cancel all active tickets the player is a member of in a given queue.
        /// </summary>
        public static Task<CancelAllMatchmakingTicketsForPlayerResult> CancelAllMatchmakingTicketsForPlayer(string QueueName, EntityKey Entity = default, 
            CancelAllMatchmakingTicketsForPlayerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CancelAllMatchmakingTicketsForPlayerRequest();
            if(QueueName != default)
                request.QueueName = QueueName;
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CancelAllMatchmakingTicketsForPlayerResult>("/Match/CancelAllMatchmakingTicketsForPlayer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Cancel a matchmaking ticket.
        /// </summary>
        public static Task<CancelMatchmakingTicketResult> CancelMatchmakingTicket(string QueueName, string TicketId, 
            CancelMatchmakingTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CancelMatchmakingTicketRequest();
            if(QueueName != default)
                request.QueueName = QueueName;
            if(TicketId != default)
                request.TicketId = TicketId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CancelMatchmakingTicketResult>("/Match/CancelMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a multiplayer server build with a custom container.
        /// </summary>
        public static Task<CreateBuildWithCustomContainerResponse> CreateBuildWithCustomContainer(string BuildName, int MultiplayerServerCountPerVm, List<Port> Ports, List<BuildRegionParams> RegionConfigurations, List<AssetReferenceParams> GameAssetReferences = default, ContainerFlavor? ContainerFlavor = default, Dictionary<string,string> Metadata = default, AzureVmSize? VmSize = default, ContainerImageReference ContainerImageReference = default, string ContainerRunCommand = default, List<GameCertificateReferenceParams> GameCertificateReferences = default, 
            CreateBuildWithCustomContainerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateBuildWithCustomContainerRequest();
            if(BuildName != default)
                request.BuildName = BuildName;
            if(MultiplayerServerCountPerVm != default)
                request.MultiplayerServerCountPerVm = MultiplayerServerCountPerVm;
            if(Ports != default)
                request.Ports = Ports;
            if(RegionConfigurations != default)
                request.RegionConfigurations = RegionConfigurations;
            if(GameAssetReferences != default)
                request.GameAssetReferences = GameAssetReferences;
            if(ContainerFlavor != default)
                request.ContainerFlavor = ContainerFlavor;
            if(Metadata != default)
                request.Metadata = Metadata;
            if(VmSize != default)
                request.VmSize = VmSize;
            if(ContainerImageReference != default)
                request.ContainerImageReference = ContainerImageReference;
            if(ContainerRunCommand != default)
                request.ContainerRunCommand = ContainerRunCommand;
            if(GameCertificateReferences != default)
                request.GameCertificateReferences = GameCertificateReferences;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateBuildWithCustomContainerResponse>("/MultiplayerServer/CreateBuildWithCustomContainer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a multiplayer server build with a managed container.
        /// </summary>
        public static Task<CreateBuildWithManagedContainerResponse> CreateBuildWithManagedContainer(string BuildName, List<AssetReferenceParams> GameAssetReferences, int MultiplayerServerCountPerVm, List<Port> Ports, List<BuildRegionParams> RegionConfigurations, string StartMultiplayerServerCommand, AzureVmSize? VmSize = default, List<GameCertificateReferenceParams> GameCertificateReferences = default, InstrumentationConfiguration InstrumentationConfiguration = default, ContainerFlavor? ContainerFlavor = default, Dictionary<string,string> Metadata = default, 
            CreateBuildWithManagedContainerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateBuildWithManagedContainerRequest();
            if(BuildName != default)
                request.BuildName = BuildName;
            if(GameAssetReferences != default)
                request.GameAssetReferences = GameAssetReferences;
            if(MultiplayerServerCountPerVm != default)
                request.MultiplayerServerCountPerVm = MultiplayerServerCountPerVm;
            if(Ports != default)
                request.Ports = Ports;
            if(RegionConfigurations != default)
                request.RegionConfigurations = RegionConfigurations;
            if(StartMultiplayerServerCommand != default)
                request.StartMultiplayerServerCommand = StartMultiplayerServerCommand;
            if(VmSize != default)
                request.VmSize = VmSize;
            if(GameCertificateReferences != default)
                request.GameCertificateReferences = GameCertificateReferences;
            if(InstrumentationConfiguration != default)
                request.InstrumentationConfiguration = InstrumentationConfiguration;
            if(ContainerFlavor != default)
                request.ContainerFlavor = ContainerFlavor;
            if(Metadata != default)
                request.Metadata = Metadata;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateBuildWithManagedContainerResponse>("/MultiplayerServer/CreateBuildWithManagedContainer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create a matchmaking ticket as a client.
        /// </summary>
        public static Task<CreateMatchmakingTicketResult> CreateMatchmakingTicket(MatchmakingPlayer Creator, int GiveUpAfterSeconds, string QueueName, List<EntityKey> MembersToMatchWith = default, 
            CreateMatchmakingTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateMatchmakingTicketRequest();
            if(Creator != default)
                request.Creator = Creator;
            if(GiveUpAfterSeconds != default)
                request.GiveUpAfterSeconds = GiveUpAfterSeconds;
            if(QueueName != default)
                request.QueueName = QueueName;
            if(MembersToMatchWith != default)
                request.MembersToMatchWith = MembersToMatchWith;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateMatchmakingTicketResult>("/Match/CreateMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a remote user to log on to a VM for a multiplayer server build.
        /// </summary>
        public static Task<CreateRemoteUserResponse> CreateRemoteUser(string BuildId, string Region, string Username, string VmId, DateTime? ExpirationTime = default, 
            CreateRemoteUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateRemoteUserRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(Username != default)
                request.Username = Username;
            if(VmId != default)
                request.VmId = VmId;
            if(ExpirationTime != default)
                request.ExpirationTime = ExpirationTime;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateRemoteUserResponse>("/MultiplayerServer/CreateRemoteUser", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create a matchmaking ticket as a server. The matchmaking service automatically starts matching the ticket against other
        /// matchmaking tickets.
        /// </summary>
        public static Task<CreateMatchmakingTicketResult> CreateServerMatchmakingTicket(int GiveUpAfterSeconds, List<MatchmakingPlayer> Members, string QueueName, 
            CreateServerMatchmakingTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateServerMatchmakingTicketRequest();
            if(GiveUpAfterSeconds != default)
                request.GiveUpAfterSeconds = GiveUpAfterSeconds;
            if(Members != default)
                request.Members = Members;
            if(QueueName != default)
                request.QueueName = QueueName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateMatchmakingTicketResult>("/Match/CreateServerMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a multiplayer server game asset for a title.
        /// </summary>
        public static Task<EmptyResponse> DeleteAsset(string FileName, 
            DeleteAssetRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteAssetRequest();
            if(FileName != default)
                request.FileName = FileName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteAsset", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a multiplayer server build.
        /// </summary>
        public static Task<EmptyResponse> DeleteBuild(string BuildId, 
            DeleteBuildRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteBuildRequest();
            if(BuildId != default)
                request.BuildId = BuildId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteBuild", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a multiplayer server game certificate.
        /// </summary>
        public static Task<EmptyResponse> DeleteCertificate(string Name, 
            DeleteCertificateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteCertificateRequest();
            if(Name != default)
                request.Name = Name;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteCertificate", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a remote user to log on to a VM for a multiplayer server build.
        /// </summary>
        public static Task<EmptyResponse> DeleteRemoteUser(string BuildId, string Region, string Username, string VmId, 
            DeleteRemoteUserRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteRemoteUserRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(Username != default)
                request.Username = Username;
            if(VmId != default)
                request.VmId = VmId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteRemoteUser", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Enables the multiplayer server feature for a title.
        /// </summary>
        public static Task<EnableMultiplayerServersForTitleResponse> EnableMultiplayerServersForTitle(
            EnableMultiplayerServersForTitleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new EnableMultiplayerServersForTitleRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EnableMultiplayerServersForTitleResponse>("/MultiplayerServer/EnableMultiplayerServersForTitle", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the URL to upload assets to.
        /// </summary>
        public static Task<GetAssetUploadUrlResponse> GetAssetUploadUrl(string FileName, 
            GetAssetUploadUrlRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetAssetUploadUrlRequest();
            if(FileName != default)
                request.FileName = FileName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetAssetUploadUrlResponse>("/MultiplayerServer/GetAssetUploadUrl", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a multiplayer server build.
        /// </summary>
        public static Task<GetBuildResponse> GetBuild(string BuildId, 
            GetBuildRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetBuildRequest();
            if(BuildId != default)
                request.BuildId = BuildId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetBuildResponse>("/MultiplayerServer/GetBuild", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the credentials to the container registry.
        /// </summary>
        public static Task<GetContainerRegistryCredentialsResponse> GetContainerRegistryCredentials(
            GetContainerRegistryCredentialsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetContainerRegistryCredentialsRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetContainerRegistryCredentialsResponse>("/MultiplayerServer/GetContainerRegistryCredentials", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get a match.
        /// </summary>
        public static Task<GetMatchResult> GetMatch(bool EscapeObject, string MatchId, string QueueName, bool ReturnMemberAttributes, 
            GetMatchRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetMatchRequest();
            if(EscapeObject != default)
                request.EscapeObject = EscapeObject;
            if(MatchId != default)
                request.MatchId = MatchId;
            if(QueueName != default)
                request.QueueName = QueueName;
            if(ReturnMemberAttributes != default)
                request.ReturnMemberAttributes = ReturnMemberAttributes;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetMatchResult>("/Match/GetMatch", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get a matchmaking ticket by ticket Id.
        /// </summary>
        public static Task<GetMatchmakingTicketResult> GetMatchmakingTicket(bool EscapeObject, string QueueName, string TicketId, 
            GetMatchmakingTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetMatchmakingTicketRequest();
            if(EscapeObject != default)
                request.EscapeObject = EscapeObject;
            if(QueueName != default)
                request.QueueName = QueueName;
            if(TicketId != default)
                request.TicketId = TicketId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetMatchmakingTicketResult>("/Match/GetMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets multiplayer server session details for a build.
        /// </summary>
        public static Task<GetMultiplayerServerDetailsResponse> GetMultiplayerServerDetails(string BuildId, string Region, string SessionId, 
            GetMultiplayerServerDetailsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetMultiplayerServerDetailsRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(SessionId != default)
                request.SessionId = SessionId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetMultiplayerServerDetailsResponse>("/MultiplayerServer/GetMultiplayerServerDetails", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get the statistics for a queue.
        /// </summary>
        public static Task<GetQueueStatisticsResult> GetQueueStatistics(string QueueName, 
            GetQueueStatisticsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetQueueStatisticsRequest();
            if(QueueName != default)
                request.QueueName = QueueName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetQueueStatisticsResult>("/Match/GetQueueStatistics", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a remote login endpoint to a VM that is hosting a multiplayer server build.
        /// </summary>
        public static Task<GetRemoteLoginEndpointResponse> GetRemoteLoginEndpoint(string BuildId, string Region, string VmId, 
            GetRemoteLoginEndpointRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetRemoteLoginEndpointRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(VmId != default)
                request.VmId = VmId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetRemoteLoginEndpointResponse>("/MultiplayerServer/GetRemoteLoginEndpoint", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the status of whether a title is enabled for the multiplayer server feature.
        /// </summary>
        public static Task<GetTitleEnabledForMultiplayerServersStatusResponse> GetTitleEnabledForMultiplayerServersStatus(
            GetTitleEnabledForMultiplayerServersStatusRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleEnabledForMultiplayerServersStatusRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleEnabledForMultiplayerServersStatusResponse>("/MultiplayerServer/GetTitleEnabledForMultiplayerServersStatus", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the quotas for a title in relation to multiplayer servers.
        /// </summary>
        public static Task<GetTitleMultiplayerServersQuotasResponse> GetTitleMultiplayerServersQuotas(
            GetTitleMultiplayerServersQuotasRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetTitleMultiplayerServersQuotasRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetTitleMultiplayerServersQuotasResponse>("/MultiplayerServer/GetTitleMultiplayerServersQuotas", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Join a matchmaking ticket.
        /// </summary>
        public static Task<JoinMatchmakingTicketResult> JoinMatchmakingTicket(MatchmakingPlayer Member, string QueueName, string TicketId, 
            JoinMatchmakingTicketRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new JoinMatchmakingTicketRequest();
            if(Member != default)
                request.Member = Member;
            if(QueueName != default)
                request.QueueName = QueueName;
            if(TicketId != default)
                request.TicketId = TicketId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<JoinMatchmakingTicketResult>("/Match/JoinMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists archived multiplayer server sessions for a build.
        /// </summary>
        public static Task<ListMultiplayerServersResponse> ListArchivedMultiplayerServers(string BuildId, string Region, int? PageSize = default, string SkipToken = default, 
            ListMultiplayerServersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListMultiplayerServersRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListMultiplayerServersResponse>("/MultiplayerServer/ListArchivedMultiplayerServers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists multiplayer server game assets for a title.
        /// </summary>
        public static Task<ListAssetSummariesResponse> ListAssetSummaries(int? PageSize = default, string SkipToken = default, 
            ListAssetSummariesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListAssetSummariesRequest();
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListAssetSummariesResponse>("/MultiplayerServer/ListAssetSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists summarized details of all multiplayer server builds for a title.
        /// </summary>
        public static Task<ListBuildSummariesResponse> ListBuildSummaries(int? PageSize = default, string SkipToken = default, 
            ListBuildSummariesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListBuildSummariesRequest();
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListBuildSummariesResponse>("/MultiplayerServer/ListBuildSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists multiplayer server game certificates for a title.
        /// </summary>
        public static Task<ListCertificateSummariesResponse> ListCertificateSummaries(int? PageSize = default, string SkipToken = default, 
            ListCertificateSummariesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListCertificateSummariesRequest();
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListCertificateSummariesResponse>("/MultiplayerServer/ListCertificateSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists custom container images for a title.
        /// </summary>
        public static Task<ListContainerImagesResponse> ListContainerImages(int? PageSize = default, string SkipToken = default, 
            ListContainerImagesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListContainerImagesRequest();
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListContainerImagesResponse>("/MultiplayerServer/ListContainerImages", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists the tags for a custom container image.
        /// </summary>
        public static Task<ListContainerImageTagsResponse> ListContainerImageTags(string ImageName = default, 
            ListContainerImageTagsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListContainerImageTagsRequest();
            if(ImageName != default)
                request.ImageName = ImageName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListContainerImageTagsResponse>("/MultiplayerServer/ListContainerImageTags", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all matchmaking ticket Ids the user is a member of.
        /// </summary>
        public static Task<ListMatchmakingTicketsForPlayerResult> ListMatchmakingTicketsForPlayer(string QueueName, EntityKey Entity = default, 
            ListMatchmakingTicketsForPlayerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListMatchmakingTicketsForPlayerRequest();
            if(QueueName != default)
                request.QueueName = QueueName;
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListMatchmakingTicketsForPlayerResult>("/Match/ListMatchmakingTicketsForPlayer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists multiplayer server sessions for a build.
        /// </summary>
        public static Task<ListMultiplayerServersResponse> ListMultiplayerServers(string BuildId, string Region, int? PageSize = default, string SkipToken = default, 
            ListMultiplayerServersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListMultiplayerServersRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListMultiplayerServersResponse>("/MultiplayerServer/ListMultiplayerServers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists quality of service servers for party.
        /// </summary>
        public static Task<ListPartyQosServersResponse> ListPartyQosServers(string Version, 
            ListPartyQosServersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListPartyQosServersRequest();
            if(Version != default)
                request.Version = Version;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListPartyQosServersResponse>("/MultiplayerServer/ListPartyQosServers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists quality of service servers.
        /// </summary>
        public static Task<ListQosServersResponse> ListQosServers(
            ListQosServersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListQosServersRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListQosServersResponse>("/MultiplayerServer/ListQosServers", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists quality of service servers.
        /// </summary>
        public static Task<ListQosServersForTitleResponse> ListQosServersForTitle(
            ListQosServersForTitleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListQosServersForTitleRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListQosServersForTitleResponse>("/MultiplayerServer/ListQosServersForTitle", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists virtual machines for a title.
        /// </summary>
        public static Task<ListVirtualMachineSummariesResponse> ListVirtualMachineSummaries(string BuildId, string Region, int? PageSize = default, string SkipToken = default, 
            ListVirtualMachineSummariesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListVirtualMachineSummariesRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(PageSize != default)
                request.PageSize = PageSize;
            if(SkipToken != default)
                request.SkipToken = SkipToken;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListVirtualMachineSummariesResponse>("/MultiplayerServer/ListVirtualMachineSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Request a multiplayer server session. Accepts tokens for title and if game client accesss is enabled, allows game client
        /// to request a server with player entity token.
        /// </summary>
        public static Task<RequestMultiplayerServerResponse> RequestMultiplayerServer(string BuildId, List<string> PreferredRegions, string SessionId, List<string> InitialPlayers = default, string SessionCookie = default, 
            RequestMultiplayerServerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RequestMultiplayerServerRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(PreferredRegions != default)
                request.PreferredRegions = PreferredRegions;
            if(SessionId != default)
                request.SessionId = SessionId;
            if(InitialPlayers != default)
                request.InitialPlayers = InitialPlayers;
            if(SessionCookie != default)
                request.SessionCookie = SessionCookie;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RequestMultiplayerServerResponse>("/MultiplayerServer/RequestMultiplayerServer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Rolls over the credentials to the container registry.
        /// </summary>
        public static Task<RolloverContainerRegistryCredentialsResponse> RolloverContainerRegistryCredentials(
            RolloverContainerRegistryCredentialsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RolloverContainerRegistryCredentialsRequest();

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<RolloverContainerRegistryCredentialsResponse>("/MultiplayerServer/RolloverContainerRegistryCredentials", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Shuts down a multiplayer server session.
        /// </summary>
        public static Task<EmptyResponse> ShutdownMultiplayerServer(string BuildId, string Region, string SessionId, 
            ShutdownMultiplayerServerRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ShutdownMultiplayerServerRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(Region != default)
                request.Region = Region;
            if(SessionId != default)
                request.SessionId = SessionId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/ShutdownMultiplayerServer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates a multiplayer server build's regions.
        /// </summary>
        public static Task<EmptyResponse> UpdateBuildRegions(string BuildId, List<BuildRegionParams> BuildRegions, 
            UpdateBuildRegionsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateBuildRegionsRequest();
            if(BuildId != default)
                request.BuildId = BuildId;
            if(BuildRegions != default)
                request.BuildRegions = BuildRegions;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/UpdateBuildRegions", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Uploads a multiplayer server game certificate.
        /// </summary>
        public static Task<EmptyResponse> UploadCertificate(Certificate GameCertificate, 
            UploadCertificateRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UploadCertificateRequest();
            if(GameCertificate != default)
                request.GameCertificate = GameCertificate;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/UploadCertificate", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
