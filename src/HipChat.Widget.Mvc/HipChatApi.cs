using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using HipChat.Widget.Mvc.Entities;

namespace HipChat.Widget.Mvc
{
    public class HipChatApi
    {
        private readonly string _authToken;

        public HipChatApi(string authToken)
        {
            _authToken = authToken;
        }


        public IEnumerable<User> UsersList()
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(FormatUri("users", "list"));
            var result = CallApi(request);
            var xmlSerializer = new XmlSerializer(typeof(Users));
            var theUsers = xmlSerializer.Deserialize(new StringReader(result)) as Users;
            return new List<User>(theUsers.UserList);
        }

        public Rooms RoomsList()
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(FormatUri("rooms", "list"));
            var result = CallApi(request);
            var xmlSerializer = new XmlSerializer(typeof(Rooms));
            var theRooms = xmlSerializer.Deserialize(new StringReader(result)) as Rooms;
            return theRooms;
        }

        public Room RoomsShow(int roomId)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(FormatUri("rooms", "show") + "&room_id=" + roomId);
            var result = CallApi(request);
            var xmlSerializer = new XmlSerializer(typeof(Room));
            var theRoom = xmlSerializer.Deserialize(new StringReader(result)) as Room;
            return theRoom;
        } 

        public Room RoomsCreate(string name, int ownerUserId, bool guestAccess = false)
        {
            var postData = new StringBuilder();
            postData.Append("name=" + HttpUtility.UrlEncode(name) + "&");
            postData.Append("owner_user_id=" + ownerUserId + "&");
            postData.Append("guest_access=" + (guestAccess ? 1 : 0) + "&");
            var bytedata = Encoding.UTF8.GetBytes(postData.ToString());
            
            var request = (HttpWebRequest)WebRequest.Create(FormatUri("rooms", "create"));
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.ContentLength = bytedata.Length;

            var requestStream = request.GetRequestStream();
            
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            var result = CallApi(request);
            var xmlSerializer = new XmlSerializer(typeof(Room));
            var theRoom = xmlSerializer.Deserialize(new StringReader(result)) as Room;
            return theRoom;
        }

        public bool RoomsDelete(int roomId)
        {
            var postData = new StringBuilder();
            postData.Append("room_id=" + roomId + "&");
            var bytedata = Encoding.UTF8.GetBytes(postData.ToString());

            var request = (HttpWebRequest)WebRequest.Create(FormatUri("rooms", "delete"));
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.ContentLength = bytedata.Length;

            var requestStream = request.GetRequestStream();

            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            CallApi(request);
            return true;
        }

        public bool RoomsMessage(int roomId, string from, string message, bool notify = false)
        {
            var postData = new StringBuilder();
            postData.Append("room_id=" + roomId + "&");
            postData.Append("from=" + HttpUtility.UrlEncode(from) + "&");
            postData.Append("message=" + HttpUtility.UrlEncode(message) + "&");
            postData.Append("notify=" + (notify ? 1 : 0));
            var bytedata = Encoding.UTF8.GetBytes(postData.ToString());

            var request = (HttpWebRequest)WebRequest.Create(FormatUri("rooms", "message"));
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.ContentLength = bytedata.Length;

            var requestStream = request.GetRequestStream();

            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            CallApi(request);
            return true;
        }


        private string FormatUri(string controller, string action)
        {
            return string.Format("https://api.hipchat.com/v1/{0}/{1}?format={2}&auth_token={3}",
                controller,
                action,
                "xml",
                _authToken);
        }
        
        /// <summary>
        /// Calls the API, returns the contents of the response as a string
        /// </summary>
        /// <param name="request">An HTTP request object. This is not just a URL, as the request might be a POST, not a GET.</param>
        /// <returns>The raw response body - as JSON / XML</returns>
        private static string CallApi(HttpWebRequest request)
        {
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                // in the case of a HipChat exception, the reason is in the response body, so need to extract this.
                using (var receiveStream = response.GetResponseStream())
                {
                    var encode = Encoding.GetEncoding("utf-8");
                    using (var readStream = new StreamReader(receiveStream, encode))
                    {
                        return readStream.ReadToEnd();
                    }
                }
            }
        }
    }
}