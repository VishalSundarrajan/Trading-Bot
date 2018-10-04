using System.Collections.Generic;
namespace simpletradingbot.src.business.strategy.movingaverage
{
    public class EMA : MovingAverage
    {

        public override float PredictedPrice (List<float> lst_price, float initialPrice, int period)
        {
            float smoothingConstant = ((float)2 / ((float)period + 1)), ema = initialPrice;
            foreach (var item in lst_price)
            {
                 ema = ((item - ema) * smoothingConstant) + ema;
            }
            return ema;
        }

        public override float OneTimeUpdate(float currentPrice, float previousEma, int period) {
            return ((currentPrice - previousEma) * (float)2 / ((float)period + 1)) + previousEma;
        }
    }
}
