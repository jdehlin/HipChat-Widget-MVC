using System;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;

namespace HipChat.Widget.Mvc.Entities
{
    /// <summary>
    /// Strongly-typed entity representing a HipChat room - https://www.hipchat.com/docs/api/method/rooms/list
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName="room")]
    public class Room
    {
        /// <summary>
        /// Unique identifier - used in the SendMessage API method
        /// </summary>
        [XmlElement (ElementName="room_id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the room.
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Current topic.
        /// </summary>  
        [XmlElement(ElementName = "topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Url for guest access.
        /// </summary>  
        [XmlElement(ElementName = "guest_access_url")]
        public string GuestAccessUrl { get; set; }

        /// <summary>
        /// Time of last activity (sent message) in the room in UNIX time (UTC). May be 0 in rare cases when the time is unknown.
        /// </summary>
        [XmlElement(ElementName = "last_active")]
        public string XmlLastActive
        {
            get { return LastActive.ToString("s"); }
            set
            {
                var time = new DateTime(1970, 1, 1, 0, 0, 0);
                LastActive = time.AddSeconds(int.Parse(value));
            }
        }

        /// <summary>
        /// The XMPP JID for the room.
        /// </summary>
        [XmlElement(ElementName = "xmpp_jid")]
        public string XmppJid { get; set; }

        [XmlIgnore]
        public DateTime LastActive
        {
            get;
            set;
        }

        /// <summary>
        /// User ID of the room owner.
        /// </summary>
        [XmlElement(ElementName = "owner_user_id")]
        public int Owner { get; set; }

        [XmlElement(ElementName = "participants")]
        public Participants Partcipants { get; set; }

        public Room(int id, string name, string topic, DateTime lastActive, int owner)
        {
            this.Id = id;
            this.Name = name;
            this.Topic = topic;
            this.LastActive = lastActive;
            this.Owner = owner;
        }

        public override string ToString()
        {
            return string.Format("Id:{0},Name:{1}",Id,Name);
        }

        public Room()
        {}
    }
}
