using Xunit;
using simpletradingbot.src.business.strategy.movingaverage;
using System.Collections.Generic;

namespace SimpleBotTest
{
    public class TextUtils_GetWordCountShould
    {
        [Fact]
        public void TestGetAverage()
        {
            Assert.Equal(4, new EMA().GetAverage(new List<float>{1.25f,6.25f,5.25f,3.25f}));
        }

        [Fact]
        public void TestPredictedPrice()
        {
            MovingAverage ma = new EMA();
            Assert.Equal(3.25,ma.PredictedPrice(new List<float> { 1.25f, 6.25f, 5.25f, 3.25f },1.34f, 1));
        }

        [Fact]
        public void TestOneTimeUpdate(){
            MovingAverage ma = new EMA();
            Assert.Equal(6672.21f, ma.OneTimeUpdate(6672.21f, 1.34f, 1));
           
        }
    }
}
