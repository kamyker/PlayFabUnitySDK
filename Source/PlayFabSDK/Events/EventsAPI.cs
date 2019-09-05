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
    public static class EventsAPI
    {
        static EventsAPI() {}


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
        /// Write batches of entity based events to PlayStream.
        /// </summary>
        /// <param name="Events">Collection of events to write to PlayStream. (Required)</param>
        public static Task<WriteEventsResponse> WriteEvents(List<EventContents> Events, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteEventsRequest request = new WriteEventsRequest()
            {
                Events = Events,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<WriteEventsResponse>("/Event/WriteEvents", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Write batches of entity based events to as Telemetry events (bypass PlayStream).
        /// </summary>
        /// <param name="Events">Collection of events to write to PlayStream. (Required)</param>
        public static Task<WriteEventsResponse> WriteTelemetryEvents(List<EventContents> Events, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            WriteEventsRequest request = new WriteEventsRequest()
            {
                Events = Events,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<WriteEventsResponse>("/Event/WriteTelemetryEvents", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
