using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using System.Windows.Forms;
using System.IdentityModel.Tokens.Jwt;

namespace UpbitDealer.src
{
    static class ac
    {
        public static string BASE_URL = "https://api.upbit.com/v1/";

        public static string CANDLE_MIN1 = "minutes/1";
        public static string CANDLE_MIN3 = "minutes/3";
        public static string CANDLE_MIN5 = "minutes/5";
        public static string CANDLE_MIN10 = "minutes/10";
        public static string CANDLE_MIN15 = "minutes/15";
        public static string CANDLE_MIN30 = "minutes/30";
        public static string CANDLE_HOUR1 = "minutes/60";
        public static string CANDLE_HOUR4 = "minutes/240";
        public static string CANDLE_DAY = "days";
        public static string CANDLE_WEEK = "weeks";
        public static string CANDLE_MONTH = "months";
    }

    class ApiData
    {
        private string access_key;
        private string secret_key;


        public ApiData(string access_key, string secret_key)
        {
            this.access_key = access_key;
            this.secret_key = secret_key;
        }


        public int checkApiKey()
        {
            string url = ac.BASE_URL + "api_keys";

            var payload = new Dictionary<string, object>
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization:" + authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                JArray jarray = JArray.Parse(reader.ReadToEnd());

                for (int i = 0; i < jarray.Count; i++)
                {
                    if (jarray[i]["access_key"].ToString() == access_key)
                    {
                        string date = jarray[i]["expire_at"].ToString();
                        DateTime expireDate = Convert.ToDateTime(jarray[0]["expire_at"]);
                        if (DateTime.Compare(DateTime.Now, expireDate.AddDays(-5)) > 0)
                            MessageBox.Show("API key expire date : " + expireDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (DateTime.Compare(DateTime.Now, expireDate) < 0)
                            return 1;
                    }
                }

                return -2;
            }
            catch
            {
                return -1;
            }

        }


        public JArray getAsset()
        {
            string url = ac.BASE_URL + "accounts";

            var payload = new Dictionary<string, object>
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add(string.Format("Authorization:{0}", authorize_token));

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }

        public JObject getOrdersChance(string coinName)
        {
            string url = ac.BASE_URL + "orders/chance";
            string queryString = "market=KRW-" + coinName;

            byte[] queryHashByteArray = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new Dictionary<string, object>
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "GET";
            request.Headers.Add("Authorization:" + authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JObject.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JObject order(Dictionary<string, string> par)
        {
            string url = ac.BASE_URL + "orders";
            string queryString = string.Join("&", par.Select(x => x.Key + "=" + x.Value).ToArray());

            byte[] queryHashByteArray = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "POST";
            request.Headers.Add("Authorization:" + authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JObject.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JObject cancelOrder(string uuid)
        {
            string url = ac.BASE_URL + "order";
            string queryString = "uuid=" + uuid;

            SHA512 sha512 = SHA512.Create();
            byte[] queryHashByteArray = sha512.ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "DELETE";
            request.Headers.Add("Authorization", authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JObject.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }

        public JObject checkOrder(string uuid)
        {
            string url = ac.BASE_URL + "order";
            string queryString = "uuid=" + uuid;

            byte[] queryHashByteArray = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "GET";
            request.Headers.Add("Authorization", authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JObject.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JArray getDoneOrder(string coinName = "", int page = 1, int count = 20)
        {
            string url = ac.BASE_URL + "orders";
            string queryString;
            if (coinName == "")
                queryString = "states[]=done&page=" + page + "&count=" + count;
            else
                queryString = "market=KRW-" + coinName + "&states[]=done&page=" + page + "&count=" + count;

            byte[] queryHashByteArray = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "GET";
            request.Headers.Add("Authorization", authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JArray getCancelOrder(string coinName = "", int page = 1, int count = 20)
        {
            string url = ac.BASE_URL + "orders";
            string queryString;
            if (coinName == "")
                queryString = "states[]=cancel&page=" + page + "&count=" + count;
            else
                queryString = "market=KRW-" + coinName + "&states[]=cancel&page=" + page + "&count=" + count;

            byte[] queryHashByteArray = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "GET";
            request.Headers.Add("Authorization", authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JArray getDoneCancelOrder(string coinName = "", int page = 1, int count = 20)
        {
            string url = ac.BASE_URL + "orders";
            string queryString;
            if (coinName == "")
                queryString = "states[]=done&states[]=cancel&page=" + page + "&count=" + count;
            else
                queryString = "market=KRW-" + coinName + "&states[]=done&states[]=cancel&page=" + page + "&count=" + count;

            byte[] queryHashByteArray = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            var payload = new JwtPayload
            {
                { "access_key" , access_key },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , queryHash },
                { "query_hash_alg" , "SHA512" },
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(payload, secret_key);
            string authorize_token = "Bearer " + token;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queryString);
            request.Method = "GET";
            request.Headers.Add("Authorization", authorize_token);

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }


        public JArray getCoinList(bool detail = false)
        {
            string url = ac.BASE_URL + "market/all";
            string dataParams;
            if (detail)
                dataParams = "isDetails=true";
            else
                dataParams = "isDetails=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + dataParams);
            request.Method = "GET";

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }

        public JArray getCandle(string coinName, string candleType, int num = 0)
        {
            string url = ac.BASE_URL + "candles/" + candleType;
            string dataParams = "market=KRW-" + coinName;
            if (num > 0) dataParams += "&count=" + num;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + dataParams);
            request.Method = "GET";

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }

        public JArray getTrans(string coinName, int num = 0)
        {
            string url = ac.BASE_URL + "trades/ticks";
            string dataParams = "market=KRW-" + coinName;
            if (num > 0) dataParams += ("&count=" + num);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + dataParams);
            request.Method = "GET";

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }

        public JArray getTicker(List<string> coinName)
        {
            string url = ac.BASE_URL + "ticker";
            string dataParams = "markets=KRW-" + coinName[0];
            for (int i = 1; i < coinName.Count; i++)
                dataParams += ",KRW-" + coinName[i];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + dataParams);
            request.Method = "GET";

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JArray getTicker(string coinName)
        {
            return getTicker(new List<string> { coinName });
        }

        public JArray getOrderBook(List<string> coinName)
        {
            string url = ac.BASE_URL + "orderbook";
            string dataParams = "markets=KRW-" + coinName[0];
            for (int i = 1; i < coinName.Count; i++)
                dataParams += ",KRW-" + coinName[i];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + dataParams);
            request.Method = "GET";

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return JArray.Parse(reader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        public JArray getOrderBook(string coinName)
        {
            return getOrderBook(new List<string> { coinName });
        }
    }
}
