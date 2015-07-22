
namespace PlayFab
{
	public enum PlayFabLogLevel
	{
		None    = 0,
		Debug   = 1,
		Info    = 2,
		Warning = 4,
		Error   = 8,
		All     = Debug | Info | Warning | Error,
	}

	public class PlayFabSettings
	{
		public static string ProductionEnvironmentURL = ".playfabapi.com";
		public static string LogicServerURL = null;
		public static string TitleId = null;
		public static PlayFabLogLevel LogLevel = PlayFabLogLevel.Warning | PlayFabLogLevel.Error;
		public static ErrorCallback GlobalErrorHandler  { get; set; }

		public static string GetURL()
		{
			string baseUrl = ProductionEnvironmentURL;
			if(baseUrl.StartsWith("http"))
				return baseUrl;
			return "https://"+TitleId+baseUrl;
		}
		
		public static string GetLogicURL()
		{
			return LogicServerURL;
		}
	}
}
