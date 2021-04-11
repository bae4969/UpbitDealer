using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json.Linq;

namespace UpbitDealer.src
{
    public class MacroSetting
    {
        private ApiData apiData;
        private List<string> coinList = new List<string>();
        private List<string> errorList = new List<string>();
        public List<Output> executionStr = new List<Output>();

        public MacroSettingData setting = new MacroSettingData();
        public DataSet state = new DataSet();
        public DataTable order = new DataTable();

        private double holdKRW = 0;
        private Dictionary<string, double> quote = new Dictionary<string, double>();
        private DataSet[] candle = new DataSet[5];
        private DataSet[] bollinger = new DataSet[5];
        public DataSet[] indexBollinger = new DataSet[5];
        private bool[] needAvgAdd = new bool[5] { false, false, false, false, false };


        public MacroSetting(string access_key, string secret_key, List<string> coinList)
        {
            apiData = new ApiData(access_key, secret_key);
            this.coinList = coinList;

            initDefaultSetting();
            for (int i = 0; i < coinList.Count; i++)
            {
                DataTable state_table = new DataTable(coinList[i]);
                state_table.Columns.Add("uuid", typeof(string));
                state_table.Columns.Add("date", typeof(DateTime));
                state_table.Columns.Add("unit", typeof(double));
                state_table.Columns.Add("price", typeof(double));
                state_table.Columns.Add("krw", typeof(double));
                state.Tables.Add(state_table);
            }

            order.Columns.Add("coinName", typeof(string));
            order.Columns.Add("uuid", typeof(string));
            order.Columns.Add("target_uuid", typeof(string));

            for (int i = 0; i < coinList.Count; i++)
                quote.Add(coinList[i], 0);

            for (int i = 0; i < 5; i++)
            {
                candle[i] = new DataSet();
                bollinger[i] = new DataSet();
                for (int j = 0; j < coinList.Count; j++)
                {
                    DataTable dataTable = new DataTable(coinList[j]);
                    dataTable.Columns.Add("date", typeof(DateTime));
                    dataTable.Columns.Add("open", typeof(double));
                    dataTable.Columns.Add("close", typeof(double));
                    dataTable.Columns.Add("max", typeof(double));
                    dataTable.Columns.Add("min", typeof(double));
                    candle[i].Tables.Add(dataTable);

                    dataTable = new DataTable(coinList[j]);
                    dataTable.Columns.Add("date", typeof(DateTime));
                    dataTable.Columns.Add("value", typeof(double));
                    bollinger[i].Tables.Add(dataTable);
                }

                indexBollinger[i] = new DataSet();
                for (int j = 0; j < 2; j++)
                {
                    DataTable dataTable = null;
                    switch (j)
                    {
                        case 0: dataTable = new DataTable("btc"); break;
                        case 1: dataTable = new DataTable("avg"); break;
                    }
                    dataTable.Columns.Add("date", typeof(DateTime));
                    dataTable.Columns.Add("value", typeof(double));
                    indexBollinger[i].Tables.Add(dataTable);
                }
            }
        }
        public int loadFile()
        {
            string macroSettingDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroSetting.dat";
            string macroStateDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroState.dat";
            string macroOrderDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroOrder.dat";

            try
            {
                if (!System.IO.File.Exists(macroSettingDataPath)) System.IO.File.Create(macroSettingDataPath);
                else
                {
                    string[] reader = System.IO.File.ReadAllLines(macroSettingDataPath);
                    if (reader.Length > 0)
                    {
                        string[] singleData = reader[0].Split('\t');
                        if (singleData.Length > 18)
                        {
                            setting.yield = double.Parse(singleData[0]);
                            setting.krw = double.Parse(singleData[1]);
                            setting.time = double.Parse(singleData[2]);
                            setting.limit = double.Parse(singleData[3]);
                            setting.week_from = double.Parse(singleData[4]);
                            setting.week_to = double.Parse(singleData[5]);
                            setting.day_from = double.Parse(singleData[6]);
                            setting.day_to = double.Parse(singleData[7]);
                            setting.hour4_from = double.Parse(singleData[8]);
                            setting.hour4_to = double.Parse(singleData[9]);
                            setting.hour1_from = double.Parse(singleData[10]);
                            setting.hour1_to = double.Parse(singleData[11]);
                            setting.min30_from = double.Parse(singleData[12]);
                            setting.min30_to = double.Parse(singleData[13]);

                            setting.week_bias = bool.Parse(singleData[14]);
                            setting.day_bias = bool.Parse(singleData[15]);
                            setting.hour4_bias = bool.Parse(singleData[16]);
                            setting.hour1_bias = bool.Parse(singleData[17]);
                            setting.min30_bias = bool.Parse(singleData[18]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to load macro setting (" + ex.Message + ")"));
                return -1;
            }
            try
            {
                if (!System.IO.File.Exists(macroStateDataPath)) System.IO.File.Create(macroStateDataPath);
                else
                {
                    string[] reader = System.IO.File.ReadAllLines(macroStateDataPath);
                    for (int i = 0; i < reader.Length; i++)
                    {
                        string[] singleData = reader[i].Split('\t');
                        if (singleData.Length < 6) continue;
                        if (!state.Tables.Contains(singleData[0])) continue;

                        DataRow tempRow = state.Tables[singleData[0]].NewRow();
                        tempRow["uuid"] = singleData[1];
                        tempRow["date"] = DateTime.ParseExact(singleData[2], "u", null);
                        tempRow["unit"] = double.Parse(singleData[3]);
                        tempRow["price"] = double.Parse(singleData[4]);
                        tempRow["krw"] = double.Parse(singleData[5]);
                        state.Tables[singleData[0]].Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to load macro state (" + ex.Message + ")"));
                return -2;
            }
            try
            {
                if (!System.IO.File.Exists(macroOrderDataPath)) System.IO.File.Create(macroOrderDataPath);
                else
                {
                    string[] reader = System.IO.File.ReadAllLines(macroOrderDataPath);
                    for (int i = 0; i < reader.Length; i++)
                    {
                        string[] singleData = reader[i].Split('\t');
                        if (singleData.Length < 3) continue;

                        DataRow tempRow = order.NewRow();
                        tempRow["coinName"] = singleData[0];
                        tempRow["uuid"] = singleData[1];
                        tempRow["target_uuid"] = singleData[2];
                        order.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to load macro order (" + ex.Message + ")"));
                return -3;
            }

            return 0;
        }
        public int saveFile()
        {
            string macroSettingDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroSetting.dat";
            string macroStateDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroState.dat";
            string macroOrderDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroOrder.dat";

            try
            {
                if (!System.IO.File.Exists(macroSettingDataPath)) System.IO.File.Create(macroSettingDataPath);
                else
                {
                    string tempStr
                        = setting.yield.ToString("0.########") + '\t'
                        + setting.krw.ToString("0.########") + '\t'
                        + setting.time.ToString("0.########") + '\t'
                        + setting.limit.ToString("0.########") + '\t'
                        + setting.week_from.ToString("0.########") + '\t'
                        + setting.week_to.ToString("0.########") + '\t'
                        + setting.day_from.ToString("0.########") + '\t'
                        + setting.day_to.ToString("0.########") + '\t'
                        + setting.hour4_from.ToString("0.########") + '\t'
                        + setting.hour4_to.ToString("0.########") + '\t'
                        + setting.hour1_from.ToString("0.########") + '\t'
                        + setting.hour1_to.ToString("0.########") + '\t'
                        + setting.min30_from.ToString("0.########") + '\t'
                        + setting.min30_to.ToString("0.########") + '\t'
                        + setting.week_bias.ToString() + '\t'
                        + setting.day_bias.ToString() + '\t'
                        + setting.hour4_bias.ToString() + '\t'
                        + setting.hour1_bias.ToString() + '\t'
                        + setting.min30_bias.ToString();
                    System.IO.File.WriteAllText(macroSettingDataPath, tempStr + "\n");
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to save macro setting (" + ex.Message + ")"));
                return -1;
            }
            try
            {
                if (!System.IO.File.Exists(macroStateDataPath)) System.IO.File.Create(macroStateDataPath);
                if (state.Tables.Count == 0) System.IO.File.WriteAllText(macroStateDataPath, "");
                else
                {
                    List<string> savingList = new List<string>();
                    for (int i = 0; i < state.Tables.Count; i++)
                    {
                        for (int j = 0; j < state.Tables[i].Rows.Count; j++)
                        {
                            string tempStr
                                = state.Tables[i].TableName + '\t'
                                + state.Tables[i].Rows[j][0].ToString() + '\t'
                                + ((DateTime)state.Tables[i].Rows[j][1]).ToString("u") + '\t'
                                + state.Tables[i].Rows[j][2].ToString() + '\t'
                                + state.Tables[i].Rows[j][3].ToString() + '\t'
                                + state.Tables[i].Rows[j][4].ToString();
                            savingList.Add(tempStr);
                        }
                    }
                    System.IO.File.WriteAllText(macroStateDataPath, string.Join("\n", savingList) + '\n');
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to save macro state (" + ex.Message + ")"));
                return -2;
            }
            try
            {
                if (!System.IO.File.Exists(macroOrderDataPath)) System.IO.File.Create(macroOrderDataPath);
                if (order.Rows.Count == 0) System.IO.File.WriteAllText(macroOrderDataPath, "");
                else
                {
                    List<string> savingList = new List<string>();
                    for (int i = 0; i < order.Rows.Count; i++)
                    {
                        string tempStr
                            = order.Rows[i][0].ToString() + '\t'
                            + order.Rows[i][1].ToString() + '\t'
                            + order.Rows[i][2].ToString();
                        savingList.Add(tempStr);
                    }
                    System.IO.File.WriteAllText(macroOrderDataPath, string.Join("\n", savingList) + '\n');
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to save macro order (" + ex.Message + ")"));
                return -3;
            }

            return 0;
        }


        private void initDefaultSetting()
        {
            setting.yield = 1;
            setting.krw = 5000;
            setting.time = 1;
            setting.limit = 0;
            setting.week_from = -100;
            setting.week_to = 100;
            setting.day_from = -100;
            setting.day_to = 100;
            setting.hour4_from = -100;
            setting.hour4_to = 100;
            setting.hour1_from = -100;
            setting.hour1_to = 100;
            setting.min30_from = -100;
            setting.min30_to = 100;

            setting.week_bias = false;
            setting.day_bias = false;
            setting.hour4_bias = false;
            setting.hour1_bias = false;
            setting.min30_bias = false;
        }
        public int initCandleData(int index)
        {
            string coinName = coinList[index];

            for (int i = 0; i < 5; i++)
            {
                DateTime now = DateTime.Now.AddSeconds(-10);
                JArray jArray;
                switch (i)
                {
                    case 0: jArray = apiData.getCandle(coinName, ac.CANDLE_MIN30, 200); break;
                    case 1: jArray = apiData.getCandle(coinName, ac.CANDLE_HOUR1, 200); break;
                    case 2: jArray = apiData.getCandle(coinName, ac.CANDLE_HOUR4, 200); break;
                    case 3: jArray = apiData.getCandle(coinName, ac.CANDLE_DAY, 200); break;
                    case 4: jArray = apiData.getCandle(coinName, ac.CANDLE_WEEK, 200); break;
                    default: jArray = null; break;
                }
                if (jArray == null) return -10 * i;
                if (jArray.Count < 1) return -10 * i - 1;
                candle[i].Tables[coinName].Rows.Clear();
                for (int j = 0; j < jArray.Count; j++)
                {
                    DataRow dataRow = candle[i].Tables[coinName].NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[j]["candle_date_time_kst"]);
                    dataRow["open"] = (double)jArray[j]["opening_price"];
                    dataRow["close"] = (double)jArray[j]["trade_price"];
                    dataRow["max"] = (double)jArray[j]["high_price"];
                    dataRow["min"] = (double)jArray[j]["low_price"];
                    candle[i].Tables[coinName].Rows.Add(dataRow);
                }

                for (int j = 0; j < 60 && j < candle[i].Tables[coinName].Rows.Count - 28; j++)
                {
                    double averagePrice = 0;
                    double dispersion = 0;
                    double value;

                    for (int k = 0; k < 28; k++)
                        averagePrice += (double)candle[i].Tables[coinName].Rows[j + k]["close"];
                    averagePrice /= 28;
                    for (int k = 0; k < 28; k++)
                        dispersion += Math.Pow(averagePrice - (double)candle[i].Tables[coinName].Rows[j + k]["close"], 2);
                    dispersion = Math.Sqrt(dispersion / 28d);
                    value = (double)candle[i].Tables[coinName].Rows[j]["close"] - averagePrice;
                    value /= 2 * dispersion;
                    value *= 100;

                    DataRow dataRow = bollinger[i].Tables[coinName].NewRow();
                    dataRow["date"] = candle[i].Tables[coinName].Rows[j]["date"];
                    dataRow["value"] = value;
                    bollinger[i].Tables[coinName].Rows.Add(dataRow);
                }
            }

            return 0;
        }
        public void initBollingerWeightAvg()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 60 && j < bollinger[i].Tables[0].Rows.Count; j++)
                {
                    double btc
                        = ((double)bollinger[i].Tables["BTC"].Rows[j]["value"]
                        + (double)bollinger[i].Tables["ETH"].Rows[j]["value"]) * 0.5;
                    double avg = 0;
                    double count = 0;

                    for (int k = 0; k < coinList.Count; k++)
                    {
                        if (j < bollinger[i].Tables[k].Rows.Count)
                        {
                            avg += (double)bollinger[i].Tables[k].Rows[j]["value"];
                            count += 1;
                        }
                    }

                    for (int k = 0; k < 2; k++)
                    {
                        DataRow dataRow = indexBollinger[i].Tables[k].NewRow();
                        dataRow["date"] = bollinger[i].Tables[0].Rows[j]["date"];
                        switch (k)
                        {
                            case 0: dataRow["value"] = btc; break;
                            case 1: dataRow["value"] = avg / count; break;
                        }
                        indexBollinger[i].Tables[k].Rows.Add(dataRow);
                    }
                }
            }
        }


        public void saveMacroSetting(MacroSettingData setting)
        {
            this.setting = setting;
            saveFile();
        }
        public int deleteMacroState(string coinName, string uuid)
        {
            for (int i = 0; i < state.Tables[coinName].Rows.Count; i++)
            {
                if (uuid == state.Tables[coinName].Rows[i]["uuid"].ToString())
                {
                    state.Tables[coinName].Rows.RemoveAt(i);
                    saveFile();
                    return 0;
                }
            }
            return -1;
        }


        public void updateLastKrw(List<Account> account)
        {
            for (int i = 0; i < account.Count; i++)
                if (account[i].coinName == "KRW")
                {
                    holdKRW = account[i].valid;
                    break;
                }
        }
        public int updateQuote(int index, Dictionary<string, Ticker> ticker)
        {
            string coinName = coinList[index];
            if (!ticker.ContainsKey(coinName)) return -1;
            quote[coinName] = ticker[coinName].close;
            return 0;
        }
        public void updateCandle(int index)
        {
            string coinName = coinList[index];

            for (int i = 0; i < 5; i++)
            {
                DateTime now = DateTime.Now;
                DateTime last = (DateTime)candle[i].Tables[coinName].Rows[0]["date"];
                bool isAdd = false;
                switch (i)
                {
                    case 0: isAdd = (now - last).TotalMinutes > 30; break;
                    case 1: isAdd = (now - last).TotalHours > 1; break;
                    case 2: isAdd = (now - last).TotalHours > 4; break;
                    case 3: isAdd = (now - last).TotalDays > 1; break;
                    case 4: isAdd = (now - last).TotalDays > 7; break;
                }
                if (isAdd)
                {
                    DataRow dataRow = candle[i].Tables[coinName].NewRow();
                    switch (i)
                    {
                        case 0: dataRow["date"] = last.AddMinutes(30); break;
                        case 1: dataRow["date"] = last.AddHours(1); break;
                        case 2: dataRow["date"] = last.AddHours(4); break;
                        case 3: dataRow["date"] = last.AddDays(1); break;
                        case 4: dataRow["date"] = last.AddDays(7); break;
                    }
                    dataRow["open"] = quote[coinName];
                    dataRow["close"] = quote[coinName];
                    dataRow["max"] = quote[coinName];
                    dataRow["min"] = quote[coinName];
                    candle[i].Tables[coinName].Rows.InsertAt(dataRow, 0);

                    if (candle[i].Tables[coinName].Rows.Count > 200)
                        candle[i].Tables[coinName].Rows.RemoveAt(candle[i].Tables[coinName].Rows.Count - 1);
                }
                else
                {
                    candle[i].Tables[coinName].Rows[0]["close"] = quote[coinName];
                    if ((double)candle[i].Tables[coinName].Rows[0]["max"] < quote[coinName])
                        candle[i].Tables[coinName].Rows[0]["max"] = quote[coinName];
                    if ((double)candle[i].Tables[coinName].Rows[0]["min"] > quote[coinName])
                        candle[i].Tables[coinName].Rows[0]["min"] = quote[coinName];
                }


                double averagePrice = 0;
                double dispersion = 0;
                double value;

                for (int j = 0; j < 28; j++)
                    averagePrice += (double)candle[i].Tables[coinName].Rows[j]["close"];
                averagePrice /= 28;
                for (int j = 0; j < 28; j++)
                    dispersion += Math.Pow(averagePrice - (double)candle[i].Tables[coinName].Rows[j]["close"], 2);
                dispersion = Math.Sqrt(dispersion / 28d);
                value = (double)candle[i].Tables[coinName].Rows[0]["close"] - averagePrice;
                value /= 2 * dispersion;
                value *= 100;
                if (isAdd)
                {
                    DataRow dataRow = bollinger[i].Tables[coinName].NewRow();
                    dataRow["date"] = candle[i].Tables[coinName].Rows[0]["date"];
                    dataRow["value"] = value;
                    bollinger[i].Tables[coinName].Rows.InsertAt(dataRow, 0);


                    if (bollinger[i].Tables[coinName].Rows.Count > 60)
                        bollinger[i].Tables[coinName].Rows.RemoveAt(bollinger[i].Tables[coinName].Rows.Count - 1);
                }
                else
                {
                    bollinger[i].Tables[coinName].Rows[0]["date"] = candle[i].Tables[coinName].Rows[0]["date"];
                    bollinger[i].Tables[coinName].Rows[0]["value"] = value;
                }

                if (index == 0) needAvgAdd[i] = isAdd;
            }
        }
        public void updateBollingerAvg()
        {
            for (int i = 0; i < 5; i++)
            {
                double btc
                    = ((double)bollinger[i].Tables["BTC"].Rows[0]["value"]
                    + (double)bollinger[i].Tables["ETH"].Rows[0]["value"]) * 0.5;
                double avg = 0;
                double count = 0;
                for (int j = 0; j < coinList.Count; j++)
                {
                    if (bollinger[i].Tables[j].Rows.Count > 0)
                    {
                        avg += (double)bollinger[i].Tables[j].Rows[0]["value"];
                        count += 1;
                    }
                }

                if (needAvgAdd[i])
                {
                    for (int j = 0; j < 2; j++)
                    {
                        DataRow dataRow = indexBollinger[i].Tables[j].NewRow();
                        dataRow["date"] = bollinger[i].Tables[0].Rows[0]["date"];
                        switch (j)
                        {
                            case 0: dataRow["value"] = btc; break;
                            case 1: dataRow["value"] = avg / count; break;
                        }
                        indexBollinger[i].Tables[j].Rows.InsertAt(dataRow, 0);
                        if (indexBollinger[i].Tables[j].Rows.Count > 60)
                            indexBollinger[i].Tables[j].Rows.RemoveAt(indexBollinger[i].Tables[j].Rows.Count - 1);
                    }
                }
                else
                {
                    for (int j = 0; j < 2; j++)
                    {
                        indexBollinger[i].Tables[j].Rows[0]["date"] = bollinger[i].Tables[0].Rows[0]["date"];
                        switch (j)
                        {
                            case 0: indexBollinger[i].Tables[j].Rows[0]["value"] = btc; break;
                            case 1: indexBollinger[i].Tables[j].Rows[0]["value"] = avg / count; break;
                        }
                    }
                }
            }
        }


        public string[] getLowestBollinger(int top)
        {
            string[] retStr = new string[5];
            for (int i = 0; i < 5; i++)
            {
                double lowest = double.MaxValue;
                int lowestIndex = -1;
                for (int j = 0; j < top; j++)
                {
                    if (bollinger[i].Tables[j].Rows.Count > 0)
                    {
                        if (lowest > (double)bollinger[i].Tables[j].Rows[0]["value"])
                        {
                            lowest = (double)bollinger[i].Tables[j].Rows[0]["value"];
                            lowestIndex = i;
                        }
                    }
                }
                retStr[i] = coinList[lowestIndex] + "\t" + lowest.ToString("0.##");
            }
            return retStr;
        }
        private int getBollingerResult(string coinName, int dataType, int index, double targetPercent)
        {
            if (bollinger[dataType].Tables[coinName].Rows.Count < index) return 0;

            double curPercent = (double)bollinger[dataType].Tables[coinName].Rows[index]["value"];
            if (curPercent < targetPercent) return -1;
            else return 1;
        }


        public int executeCheckResult(int index)
        {
            JObject jObject = apiData.checkOrder(order.Rows[index]["uuid"].ToString());
            if (jObject == null) return -1;
            if (jObject["state"].ToString() != "done" && jObject["state"].ToString() != "cancel") return 0;

            string[] coinName = jObject["market"].ToString().Split('-');
            double unit = 0d;
            double price = 0d;
            double fee = (double)jObject["paid_fee"];

            JArray jArray = (JArray)jObject["trades"];
            for (int i = 0; i < jArray.Count; i++)
            {
                unit += (double)jArray[i]["volume"];
                price += (double)jArray[i]["price"] * (double)jArray[i]["volume"];
            }
            price /= unit;

            TradeData tradeData = new TradeData();
            tradeData.uuid = jObject["uuid"].ToString();
            tradeData.date = Convert.ToDateTime(jObject["created_at"]);
            tradeData.coinName = coinName[1];
            tradeData.isBid = jObject["side"].ToString() == "bid" ? true : false;
            tradeData.unit = unit;
            tradeData.price = price;
            tradeData.fee = fee;

            if (tradeData.isBid)
            {
                DataRow row = state.Tables[tradeData.coinName].NewRow();
                row["uuid"] = tradeData.uuid;
                row["date"] = tradeData.date;
                row["unit"] = tradeData.unit;
                row["price"] = tradeData.price;
                row["krw"] = tradeData.unit * tradeData.price;
                state.Tables[tradeData.coinName].Rows.Add(row);

                executionStr.Add(new Output(1, "Macro Execution",
                    "Buy " + tradeData.unit.ToString("0.########") + " "
                    + tradeData.coinName + " for " + (tradeData.price * tradeData.unit).ToString("0.##") + " KRW"));
            }
            else
            {
                for (int i = 0; i < state.Tables[tradeData.coinName].Rows.Count; i++)
                {
                    if (order.Rows[index]["target_uuid"].ToString() == state.Tables[tradeData.coinName].Rows[i]["uuid"].ToString())
                    {
                        double temp_price = (double)state.Tables[tradeData.coinName].Rows[i]["price"];

                        executionStr.Add(new Output(1, "Macro Execution",
                            "Sold " + tradeData.unit.ToString("0.########") + " "
                            + tradeData.coinName + " for " + (tradeData.price * tradeData.unit).ToString("0.##")
                            + " KRW (yield : " + ((tradeData.price - temp_price) * tradeData.unit - (tradeData.fee * 2f)).ToString("0.##") + " KRW)"));
                        state.Tables[tradeData.coinName].Rows.RemoveAt(i);
                        break;
                    }
                }
            }

            order.Rows.RemoveAt(index);
            return 1;
        }
        public int executeMacroBuy(int index)
        {
            string coinName = coinList[index];
            if (holdKRW - setting.limit < setting.krw * 1.0005d) return 0;
            if (state.Tables[coinName].Rows.Count >= setting.time && setting.time != 0) return 0;


            DataTable buyCandle = null;
            if (setting.min30_from > -90000d) buyCandle = candle[0].Tables[coinName];
            else if (setting.hour1_from > -90000d) buyCandle = candle[1].Tables[coinName];
            else if (setting.hour4_from > -90000d) buyCandle = candle[2].Tables[coinName];
            else if (setting.day_from > -90000d) buyCandle = candle[3].Tables[coinName];
            else if (setting.week_from > -90000d) buyCandle = candle[4].Tables[coinName];
            if (buyCandle == null)
            {
                executionStr.Add(new Output(0, "Macro Execution", "Fail to load " + coinName + " buy candle (NULL)"));
                return -1;
            }
            if (buyCandle.Rows.Count < 28)
            {
                if (errorList.Contains(coinName))
                {
                    coinList.Remove(coinName);
                    errorList.Remove(coinName);
                    executionStr.Add(new Output(0, "Macro Execution",
                        "Fail to load " + coinName + " buy candle (Not Enouph) " + coinName + " remove from macro list"));
                }
                else
                {
                    errorList.Add(coinName);
                    executionStr.Add(new Output(0, "Macro Execution",
                        "Fail to load " + coinName + " buy candle (Not Enouph) If one more error, " + coinName + " remove from macro list"));
                }
                return -1;
            }
            if (errorList.Contains(coinName)) errorList.Remove(coinName);
            if ((double)buyCandle.Rows[0]["open"] >= (double)buyCandle.Rows[0]["close"]) return 0;
            if (((double)buyCandle.Rows[0]["open"] + (double)buyCandle.Rows[0]["close"]) / 2d <
                ((double)buyCandle.Rows[1]["open"] + (double)buyCandle.Rows[1]["close"] * 3d) / 4d) return 0;


            DateTime lastBuyDate = DateTime.Now.AddYears(-1);
            for (int i = 0; i < state.Tables[coinName].Rows.Count; i++)
                if (DateTime.Compare(lastBuyDate, (DateTime)state.Tables[coinName].Rows[i]["date"]) < 0)
                    lastBuyDate = (DateTime)state.Tables[coinName].Rows[i]["date"];

            if (setting.min30_from > -90000d) { if (DateTime.Compare(DateTime.Now, lastBuyDate.AddMinutes(30)) <= 0) return 0; }
            else if (setting.hour1_from > -90000d) { if (DateTime.Compare(DateTime.Now, lastBuyDate.AddHours(1)) <= 0) return 0; }
            else if (setting.hour4_from > -90000d) { if (DateTime.Compare(DateTime.Now, lastBuyDate.AddHours(4)) <= 0) return 0; }
            else if (setting.day_from > -90000d) { if (DateTime.Compare(DateTime.Now, lastBuyDate.AddDays(1)) <= 0) return 0; }
            else if (setting.week_from > -90000d) { if (DateTime.Compare(DateTime.Now, lastBuyDate.AddDays(7)) <= 0) return 0; }


            if (setting.week_from > -90000d)
            {
                if (setting.week_bias)
                { if (getBollingerResult(coinName, 4, 0, setting.week_from + (double)indexBollinger[4].Tables[1].Rows[0]["value"]) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, 4, 0, setting.week_from) >= 0) return 0; }
            }
            if (setting.day_from > -90000d)
            {
                if (setting.day_bias)
                { if (getBollingerResult(coinName, 3, 0, setting.day_from + (double)indexBollinger[3].Tables[1].Rows[0]["value"]) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, 3, 0, setting.day_from) >= 0) return 0; }
            }
            if (setting.hour4_from > -90000d)
            {
                if (setting.hour4_bias)
                { if (getBollingerResult(coinName, 2, 0, setting.hour4_from + (double)indexBollinger[2].Tables[1].Rows[0]["value"]) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, 2, 0, setting.hour4_from) >= 0) return 0; }
            }
            if (setting.hour1_from > -90000d)
            {
                if (setting.hour1_bias)
                { if (getBollingerResult(coinName, 1, 0, setting.hour1_from + (double)indexBollinger[1].Tables[1].Rows[0]["value"]) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, 1, 0, setting.hour1_from) >= 0) return 0; }
            }
            if (setting.min30_from > -90000d)
            {
                if (setting.min30_bias)
                { if (getBollingerResult(coinName, 0, 0, setting.min30_from + (double)indexBollinger[0].Tables[1].Rows[0]["value"]) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, 0, 0, setting.min30_from) >= 0) return 0; }
            }


            Dictionary<string, string> par = new Dictionary<string, string>();
            par.Add("market", "KRW-" + coinName);
            par.Add("side", "bid");
            par.Add("price", setting.krw.ToString());
            par.Add("ord_type", "price");

            JObject jObject = apiData.order(par);
            if (jObject == null)
            {
                executionStr.Add(new Output(0, "Macro Execution", "Fail to buy " + coinName + " (NULL)"));
                return -2;
            }

            DataRow row = order.NewRow();
            row["coinName"] = coinName;
            row["uuid"] = jObject["uuid"];
            order.Rows.Add(row);

            return 1;
        }
        public int executeMacroSell(int index)
        {
            int ret = 0;
            string coinName = coinList[index];
            if (state.Tables[coinName].Rows.Count < 1) return 0;


            DataTable sellCandle = candle[0].Tables[coinName];
            if (sellCandle == null)
            {
                executionStr.Add(new Output(0, "Macro Execution", "Fail to load " + coinName + " sell candle (NULL)"));
                return -1;
            }
            if (sellCandle.Rows.Count < 28)
            {
                executionStr.Add(new Output(0, "Macro Execution", "Fail to load " + coinName + " sell candle (Not Enouph)"));
                return -1;
            }
            if ((double)sellCandle.Rows[0]["open"] <= (double)sellCandle.Rows[0]["close"]) return 0;
            if (((double)sellCandle.Rows[0]["open"] + (double)sellCandle.Rows[0]["close"]) / 2d >
                ((double)sellCandle.Rows[1]["open"] + (double)sellCandle.Rows[1]["close"] * 3d) / 4d) return 0;


            for (int i = 0; i < state.Tables[coinName].Rows.Count; i++)
            {
                double unit = (double)state.Tables[coinName].Rows[i]["unit"];
                double buyPrice = (double)state.Tables[coinName].Rows[i]["price"];
                buyPrice *= (100d + setting.yield) / 100d;
                if ((double)sellCandle.Rows[0]["close"] < buyPrice) continue;

                if (setting.week_to > -90000d)
                {
                    if (setting.week_bias)
                    { if (getBollingerResult(coinName, 4, 0, setting.week_to + (double)indexBollinger[4].Tables[1].Rows[0]["value"]) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, 4, 0, setting.week_to) <= 0) return 0; }
                }
                if (setting.day_to > -90000d)
                {
                    if (setting.day_bias)
                    { if (getBollingerResult(coinName, 3, 0, setting.day_to + (double)indexBollinger[3].Tables[1].Rows[0]["value"]) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, 3, 0, setting.day_to) <= 0) return 0; }
                }
                if (setting.hour4_to > -90000d)
                {
                    if (setting.hour4_bias)
                    { if (getBollingerResult(coinName, 2, 0, setting.hour4_to + (double)indexBollinger[2].Tables[1].Rows[0]["value"]) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, 2, 0, setting.hour4_to) <= 0) return 0; }
                }
                if (setting.hour1_to > -90000d)
                {
                    if (setting.hour1_bias)
                    { if (getBollingerResult(coinName, 1, 0, setting.hour1_to + (double)indexBollinger[1].Tables[1].Rows[0]["value"]) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, 1, 0, setting.hour1_to) <= 0) return 0; }
                }
                if (setting.min30_to > -90000d)
                {
                    if (setting.min30_bias)
                    { if (getBollingerResult(coinName, 0, 0, setting.min30_to + (double)indexBollinger[0].Tables[1].Rows[0]["value"]) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, 0, 0, setting.min30_to) <= 0) return 0; }
                }


                Dictionary<string, string> par = new Dictionary<string, string>();
                par.Add("market", "KRW-" + coinName);
                par.Add("side", "ask");
                par.Add("volume", unit.ToString("0.########"));
                par.Add("ord_type", "market");
                JObject jObject = apiData.order(par);
                if (jObject == null)
                {
                    executionStr.Add(new Output(0, "Macro Execution", "Fail to sell " + coinName + " (NULL)"));
                    return -2;
                }

                DataRow row = order.NewRow();
                row["coinName"] = coinName;
                row["uuid"] = jObject["uuid"];
                row["target_uuid"] = state.Tables[coinName].Rows[i]["uuid"];
                order.Rows.Add(row);
                ret = 1;
            }

            return ret;
        }
    }
}
