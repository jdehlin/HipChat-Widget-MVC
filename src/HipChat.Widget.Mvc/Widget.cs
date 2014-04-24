using HipChat.Widget.Mvc.Entities;

namespace HipChat.Widget.Mvc
{
    public class Widget
    {
        public Widget(Room room)
        {
            Room = room;
        }

        public Room Room { get; private set; }
    }
}