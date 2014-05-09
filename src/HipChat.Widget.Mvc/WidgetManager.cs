using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace HipChat.Widget.Mvc
{
    public static class WidgetManager
    {
        public static Widget SetupRoom(string username)
        {
            // check if it's a robot
            if (HttpContext.Current.Request.UserAgent != null 
                && Regex.IsMatch(HttpContext.Current.Request.UserAgent, "msnbot|Baiduspider|YandexBot|googlebot|slurp|SiteExplorer|java|Lipperhey|AhrefsBot|DoCoMo|rogerbot|crawler4j|EasouSpider|BeetleBot|spbot|aiHitBot|ia_archiver"))
                throw new Exception("I'm a robot");
 
            // setup hipchat client    
            var client = new HipChatApi(Settings.AuthToken);
 
            // check if at least 1 staff is online
            var notifyRoom = client.RoomsList().RoomList.FirstOrDefault(r => r.Name == Settings.NotifyRoom);
            if (notifyRoom == null)
                throw new Exception("There's no room to notify");
            notifyRoom = client.RoomsShow(notifyRoom.Id);
            var users = notifyRoom.Partcipants.UserList.Where(u => u.Status != "available").ToList();
            if (!users.Any())
                throw new Exception("We're offline");
    
            // setup room name
            var roomName = string.Format("{0} {1} {2}", Settings.RoomNamePrefix, username, new Random().Next(10000));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("hipchat_room", roomName));

            // create room
            var room = client.RoomsCreate(roomName, Settings.OwnerUserId, guestAccess: true);
            room = client.RoomsShow(room.Id);

            // notify administrators
            if (room.Partcipants.UserList == null || room.Partcipants.UserList.All(p => users.All(u => u.Id != p.Id)))
                client.RoomsMessage(notifyRoom.Id, username, string.Format("{0} wants to chat, please join room '{1}'", username, room.Name), true);

            return new Widget(room);
        }

        public static HtmlString RenderWidget()
        {
            const string resourceName = "HipChat.Widget.Mvc.widget.html";
            var assembly = Assembly.GetExecutingAssembly();

            var result = default(string);
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            result = result.Replace("{intro-message}", Settings.IntroMessage);
            result = result.Replace("{welcome-message}", Settings.WelcomeMessage);
            result = result.Replace("{offline-message}", Settings.OfflineMessage);
            return new HtmlString(result);
        }
    }
}