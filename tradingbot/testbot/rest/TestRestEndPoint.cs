﻿//@Vishal 
using System;
using Xunit;
using RestSharp;
using simpletradingbot.src.business.svc.rest;
namespace SimpleBotTest.rest
{
    public class TestRestEndPoint
    {
        [Theory]
        [InlineData("price?fsym=BTC&tsyms=USD")]
        public void TestGetResponse(String url)
        {
            RequestController rc = new RequestController("https://min-api.cryptocompare.com/data/");
            RestResponse rstRes=rc.GetResponse("https://min-api.cryptocompare.com/data/" + url);
            Assert.True(rstRes.IsSuccessful);
        }

        [Theory]
        [InlineData("LTC")]
        [InlineData("BTC")]
        [InlineData("ETH")]
        [InlineData("SXC")]
        public void TestGetCurrentPrice(String coin){
            RequestController rc = new RequestController("https://min-api.cryptocompare.com/data/");
            Assert.NotNull(rc.GetCurrentPrice(coin));
        }

        [Theory]
        [InlineData("LTC",10)]
        [InlineData("BTC",20)]
        [InlineData("ETH",24)]
        [InlineData("SXC",14)]
        public void TestGetHistoricalData(String coin, int limit)
        {
            RequestController rc = new RequestController("https://min-api.cryptocompare.com/data/");
            Assert.NotNull(rc.GetHistoricalData(coin,limit));
        }
    }
}
