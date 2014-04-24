using System.Web.Mvc;

namespace HipChat.Widget.Mvc
{
    public class HipChatWidgetController : Controller
    {
        public ActionResult Index(string resource)
        {
            return Content("");
        }
    }
}