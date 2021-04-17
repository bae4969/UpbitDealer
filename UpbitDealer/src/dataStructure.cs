using System;

namespace UpbitDealer.src
{
    public struct Output
    {
        public int level;
        public string title;
        public string str;

        public Output(int level, string title, string str)
        {
            this.level = level;
            this.title = title;
            this.str = str;
        }
    }

    public class Ticker
    {
        public string coinName;
        public double open;
        public double close;
        public double max;
        public double min;
        public double volume;
        public double prePrice;
        public double accTotal;
        public double accVolume;
        public double change;
        public double changeRate;
    }

    public class Account
    {
        public string coinName;
        public double locked;
        public double valid;

        public Account()
        {
            this.coinName = "";
            this.locked = 0;
            this.valid = 0;
        }
        public Account(string coinName, double locked, double valid)
        {
            this.coinName = coinName;
            this.locked = locked;
            this.valid = valid;
        }
    }

    public struct TradeData
    {
        public string uuid;
        public DateTime date;
        public string coinName;
        public bool isBid;
        public double unit;
        public double price;
        public double fee;
    }

    public struct MacroSettingData
    {
        public bool pause;

        public int top;
        public double yield;
        public double krw;
        public double time;
        public double limit;
        public double week;
        public double day;
        public double hour4;
        public double hour1;
        public double min30;

        public bool week_bias;
        public bool day_bias;
        public bool hour4_bias;
        public bool hour1_bias;
        public bool min30_bias;

        public bool week_auto;
        public bool day_auto;
        public bool hour4_auto;
        public bool hour1_auto;
        public bool min30_auto;
    }

    public class NameValue
    {
        public string coinName;
        public double value;


        public NameValue(NameValue nameValue)
        {
            coinName = nameValue.coinName;
            value = nameValue.value;
        }
        public NameValue(string coinName, double value)
        {
            this.coinName = coinName;
            this.value = value;
        }
    }
}
