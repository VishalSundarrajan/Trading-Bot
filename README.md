# Trading-Bot

### Description
A simple trading bot in C# [Programming Exercise]

### Requirements

1. [.NET Framework core 2.1 or above](https://www.microsoft.com/net/download)
2. [NuGet client tools](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools)

### How to test and run the bot

Go to the following directory (after cloning the repo),

`cd trading-Bot/tradingbot/simpletradingbot`

To Start the application

 `dotnet run`
 
To run Tests

 `dotnet test`
 
or you can open the application with IDE (Visual Studio) with NuGet.


### Configuration 

Modify the config.json accordingly under resources/config.json,

`{
    "strategy" : "EMA",
    "limit"  : "10",
    "period": "10",
    "period_1": "50",
    "coins": ["BTC","ETH", "LTC", "NEO"] }`
  
 #### Options:
 ##### Strategy
    i) SMA - Simple Moving Average Interface
    ii) EMA - Exponential Moving Average
    iii) EMADC - Exponential Moving Average - Double crossover
    
 ##### Limit
  Weightage given to the latest `N (limit)` days
  
 ##### Period
  
  Period for crossover EMA_1 (Should be less than crossover EMA_2).
  
 ##### Coins
  
  Number of cryptocurrencies (Can be og any length)
    
 
 
