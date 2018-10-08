using System;
using RestSharp;
using Newtonsoft.Json;
using simpletradingbot.src.data.model.historicalprice;
using simpletradingbot.src.data.model.currentprice;

//Restcontroller
namespace simpletradingbot.src.business.svc.rest
{
    public class RequestController
    {
        private string url;
        private RestClient restClient;

        public RequestController(string url)
        {
            this.url = url;
        }

        public RestResponse GetResponse(String url)
        {
            restClient = new RestClient(url);
            return (RestResponse)restClient.Execute(new RestRequest(Method.GET));
        }

        public HistryData GetHistoricalData(String coin, int limit){
            RestResponse response = this.GetResponse(url + "histoday?fsym=" + coin + "&tsym=USD&limit=" + limit.ToString());
            if(response.IsSuccessful){
                HistryData histryData = JsonConvert.DeserializeObject<HistryData>(response.Content);
                if (histryData.response.Equals("Success"))
                {
                    System.Diagnostics.Debug.WriteLine("Successfully pulled historical data from " + url +" for"+ coin);
                    return histryData;
                }
                else{
                    System.Diagnostics.Debug.WriteLine("Request endpoint for history failed /data/...");
                }
            }
            else{
                System.Diagnostics.Debug.WriteLine("Problem connecting to "+url+" Server returned "+(int)response.StatusCode);
            }
           return null;
        }


        public CurrentPrice GetCurrentPrice(String coin){
            RestResponse response = this.GetResponse(url + "price?fsym=" + coin + "&tsyms=USD");
            if (response.IsSuccessful)
            {
                CurrentPrice curPrice = JsonConvert.DeserializeObject<CurrentPrice>(response.Content);
                if (curPrice.USD>0){
                    System.Diagnostics.Debug.WriteLine("End point " + url + " Successful");
                    return curPrice; 
                }
                else{
                    System.Diagnostics.Debug.WriteLine("Request endpoint for current price failed");
                }
            }
            else{
                System.Diagnostics.Debug.WriteLine("Problem connecting to " + url + " Server returned " + (int)response.StatusCode);
            }
            return null;
        }
    }
}
