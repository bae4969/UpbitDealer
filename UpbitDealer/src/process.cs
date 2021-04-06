using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace UpbitDealer.src
{
    public struct output
    {
        public int level;
        public string title;
        public string str;

        public output(int level, string title, string str)
        {
            this.level = level;
            this.title = title;
            this.str = str;
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



    // for 'main' form infomation
    public class MainUpdater
    {
        private ApiData apiData;
        private Dictionary<string, string> apiParameter = new Dictionary<string, string>();

        public DataTable holdList = new DataTable();


        public MainUpdater(string access_key, string secret_key)
        {
            apiData = new ApiData(access_key, secret_key);
            apiParameter.Add("currency", "ALL");

            holdList.Columns.Add("name", typeof(string));
            holdList.Columns.Add("total", typeof(double));
            holdList.Columns.Add("locked", typeof(double));
            holdList.Columns.Add("balance", typeof(double));
            holdList.Columns.Add("last", typeof(double));
        }


        public List<string> getUpdateList()
        {
            List<string> retVal = new List<string>();

            JArray retData = apiData.getCoinList();
            if (retData == null) return retVal;

            for (int i = 0; i < retData.Count; i++)
            {
                string[] temp = retData[i]["market"].ToString().Split('-');
                if (temp.Length > 1)
                    if (temp[0] == "KRW" && !retVal.Contains(temp[1]))
                        retVal.Add(temp[1]);
            }

            return retVal;
        }


        public int update()
        {
            return updateHoldList();
        }
        private int updateHoldList()
        {
            List<string> tickerList = new List<string>();
            JArray retData = apiData.getAsset();
            if (retData == null) return -1;

            holdList.Rows.Clear();
            for (int i = 0; i < retData.Count; i++)
            {
                DataRow dataRow = holdList.NewRow();
                dataRow["name"] = retData[i]["currency"];
                dataRow["locked"] = retData[i]["locked"];
                dataRow["balance"] = (double)retData[i]["locked"] + (double)retData[i]["balance"];
                if (retData[i]["currency"].ToString() == "KRW")
                {
                    dataRow["last"] = 1d;
                    dataRow["total"] = dataRow["balance"];
                }
                else
                    tickerList.Add(retData[i]["currency"].ToString());
                holdList.Rows.Add(dataRow);
            }

            if (tickerList.Count < 1) return 0;
            retData = apiData.getTicker(tickerList);
            if (retData == null) return -2;

            for (int i = 0; i < retData.Count; i++)
            {
                string[] temp = retData[i]["market"].ToString().Split('-');
                if (temp.Length < 2) continue;
                for (int j = 0; j < holdList.Rows.Count; j++)
                {
                    if (holdList.Rows[j]["name"].ToString() == temp[1])
                    {
                        holdList.Rows[j]["last"] = retData[i]["trade_price"];
                        holdList.Rows[j]["total"] = (double)holdList.Rows[j]["last"] * ((double)holdList.Rows[j]["locked"] + (double)holdList.Rows[j]["balance"]);
                        break;
                    }
                }
            }

            return 0;
        }
    }



    // for 'trader' form trade and result
    public class React
    {
        private ApiData apiData;

        private Dictionary<string, string> parTrans = new Dictionary<string, string>();
        private Dictionary<string, string> parOrderBook = new Dictionary<string, string>();
        private Dictionary<string, string> parBalance = new Dictionary<string, string>();


        public React(string access_key, string secret_key)
        {
            apiData = new ApiData(access_key, secret_key);

            parTrans.Add("count", "5");
            parOrderBook.Add("group_orders", "0");
            parBalance.Add("currency", "");
        }


        public double[] getTickerData(string coinName)
        {
            JArray retData = apiData.getTicker(coinName);
            if (retData == null) return null;
            if (retData.Count < 1) return null;

            double[] retTickerData = new double[11];
            retTickerData[0] = (double)retData[0]["opening_price"];
            retTickerData[1] = (double)retData[0]["trade_price"];
            retTickerData[2] = (double)retData[0]["low_price"];
            retTickerData[3] = (double)retData[0]["high_price"];
            retTickerData[4] = (double)retData[0]["trade_volume"];
            retTickerData[5] = (double)retData[0]["acc_trade_price"];
            retTickerData[6] = (double)retData[0]["prev_closing_price"];
            retTickerData[7] = (double)retData[0]["acc_trade_volume_24h"];
            retTickerData[8] = (double)retData[0]["acc_trade_price_24h"];
            retTickerData[9] = (double)retData[0]["signed_change_price"];
            retTickerData[10] = (double)retData[0]["signed_change_rate"];

            return retTickerData;
        }
        public JArray getTransactionData(string coinName)
        {
            JArray retData = apiData.getTrans(coinName, 5);
            if (retData == null) return null;
            if (retData.Count < 5) return null;

            return retData;
        }
        public List<double>[] getOrderBookData(string coinName)
        {
            JArray retData = apiData.getOrderBook(coinName);
            if (retData == null) return null;
            retData = (JArray)retData[0]["orderbook_units"];
            if (retData.Count < 15) return null;

            List<double>[] retVal = new List<double>[4];
            for (int i = 0; i < 4; i++)
                retVal[i] = new List<double>();
            for (int i = 0; i < retData.Count; i++)
            {
                retVal[0].Add((double)retData[i]["ask_price"]);
                retVal[1].Add((double)retData[i]["bid_price"]);
                retVal[2].Add((double)retData[i]["ask_size"]);
                retVal[3].Add((double)retData[i]["bid_size"]);
            }

            return retVal;
        }
        public double[] getBalanceData(string coinName)
        {
            JObject retData = apiData.getOrdersChance(coinName);
            if (retData == null) return null;

            double[] retBalanceData = new double[] { 0d, 0d, 0d, 0d, 0d, 0d, 0d };
            retBalanceData[1] = (double)retData["bid_account"]["locked"];
            retBalanceData[2] = (double)retData["bid_account"]["balance"];
            retBalanceData[0] = retBalanceData[1] + retBalanceData[2];
            retBalanceData[4] = (double)retData["ask_account"]["locked"];
            retBalanceData[5] = (double)retData["ask_account"]["balance"];
            retBalanceData[3] = retBalanceData[4] + retBalanceData[5];

            return retBalanceData;
        }


        public JObject executeDeal
                (bool isBuy, bool isPlace, string coinName, double units, double price, double total)
        {
            Dictionary<string, string> par = new Dictionary<string, string>();
            par.Add("market", "KRW-" + coinName);

            if (isPlace)
            {
                if (isBuy)
                {
                    par.Add("side", "bid");
                    par.Add("volume", units.ToString());
                    par.Add("price", price.ToString());
                    par.Add("ord_type", "limit");
                    return apiData.order(par);
                }
                else
                {
                    par.Add("side", "ask");
                    par.Add("volume", units.ToString());
                    par.Add("price", price.ToString());
                    par.Add("ord_type", "limit");
                    return apiData.order(par);
                }
            }
            else
            {
                if (isBuy)
                {
                    par.Add("side", "bid");
                    par.Add("price", total.ToString());
                    par.Add("ord_type", "price");
                    return apiData.order(par);
                }
                else
                {
                    par.Add("side", "ask");
                    par.Add("volume", units.ToString());
                    par.Add("ord_type", "market");
                    return apiData.order(par);
                }
            }
        }
    }



    public class TradeHistory
    {
        private CultureInfo provider = CultureInfo.InvariantCulture;
        private ApiData apiData;

        public DataTable pendingData = new DataTable();
        public DataTable historyData = new DataTable();

        public List<output> executionStr = new List<output>();


        public TradeHistory(string access_key, string secret_key)
        {
            apiData = new ApiData(access_key, secret_key);

            pendingData.Columns.Add("uuid", typeof(string));
            pendingData.Columns.Add("date", typeof(DateTime));
            pendingData.Columns.Add("coinName", typeof(string));
            pendingData.Columns.Add("isBid", typeof(bool));
            pendingData.Columns.Add("unit", typeof(double));
            pendingData.Columns.Add("price", typeof(int));
            pendingData.Columns.Add("fee", typeof(double));

            historyData.Columns.Add("date", typeof(DateTime));
            historyData.Columns.Add("coinName", typeof(string));
            historyData.Columns.Add("isBid", typeof(bool));
            historyData.Columns.Add("unit", typeof(double));
            historyData.Columns.Add("price", typeof(int));
            historyData.Columns.Add("fee", typeof(double));
        }
        public int loadFile()
        {
            string tradePath = System.IO.Directory.GetCurrentDirectory() + "/pending.dat";
            try
            {
                if (System.IO.File.Exists(tradePath))
                {
                    string[] reader = System.IO.File.ReadAllLines(tradePath);
                    if (reader.Length > 0)
                        for (int i = 0; i < reader.Length; i++)
                        {
                            string[] tempLine = reader[i].Split('\t');
                            TradeData tempData = new TradeData();
                            tempData.uuid = tempLine[0];
                            tempData.date = DateTime.ParseExact(tempLine[1], "u", provider);
                            tempData.coinName = tempLine[2];
                            tempData.isBid = bool.Parse(tempLine[3]);
                            tempData.unit = double.Parse(tempLine[4]);
                            tempData.price = double.Parse(tempLine[5]);
                            tempData.fee = tempLine[6] == "" ? 0 : double.Parse(tempLine[6]);
                            addNewPending(tempData);
                        }
                }
                else System.IO.File.Create(tradePath);
            }
            catch (Exception ex)
            {
                executionStr.Add(new output(2, "Trade Execution", "Fail to load pending data (" + ex.Message + ")"));
                return -1;
            }

            return 0;
        }
        public int saveFile()
        {
            string tradePath = System.IO.Directory.GetCurrentDirectory() + "/pending.dat";
            try
            {
                if (!System.IO.File.Exists(tradePath)) System.IO.File.Create(tradePath);
                if (pendingData.Rows.Count == 0) System.IO.File.WriteAllText(tradePath, "");
                else
                {
                    List<string> savingList = new List<string>();
                    for (int i = 0; i < pendingData.Rows.Count; i++)
                    {
                        string temp
                            = pendingData.Rows[i][0].ToString() + '\t'
                            + ((DateTime)pendingData.Rows[i][1]).ToString("u") + '\t'
                            + pendingData.Rows[i][2].ToString() + '\t'
                            + pendingData.Rows[i][3].ToString() + '\t'
                            + pendingData.Rows[i][4].ToString() + '\t'
                            + pendingData.Rows[i][5].ToString() + '\t'
                            + pendingData.Rows[i][6].ToString();
                        savingList.Add(temp);
                    }
                    System.IO.File.WriteAllText(tradePath, string.Join("\n", savingList) + "\n");
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new output(2, "Trade Execution", "Fail to save pending data (" + ex.Message + ")"));
                return -1;
            }

            return 0;
        }


        public void addNewPending(TradeData tradeData)
        {
            DataRow row = pendingData.NewRow();
            row["uuid"] = tradeData.uuid;
            row["date"] = tradeData.date;
            row["coinName"] = tradeData.coinName;
            row["isBid"] = tradeData.isBid;
            row["unit"] = tradeData.unit;
            row["price"] = tradeData.price;
            pendingData.Rows.Add(row);
        }


        public int cancelPending(string uuid)
        {
            JObject jObject = apiData.cancelOrder(uuid);
            if (jObject == null)
            {
                executionStr.Add(new output(0, "Cancel Execution",
                    "Fail to cancel pending (NULL)"));
                return -1;
            }

            executionStr.Add(new output(0, "Cancel Execution",
                "Success to cancel pending, Remaining volume is " + jObject["remaining_volume"].ToString()));

            return 0;
        }
        public int updateSinglePendingData(int index)
        {
            JObject retData = apiData.checkOrder(pendingData.Rows[index]["uuid"].ToString());
            if (retData == null) return -1;
            if (retData["state"].ToString() != "done" && retData["state"].ToString() != "cancel") return 0;

            string[] coinName = retData["market"].ToString().Split('-');
            double volume = 0d;
            double price = 0d;
            double fee = (double)retData["paid_fee"];

            JArray trades = (JArray)retData["trades"];
            for (int i = 0; i < trades.Count; i++)
            {
                volume += (double)trades[i]["volume"];
                price += (double)trades[i]["price"] * (double)trades[i]["volume"];
            }
            price /= volume;

            if ((bool)pendingData.Rows[index]["isBid"])
                executionStr.Add(new output(1, "Trade Execution",
                    pendingData.Rows[index]["coinName"]
                    + " buy " + volume.ToString("0.########") + " " + pendingData.Rows[index]["coinName"]
                    + " for " + (volume * price).ToString("0.##") + " KRW"));

            else
                executionStr.Add(new output(1, "Trade Execution",
                    pendingData.Rows[index]["coinName"]
                    + " sell (" + volume.ToString("0.####") + " " + pendingData.Rows[index]["coinName"]
                    + " for " + (volume * price).ToString("0.##") + " KRW"));

            pendingData.Rows.RemoveAt(index);
            return 1;
        }


        public int updateHistoryData(string coinName, int page = 1)
        {
            JArray jArray = apiData.getDoneCancelOrder(coinName, page);
            if (jArray == null) return -1;

            historyData.Rows.Clear();
            for (int i = 0; i < jArray.Count; i++)
            {
                if (jArray[i]["state"].ToString() == "done" || jArray[i]["state"].ToString() == "cancel")
                {
                    double buf = 0;
                    DataRow dataRow = historyData.NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["created_at"]);
                    dataRow["coinName"] = coinName;
                    dataRow["isBid"] = jArray[i]["side"].ToString() == "bid" ? true : false;
                    dataRow["unit"] = double.TryParse(jArray[i]["volume"].ToString(), out buf) ? buf : 0;
                    dataRow["price"] = double.TryParse(jArray[i]["price"].ToString(), out buf) ? buf : 0;
                    dataRow["fee"] = double.TryParse(jArray[i]["paid_fee"].ToString(), out buf) ? buf : 0;
                    historyData.Rows.Add(dataRow);
                }
            }

            return 1;
        }
    }
}
