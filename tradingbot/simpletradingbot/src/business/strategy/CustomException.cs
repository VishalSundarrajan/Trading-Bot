using System;
namespace simpletradingbot.src.business.strategy
{
    public class CustomException:Exception
    {
        public CustomException(String Message):base(Message)
        {
        }
    }
}
