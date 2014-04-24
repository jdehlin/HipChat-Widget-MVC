using System.Configuration;

namespace HipChat.Widget.Mvc
{
    public class Settings
    {
        public static string AuthToken
        {
            get { return ConfigurationManager.AppSettings["HipChat.Widget.Mvc.AuthToken"] ?? ""; }
        }

        public static string NotifyRoom
        {
            get { return ConfigurationManager.AppSettings["HipChat.Widget.Mvc.NotifyRoom"] ?? ""; }
        }

        public static string RoomNamePrefix
        {
            get { return ConfigurationManager.AppSettings["HipChat.Widget.Mvc.RoomNamePrefix"] ?? "Support: "; }
        }

        public static int OwnerUserId
        {
            get
            {
                var result = default(int);
                int.TryParse(ConfigurationManager.AppSettings["HipChat.Widget.Mvc.OwnerUserId"], out result);
                return result;
            }
        }

        public static string Route
        {
            get { return ConfigurationManager.AppSettings["HipChat.Widget.Mvc.Route"] ?? "HipChatWidget"; }
        }

        public static bool IgnoreDefaultRoute
        {
            get { return GetBoolValue("HipChat.Widget.Mvc.IgnoreDefaultRoute", false); }
        }

        private static bool GetBoolValue(string key, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return defaultValue;
            bool returnValue;
            if (!bool.TryParse(value, out returnValue))
                return defaultValue;
            return returnValue;
        }
    }
}