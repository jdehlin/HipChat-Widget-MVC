using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Xunit;

namespace HipChat.Widget.Mvc.UnitTests
{
    public class WidgetTests : IDisposable
    {
        public WidgetTests()
        {
            Bootstrapper.Initialize();

            // have to fake an http context to use http context cache
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
        }


        [Fact]
        public void CanSetupRoom()
        {
            var result = WidgetManager.SetupRoom("Guest");
            Assert.NotNull(result);
        }

        [Fact]
        public void CanRenderWidget()
        {
            var result = WidgetManager.RenderWidget();
            Assert.NotNull(result);
        }


        public void Dispose()
        {
            HttpContext.Current = null;
        }
    }
}
