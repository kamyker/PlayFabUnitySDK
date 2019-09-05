#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.EventsModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// Write custom PlayStream and Telemetry events for any PlayFab entity. Telemetry events can be used for analytic,
    /// reporting, or debugging. PlayStream events can do all of that and also trigger custom actions in near real-time.
    /// </summary>
    public static class PlayFabEventsAPI
    {
        static PlayFabEventsAPI() {}


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
        /// Write batches of entity based events to PlayStream.
        /// </summary>
        public static Task<WriteEventsResponse> WriteEvents(List<EventContents> Events, 
            WriteEventsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new WriteEventsRequest();
            if(Events != default)
                request.Events = Events;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<WriteEventsResponse>("/Event/WriteEvents", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Write batches of entity based events to as Telemetry events (bypass PlayStream).
        /// </summary>
        public static Task<WriteEventsResponse> WriteTelemetryEvents(List<EventContents> Events, 
            WriteEventsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new WriteEventsRequest();
            if(Events != default)
                request.Events = Events;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<WriteEventsResponse>("/Event/WriteTelemetryEvents", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
