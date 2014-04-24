using System;
using System.Xml.Serialization;

namespace HipChat.Widget.Mvc.Entities
{
    /// <summary>
    /// Strongly-typed entity representing a list of HipChat users - https://www.hipchat.com/docs/api/method/users/list
    /// </summary>
    [Serializable]
    [XmlRoot("users")]
    public class Users
    {
        /// <summary>
        /// list of users
        /// </summary>
        [XmlElement("user")]
        public User[] UserList { get; set; }
    }
}