using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpbitDealer.src
{
    public class CoinType
    {
        public List<string> Bit = new List<string>{
            "BTC", "BCH", "BTG", "LTC"
        };
        public List<string> Eth = new List<string>{
            "ETH", "ETC", "ZRX", "SNT"
        };
        public List<string> Xrp = new List<string>{
            "XRP", "XLM"
        };
        public List<string> Platform = new List<string>{
            "EOS", "LSK", "ARK", "WAVES", "IOTA", "IOST", "XEM", "XTZ"
        };
        public List<string> Util = new List<string>{
            "STEEM", "OMG", "SC", "GLM", "POWR", "CVC", "REP", "WAXP", "LINK"
        };
        public List<string> Pay = new List<string>{
            "CRO", "PUNDIX"
        };
        public List<string> Kor = new List<string>{
            "ICX", "MED"
        };
        public List<string> Chi = new List<string>{
            "NEO", "QTUM", "TRON", "ELF", "VET"
        };
        public List<string> Sea = new List<string>{
            "ZIL", "KNC"
        };
    }
}
