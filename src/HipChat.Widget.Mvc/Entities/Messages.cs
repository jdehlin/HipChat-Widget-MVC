﻿using System;
using System.Xml.Serialization;

namespace HipChat.Widget.Mvc.Entities
{
    /// <summary>
    /// Strongly-typed entity representing a list of HipChat messages - https://www.hipchat.com/docs/api/method/rooms/history
    /// </summary>
    [Serializable]
    [XmlRoot("messages")]
    public class Messages
    {
        /// <summary>
        /// list of messages
        /// </summary>
        [XmlElement("message")]
        public Message[] MessageList { get; set; }
    }
}
