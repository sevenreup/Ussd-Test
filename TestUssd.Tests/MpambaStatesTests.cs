using TestUssd.States.Mpamba;
using Xunit;
using StackExchange.Redis;
using Moq;
using TestUssd.Core;

namespace TestUssd.Tests
{
    public class MpambaStatesTests
    {
        [Fact]
        public void StartScreen()
        {
            var mockMultiplexer = new Mock<IConnectionMultiplexer>();
            mockMultiplexer.Setup(_ => _.IsConnected).Returns(false);
            var mockDatabase = new Mock<IDatabase>();
            mockMultiplexer
                .Setup(_ => _.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(mockDatabase.Object);
            var cacheHandler = new RedisStore(mockMultiplexer.Object);
            var context = new UssdContext(cacheHandler, nameof(MpambaStartState));

            var startState = new MpambaStartState()
            {
                Context = context
            };

            startState.HandleState();
        }
    }
}
