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
    public static partial class MultiplayerAPI
    {
        static MultiplayerAPI() {}


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

        private static PlayFabAuthenticationContext GetContext(PlayFabAuthenticationContext context) => context ?? PlayFabSettings.staticPlayer;

        /// <summary>
        /// Cancel all active tickets the player is a member of in a given queue.
        /// </summary>
        /// <param name="Entity">The entity key of the player whose tickets should be canceled. (Optional)</param>
        /// <param name="QueueName">The name of the queue from which a player's tickets should be canceled. (Required)</param>
        public static Task<CancelAllMatchmakingTicketsForPlayerResult> CancelAllMatchmakingTicketsForPlayer(string QueueName, EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CancelAllMatchmakingTicketsForPlayerRequest request = new CancelAllMatchmakingTicketsForPlayerRequest()
            {
                QueueName = QueueName,
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CancelAllMatchmakingTicketsForPlayerResult>("/Match/CancelAllMatchmakingTicketsForPlayer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Cancel a matchmaking ticket.
        /// </summary>
        /// <param name="QueueName">The name of the queue the ticket is in. (Required)</param>
        /// <param name="TicketId">The Id of the ticket to find a match for. (Required)</param>
        public static Task<CancelMatchmakingTicketResult> CancelMatchmakingTicket(string QueueName, string TicketId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CancelMatchmakingTicketRequest request = new CancelMatchmakingTicketRequest()
            {
                QueueName = QueueName,
                TicketId = TicketId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CancelMatchmakingTicketResult>("/Match/CancelMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a multiplayer server build with a custom container.
        /// </summary>
        /// <param name="BuildName">The build name. (Required)</param>
        /// <param name="ContainerFlavor">The flavor of container to create a build from. (Optional)</param>
        /// <param name="ContainerImageReference">The container reference, consisting of the image name and tag. (Optional)</param>
        /// <param name="ContainerRunCommand">The container command to run when the multiplayer server has been allocated, including any arguments. (Optional)</param>
        /// <param name="GameAssetReferences">The list of game assets related to the build. (Optional)</param>
        /// <param name="GameCertificateReferences">The game certificates for the build. (Optional)</param>
        /// <param name="Metadata">Metadata to tag the build. The keys are case insensitive. The build metadata is made available to the server through Game Server SDK (GSDK). (Optional)</param>
        /// <param name="MultiplayerServerCountPerVm">The number of multiplayer servers to host on a single VM. (Required)</param>
        /// <param name="Ports">The ports to map the build on. (Required)</param>
        /// <param name="RegionConfigurations">The region configurations for the build. (Required)</param>
        /// <param name="VmSize">The VM size to create the build on. (Optional)</param>
        public static Task<CreateBuildWithCustomContainerResponse> CreateBuildWithCustomContainer(string BuildName, int MultiplayerServerCountPerVm, List<Port> Ports, List<BuildRegionParams> RegionConfigurations, List<AssetReferenceParams> GameAssetReferences = default, ContainerFlavor? ContainerFlavor = default, Dictionary<string,string> Metadata = default, AzureVmSize? VmSize = default, ContainerImageReference ContainerImageReference = default, string ContainerRunCommand = default, List<GameCertificateReferenceParams> GameCertificateReferences = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateBuildWithCustomContainerRequest request = new CreateBuildWithCustomContainerRequest()
            {
                BuildName = BuildName,
                MultiplayerServerCountPerVm = MultiplayerServerCountPerVm,
                Ports = Ports,
                RegionConfigurations = RegionConfigurations,
                GameAssetReferences = GameAssetReferences,
                ContainerFlavor = ContainerFlavor,
                Metadata = Metadata,
                VmSize = VmSize,
                ContainerImageReference = ContainerImageReference,
                ContainerRunCommand = ContainerRunCommand,
                GameCertificateReferences = GameCertificateReferences,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateBuildWithCustomContainerResponse>("/MultiplayerServer/CreateBuildWithCustomContainer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a multiplayer server build with a managed container.
        /// </summary>
        /// <param name="BuildName">The build name. (Required)</param>
        /// <param name="ContainerFlavor">The flavor of container to create a build from. (Optional)</param>
        /// <param name="GameAssetReferences">The list of game assets related to the build. (Required)</param>
        /// <param name="GameCertificateReferences">The game certificates for the build. (Optional)</param>
        /// <param name="InstrumentationConfiguration">The instrumentation configuration for the build. (Optional)</param>
        /// <param name="Metadata">Metadata to tag the build. The keys are case insensitive. The build metadata is made available to the server through Game Server SDK (GSDK). (Optional)</param>
        /// <param name="MultiplayerServerCountPerVm">The number of multiplayer servers to host on a single VM. (Required)</param>
        /// <param name="Ports">The ports to map the build on. (Required)</param>
        /// <param name="RegionConfigurations">The region configurations for the build. (Required)</param>
        /// <param name="StartMultiplayerServerCommand">The command to run when the multiplayer server is started, including any arguments. (Required)</param>
        /// <param name="VmSize">The VM size to create the build on. (Optional)</param>
        public static Task<CreateBuildWithManagedContainerResponse> CreateBuildWithManagedContainer(string BuildName, List<AssetReferenceParams> GameAssetReferences, int MultiplayerServerCountPerVm, List<Port> Ports, List<BuildRegionParams> RegionConfigurations, string StartMultiplayerServerCommand, AzureVmSize? VmSize = default, List<GameCertificateReferenceParams> GameCertificateReferences = default, InstrumentationConfiguration InstrumentationConfiguration = default, ContainerFlavor? ContainerFlavor = default, Dictionary<string,string> Metadata = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateBuildWithManagedContainerRequest request = new CreateBuildWithManagedContainerRequest()
            {
                BuildName = BuildName,
                GameAssetReferences = GameAssetReferences,
                MultiplayerServerCountPerVm = MultiplayerServerCountPerVm,
                Ports = Ports,
                RegionConfigurations = RegionConfigurations,
                StartMultiplayerServerCommand = StartMultiplayerServerCommand,
                VmSize = VmSize,
                GameCertificateReferences = GameCertificateReferences,
                InstrumentationConfiguration = InstrumentationConfiguration,
                ContainerFlavor = ContainerFlavor,
                Metadata = Metadata,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateBuildWithManagedContainerResponse>("/MultiplayerServer/CreateBuildWithManagedContainer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create a matchmaking ticket as a client.
        /// </summary>
        /// <param name="Creator">The User who created this ticket. (Required)</param>
        /// <param name="GiveUpAfterSeconds">How long to attempt matching this ticket in seconds. (Required)</param>
        /// <param name="MembersToMatchWith">A list of Entity Keys of other users to match with. (Optional)</param>
        /// <param name="QueueName">The Id of a match queue. (Required)</param>
        public static Task<CreateMatchmakingTicketResult> CreateMatchmakingTicket(MatchmakingPlayer Creator, int GiveUpAfterSeconds, string QueueName, List<EntityKey> MembersToMatchWith = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateMatchmakingTicketRequest request = new CreateMatchmakingTicketRequest()
            {
                Creator = Creator,
                GiveUpAfterSeconds = GiveUpAfterSeconds,
                QueueName = QueueName,
                MembersToMatchWith = MembersToMatchWith,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateMatchmakingTicketResult>("/Match/CreateMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a remote user to log on to a VM for a multiplayer server build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of to create the remote user for. (Required)</param>
        /// <param name="ExpirationTime">The expiration time for the remote user created. Defaults to expiring in one day if not specified. (Optional)</param>
        /// <param name="Region">The region of virtual machine to create the remote user for. (Required)</param>
        /// <param name="Username">The username to create the remote user with. (Required)</param>
        /// <param name="VmId">The virtual machine ID the multiplayer server is located on. (Required)</param>
        public static Task<CreateRemoteUserResponse> CreateRemoteUser(string BuildId, string Region, string Username, string VmId, DateTime? ExpirationTime = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateRemoteUserRequest request = new CreateRemoteUserRequest()
            {
                BuildId = BuildId,
                Region = Region,
                Username = Username,
                VmId = VmId,
                ExpirationTime = ExpirationTime,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateRemoteUserResponse>("/MultiplayerServer/CreateRemoteUser", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Create a matchmaking ticket as a server. The matchmaking service automatically starts matching the ticket against other
        /// matchmaking tickets.
        /// </summary>
        /// <param name="GiveUpAfterSeconds">How long to attempt matching this ticket in seconds. (Required)</param>
        /// <param name="Members">The users who will be part of this ticket. (Required)</param>
        /// <param name="QueueName">The Id of a match queue. (Required)</param>
        public static Task<CreateMatchmakingTicketResult> CreateServerMatchmakingTicket(int GiveUpAfterSeconds, List<MatchmakingPlayer> Members, string QueueName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateServerMatchmakingTicketRequest request = new CreateServerMatchmakingTicketRequest()
            {
                GiveUpAfterSeconds = GiveUpAfterSeconds,
                Members = Members,
                QueueName = QueueName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateMatchmakingTicketResult>("/Match/CreateServerMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a multiplayer server game asset for a title.
        /// </summary>
        /// <param name="FileName">The filename of the asset to delete. (Required)</param>
        public static Task<EmptyResponse> DeleteAsset(string FileName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteAssetRequest request = new DeleteAssetRequest()
            {
                FileName = FileName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteAsset", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a multiplayer server build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the build to delete. (Required)</param>
        public static Task<EmptyResponse> DeleteBuild(string BuildId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteBuildRequest request = new DeleteBuildRequest()
            {
                BuildId = BuildId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteBuild", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a multiplayer server game certificate.
        /// </summary>
        /// <param name="Name">The name of the certificate. (Required)</param>
        public static Task<EmptyResponse> DeleteCertificate(string Name, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteCertificateRequest request = new DeleteCertificateRequest()
            {
                Name = Name,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteCertificate", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a remote user to log on to a VM for a multiplayer server build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer server where the remote user is to delete. (Required)</param>
        /// <param name="Region">The region of the multiplayer server where the remote user is to delete. (Required)</param>
        /// <param name="Username">The username of the remote user to delete. (Required)</param>
        /// <param name="VmId">The virtual machine ID the multiplayer server is located on. (Required)</param>
        public static Task<EmptyResponse> DeleteRemoteUser(string BuildId, string Region, string Username, string VmId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteRemoteUserRequest request = new DeleteRemoteUserRequest()
            {
                BuildId = BuildId,
                Region = Region,
                Username = Username,
                VmId = VmId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/DeleteRemoteUser", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Enables the multiplayer server feature for a title.
        /// </summary>
        public static Task<EnableMultiplayerServersForTitleResponse> EnableMultiplayerServersForTitle(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            EnableMultiplayerServersForTitleRequest request = new EnableMultiplayerServersForTitleRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EnableMultiplayerServersForTitleResponse>("/MultiplayerServer/EnableMultiplayerServersForTitle", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the URL to upload assets to.
        /// </summary>
        /// <param name="FileName">The asset's file name to get the upload URL for. (Required)</param>
        public static Task<GetAssetUploadUrlResponse> GetAssetUploadUrl(string FileName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetAssetUploadUrlRequest request = new GetAssetUploadUrlRequest()
            {
                FileName = FileName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetAssetUploadUrlResponse>("/MultiplayerServer/GetAssetUploadUrl", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a multiplayer server build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the build to get. (Required)</param>
        public static Task<GetBuildResponse> GetBuild(string BuildId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetBuildRequest request = new GetBuildRequest()
            {
                BuildId = BuildId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetBuildResponse>("/MultiplayerServer/GetBuild", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the credentials to the container registry.
        /// </summary>
        public static Task<GetContainerRegistryCredentialsResponse> GetContainerRegistryCredentials(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetContainerRegistryCredentialsRequest request = new GetContainerRegistryCredentialsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetContainerRegistryCredentialsResponse>("/MultiplayerServer/GetContainerRegistryCredentials", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get a match.
        /// </summary>
        /// <param name="EscapeObject">Determines whether the matchmaking attributes will be returned as an escaped JSON string or as an un-escaped JSON object. (Required)</param>
        /// <param name="MatchId">The Id of a match. (Required)</param>
        /// <param name="QueueName">The name of the queue to join. (Required)</param>
        /// <param name="ReturnMemberAttributes">Determines whether the matchmaking attributes for each user should be returned in the response for match request. (Required)</param>
        public static Task<GetMatchResult> GetMatch(bool EscapeObject, string MatchId, string QueueName, bool ReturnMemberAttributes, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetMatchRequest request = new GetMatchRequest()
            {
                EscapeObject = EscapeObject,
                MatchId = MatchId,
                QueueName = QueueName,
                ReturnMemberAttributes = ReturnMemberAttributes,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetMatchResult>("/Match/GetMatch", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get a matchmaking ticket by ticket Id.
        /// </summary>
        /// <param name="EscapeObject">Determines whether the matchmaking attributes will be returned as an escaped JSON string or as an un-escaped JSON object. (Required)</param>
        /// <param name="QueueName">The name of the queue to find a match for. (Required)</param>
        /// <param name="TicketId">The Id of the ticket to find a match for. (Required)</param>
        public static Task<GetMatchmakingTicketResult> GetMatchmakingTicket(bool EscapeObject, string QueueName, string TicketId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetMatchmakingTicketRequest request = new GetMatchmakingTicketRequest()
            {
                EscapeObject = EscapeObject,
                QueueName = QueueName,
                TicketId = TicketId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetMatchmakingTicketResult>("/Match/GetMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets multiplayer server session details for a build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer server to get details for. (Required)</param>
        /// <param name="Region">The region the multiplayer server is located in to get details for. (Required)</param>
        /// <param name="SessionId">The title generated guid string session ID of the multiplayer server to get details for. This is to keep track of multiplayer server sessions. (Required)</param>
        public static Task<GetMultiplayerServerDetailsResponse> GetMultiplayerServerDetails(string BuildId, string Region, string SessionId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetMultiplayerServerDetailsRequest request = new GetMultiplayerServerDetailsRequest()
            {
                BuildId = BuildId,
                Region = Region,
                SessionId = SessionId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetMultiplayerServerDetailsResponse>("/MultiplayerServer/GetMultiplayerServerDetails", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Get the statistics for a queue.
        /// </summary>
        /// <param name="QueueName">The name of the queue. (Required)</param>
        public static Task<GetQueueStatisticsResult> GetQueueStatistics(string QueueName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetQueueStatisticsRequest request = new GetQueueStatisticsRequest()
            {
                QueueName = QueueName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetQueueStatisticsResult>("/Match/GetQueueStatistics", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets a remote login endpoint to a VM that is hosting a multiplayer server build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer server to get remote login information for. (Required)</param>
        /// <param name="Region">The region of the multiplayer server to get remote login information for. (Required)</param>
        /// <param name="VmId">The virtual machine ID the multiplayer server is located on. (Required)</param>
        public static Task<GetRemoteLoginEndpointResponse> GetRemoteLoginEndpoint(string BuildId, string Region, string VmId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetRemoteLoginEndpointRequest request = new GetRemoteLoginEndpointRequest()
            {
                BuildId = BuildId,
                Region = Region,
                VmId = VmId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetRemoteLoginEndpointResponse>("/MultiplayerServer/GetRemoteLoginEndpoint", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the status of whether a title is enabled for the multiplayer server feature.
        /// </summary>
        public static Task<GetTitleEnabledForMultiplayerServersStatusResponse> GetTitleEnabledForMultiplayerServersStatus(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitleEnabledForMultiplayerServersStatusRequest request = new GetTitleEnabledForMultiplayerServersStatusRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitleEnabledForMultiplayerServersStatusResponse>("/MultiplayerServer/GetTitleEnabledForMultiplayerServersStatus", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets the quotas for a title in relation to multiplayer servers.
        /// </summary>
        public static Task<GetTitleMultiplayerServersQuotasResponse> GetTitleMultiplayerServersQuotas(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetTitleMultiplayerServersQuotasRequest request = new GetTitleMultiplayerServersQuotasRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetTitleMultiplayerServersQuotasResponse>("/MultiplayerServer/GetTitleMultiplayerServersQuotas", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Join a matchmaking ticket.
        /// </summary>
        /// <param name="Member">The User who wants to join the ticket. Their Id must be listed in PlayFabIdsToMatchWith. (Required)</param>
        /// <param name="QueueName">The name of the queue to join. (Required)</param>
        /// <param name="TicketId">The Id of the ticket to find a match for. (Required)</param>
        public static Task<JoinMatchmakingTicketResult> JoinMatchmakingTicket(MatchmakingPlayer Member, string QueueName, string TicketId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            JoinMatchmakingTicketRequest request = new JoinMatchmakingTicketRequest()
            {
                Member = Member,
                QueueName = QueueName,
                TicketId = TicketId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<JoinMatchmakingTicketResult>("/Match/JoinMatchmakingTicket", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists archived multiplayer server sessions for a build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer servers to list. (Required)</param>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="Region">The region the multiplayer servers to list. (Required)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListMultiplayerServersResponse> ListArchivedMultiplayerServers(string BuildId, string Region, int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListMultiplayerServersRequest request = new ListMultiplayerServersRequest()
            {
                BuildId = BuildId,
                Region = Region,
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListMultiplayerServersResponse>("/MultiplayerServer/ListArchivedMultiplayerServers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists multiplayer server game assets for a title.
        /// </summary>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListAssetSummariesResponse> ListAssetSummaries(int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListAssetSummariesRequest request = new ListAssetSummariesRequest()
            {
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListAssetSummariesResponse>("/MultiplayerServer/ListAssetSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists summarized details of all multiplayer server builds for a title.
        /// </summary>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListBuildSummariesResponse> ListBuildSummaries(int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListBuildSummariesRequest request = new ListBuildSummariesRequest()
            {
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListBuildSummariesResponse>("/MultiplayerServer/ListBuildSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists multiplayer server game certificates for a title.
        /// </summary>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListCertificateSummariesResponse> ListCertificateSummaries(int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListCertificateSummariesRequest request = new ListCertificateSummariesRequest()
            {
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListCertificateSummariesResponse>("/MultiplayerServer/ListCertificateSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists custom container images for a title.
        /// </summary>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListContainerImagesResponse> ListContainerImages(int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListContainerImagesRequest request = new ListContainerImagesRequest()
            {
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListContainerImagesResponse>("/MultiplayerServer/ListContainerImages", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists the tags for a custom container image.
        /// </summary>
        /// <param name="ImageName">The container images we want to list tags for. (Optional)</param>
        public static Task<ListContainerImageTagsResponse> ListContainerImageTags(string ImageName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListContainerImageTagsRequest request = new ListContainerImageTagsRequest()
            {
                ImageName = ImageName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListContainerImageTagsResponse>("/MultiplayerServer/ListContainerImageTags", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// List all matchmaking ticket Ids the user is a member of.
        /// </summary>
        /// <param name="Entity">The entity key for which to find the ticket Ids. (Optional)</param>
        /// <param name="QueueName">The name of the queue to find a match for. (Required)</param>
        public static Task<ListMatchmakingTicketsForPlayerResult> ListMatchmakingTicketsForPlayer(string QueueName, EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListMatchmakingTicketsForPlayerRequest request = new ListMatchmakingTicketsForPlayerRequest()
            {
                QueueName = QueueName,
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListMatchmakingTicketsForPlayerResult>("/Match/ListMatchmakingTicketsForPlayer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists multiplayer server sessions for a build.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer servers to list. (Required)</param>
        /// <param name="Region">The region the multiplayer servers to list. (Required)</param>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListMultiplayerServersResponse> ListMultiplayerServers(string BuildId, string Region, int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListMultiplayerServersRequest request = new ListMultiplayerServersRequest()
            {
                BuildId = BuildId,
                Region = Region,
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListMultiplayerServersResponse>("/MultiplayerServer/ListMultiplayerServers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists quality of service servers for party.
        /// </summary>
        /// <param name="Version">Qos servers version (Required)</param>
        public static Task<ListPartyQosServersResponse> ListPartyQosServers(string Version, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListPartyQosServersRequest request = new ListPartyQosServersRequest()
            {
                Version = Version,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListPartyQosServersResponse>("/MultiplayerServer/ListPartyQosServers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists quality of service servers.
        /// </summary>
        public static Task<ListQosServersResponse> ListQosServers(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListQosServersRequest request = new ListQosServersRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListQosServersResponse>("/MultiplayerServer/ListQosServers", request,
				AuthType.None,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists quality of service servers.
        /// </summary>
        public static Task<ListQosServersForTitleResponse> ListQosServersForTitle(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListQosServersForTitleRequest request = new ListQosServersForTitleRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListQosServersForTitleResponse>("/MultiplayerServer/ListQosServersForTitle", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists virtual machines for a title.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the virtual machines to list. (Required)</param>
        /// <param name="PageSize">The page size for the request. (Optional)</param>
        /// <param name="Region">The region of the virtual machines to list. (Required)</param>
        /// <param name="SkipToken">The skip token for the paged request. (Optional)</param>
        public static Task<ListVirtualMachineSummariesResponse> ListVirtualMachineSummaries(string BuildId, string Region, int? PageSize = default, string SkipToken = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListVirtualMachineSummariesRequest request = new ListVirtualMachineSummariesRequest()
            {
                BuildId = BuildId,
                Region = Region,
                PageSize = PageSize,
                SkipToken = SkipToken,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListVirtualMachineSummariesResponse>("/MultiplayerServer/ListVirtualMachineSummaries", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Request a multiplayer server session. Accepts tokens for title and if game client accesss is enabled, allows game client
        /// to request a server with player entity token.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer server to request. (Required)</param>
        /// <param name="InitialPlayers">Initial list of players (potentially matchmade) allowed to connect to the game. This list is passed to the game server when requested (via GSDK) and can be used to validate players connecting to it. (Optional)</param>
        /// <param name="PreferredRegions">The preferred regions to request a multiplayer server from. The Multiplayer Service will iterate through the regions in the specified order and allocate a server from the first one that has servers available. (Required)</param>
        /// <param name="SessionCookie">Data encoded as a string that is passed to the game server when requested. This can be used to to communicate information such as game mode or map through the request flow. (Optional)</param>
        /// <param name="SessionId">A guid string session ID created track the multiplayer server session over its life. (Required)</param>
        public static Task<RequestMultiplayerServerResponse> RequestMultiplayerServer(string BuildId, List<string> PreferredRegions, string SessionId, List<string> InitialPlayers = default, string SessionCookie = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RequestMultiplayerServerRequest request = new RequestMultiplayerServerRequest()
            {
                BuildId = BuildId,
                PreferredRegions = PreferredRegions,
                SessionId = SessionId,
                InitialPlayers = InitialPlayers,
                SessionCookie = SessionCookie,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RequestMultiplayerServerResponse>("/MultiplayerServer/RequestMultiplayerServer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Rolls over the credentials to the container registry.
        /// </summary>
        public static Task<RolloverContainerRegistryCredentialsResponse> RolloverContainerRegistryCredentials(
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RolloverContainerRegistryCredentialsRequest request = new RolloverContainerRegistryCredentialsRequest()
            {
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<RolloverContainerRegistryCredentialsResponse>("/MultiplayerServer/RolloverContainerRegistryCredentials", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Shuts down a multiplayer server session.
        /// </summary>
        /// <param name="BuildId">The guid string build ID of the multiplayer server to delete. (Required)</param>
        /// <param name="Region">The region of the multiplayer server to shut down. (Required)</param>
        /// <param name="SessionId">A guid string session ID of the multiplayer server to shut down. (Required)</param>
        public static Task<EmptyResponse> ShutdownMultiplayerServer(string BuildId, string Region, string SessionId, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ShutdownMultiplayerServerRequest request = new ShutdownMultiplayerServerRequest()
            {
                BuildId = BuildId,
                Region = Region,
                SessionId = SessionId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/ShutdownMultiplayerServer", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates a multiplayer server build's regions.
        /// </summary>
        /// <param name="BuildId">The guid string ID of the build we want to update regions for. (Required)</param>
        /// <param name="BuildRegions">The updated region configuration that should be applied to the specified build. (Required)</param>
        public static Task<EmptyResponse> UpdateBuildRegions(string BuildId, List<BuildRegionParams> BuildRegions, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateBuildRegionsRequest request = new UpdateBuildRegionsRequest()
            {
                BuildId = BuildId,
                BuildRegions = BuildRegions,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/UpdateBuildRegions", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Uploads a multiplayer server game certificate.
        /// </summary>
        /// <param name="GameCertificate">The game certificate to upload. (Required)</param>
        public static Task<EmptyResponse> UploadCertificate(Certificate GameCertificate, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UploadCertificateRequest request = new UploadCertificateRequest()
            {
                GameCertificate = GameCertificate,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/MultiplayerServer/UploadCertificate", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
