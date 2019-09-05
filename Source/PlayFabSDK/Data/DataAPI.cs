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
    public static class DataAPI
    {
        static DataAPI() {}


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
        /// Abort pending file uploads to an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="FileNames">Names of the files to have their pending uploads aborted. (Required)</param>
        /// <param name="ProfileVersion">The expected version of the profile, if set and doesn't match the current version of the profile the operation will not be performed. (Optional)</param>
        public static Task<AbortFileUploadsResponse> AbortFileUploads(EntityKey Entity, List<string> FileNames, int? ProfileVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AbortFileUploadsRequest request = new AbortFileUploadsRequest()
            {
                Entity = Entity,
                FileNames = FileNames,
                ProfileVersion = ProfileVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<AbortFileUploadsResponse>("/File/AbortFileUploads", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Delete files on an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="FileNames">Names of the files to be deleted. (Required)</param>
        /// <param name="ProfileVersion">The expected version of the profile, if set and doesn't match the current version of the profile the operation will not be performed. (Optional)</param>
        public static Task<DeleteFilesResponse> DeleteFiles(EntityKey Entity, List<string> FileNames, int? ProfileVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteFilesRequest request = new DeleteFilesRequest()
            {
                Entity = Entity,
                FileNames = FileNames,
                ProfileVersion = ProfileVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<DeleteFilesResponse>("/File/DeleteFiles", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Finalize file uploads to an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="FileNames">Names of the files to be finalized. Restricted to a-Z, 0-9, '(', ')', '_', '-' and '.' (Required)</param>
        public static Task<FinalizeFileUploadsResponse> FinalizeFileUploads(EntityKey Entity, List<string> FileNames, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            FinalizeFileUploadsRequest request = new FinalizeFileUploadsRequest()
            {
                Entity = Entity,
                FileNames = FileNames,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<FinalizeFileUploadsResponse>("/File/FinalizeFileUploads", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves file metadata from an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        public static Task<GetFilesResponse> GetFiles(EntityKey Entity, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetFilesRequest request = new GetFilesRequest()
            {
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetFilesResponse>("/File/GetFiles", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Retrieves objects from an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="EscapeObject">Determines whether the object will be returned as an escaped JSON string or as a un-escaped JSON object. Default is JSON object. (Optional)</param>
        public static Task<GetObjectsResponse> GetObjects(EntityKey Entity, bool? EscapeObject = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetObjectsRequest request = new GetObjectsRequest()
            {
                Entity = Entity,
                EscapeObject = EscapeObject,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetObjectsResponse>("/Object/GetObjects", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Initiates file uploads to an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="FileNames">Names of the files to be set. Restricted to a-Z, 0-9, '(', ')', '_', '-' and '.' (Required)</param>
        /// <param name="ProfileVersion">The expected version of the profile, if set and doesn't match the current version of the profile the operation will not be performed. (Optional)</param>
        public static Task<InitiateFileUploadsResponse> InitiateFileUploads(EntityKey Entity, List<string> FileNames, int? ProfileVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            InitiateFileUploadsRequest request = new InitiateFileUploadsRequest()
            {
                Entity = Entity,
                FileNames = FileNames,
                ProfileVersion = ProfileVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<InitiateFileUploadsResponse>("/File/InitiateFileUploads", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Sets objects on an entity's profile.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="ExpectedProfileVersion">Optional field used for concurrency control. By specifying the previously returned value of ProfileVersion from GetProfile API, you can ensure that the object set will only be performed if the profile has not been updated by any other clients since the version you last loaded. (Optional)</param>
        /// <param name="Objects">Collection of objects to set on the profile. (Required)</param>
        public static Task<SetObjectsResponse> SetObjects(EntityKey Entity, List<SetObject> Objects, int? ExpectedProfileVersion = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            SetObjectsRequest request = new SetObjectsRequest()
            {
                Entity = Entity,
                Objects = Objects,
                ExpectedProfileVersion = ExpectedProfileVersion,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<SetObjectsResponse>("/Object/SetObjects", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
