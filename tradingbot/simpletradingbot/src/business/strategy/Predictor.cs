using System;
using simpletradingbot.src.business.svc.rest;
using simpletradingbot.src.data.model.currentprice;
using simpletradingbot.src.data.model.historicalprice;
using simpletradingbot.src.business.strategy.movingaverage;
using System.Collections.Generic;
using simpletradingbot.src.business.strategy.model;
using FizzWare.NBuilder;

namespace simpletradingbot.src.business.strategy
{
    public class Predictor
    {
        private Parameters parameters;
        private Dictionary<String, OutParameter> coinUpdate;
        RequestController rqstController;

        public Predictor(Parameters parameters)
        {
            this.parameters  = parameters;
            coinUpdate = new Dictionary<string, OutParameter>();
            rqstController = new RequestController("https://min-api.cryptocompare.com/data/");
        }

        public string Decision(float current, float predicted){
            return (current > predicted) ? "BUY" : "SELL";
        }

        public float LiveMarketPrice(){
            CurrentPrice crntPrice;
            int attempts = 0;
            for (; ;){
                if ((crntPrice = rqstController.GetCurrentPrice(parameters.Coin)) != null)
                    return crntPrice.USD;
                else
                {
                    attempts++;
                    if (attempts > 10) 
                        throw new CustomException("Rest endpoint for live market price failed several times");
                }
            }
        }

        public OutParameter InitialForecast(){ 
            HistryData histryData;
            float avg=0, crntPrice=0, initialAvg;
            try{
                if ((histryData = rqstController.GetHistoricalData(parameters.Coin, 2000)) != null)
                {
                    if (parameters.Strategy.Equals("SMA"))
                    {
                        avg = new SMA().GetAverage(histryData.Data.ConvertAll(data => data.close).GetRange(2000 - parameters.Limit - 2, parameters.Limit));
                        crntPrice = this.LiveMarketPrice();
                    }
                    else if (parameters.Strategy.Equals("EMA"))
                    {
                        MovingAverage ma = new EMA();
                        initialAvg = ma.GetAverage(histryData.Data.ConvertAll(data => data.close).GetRange(0, 100));
                        avg = ma.PredictedPrice(histryData.Data.ConvertAll(data => data.close).GetRange(100, 1900), initialAvg, parameters.Period);
                        crntPrice = this.LiveMarketPrice();
                    }
                    else if (parameters.Strategy.Equals("EMADC"))
                    {
                        MovingAverage ma = new EMA();
                        initialAvg = ma.GetAverage(histryData.Data.ConvertAll(data => data.close).GetRange(0, 100));
                        avg = ma.PredictedPrice(histryData.Data.ConvertAll(data => data.close).GetRange(100, 1900), initialAvg, parameters.Period_1);
                        crntPrice = ma.PredictedPrice(histryData.Data.ConvertAll(data => data.close).GetRange(100, 1900), initialAvg, parameters.Period);
                    }
                    coinUpdate[parameters.Coin] = Builder<OutParameter>.CreateNew().With(x => x.Coin = parameters.Coin)
                                                                           .With(x => x.Current = crntPrice).With(x => x.Predicted = avg)
                                                                           .With(x => x.Decision = this.Decision(crntPrice, avg)).Build();
                    return coinUpdate[parameters.Coin];
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Exiting App...");
                }
            }
            catch (Exception ex){
                Console.WriteLine("Exception: " + ex);
            }
            return null;
        }

        public OutParameter ForeCastNextPrice(){
            MovingAverage ma = new EMA();
            try{
                float current = this.LiveMarketPrice();
                float avg = ma.OneTimeUpdate(current, coinUpdate[parameters.Coin].Predicted, parameters.Period);
                if ((parameters.Strategy.Equals("EMADC"))){
                    current = ma.OneTimeUpdate(current, coinUpdate[parameters.Coin].Predicted, parameters.Period_1);
                    coinUpdate[parameters.Coin] = Builder<OutParameter>.CreateNew().With(x => x.Coin = parameters.Coin)
                                                                               .With(x => x.Current = current).With(x => x.Predicted = avg)
                                                                               .With(x => x.Decision = this.Decision(avg, current)).Build();
                }
                else{
                    coinUpdate[parameters.Coin] = Builder<OutParameter>.CreateNew().With(x => x.Coin = parameters.Coin)
                                                                               .With(x => x.Current = current).With(x => x.Predicted = avg)
                                                                               .With(x => x.Decision = this.Decision(current, avg)).Build();  
                }

            }
            catch(Exception ex){
                Console.WriteLine("Exception: " + ex);
            }
            return coinUpdate[parameters.Coin];
        }
    }
}
