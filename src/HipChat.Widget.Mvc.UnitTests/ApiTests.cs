using System;
using System.Linq;
using Xunit;

namespace HipChat.Widget.Mvc.UnitTests
{
    public class ApiTests : IDisposable
    {
        private HipChatApi _api;

        public ApiTests()
        {
            Bootstrapper.Initialize();
            _api = new HipChatApi(Settings.AuthToken);
        }


        [Fact]
        public void CanGetUsers()
        {
            var result = _api.UsersList();
            Assert.True(result.Any());
        }

        [Fact]
        public void CanCreateRoom()
        {
            var result = _api.RoomsCreate("Test" + new Random().Next(1000), Settings.OwnerUserId, true);
            Assert.NotNull(result);
            _api.RoomsDelete(result.Id);
        }

        [Fact]
        public void CanGetRoom()
        {
            var createResult = _api.RoomsCreate("Test" + new Random().Next(1000), Settings.OwnerUserId, true);
            var showResult = _api.RoomsShow(createResult.Id);
            Assert.NotNull(showResult);
            _api.RoomsDelete(createResult.Id);
        }


        public void Dispose()
        {
            _api = null;
        }
    }
}