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
        public bool pauseBuy;

        public int top;
        public double yield;
        public double krw;
        public double time;
        public double limit;
        public double lostCut;

        public bool week_bb;
        public bool day_bb;
        public bool hour4_bb;
        public bool hour1_bb;
        public bool min30_bb;

        public bool week_tl;
        public bool day_tl;
        public bool hour4_tl;
        public bool hour1_tl;
        public bool min30_tl;
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
