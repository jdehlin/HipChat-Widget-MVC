using System.Xml.Serialization;

namespace HipChat.Widget.Mvc.Entities
{
    /// <summary>
    /// Strongly-typed entity representing a HipChat User - https://www.hipchat.com/docs/api/method/rooms/history
    /// </summary>
    public class User
    {
        /// <summary>
        /// Name of user
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// id of user
        /// </summary>
        [XmlElement(ElementName = "user_id")]
        public string Id { get; set; }

        /// <summary>
        /// status of user
        /// </summary>
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        public User(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        public User(string name, string id, string status)
        {
            this.Name = name;
            this.Id = id;
            this.Status = status;
        }

        public User() { }
    }
}
