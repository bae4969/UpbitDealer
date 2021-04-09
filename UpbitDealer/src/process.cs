using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace UpbitDealer.src
{
    public class MainUpdater
    {
        private ApiData apiData;
        private Dictionary<string, string> apiParameter = new Dictionary<string, string>();

        public List<string> coinList = new List<string>();
        public List<Account> account = new List<Account>();
        public Dictionary<string, Ticker> ticker = new Dictionary<string, Ticker>();


        public MainUpdater(string access_key, string secret_key)
        {
            apiData = new ApiData(access_key, secret_key);
            apiParameter.Add("currency", "ALL");
        }


        public List<string> setCoinList()
        {
            JArray jArray = apiData.getCoinList();
            if (jArray == null) return null;

            ticker.Clear();
            for (int i = 0; i < jArray.Count; i++)
            {
                string[] coinName = jArray[i]["market"].ToString().Split('-');
                if (coinName.Length > 1)
                    if (coinName[0] == "KRW" && !coinList.Contains(coinName[1]))
                    {
                        coinList.Add(coinName[1]);
                        ticker.Add(coinName[1], new Ticker());
                    }
            }

            return coinList;
        }


        public int update()
        {
            if (updateAccount() < 0) return -1;
            if (updateTicker() < 0) return -2;
            return 0;
        }
        private int updateAccount()
        {
            JArray jArray = apiData.getAsset();
            if (jArray == null) return -1;

            account.Clear();
            for (int i = 0; i < jArray.Count; i++)
            {
                account.Add(new Account(
                    jArray[i]["currency"].ToString(),
                    (double)jArray[i]["locked"],
                    (double)jArray[i]["balance"])
                    );
            }

            return 0;
        }
        private int updateTicker()
        {
            JArray jArray = apiData.getTicker(coinList);
            if (jArray == null) return -1;

            for (int i = 0; i < jArray.Count; i++)
            {
                string[] coinName = jArray[i]["market"].ToString().Split('-');
                ticker[coinName[1]].open = (double)jArray[i]["opening_price"];
                ticker[coinName[1]].close = (double)jArray[i]["trade_price"];
                ticker[coinName[1]].max = (double)jArray[i]["high_price"];
                ticker[coinName[1]].min = (double)jArray[i]["low_price"];
                ticker[coinName[1]].volume = (double)jArray[i]["trade_volume"];
                ticker[coinName[1]].prePrice = (double)jArray[i]["prev_closing_price"];
                ticker[coinName[1]].accTotal = (double)jArray[i]["acc_trade_price"];
                ticker[coinName[1]].accVolume = (double)jArray[i]["acc_trade_volume"];
                ticker[coinName[1]].change = (double)jArray[i]["signed_change_price"];
                ticker[coinName[1]].changeRate = (double)jArray[i]["signed_change_rate"];
            }

            return 0;
        }
    }



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
        private ApiData apiData;
        public List<Output> executionStr = new List<Output>();

        public DataTable pendingData = new DataTable();
        public DataTable historyData = new DataTable();


        public TradeHistory(string access_key, string secret_key)
        {
            apiData = new ApiData(access_key, secret_key);

            pendingData.Columns.Add("uuid", typeof(string));
            pendingData.Columns.Add("coinName", typeof(string));
            pendingData.Columns.Add("date", typeof(DateTime));
            pendingData.Columns.Add("isBid", typeof(bool));
            pendingData.Columns.Add("unit", typeof(double));
            pendingData.Columns.Add("price", typeof(double));
            pendingData.Columns.Add("fee", typeof(double));

            historyData.Columns.Add("coinName", typeof(string));
            historyData.Columns.Add("date", typeof(DateTime));
            historyData.Columns.Add("isBid", typeof(bool));
            historyData.Columns.Add("unit", typeof(double));
            historyData.Columns.Add("price", typeof(double));
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
                            tempData.coinName = tempLine[1];
                            tempData.date = DateTime.ParseExact(tempLine[2], "u", null);
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
                executionStr.Add(new Output(2, "Trade Execution", "Fail to load pending data (" + ex.Message + ")"));
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
                            + pendingData.Rows[i][1].ToString() + '\t'
                            + ((DateTime)pendingData.Rows[i][2]).ToString("u") + '\t'
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
                executionStr.Add(new Output(2, "Trade Execution", "Fail to save pending data (" + ex.Message + ")"));
                return -1;
            }

            return 0;
        }


        public void addNewPending(TradeData tradeData)
        {
            DataRow row = pendingData.NewRow();
            row["uuid"] = tradeData.uuid;
            row["coinName"] = tradeData.coinName;
            row["date"] = tradeData.date;
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
                executionStr.Add(new Output(0, "Cancel Execution",
                    "Fail to cancel pending (NULL)"));
                return -1;
            }

            executionStr.Add(new Output(0, "Cancel Execution",
                "Success to cancel pending, Remaining volume is " + jObject["remaining_volume"].ToString()));

            return 0;
        }
        public int updatePendingData(int index)
        {
            JObject retData = apiData.checkOrder(pendingData.Rows[index]["uuid"].ToString());
            if (retData == null) return -1;
            if (retData["state"].ToString() != "done" && retData["state"].ToString() != "cancel") return 0;

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
                executionStr.Add(new Output(1, "Trade Execution",
                    pendingData.Rows[index]["coinName"]
                    + " buy " + volume.ToString("0.########") + " " + pendingData.Rows[index]["coinName"]
                    + " for " + (volume * price).ToString("0.##") + " KRW (fee : " + fee.ToString("0.##") + ")"));

            else
                executionStr.Add(new Output(1, "Trade Execution",
                    pendingData.Rows[index]["coinName"]
                    + " sell " + volume.ToString("0.####") + " " + pendingData.Rows[index]["coinName"]
                    + " for " + (volume * price).ToString("0.##") + " KRW (fee : " + fee.ToString("0.##") + ")"));

            pendingData.Rows.RemoveAt(index);
            return 1;
        }


        public int getHistoryData(string coinName, int page = 1)
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
                    dataRow["coinName"] = coinName;
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["created_at"]);
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
