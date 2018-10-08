using Xunit;
using FizzWare.NBuilder;
using simpletradingbot.src.business.strategy;

namespace SimpleBotTest.strategy.buisness
{
    public class PredictorTest
    {

        [Theory]
        [InlineData(20, 10)]
        public void DecisionTest(float high, float low)
        {

            Assert.DoesNotMatch("SELL", new Predictor(Builder<Parameters>.CreateNew().With(x => x.Strategy = "SMA")
                                                  .With(x => x.Period = 10).With(x => x.Period_1 = 50)
                                                  .With(x => x.Limit = 10).Build()).Decision(high, low));
        }

        [Fact]
        public void TestLiveMarketPrice()
        {
            Assert.True(new Predictor(Builder<Parameters>.CreateNew().With(x => x.Strategy = "SMA").With(x => x.Coin = "BTC")
                                                  .With(x => x.Period = 10).With(x => x.Period_1 = 50)
                                                  .With(x => x.Limit = 10).Build()).LiveMarketPrice() > 0);
        }

        [Fact]
        public void TestInitialForecast()
        {
            Assert.NotNull(new Predictor(Builder<Parameters>.CreateNew().With(x => x.Strategy = "SMA").With(x => x.Coin = "BTC")
                                                  .With(x => x.Period = 10).With(x => x.Period_1 = 50)
                                                  .With(x => x.Limit = 10).Build()).InitialForecast());
        }

        [Fact]
        public void TestForeCastNextPrice()
        {
            Assert.NotNull(new Predictor(Builder<Parameters>.CreateNew().With(x => x.Strategy = "SMA").With(x => x.Coin = "BTC")
                                                  .With(x => x.Period = 10).With(x => x.Period_1 = 50)
                                                  .With(x => x.Limit = 10).Build()).InitialForecast());
        }
    }
}
