using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace UpbitDealer.src
{
    public struct MacroSettingData
    {
        public double yield;
        public double krw;
        public double time;
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
    public class BollingerAverage
    {
        public double total = 0;
        public double count = 0;
        public double avg = 0;


        public void addTotal(double val, double weight)
        {
            total += (val * weight);
            count += weight;
        }
        public double setAverage()
        {
            avg = count < 1 ? 0 : total / count;
            total = 0;
            count = 0;
            return avg;
        }
    }



    public class MacroSetting
    {
        private CultureInfo provider = CultureInfo.InvariantCulture;
        private ApiData apiData;

        public MacroSettingData setting = new MacroSettingData();
        public DataSet state = new DataSet();
        public DataTable order = new DataTable();

        private List<string> coinList = new List<string>();
        private List<string> errorList = new List<string>();
        public List<output> executionStr = new List<output>();

        private double holdKRW = 0;
        private DataTable lastCandleUpdate = new DataTable();
        private DataSet min30_candle = new DataSet();
        private DataSet hour1_candle = new DataSet();
        private DataSet hour4_candle = new DataSet();
        private DataSet day_candle = new DataSet();
        private DataSet week_candle = new DataSet();

        public List<BollingerAverage> ba0 = new List<BollingerAverage>();
        public List<BollingerAverage> ba1 = new List<BollingerAverage>();
        public List<BollingerAverage> ba2 = new List<BollingerAverage>();

        public List<BollingerAverage> bb0 = new List<BollingerAverage>();
        public List<BollingerAverage> bb1 = new List<BollingerAverage>();
        public List<BollingerAverage> bb2 = new List<BollingerAverage>();



        public MacroSetting(string access_key, string secret_key, List<string> coinList)
        {
            apiData = new ApiData(access_key, secret_key);

            lastCandleUpdate.Columns.Add("week", typeof(DateTime));
            lastCandleUpdate.Columns.Add("day", typeof(DateTime));
            lastCandleUpdate.Columns.Add("hour4", typeof(DateTime));
            lastCandleUpdate.Columns.Add("hour1", typeof(DateTime));
            lastCandleUpdate.Columns.Add("min30", typeof(DateTime));

            order.Columns.Add("coinName", typeof(string));
            order.Columns.Add("uuid", typeof(string));
            order.Columns.Add("target_uuid", typeof(string));

            for (int i = 0; i < 5; i++)
            {
                ba0.Add(new BollingerAverage());
                ba1.Add(new BollingerAverage());
                ba2.Add(new BollingerAverage());
                bb0.Add(new BollingerAverage());
                bb1.Add(new BollingerAverage());
                bb2.Add(new BollingerAverage());
            }

            this.coinList = coinList;
            for (int i = 0; i < coinList.Count; i++)
            {
                DataRow dataRow = lastCandleUpdate.NewRow();
                dataRow["week"] = DateTime.Now.AddMonths(-1);
                dataRow["day"] = DateTime.Now.AddMonths(-1);
                dataRow["hour4"] = DateTime.Now.AddMonths(-1);
                dataRow["hour1"] = DateTime.Now.AddMonths(-1);
                dataRow["min30"] = DateTime.Now.AddMonths(-1);
                lastCandleUpdate.Rows.Add(dataRow);

                DataTable week_candle_single = new DataTable(coinList[i]);
                week_candle_single.Columns.Add("date", typeof(DateTime));
                week_candle_single.Columns.Add("open", typeof(double));
                week_candle_single.Columns.Add("close", typeof(double));
                week_candle_single.Columns.Add("max", typeof(double));
                week_candle_single.Columns.Add("min", typeof(double));
                week_candle_single.Columns.Add("volume", typeof(double));
                week_candle.Tables.Add(week_candle_single);

                DataTable day_candle_single = new DataTable(coinList[i]);
                day_candle_single.Columns.Add("date", typeof(DateTime));
                day_candle_single.Columns.Add("open", typeof(double));
                day_candle_single.Columns.Add("close", typeof(double));
                day_candle_single.Columns.Add("max", typeof(double));
                day_candle_single.Columns.Add("min", typeof(double));
                day_candle_single.Columns.Add("volume", typeof(double));
                day_candle.Tables.Add(day_candle_single);

                DataTable hour4_candle_single = new DataTable(coinList[i]);
                hour4_candle_single.Columns.Add("date", typeof(DateTime));
                hour4_candle_single.Columns.Add("open", typeof(double));
                hour4_candle_single.Columns.Add("close", typeof(double));
                hour4_candle_single.Columns.Add("max", typeof(double));
                hour4_candle_single.Columns.Add("min", typeof(double));
                hour4_candle_single.Columns.Add("volume", typeof(double));
                hour4_candle.Tables.Add(hour4_candle_single);

                DataTable hour1_candle_single = new DataTable(coinList[i]);
                hour1_candle_single.Columns.Add("date", typeof(DateTime));
                hour1_candle_single.Columns.Add("open", typeof(double));
                hour1_candle_single.Columns.Add("close", typeof(double));
                hour1_candle_single.Columns.Add("max", typeof(double));
                hour1_candle_single.Columns.Add("min", typeof(double));
                hour1_candle_single.Columns.Add("volume", typeof(double));
                hour1_candle.Tables.Add(hour1_candle_single);

                DataTable min30_candle_single = new DataTable(coinList[i]);
                min30_candle_single.Columns.Add("date", typeof(DateTime));
                min30_candle_single.Columns.Add("open", typeof(double));
                min30_candle_single.Columns.Add("close", typeof(double));
                min30_candle_single.Columns.Add("max", typeof(double));
                min30_candle_single.Columns.Add("min", typeof(double));
                min30_candle_single.Columns.Add("volume", typeof(double));
                min30_candle.Tables.Add(min30_candle_single);


                DataTable state_table = new DataTable(coinList[i]);
                state_table.Columns.Add("uuid", typeof(string));
                state_table.Columns.Add("date", typeof(DateTime));
                state_table.Columns.Add("unit", typeof(double));
                state_table.Columns.Add("price", typeof(double));
                state_table.Columns.Add("krw", typeof(double));
                state.Tables.Add(state_table);
            }
        }
        public int loadFile()
        {
            string macroSettingDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroSetting.dat";
            string macroStateDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroState.dat";
            string macroOrderDataPath = System.IO.Directory.GetCurrentDirectory() + "/macroOrder.dat";

            try
            {
                if (!System.IO.File.Exists(macroSettingDataPath))
                {
                    setDefaultSetting();
                    System.IO.File.Create(macroSettingDataPath);
                }
                else
                {
                    string[] reader = System.IO.File.ReadAllLines(macroSettingDataPath);
                    if (reader.Length < 1) setDefaultSetting();
                    else
                    {
                        string[] singleData = reader[0].Split('\t');
                        if (singleData.Length < 18) setDefaultSetting();
                        else
                        {
                            setting.yield = double.Parse(singleData[0]);
                            setting.krw = double.Parse(singleData[1]);
                            setting.time = double.Parse(singleData[2]);
                            setting.week_from = double.Parse(singleData[3]);
                            setting.week_to = double.Parse(singleData[4]);
                            setting.day_from = double.Parse(singleData[5]);
                            setting.day_to = double.Parse(singleData[6]);
                            setting.hour4_from = double.Parse(singleData[7]);
                            setting.hour4_to = double.Parse(singleData[8]);
                            setting.hour1_from = double.Parse(singleData[9]);
                            setting.hour1_to = double.Parse(singleData[10]);
                            setting.min30_from = double.Parse(singleData[11]);
                            setting.min30_to = double.Parse(singleData[12]);

                            setting.week_bias = bool.Parse(singleData[13]);
                            setting.day_bias = bool.Parse(singleData[14]);
                            setting.hour4_bias = bool.Parse(singleData[15]);
                            setting.hour1_bias = bool.Parse(singleData[16]);
                            setting.min30_bias = bool.Parse(singleData[17]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new output(2, "Macro Execution", "Fail to load macro setting (" + ex.Message + ")"));
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
                        tempRow["date"] = DateTime.ParseExact(singleData[2], "u", provider);
                        tempRow["unit"] = double.Parse(singleData[3]);
                        tempRow["price"] = double.Parse(singleData[4]);
                        tempRow["krw"] = double.Parse(singleData[5]);
                        state.Tables[singleData[0]].Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                executionStr.Add(new output(2, "Macro Execution", "Fail to load macro state (" + ex.Message + ")"));
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
                executionStr.Add(new output(2, "Macro Execution", "Fail to load macro order (" + ex.Message + ")"));
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
                executionStr.Add(new output(2, "Macro Execution", "Fail to save macro setting (" + ex.Message + ")"));
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
                executionStr.Add(new output(2, "Macro Execution", "Fail to save macro state (" + ex.Message + ")"));
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
                executionStr.Add(new output(2, "Macro Execution", "Fail to save macro order (" + ex.Message + ")"));
                return -3;
            }

            return 0;
        }
        public void saveMacroSetting(MacroSettingData setting)
        {
            this.setting = setting;
            saveFile();
        }


        private void setDefaultSetting()
        {
            setting.yield = 1;
            setting.krw = 5000;
            setting.time = 1;
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
        public int setLastKrw()
        {
            JArray retData = apiData.getAsset();
            if (retData == null) return -1;

            for (int i = 0; i < retData.Count; i++)
            {
                if (retData[i]["currency"].ToString() == "KRW")
                {
                    holdKRW = (double)retData[i]["balance"];
                    return 0;
                }
            }

            return -2;
        }
        public int setCandleData(int index)
        {
            string coinName = coinList[index];
            DateTime last;
            DateTime now = DateTime.Now.AddSeconds(-10);

            last = (DateTime)lastCandleUpdate.Rows[index]["min30"];
            if (min30_candle.Tables[coinName].Rows.Count < 1 || now.Minute % 30 < last.Minute % 30)
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_MIN30, 200);
                if (jArray == null) return -10;
                if (jArray.Count < min30_candle.Tables[coinName].Rows.Count) return -11;

                min30_candle.Tables[coinName].Rows.Clear();
                for (int i = 0; i < jArray.Count; i++)
                {
                    DataRow dataRow = min30_candle.Tables[coinName].NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["candle_date_time_kst"]);
                    dataRow["open"] = (double)jArray[i]["opening_price"];
                    dataRow["close"] = (double)jArray[i]["trade_price"];
                    dataRow["max"] = (double)jArray[i]["high_price"];
                    dataRow["min"] = (double)jArray[i]["low_price"];
                    dataRow["volume"] = (double)jArray[i]["candle_acc_trade_volume"];
                    min30_candle.Tables[coinName].Rows.Add(dataRow);
                }
                lastCandleUpdate.Rows[index]["min30"] = now;
            }
            else
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_MIN30, 1);
                if (jArray == null) return -12;
                if (jArray.Count < 1) return -13;

                min30_candle.Tables[coinName].Rows[0]["date"] = Convert.ToDateTime(jArray[0]["candle_date_time_kst"]);
                min30_candle.Tables[coinName].Rows[0]["open"] = (double)jArray[0]["opening_price"];
                min30_candle.Tables[coinName].Rows[0]["close"] = (double)jArray[0]["trade_price"];
                min30_candle.Tables[coinName].Rows[0]["max"] = (double)jArray[0]["high_price"];
                min30_candle.Tables[coinName].Rows[0]["min"] = (double)jArray[0]["low_price"];
                min30_candle.Tables[coinName].Rows[0]["volume"] = (double)jArray[0]["candle_acc_trade_volume"];
            }

            last = (DateTime)lastCandleUpdate.Rows[index]["hour1"];
            if (hour1_candle.Tables[coinName].Rows.Count < 1 || now.Minute < last.Minute)
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_HOUR1, 200);
                if (jArray == null) return -20;
                if (jArray.Count < hour1_candle.Tables[coinName].Rows.Count) return -21;

                hour1_candle.Tables[coinName].Rows.Clear();
                for (int i = 0; i < jArray.Count; i++)
                {
                    DataRow dataRow = hour1_candle.Tables[coinName].NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["candle_date_time_kst"]);
                    dataRow["open"] = (double)jArray[i]["opening_price"];
                    dataRow["close"] = (double)jArray[i]["trade_price"];
                    dataRow["max"] = (double)jArray[i]["high_price"];
                    dataRow["min"] = (double)jArray[i]["low_price"];
                    dataRow["volume"] = (double)jArray[i]["candle_acc_trade_volume"];
                    hour1_candle.Tables[coinName].Rows.Add(dataRow);
                }
                lastCandleUpdate.Rows[index]["hour1"] = now;
            }
            else
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_HOUR1, 1);
                if (jArray == null) return -22;
                if (jArray.Count < 1) return -23;

                hour1_candle.Tables[coinName].Rows[0]["date"] = Convert.ToDateTime(jArray[0]["candle_date_time_kst"]);
                hour1_candle.Tables[coinName].Rows[0]["open"] = (double)jArray[0]["opening_price"];
                hour1_candle.Tables[coinName].Rows[0]["close"] = (double)jArray[0]["trade_price"];
                hour1_candle.Tables[coinName].Rows[0]["max"] = (double)jArray[0]["high_price"];
                hour1_candle.Tables[coinName].Rows[0]["min"] = (double)jArray[0]["low_price"];
                hour1_candle.Tables[coinName].Rows[0]["volume"] = (double)jArray[0]["candle_acc_trade_volume"];
            }

            last = (DateTime)lastCandleUpdate.Rows[index]["hour4"];
            if (hour4_candle.Tables[coinName].Rows.Count < 1 || now.Hour % 4 < last.Hour % 4)
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_HOUR4, 200);
                if (jArray == null) return -30;
                if (jArray.Count < hour4_candle.Tables[coinName].Rows.Count) return -31;

                hour4_candle.Tables[coinName].Rows.Clear();
                for (int i = 0; i < jArray.Count; i++)
                {
                    DataRow dataRow = hour4_candle.Tables[coinName].NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["candle_date_time_kst"]);
                    dataRow["open"] = (double)jArray[i]["opening_price"];
                    dataRow["close"] = (double)jArray[i]["trade_price"];
                    dataRow["max"] = (double)jArray[i]["high_price"];
                    dataRow["min"] = (double)jArray[i]["low_price"];
                    dataRow["volume"] = (double)jArray[i]["candle_acc_trade_volume"];
                    hour4_candle.Tables[coinName].Rows.Add(dataRow);
                }
                lastCandleUpdate.Rows[index]["hour4"] = now;
            }
            else
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_HOUR4, 1);
                if (jArray == null) return -32;
                if (jArray.Count < 1) return -33;

                hour4_candle.Tables[coinName].Rows[0]["date"] = Convert.ToDateTime(jArray[0]["candle_date_time_kst"]);
                hour4_candle.Tables[coinName].Rows[0]["open"] = (double)jArray[0]["opening_price"];
                hour4_candle.Tables[coinName].Rows[0]["close"] = (double)jArray[0]["trade_price"];
                hour4_candle.Tables[coinName].Rows[0]["max"] = (double)jArray[0]["high_price"];
                hour4_candle.Tables[coinName].Rows[0]["min"] = (double)jArray[0]["low_price"];
                hour4_candle.Tables[coinName].Rows[0]["volume"] = (double)jArray[0]["candle_acc_trade_volume"];
            }

            last = (DateTime)lastCandleUpdate.Rows[index]["day"];
            if (day_candle.Tables[coinName].Rows.Count < 1 || now.Hour < last.Hour)
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_DAY, 200);
                if (jArray == null) return -40;
                if (jArray.Count < day_candle.Tables[coinName].Rows.Count) return -41;

                day_candle.Tables[coinName].Rows.Clear();
                for (int i = 0; i < jArray.Count; i++)
                {
                    DataRow dataRow = day_candle.Tables[coinName].NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["candle_date_time_kst"]);
                    dataRow["open"] = (double)jArray[i]["opening_price"];
                    dataRow["close"] = (double)jArray[i]["trade_price"];
                    dataRow["max"] = (double)jArray[i]["high_price"];
                    dataRow["min"] = (double)jArray[i]["low_price"];
                    dataRow["volume"] = (double)jArray[i]["candle_acc_trade_volume"];
                    day_candle.Tables[coinName].Rows.Add(dataRow);
                }
                lastCandleUpdate.Rows[index]["day"] = now;
            }
            else
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_DAY, 1);
                if (jArray == null) return -42;
                if (jArray.Count < 1) return -43;

                day_candle.Tables[coinName].Rows[0]["date"] = Convert.ToDateTime(jArray[0]["candle_date_time_kst"]);
                day_candle.Tables[coinName].Rows[0]["open"] = (double)jArray[0]["opening_price"];
                day_candle.Tables[coinName].Rows[0]["close"] = (double)jArray[0]["trade_price"];
                day_candle.Tables[coinName].Rows[0]["max"] = (double)jArray[0]["high_price"];
                day_candle.Tables[coinName].Rows[0]["min"] = (double)jArray[0]["low_price"];
                day_candle.Tables[coinName].Rows[0]["volume"] = (double)jArray[0]["candle_acc_trade_volume"];
            }

            last = (DateTime)lastCandleUpdate.Rows[index]["week"];
            int nowWeek = (int)now.DayOfWeek == 6 ? -1 : (int)now.DayOfWeek;
            int lastWeek = (int)last.DayOfWeek == 6 ? -1 : (int)last.DayOfWeek;
            if (week_candle.Tables[coinName].Rows.Count < 1 || nowWeek < lastWeek)
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_WEEK, 200);
                if (jArray == null) return -50;
                if (jArray.Count < week_candle.Tables[coinName].Rows.Count) return -51;

                week_candle.Tables[coinName].Rows.Clear();
                for (int i = 0; i < jArray.Count; i++)
                {
                    DataRow dataRow = week_candle.Tables[coinName].NewRow();
                    dataRow["date"] = Convert.ToDateTime(jArray[i]["candle_date_time_kst"]);
                    dataRow["open"] = (double)jArray[i]["opening_price"];
                    dataRow["close"] = (double)jArray[i]["trade_price"];
                    dataRow["max"] = (double)jArray[i]["high_price"];
                    dataRow["min"] = (double)jArray[i]["low_price"];
                    dataRow["volume"] = (double)jArray[i]["candle_acc_trade_volume"];
                    week_candle.Tables[coinName].Rows.Add(dataRow);
                }
                lastCandleUpdate.Rows[index]["week"] = now;
            }
            else
            {
                JArray jArray = apiData.getCandle(coinName, ac.CANDLE_WEEK, 1);
                if (jArray == null) return -52;
                if (jArray.Count < 1) return -53;

                week_candle.Tables[coinName].Rows[0]["date"] = Convert.ToDateTime(jArray[0]["candle_date_time_kst"]);
                week_candle.Tables[coinName].Rows[0]["open"] = (double)jArray[0]["opening_price"];
                week_candle.Tables[coinName].Rows[0]["close"] = (double)jArray[0]["trade_price"];
                week_candle.Tables[coinName].Rows[0]["max"] = (double)jArray[0]["high_price"];
                week_candle.Tables[coinName].Rows[0]["min"] = (double)jArray[0]["low_price"];
                week_candle.Tables[coinName].Rows[0]["volume"] = (double)jArray[0]["candle_acc_trade_volume"];
            }


            return 0;
        }
        public void addBABB(int index)
        {
            string coinName = coinList[index];
            double tempPercent;

            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_MIN30, 0)) > -10000)
            {
                ba0[0].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb0[0].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_HOUR1, 0)) > -10000)
            {
                ba0[1].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb0[1].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_HOUR4, 0)) > -10000)
            {
                ba0[2].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb0[2].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_DAY, 0)) > -10000)
            {
                ba0[3].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb0[3].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_WEEK, 0)) > -10000)
            {
                ba0[4].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb0[4].addTotal(tempPercent, 1);
            }

            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_MIN30, 1)) > -10000)
            {
                ba1[0].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb1[0].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_HOUR1, 1)) > -10000)
            {
                ba1[1].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb1[1].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_HOUR4, 1)) > -10000)
            {
                ba1[2].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb1[2].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_DAY, 1)) > -10000)
            {
                ba1[3].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb1[3].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_WEEK, 1)) > -10000)
            {
                ba1[4].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb1[4].addTotal(tempPercent, 1);
            }

            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_MIN30, 2)) > -10000)
            {
                ba2[0].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb2[0].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_HOUR1, 2)) > -10000)
            {
                ba2[1].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb2[1].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_HOUR4, 2)) > -10000)
            {
                ba2[2].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb2[2].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_DAY, 2)) > -10000)
            {
                ba2[3].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb2[3].addTotal(tempPercent, 1);
            }
            if ((tempPercent = getBollingerPercent(coinName, ac.CANDLE_WEEK, 2)) > -10000)
            {
                ba2[4].addTotal(tempPercent, 1 / (index + 1) / (index + 1));
                bb2[4].addTotal(tempPercent, 1);
            }
        }
        public void setBABB()
        {
            for (int i = 0; i < 5; i++)
            {
                ba0[i].setAverage();
                ba1[i].setAverage();
                ba2[i].setAverage();
                bb0[i].setAverage();
                bb1[i].setAverage();
                bb2[i].setAverage();
            }
        }


        private double getCandleAverage(string coinName, string dataType, int index)
        {
            DataTable lastCandle = null;
            if (dataType == ac.CANDLE_WEEK) lastCandle = week_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_DAY) lastCandle = day_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_HOUR4) lastCandle = hour4_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_HOUR1) lastCandle = hour1_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_MIN30) lastCandle = min30_candle.Tables[coinName];
            if (lastCandle.Rows.Count < 28 + index) return -1d;

            double averagePrice = 0;
            for (int i = 0; i < 28; i++)
                averagePrice += (double)lastCandle.Rows[i + index]["close"];

            return averagePrice / 28d;
        }
        private double[] getBollingerValue(string coinName, string dataType, int index)
        {
            DataTable lastCandle = null;
            if (dataType == ac.CANDLE_WEEK) lastCandle = week_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_DAY) lastCandle = day_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_HOUR4) lastCandle = hour4_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_HOUR1) lastCandle = hour1_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_MIN30) lastCandle = min30_candle.Tables[coinName];
            if (lastCandle.Rows.Count < 28 + index) return null;

            double averagePrice = getCandleAverage(coinName, dataType, index);
            if (averagePrice < 0d) return null;
            double dispersion = 0;
            for (int i = 0; i < 28; i++)
                dispersion += Math.Pow(averagePrice - (double)lastCandle.Rows[i + index]["close"], 2);
            dispersion = Math.Sqrt(dispersion / 28d);

            return new double[] { averagePrice, dispersion };
        }
        private double getBollingerPercent(string coinName, string dataType, int index)
        {
            DataTable lastCandle = null;
            if (dataType == ac.CANDLE_WEEK) lastCandle = week_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_DAY) lastCandle = day_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_HOUR4) lastCandle = hour4_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_HOUR1) lastCandle = hour1_candle.Tables[coinName];
            else if (dataType == ac.CANDLE_MIN30) lastCandle = min30_candle.Tables[coinName];
            if (lastCandle.Rows.Count < 28 + index) return -100000;

            double[] avgDis = getBollingerValue(coinName, dataType, index);
            double retVal = (double)lastCandle.Rows[index]["close"];

            retVal -= avgDis[0];
            retVal /= 2 * avgDis[1];
            retVal *= 100d;

            return retVal;
        }


        private int getBollingerResult(string coinName, string dataType, int index, double targetPercent)
        {
            double curPercent = getBollingerPercent(coinName, dataType, index);
            if (curPercent < -10000) return 0;
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

                executionStr.Add(new output(1, "Macro Execution",
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

                        executionStr.Add(new output(1, "Macro Execution",
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
            if (holdKRW < setting.krw * 1.0005d) return 0;
            if (state.Tables[coinName].Rows.Count >= setting.time && setting.time != 0) return 0;


            DataTable buyCandle = null;
            if (setting.min30_from > -90000d) buyCandle = min30_candle.Tables[coinName];
            else if (setting.hour1_from > -90000d) buyCandle = hour1_candle.Tables[coinName];
            else if (setting.hour4_from > -90000d) buyCandle = hour4_candle.Tables[coinName];
            else if (setting.day_from > -90000d) buyCandle = day_candle.Tables[coinName];
            else if (setting.week_from > -90000d) buyCandle = week_candle.Tables[coinName];
            if (buyCandle == null)
            {
                executionStr.Add(new output(0, "Macro Execution", "Fail to load " + coinName + " buy candle (NULL)"));
                return -1;
            }
            if (buyCandle.Rows.Count < 28)
            {
                if (errorList.Contains(coinName))
                {
                    coinList.Remove(coinName);
                    errorList.Remove(coinName);
                    executionStr.Add(new output(0, "Macro Execution",
                        "Fail to load " + coinName + " buy candle (Not Enouph) " + coinName + " remove from macro list"));
                }
                else
                {
                    errorList.Add(coinName);
                    executionStr.Add(new output(0, "Macro Execution",
                        "Fail to load " + coinName + " buy candle (Not Enouph) If one more error, " + coinName + " remove from macro list"));
                }
                return -1;
            }
            if (errorList.Contains(coinName)) errorList.Remove(coinName);
            if ((double)buyCandle.Rows[0]["open"] >= (double)buyCandle.Rows[0]["close"]) return 0;
            if (((double)buyCandle.Rows[0]["open"] + (double)buyCandle.Rows[0]["close"]) / 2d <
                ((double)buyCandle.Rows[1]["open"] + (double)buyCandle.Rows[1]["close"] * 4d) / 5d) return 0;


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
                { if (getBollingerResult(coinName, ac.CANDLE_WEEK, 0, setting.week_from + bb0[4].avg) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, ac.CANDLE_WEEK, 0, setting.week_from) >= 0) return 0; }
            }
            if (setting.day_from > -90000d)
            {
                if (setting.day_bias)
                { if (getBollingerResult(coinName, ac.CANDLE_DAY, 0, setting.day_from + bb0[3].avg) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, ac.CANDLE_DAY, 0, setting.day_from) >= 0) return 0; }
            }
            if (setting.hour4_from > -90000d)
            {
                if (setting.hour4_bias)
                { if (getBollingerResult(coinName, ac.CANDLE_HOUR4, 0, setting.hour4_from + bb0[2].avg) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, ac.CANDLE_HOUR4, 0, setting.hour4_from) >= 0) return 0; }
            }
            if (setting.hour1_from > -90000d)
            {
                if (setting.hour1_bias)
                { if (getBollingerResult(coinName, ac.CANDLE_HOUR1, 0, setting.hour1_from + bb0[1].avg) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, ac.CANDLE_HOUR1, 0, setting.hour1_from) >= 0) return 0; }
            }
            if (setting.min30_from > -90000d)
            {
                if (setting.min30_bias)
                { if (getBollingerResult(coinName, ac.CANDLE_MIN30, 0, setting.min30_from + bb0[0].avg) >= 0) return 0; }
                else
                { if (getBollingerResult(coinName, ac.CANDLE_MIN30, 0, setting.min30_from) >= 0) return 0; }
            }


            Dictionary<string, string> par = new Dictionary<string, string>();
            par.Add("market", "KRW-" + coinName);
            par.Add("side", "bid");
            par.Add("price", setting.krw.ToString());
            par.Add("ord_type", "price");

            JObject jObject = apiData.order(par);
            if (jObject == null)
            {
                executionStr.Add(new output(0, "Macro Execution", "Fail to buy " + coinName + " (NULL)"));
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
            string coinName = coinList[index];
            if (state.Tables[coinName].Rows.Count < 1) return 0;

            DataTable sellCandle = min30_candle.Tables[coinName];
            if (sellCandle == null)
            {
                executionStr.Add(new output(0, "Macro Execution", "Fail to load " + coinName + " sell candle (NULL)"));
                return -1;
            }
            if (sellCandle.Rows.Count < 28)
            {
                executionStr.Add(new output(0, "Macro Execution", "Fail to load " + coinName + " sell candle (Not Enouph)"));
                return -1;
            }
            if ((double)sellCandle.Rows[0]["open"] <= (double)sellCandle.Rows[0]["close"]) return 0;
            if (((double)sellCandle.Rows[0]["open"] + (double)sellCandle.Rows[0]["close"]) / 2d >
                ((double)sellCandle.Rows[1]["open"] + (double)sellCandle.Rows[1]["close"] * 4d) / 5d) return 0;


            for (int i = 0; i < state.Tables[coinName].Rows.Count; i++)
            {
                double unit = (double)state.Tables[coinName].Rows[i]["unit"];
                double buyPrice = (double)state.Tables[coinName].Rows[i]["price"];
                buyPrice *= (100d + setting.yield) / 100d;
                if ((double)sellCandle.Rows[0]["close"] < buyPrice) continue;


                if (setting.week_to > -90000d)
                {
                    if (setting.week_bias)
                    { if (getBollingerResult(coinName, ac.CANDLE_WEEK, 0, setting.week_to + bb0[4].avg) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, ac.CANDLE_WEEK, 0, setting.week_to) <= 0) return 0; }
                }
                if (setting.day_to > -90000d)
                {
                    if (setting.day_bias)
                    { if (getBollingerResult(coinName, ac.CANDLE_DAY, 0, setting.day_to + bb0[3].avg) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, ac.CANDLE_DAY, 0, setting.day_to) <= 0) return 0; }
                }
                if (setting.hour4_to > -90000d)
                {
                    if (setting.hour4_bias)
                    { if (getBollingerResult(coinName, ac.CANDLE_HOUR4, 0, setting.hour4_to + bb0[2].avg) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, ac.CANDLE_HOUR4, 0, setting.hour4_to) <= 0) return 0; }
                }
                if (setting.hour1_to > -90000d)
                {
                    if (setting.hour1_bias)
                    { if (getBollingerResult(coinName, ac.CANDLE_HOUR1, 0, setting.hour1_to + bb0[1].avg) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, ac.CANDLE_HOUR1, 0, setting.hour1_to) <= 0) return 0; }
                }
                if (setting.min30_to > -90000d)
                {
                    if (setting.min30_bias)
                    { if (getBollingerResult(coinName, ac.CANDLE_MIN30, 0, setting.min30_to + bb0[0].avg) <= 0) return 0; }
                    else
                    { if (getBollingerResult(coinName, ac.CANDLE_MIN30, 0, setting.min30_to) <= 0) return 0; }
                }


                Dictionary<string, string> par = new Dictionary<string, string>();
                par.Add("market", "KRW-" + coinName);
                par.Add("side", "ask");
                par.Add("volume", unit.ToString("0.########"));
                par.Add("ord_type", "market");
                JObject jObject = apiData.order(par);
                if (jObject == null)
                {
                    executionStr.Add(new output(0, "Macro Execution", "Fail to sell " + coinName + " (NULL)"));
                    return -2;
                }

                DataRow row = order.NewRow();
                row["coinName"] = coinName;
                row["uuid"] = jObject["uuid"];
                row["target_uuid"] = state.Tables[coinName].Rows[i]["uuid"];
                order.Rows.Add(row);
            }

            return 1;
        }
    }
}
