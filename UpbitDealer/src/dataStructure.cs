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
        public double yield;
        public double krw;
        public double time;
        public double limit;
        public double week_from;
        public double week_to;
        public double day_from;
        public double day_to;
        public double hour4_from;
        public double hour4_to;
        public double hour1_from;
        public double hour1_to;
        public double min30_from;
        public double min30_to;

        public bool week_bias;
        public bool day_bias;
        public bool hour4_bias;
        public bool hour1_bias;
        public bool min30_bias;
    }
}
