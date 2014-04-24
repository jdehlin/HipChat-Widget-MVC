using System;
using System.Xml.Serialization;

namespace HipChat.Widget.Mvc.Entities
{
    /// <summary>
    /// Strongly-typed entity representing a list of HipChat users - https://www.hipchat.com/docs/api/method/users/list
    /// </summary>
    [Serializable]
    [XmlRoot("participants")]
    public class Participants
    {
        /// <summary>
        /// list of users
        /// </summary>
        [XmlElement("participant")]
        public User[] UserList { get; set; } 
    }
}