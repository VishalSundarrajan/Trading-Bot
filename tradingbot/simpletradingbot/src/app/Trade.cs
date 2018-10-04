using System;
using System.IO;
using System.Threading;
using FizzWare.NBuilder;
using Newtonsoft.Json.Linq;
using simpletradingbot.src.business.strategy;
using simpletradingbot.src.business.strategy.model;

namespace simpletradingbot.src.app
{
    public class Trade
<<<<<<< HEAD
    {
        //main controller
        public void Ccntroller()
=======
    {
        public void Controller()
>>>>>>> 9924249880efbc682f1b00159d95acc0bff47af9
        {
            dynamic jParsed;
            using (StreamReader r = new StreamReader("./src/resources/config.json"))
            {
                jParsed = JObject.Parse(r.ReadToEnd());
            }

            var parameters = Builder<Parameters>.CreateNew().With(x => x.Strategy = jParsed.strategy)
                                                  .With(x => x.Period = jParsed.period).With(x => x.Period_1 = jParsed.period_1)
                                                  .With(x => x.Limit = jParsed.limit).Build();
            Predictor predictor = new Predictor(parameters);
            Console.WriteLine("Coin\tCurrent\tPredictedClose\tBUY/SELL\tTime");
            foreach (var item in jParsed.coins)
            {
                parameters.Coin = item;
                OutParameter outParameter = predictor.InitialForecast();
                Console.WriteLine(outParameter.Coin+"\t"+outParameter.Current+ "\t"+ outParameter.Predicted
                                  +"\t"+ outParameter.Decision+"\t"+ DateTime.Now.ToString());
            }


            for (; ; ){
                Thread.Sleep(10000);
                foreach (var item in jParsed.coins)
                {
                    parameters.Coin = item;
                    OutParameter outParameter = predictor.ForeCastNextPrice();
                    Console.WriteLine(outParameter.Coin + "\t" + outParameter.Current + "\t" + outParameter.Predicted
                                      + "\t" + outParameter.Decision+"\t" + DateTime.Now.ToString());
                }
            }
           

        }
    }
}
