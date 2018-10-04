using System.Collections.Generic;
namespace simpletradingbot.src.business.strategy.movingaverage
{
    public abstract class MovingAverage
    {
        public float GetAverage(List<float> lst_price)
        {
            float sum = 0;
            foreach (var item in lst_price)
            {
                sum += item;
            }
            return sum / lst_price.Count;
        }

        public virtual float PredictedPrice() { return 0; }

        public virtual float OneTimeUpdate(float currentPrice, float previousEma, int period) { return 0; }

        public virtual float PredictedPrice(List<float> lst_price, float initialPrice, int period) { return 0; }
    }
}
