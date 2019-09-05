#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.DataModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// Store arbitrary data associated with an entity. Objects are small (~1KB) JSON-compatible objects which are stored
    /// directly on the entity profile. Objects are made available for use in other PlayFab contexts, such as PlayStream events
    /// and CloudScript functions. Files can efficiently store data of any size or format. Both objects and files support a
    /// flexible permissions system to control read and write access by other entities.
    /// </summary>
    public static class PlayFabDataAPI
    {
        static PlayFabDataAPI() {}


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
        /// Abort pending file uploads to an entity's profile.
        /// </summary>
        public static Task<AbortFileUploadsResponse> AbortFileUploads(EntityKey Entity, List<string> FileNames, int? ProfileVersion = default, 
            AbortFileUploadsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AbortFileUploadsRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(FileNames != default)
                request.FileNames = FileNames;
            if(ProfileVersion != default)
                request.ProfileVersion = ProfileVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<AbortFileUploadsResponse>("/File/AbortFileUploads", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Delete files on an entity's profile.
        /// </summary>
        public static Task<DeleteFilesResponse> DeleteFiles(EntityKey Entity, List<string> FileNames, int? ProfileVersion = default, 
            DeleteFilesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteFilesRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(FileNames != default)
                request.FileNames = FileNames;
            if(ProfileVersion != default)
                request.ProfileVersion = ProfileVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<DeleteFilesResponse>("/File/DeleteFiles", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Finalize file uploads to an entity's profile.
        /// </summary>
        public static Task<FinalizeFileUploadsResponse> FinalizeFileUploads(EntityKey Entity, List<string> FileNames, 
            FinalizeFileUploadsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new FinalizeFileUploadsRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(FileNames != default)
                request.FileNames = FileNames;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<FinalizeFileUploadsResponse>("/File/FinalizeFileUploads", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves file metadata from an entity's profile.
        /// </summary>
        public static Task<GetFilesResponse> GetFiles(EntityKey Entity, 
            GetFilesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetFilesRequest();
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetFilesResponse>("/File/GetFiles", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves objects from an entity's profile.
        /// </summary>
        public static Task<GetObjectsResponse> GetObjects(EntityKey Entity, bool? EscapeObject = default, 
            GetObjectsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetObjectsRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(EscapeObject != default)
                request.EscapeObject = EscapeObject;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetObjectsResponse>("/Object/GetObjects", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Initiates file uploads to an entity's profile.
        /// </summary>
        public static Task<InitiateFileUploadsResponse> InitiateFileUploads(EntityKey Entity, List<string> FileNames, int? ProfileVersion = default, 
            InitiateFileUploadsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new InitiateFileUploadsRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(FileNames != default)
                request.FileNames = FileNames;
            if(ProfileVersion != default)
                request.ProfileVersion = ProfileVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<InitiateFileUploadsResponse>("/File/InitiateFileUploads", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets objects on an entity's profile.
        /// </summary>
        public static Task<SetObjectsResponse> SetObjects(EntityKey Entity, List<SetObject> Objects, int? ExpectedProfileVersion = default, 
            SetObjectsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new SetObjectsRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Objects != default)
                request.Objects = Objects;
            if(ExpectedProfileVersion != default)
                request.ExpectedProfileVersion = ExpectedProfileVersion;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<SetObjectsResponse>("/Object/SetObjects", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
