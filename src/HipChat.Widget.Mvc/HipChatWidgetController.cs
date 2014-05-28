using System;
using System.Web.Mvc;

namespace HipChat.Widget.Mvc
{
    public class HipChatWidgetController : Controller
    {
        public ActionResult Chat(string username = "Guest")
        {
            try
            {
                var widget = WidgetManager.SetupRoom(username);
                return new JsonResult
                       {
                           Data = widget,
                           JsonRequestBehavior = JsonRequestBehavior.AllowGet
                       };
            }
            catch (Exception)
            {
                return new JsonResult
                       {
                           Data = new {},
                           JsonRequestBehavior = JsonRequestBehavior.AllowGet
                       };
            }
        }
    }
}