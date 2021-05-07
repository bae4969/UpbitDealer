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

        public List<Output> executionStr = new List<Output>();
        public MacroSettingData setting = new MacroSettingData();
        public DataSet state = new DataSet();
        public DataTable order = new DataTable();

        private double holdKRW = 0;
        private Dictionary<string, double> quote = new Dictionary<string, double>();
        private Dictionary<string, DateTime> lastTrade = new Dictionary<string, DateTime>();
        private DataSet[] candle = new DataSet[5];
        private DataSet lastQuote = new DataSet();

        public DataSet[] bollinger = new DataSet[5];
        private double[,] bollingerAvg = new double[5, 10];
        private double[] bollingerMax = new double[5];
        private double[] bollingerMin = new double[5];

        public DataSet[] trendLine = new DataSet[5];
        private double[,] trendLineAvg = new double[5, 10];
        private double[] trendLineMax = new double[5];
        private double[] trendLineMin = new double[5];


        public MacroSetting(string access_key, string secret_key, List<string> coinList)
        {
            apiData = new ApiData(access_key, secret_key);
            this.coinList = new List<string>(coinList);

            initDefaultSetting();

            order.Columns.Add("coinName", typeof(string));
            order.Columns.Add("uuid", typeof(string));
            order.Columns.Add("target_uuid", typeof(string));

            for (int i = 0; i < coinList.Count; i++)
            {
                DataTable dataTable = new DataTable(coinList[i]);
                dataTable.Columns.Add("uuid", typeof(string));
                dataTable.Columns.Add("date", typeof(DateTime));
                dataTable.Columns.Add("unit", typeof(double));
                dataTable.Columns.Add("price", typeof(double));
                dataTable.Columns.Add("krw", typeof(double));
                state.Tables.Add(dataTable);

                quote.Add(coinList[i], 0);
                lastTrade.Add(coinList[i], DateTime.Now.AddYears(-1));

                dataTable = new DataTable(coinList[i]);
                dataTable.Columns.Add("value", typeof(double));
                lastQuote.Tables.Add(dataTable);
            }

            for (int i = 0; i < 5; i++)
            {
                candle[i] = new DataSet();
                bollinger[i] = new DataSet();
                trendLine[i] = new DataSet();
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

                    dataTable = new DataTable(coinList[j]);
                    dataTable.Columns.Add("date", typeof(DateTime));
                    dataTable.Columns.Add("value", typeof(double));
                    trendLine[i].Tables.Add(dataTable);
                }
            }
        }
        public int loadFile()
        {
            string macroSettingDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroSetting.dat";
            string macroStateDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroState.dat";
            string macroOrderDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroOrder.dat";
            string macroLastTradeDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroLastTrade.dat";

            try
            {
                if (!System.IO.File.Exists(macroSettingDataPath)) System.IO.File.Create(macroSettingDataPath);
                else
                {
                    string[] reader = System.IO.File.ReadAllLines(macroSettingDataPath);
                    if (reader.Length > 21)
                    {
                        for (int i = 0; i < reader.Length; i++)
                        {
                            string[] singleData = reader[i].Split(':');
                            if (singleData.Length < 2) continue;

                            else if (singleData[0] == "pause_buy") setting.pauseBuy = bool.Parse(singleData[1]);
                                 
                            else if (singleData[0] == "top") setting.top = int.Parse(singleData[1]);
                            else if (singleData[0] == "yield") setting.yield = double.Parse(singleData[1]);
                            else if (singleData[0] == "krw") setting.krw = double.Parse(singleData[1]);
                            else if (singleData[0] == "time") setting.time = double.Parse(singleData[1]);
                            else if (singleData[0] == "limit") setting.limit = double.Parse(singleData[1]);
                            else if (singleData[0] == "lost_cut") setting.lostCut = double.Parse(singleData[1]);
                                 
                            else if (singleData[0] == "week_bollinger") setting.week_bb = bool.Parse(singleData[1]);
                            else if (singleData[0] == "day_bollinger") setting.day_bb = bool.Parse(singleData[1]);
                            else if (singleData[0] == "hour4_bollinger") setting.hour4_bb = bool.Parse(singleData[1]);
                            else if (singleData[0] == "hour1_bollinger") setting.hour1_bb = bool.Parse(singleData[1]);
                            else if (singleData[0] == "min30_bollinger") setting.min30_bb = bool.Parse(singleData[1]);
                                 
                            else if (singleData[0] == "week_trend_line") setting.week_tl = bool.Parse(singleData[1]);
                            else if (singleData[0] == "day_trend_line") setting.day_tl = bool.Parse(singleData[1]);
                            else if (singleData[0] == "hour4_trend_line") setting.hour4_tl = bool.Parse(singleData[1]);
                            else if (singleData[0] == "hour1_trend_line") setting.hour1_tl = bool.Parse(singleData[1]);
                            else if (singleData[0] == "min30_trend_line") setting.min30_tl = bool.Parse(singleData[1]);
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
            try
            {
                if (!System.IO.File.Exists(macroLastTradeDataPath)) System.IO.File.Create(macroLastTradeDataPath);
                else
                {
                    string[] reader = System.IO.File.ReadAllLines(macroLastTradeDataPath);
                    for (int i = 0; i < reader.Length; i++)
                    {
                        string[] singleData = reader[i].Split('\t');
                        if (singleData.Length < 2) continue;
                        if (lastTrade.ContainsKey(singleData[0]))
                            lastTrade[singleData[0]] = DateTime.ParseExact(singleData[1], "u", null);
                        else
                            lastTrade.Add(singleData[0], DateTime.ParseExact(singleData[1], "u", null));
                    }
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to load macro last trade (" + ex.Message + ")"));
                return -3;
            }

            return 0;
        }
        public int saveFile()
        {
            string macroSettingDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroSetting.dat";
            string macroStateDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroState.dat";
            string macroOrderDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroOrder.dat";
            string macroLastTradeDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroLastTrade.dat";

            try
            {
                if (!System.IO.File.Exists(macroSettingDataPath)) System.IO.File.Create(macroSettingDataPath);
                else
                {
                    string tempStr
                        = "pause_buy:" + setting.pauseBuy.ToString() + '\n'

                        + "top:" + setting.top.ToString() + '\n'
                        + "yield:" + setting.yield.ToString("0.########") + '\n'
                        + "krw:" + setting.krw.ToString("0.########") + '\n'
                        + "time:" + setting.time.ToString("0.########") + '\n'
                        + "limit:" + setting.limit.ToString("0.########") + '\n'
                        + "lost_cut:" + setting.lostCut.ToString("0.########") + '\n'

                        + "week_bollinger:" + setting.week_bb.ToString() + '\n'
                        + "day_bollinger:" + setting.day_bb.ToString() + '\n'
                        + "hour4_bollinger:" + setting.hour4_bb.ToString() + '\n'
                        + "hour1_bollinger:" + setting.hour1_bb.ToString() + '\n'
                        + "min30_bollinger:" + setting.min30_bb.ToString() + '\n'

                        + "week_trend_line:" + setting.week_tl.ToString() + '\n'
                        + "day_trend_line:" + setting.day_tl.ToString() + '\n'
                        + "hour4_trend_line:" + setting.hour4_tl.ToString() + '\n'
                        + "hour1_trend_line:" + setting.hour1_tl.ToString() + '\n'
                        + "min30_trend_line:" + setting.min30_tl.ToString();
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
            try
            {
                if (!System.IO.File.Exists(macroLastTradeDataPath)) System.IO.File.Create(macroLastTradeDataPath);
                else
                {
                    List<string> savingList = new List<string>();
                    for (int i = 0; i < coinList.Count; i++)
                    {
                        string tempStr
                            = coinList[i] + '\t'
                            + lastTrade[coinList[i]].ToString("u");
                        savingList.Add(tempStr);
                    }
                    System.IO.File.WriteAllText(macroLastTradeDataPath, string.Join("\n", savingList) + '\n');
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new Output(2, "Macro Execution", "Fail to save macro last trdae (" + ex.Message + ")"));
                return -3;
            }

            return 0;
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
        public int getListCount()
        {
            return coinList.Count;
        }


        private double function_y_value(double a, double x, double x_avg, double y_avg)
        {
            return a * (x - x_avg) + y_avg;
        }
        private double getBollinger(int type, string coinName, int row, int N)
        {
            DataTable candleData = candle[type].Tables[coinName];
            double averagePrice = 0;
            double dispersion = 0;
            double value;

            for (int k = 0; k < N; k++)
                averagePrice += (double)candleData.Rows[row + k]["close"];
            averagePrice /= N;
            for (int k = 0; k < N; k++)
                dispersion += Math.Pow(averagePrice - (double)candleData.Rows[row + k]["close"], 2);
            dispersion = Math.Sqrt(dispersion / N);
            value = (double)candleData.Rows[row]["close"] - averagePrice;
            value /= 2 * dispersion;
            return value * 100;
        }
        private double getTrendLine(int type, string coinName, int row, int N)
        {
            DataTable candleData = candle[type].Tables[coinName];
            double x_avg = 0;
            double y_avg = 0;
            double init_a;
            double init_dis;

            for (int k = 0; k < N; k++)
            {
                x_avg += ((DateTime)candleData.Rows[row + k]["date"]).ToOADate();
                y_avg += (double)candleData.Rows[row + k]["close"];
            }
            x_avg /= N;
            y_avg /= N;
            init_a = ((double)candleData.Rows[row]["close"] - (double)candleData.Rows[row + N - 1]["close"]) / N;

            init_dis = 0;
            for (int k = 0; k < N; k++)
            {
                double y_value = function_y_value(init_a, ((DateTime)candleData.Rows[row + k]["date"]).ToOADate(), x_avg, y_avg);
                init_dis += Math.Abs((double)candleData.Rows[row + k]["close"] - y_value);
            }

            while (true)
            {
                double adjust = 0;
                for (int k = 0; k < N; k++)
                {
                    double y_value = function_y_value(init_a, ((DateTime)candleData.Rows[row + k]["date"]).ToOADate(), x_avg, y_avg);
                    if (((DateTime)candleData.Rows[row + k]["date"]).ToOADate() == x_avg) continue;
                    adjust += ((double)candleData.Rows[row + k]["close"] - y_value) / (((DateTime)candleData.Rows[row + k]["date"]).ToOADate() - x_avg);
                }
                adjust /= N;


                double this_dis = 0;
                for (int k = 0; k < N; k++)
                {
                    double y_value = function_y_value(init_a + adjust, ((DateTime)candleData.Rows[row + k]["date"]).ToOADate(), x_avg, y_avg);
                    this_dis += Math.Abs((double)candleData.Rows[row + k]["close"] - y_value);
                }

                if (init_dis <= this_dis) break;
                else
                {
                    init_dis = this_dis;
                    init_a += adjust;
                }
            }

            return init_a / (double)candleData.Rows[row + 13]["close"] * 100;
        }


        private void initDefaultSetting()
        {
            setting.pauseBuy = true;

            setting.top = 60;
            setting.yield = 1;
            setting.krw = 10000;
            setting.time = 1;
            setting.limit = 0;
            setting.lostCut = 0;

            setting.week_bb = false;
            setting.day_bb = false;
            setting.hour4_bb = false;
            setting.hour1_bb = false;
            setting.min30_bb = false;

            setting.week_tl = false;
            setting.day_tl = false;
            setting.hour4_tl = false;
            setting.hour1_tl = false;
            setting.min30_tl = false;
        }
        public int initCandleData(int index)
        {
            string coinName = coinList[index];

            for (int i = 0; i < 5; i++)
            {
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
            }

            initBollinger(coinName);
            initTrendLine(coinName);

            return 0;
        }
        private void initBollinger(string coinName)
        {
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 60 && j < candle[i].Tables[coinName].Rows.Count - 27; j++)
                {
                    DataRow dataRow = bollinger[i].Tables[coinName].NewRow();
                    dataRow["date"] = candle[i].Tables[coinName].Rows[j]["date"];
                    dataRow["value"] = getBollinger(i, coinName, j, 28);
                    bollinger[i].Tables[coinName].Rows.Add(dataRow);
                }
            }
        }
        private void initTrendLine(string coinName)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 60 && j < candle[i].Tables[coinName].Rows.Count - 13; j++)
                {
                    DataRow dataRow = trendLine[i].Tables[coinName].NewRow();
                    dataRow["date"] = candle[i].Tables[coinName].Rows[j]["date"];
                    dataRow["value"] = getTrendLine(i, coinName, j, 14);
                    trendLine[i].Tables[coinName].Rows.Add(dataRow);
                }
            }
            if(trendLine[3].Tables[coinName].Rows.Count > 0)
            executionStr.Add(new Output(0, coinName, ((double)trendLine[3].Tables[coinName].Rows[0]["value"]).ToString("0.########")));
        }


        public void updateCoinList(List<string> coinList)
        {
            this.coinList = new List<string>(coinList);
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

            DataRow dataRow = lastQuote.Tables[coinName].NewRow();
            dataRow["value"] = quote[coinName];
            lastQuote.Tables[coinName].Rows.InsertAt(dataRow, 0);
            if (lastQuote.Tables[coinName].Rows.Count > 60)
                lastQuote.Tables[coinName].Rows.RemoveAt(lastQuote.Tables[coinName].Rows.Count - 1);

            return 0;
        }
        private void updateBollinger(int type, string coinName, bool isAdd)
        {
            double value = getBollinger(type, coinName, 0, 28);
            if (isAdd)
            {
                DataRow dataRow = bollinger[type].Tables[coinName].NewRow();
                dataRow["date"] = candle[type].Tables[coinName].Rows[0]["date"];
                dataRow["value"] = value;
                bollinger[type].Tables[coinName].Rows.InsertAt(dataRow, 0);

                if (bollinger[type].Tables[coinName].Rows.Count > 60)
                    bollinger[type].Tables[coinName].Rows.RemoveAt(bollinger[type].Tables[coinName].Rows.Count - 1);
            }
            else
            {
                bollinger[type].Tables[coinName].Rows[0]["date"] = candle[type].Tables[coinName].Rows[0]["date"];
                bollinger[type].Tables[coinName].Rows[0]["value"] = value;
            }
        }
        private void updateTrendLine(int type, string coinName, bool isAdd)
        {
            double value = getTrendLine(type, coinName, 0, 14);
            if (isAdd)
            {
                DataRow dataRow = trendLine[type].Tables[coinName].NewRow();
                dataRow["date"] = candle[type].Tables[coinName].Rows[0]["date"];
                dataRow["value"] = value;
                trendLine[type].Tables[coinName].Rows.InsertAt(dataRow, 0);

                if (trendLine[type].Tables[coinName].Rows.Count > 60)
                    trendLine[type].Tables[coinName].Rows.RemoveAt(trendLine[type].Tables[coinName].Rows.Count - 1);
            }
            else
            {
                trendLine[type].Tables[coinName].Rows[0]["date"] = candle[type].Tables[coinName].Rows[0]["date"];
                trendLine[type].Tables[coinName].Rows[0]["value"] = value;
            }
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

                if (candle[i].Tables[coinName].Rows.Count >= 28)
                    updateBollinger(i, coinName, isAdd);

                if (candle[i].Tables[coinName].Rows.Count >= 14)
                    updateTrendLine(i, coinName, isAdd);
            }
        }
        private double[] getAvg10MinMax(DataSet dataSet)
        {
            double[] ret = new double[12];

            bool maxCheck = false;
            bool minCheck = false;
            double max = double.MinValue;
            double min = double.MaxValue;

            for (int k = 0; k < 10; k++)
            {
                double avg = 0;
                double count = 0;

                for (int j = 0; j < coinList.Count; j++)
                    if (dataSet.Tables[j].Rows.Count > k)
                    {
                        avg += (double)dataSet.Tables[j].Rows[k]["value"];
                        count += 1;
                    }
                ret[k] = avg / count;
            }

            for (int j = 0; j < coinList.Count; j++)
                if (dataSet.Tables[j].Rows.Count > 0)
                {
                    if (min > (double)dataSet.Tables[j].Rows[0]["value"])
                    {
                        minCheck = true;
                        min = (double)dataSet.Tables[j].Rows[0]["value"];
                    }
                    if (max < (double)dataSet.Tables[j].Rows[0]["value"])
                    {
                        maxCheck = true;
                        max = (double)dataSet.Tables[j].Rows[0]["value"];
                    }
                }
            ret[10] = minCheck ? min : double.MinValue;
            ret[11] = maxCheck ? max : double.MaxValue;

            return ret;
        }
        public void updateAvg()
        {
            for (int i = 0; i < 5; i++)
            {
                double[] bb = getAvg10MinMax(bollinger[i]);
                double[] tl = getAvg10MinMax(trendLine[i]);

                for(int j = 0; j < 10; j++)
                {
                    bollingerAvg[i, j] = bb[j];
                    trendLineAvg[i, j] = tl[j];
                }
                bollingerMin[i] = bb[10];
                bollingerMax[i] = bb[11];
                trendLineMin[i] = tl[10];
                trendLineMax[i] = tl[11];
            }
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
            if (setting.pauseBuy) return 0;
            if (setting.top <= index) return 0;
            if (holdKRW - setting.limit < setting.krw * 1.0005d) return 0;

            string coinName = coinList[index];
            if (state.Tables[coinName].Rows.Count >= setting.time && setting.time != 0) return 0;

            int ret;
            if ((ret = buyDecision(index)) < 1) return ret;


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
            lastTrade[coinName] = DateTime.Now;

            return 1;
        }
        public int executeMacroSell(int index)
        {
            string coinName = coinList[index];
            if (state.Tables[coinName].Rows.Count < 1) return 0;

            int ret;
            if ((ret = sellDecision(index)) < 1) return ret;

            ret = 0;
            for (int i = 0; i < state.Tables[coinName].Rows.Count; i++)
            {
                double unit = (double)state.Tables[coinName].Rows[i]["unit"];
                double price = (double)state.Tables[coinName].Rows[i]["price"];
                if (quote[coinName] < price * (100d + setting.yield) * 0.01 &&
                    quote[coinName] > price * (setting.lostCut) * 0.01)
                        continue;

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


        private int buyDecision(int index)
        {
            string coinName = coinList[index];

            DataTable buyCandle;
            double dropRate = 100;

            if (setting.min30_bb || setting.min30_tl)
            {
                if (DateTime.Compare(DateTime.Now, lastTrade[coinName].AddMinutes(30)) <= 0) return 0;
                buyCandle = candle[0].Tables[coinName];
            }
            else if (setting.hour1_bb || setting.hour1_tl)
            {
                if (DateTime.Compare(DateTime.Now, lastTrade[coinName].AddHours(1)) <= 0) return 0;
                buyCandle = candle[1].Tables[coinName];
            }
            else if (setting.hour4_bb || setting.hour4_tl)
            {
                if (DateTime.Compare(DateTime.Now, lastTrade[coinName].AddHours(4)) <= 0) return 0;
                buyCandle = candle[2].Tables[coinName];
            }
            else if (setting.day_bb || setting.day_tl)
            {
                if (DateTime.Compare(DateTime.Now, lastTrade[coinName].AddDays(1)) <= 0) return 0;
                buyCandle = candle[3].Tables[coinName];
            }
            else if (setting.week_bb || setting.week_tl)
            {
                if (DateTime.Compare(DateTime.Now, lastTrade[coinName].AddDays(7)) <= 0) return 0;
                buyCandle = candle[4].Tables[coinName];
            }
            else return 0;

            if (buyCandle.Rows.Count < 2)
            {
                executionStr.Add(new Output(0, "Macro Execution",
                    "Fail to load " + coinName + " buy candle (Not Enouph Data)"));
                return -1;
            }
            if ((double)buyCandle.Rows[0]["open"] >= (double)buyCandle.Rows[0]["close"] ||
                (double)buyCandle.Rows[1]["open"] <= (double)buyCandle.Rows[1]["close"] * dropRate) return 0;
            if ((double)buyCandle.Rows[0]["close"] <
                ((double)buyCandle.Rows[1]["open"] + (double)buyCandle.Rows[1]["close"]) * 0.5) return 0;


            for (int i = 4; i >= 0; i--)
            {
                bool isBBMode = false;
                bool isTLMode = false;
                switch (i)
                {
                    case 4:
                        isBBMode = setting.week_bb;
                        isTLMode = setting.week_tl;
                        break;
                    case 3:
                        isBBMode = setting.day_bb;
                        isTLMode = setting.day_tl;
                        break;
                    case 2:
                        isBBMode = setting.hour4_bb;
                        isTLMode = setting.hour4_tl;
                        break;
                    case 1:
                        isBBMode = setting.hour1_bb;
                        isTLMode = setting.hour1_tl;
                        break;
                    case 0:
                        isBBMode = setting.min30_bb;
                        isTLMode = setting.min30_tl;
                        break;
                }

                if (isBBMode)
                {
                    if (bollinger[i].Tables[coinName].Rows.Count < 1) return 0;
                    if (bollingerAvg[i, 0] < bollingerAvg[i, 1]) return 0;
                    if ((double)bollinger[i].Tables[coinName].Rows[0]["value"] >
                        bollingerAvg[i, 0] - (bollingerAvg[i, 0] - bollingerMin[i]) * 0.5) return 0;
                }
                if (isTLMode)
                {
                    if (trendLine[i].Tables[coinName].Rows.Count < 1) return 0;
                    if ((double)trendLine[i].Tables[coinName].Rows[0]["value"] < 0) return 0;
                    return 0;
                }
            }

            return 1;
        }
        private int sellDecision(int index)
        {
            string coinName = coinList[index];

            DataTable sellCandle = candle[0].Tables[coinName];
            if (sellCandle.Rows.Count < 2)
            {
                executionStr.Add(new Output(0, "Macro Execution", "Fail to load " + coinName + " sell candle (Not Enouph)"));
                return -1;
            }

            if ((double)sellCandle.Rows[0]["open"] * (100d + setting.yield * 10d) * 0.01 <= (double)sellCandle.Rows[0]["close"])
                if (lastQuote.Tables[coinName].Rows.Count >= 5)
                    if ((double)lastQuote.Tables[coinName].Rows[1]["value"] > (double)lastQuote.Tables[coinName].Rows[0]["value"] &&
                        (double)lastQuote.Tables[coinName].Rows[2]["value"] > (double)lastQuote.Tables[coinName].Rows[1]["value"] &&
                        (double)lastQuote.Tables[coinName].Rows[3]["value"] > (double)lastQuote.Tables[coinName].Rows[2]["value"] &&
                        (double)lastQuote.Tables[coinName].Rows[4]["value"] > (double)lastQuote.Tables[coinName].Rows[3]["value"])
                        return 1;
            if ((double)sellCandle.Rows[0]["open"] <= (double)sellCandle.Rows[0]["close"]) return 0;
            if ((double)sellCandle.Rows[0]["close"] >
                ((double)sellCandle.Rows[1]["open"] + (double)sellCandle.Rows[1]["close"]) * 0.5) return 0;

            return 1;
        }
    }
}
