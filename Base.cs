using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRworksConnector
{
    public static class RequestMethod
    {
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Delete = "DELETE";
    }

    public interface IBase
    {
        System.Threading.Tasks.Task<T> SendRequestAsync<T>(string requestMethod, string target, string jsonBody, int timeoutSeconds);
    }

    public class Base : IBase
    {
        #region Consts

        public const string Host = "api.hrworks.de";

        private const string HeaderXHRworksDate = "x-hrworks-date";
        private const string HeaderXHRworksTarget = "x-hrworks-target";
        private const string ClosingString = "hrworks_api_request";
        private const string SignatureAlgorithmIdentifier = "HRWORKS-HMAC-SHA256";

        #endregion

        #region Privates

        private string accessKey = string.Empty;
        private string secretAccessKey = string.Empty;
        private string realmIdentifier = string.Empty;

        #endregion

        #region Properties

        public string AccessKey
        {
            get
            {
                return this.accessKey;
            }
        }

        public string SecretAccessKey
        {
            get
            {
                return this.secretAccessKey;
            }
        }

        public string RealmIdentifier
        {
            get
            {
                return this.realmIdentifier;
            }
        }

        #endregion

        #region Constructor

        public Base(string accessKey, string secretAccessKey, string realmIdentifier)
        {
            this.accessKey = accessKey;
            this.secretAccessKey = secretAccessKey;
            this.realmIdentifier = realmIdentifier;
        }

        #endregion

        #region Methods

        protected async System.Threading.Tasks.Task<T> PostAsync<T>(string target, Newtonsoft.Json.Linq.JObject json)
        {
            return await SendRequestAsync<T>(RequestMethod.Post, target, json.ToString(Newtonsoft.Json.Formatting.None));
        }

        protected async System.Threading.Tasks.Task<T> PostAsync<T>(string target, string jsonBody)
        {
            return await SendRequestAsync<T>(RequestMethod.Post, target, jsonBody);
        }

        public async System.Threading.Tasks.Task<T> SendRequestAsync<T>(string requestMethod, string target, string jsonBody, int timeoutSeconds = 60)
        {
            System.Net.Http.HttpResponseMessage httpResponseMessage = await SendAsync(requestMethod, target, jsonBody, timeoutSeconds);

            System.Net.Http.HttpContent requestContent = httpResponseMessage.Content;
            string resultContent = requestContent.ReadAsStringAsync().Result;

#if DEBUG
            try
            {
                string tempDirectory = System.IO.Path.GetTempPath();
                string targetFilename = string.Format("hrworks-dbg-{0}.json", target);
                string tempFilepath = System.IO.Path.Combine(tempDirectory, targetFilename);

                string tmpContent = resultContent;

                try
                {
                    Newtonsoft.Json.Linq.JObject jsonResult = Newtonsoft.Json.Linq.JObject.Parse(tmpContent);
                    tmpContent = jsonResult.ToString(Newtonsoft.Json.Formatting.Indented);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }


                System.IO.File.WriteAllText(tempFilepath, tmpContent);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
#endif
            try
            {
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Net.Http.HttpRequestException httpRequestException = new System.Net.Http.HttpRequestException(resultContent, ex);
                throw httpRequestException;
            }

            System.Threading.Tasks.Task<T> typedResponse = System.Threading.Tasks.Task.Factory.StartNew(() => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resultContent));
            return await typedResponse;
        }

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(string requestMethod, string target, string jsonBody, int timeoutSeconds = 60)
        {
            string requestDateSecret = "HRWORKS" + this.secretAccessKey;
            System.DateTime now = System.DateTime.Now;

            string requestTimestampAsText = string.Format("{0:yyyyMMdd}T{0:HHmmss}Z", now.ToUniversalTime());
            string requestDateAsText = string.Format("{0:yyyyMMdd}", now.ToUniversalTime());

            // Creating the canonical request
            string canonicalRequest = GetCanonicalRequest(Host, requestMethod, @"/", target, requestTimestampAsText, jsonBody);

            // Creating the string to sign
            System.Text.StringBuilder stringSignatur = new System.Text.StringBuilder();
            stringSignatur.Append(SignatureAlgorithmIdentifier);
            stringSignatur.Append("\n");
            stringSignatur.Append(requestTimestampAsText);
            stringSignatur.Append("\n");
            stringSignatur.Append(GetSha256Hash(canonicalRequest.ToString()));

            // Creating the string to sign
            byte[] requestDateSign = GetHMACSHA256Hash(requestDateAsText, System.Text.Encoding.ASCII.GetBytes(requestDateSecret));
            byte[] realmSign = GetHMACSHA256Hash(RealmIdentifier, requestDateSign);
            byte[] closingSign = GetHMACSHA256Hash(ClosingString, realmSign);

            byte[] stringSign = GetHMACSHA256Hash(stringSignatur.ToString(), closingSign);

            string newSignature = GetHex(stringSign);

            System.Text.StringBuilder credential = new System.Text.StringBuilder();

            credential.AppendFormat("Credential={0}/{1}, ", System.Net.WebUtility.UrlEncode(AccessKey), RealmIdentifier);
            credential.Append("SignedHeaders=content-type;host;x-hrworks-date;x-hrworks-target, ");
            credential.AppendFormat("Signature={0}", newSignature);

            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                TimeSpan timeout = TimeSpan.FromSeconds(timeoutSeconds);

                try
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                }
                catch (Exception)
                {
                }

                string url = string.Format("https://{0}", Host);

                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.Timeout = timeout;
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Date = new System.DateTimeOffset(now);
                httpClient.DefaultRequestHeaders.Add(HeaderXHRworksDate, requestTimestampAsText);
                httpClient.DefaultRequestHeaders.Add(HeaderXHRworksTarget, target);

                System.Net.Http.Headers.AuthenticationHeaderValue authenticationHeaderValue = new System.Net.Http.Headers.AuthenticationHeaderValue(SignatureAlgorithmIdentifier, credential.ToString());
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

                System.Net.Http.StringContent body = new System.Net.Http.StringContent(jsonBody, Encoding.UTF8, "application/json");

                System.Net.Http.HttpResponseMessage httpResponseMessage = null;

                switch (requestMethod.ToUpper())
                {
                    case RequestMethod.Post:
                        httpResponseMessage = await httpClient.PostAsync(url, body);
                        break;
                    case RequestMethod.Put:
                        httpResponseMessage = await httpClient.PutAsync(url, body);
                        break;
                    case RequestMethod.Get:
                        httpResponseMessage = await httpClient.GetAsync(url);
                        break;
                    case RequestMethod.Delete:
                        httpResponseMessage = await httpClient.DeleteAsync(url);
                        break;
                }

                return httpResponseMessage;
            }
        }

        #endregion

        #region Private Methods

        private static string GetCanonicalRequest(string host, string requestMethod, string uri, string target, string requestTimestamp, string body)
        {
            System.Text.StringBuilder canonicalRequest = new System.Text.StringBuilder();

            canonicalRequest.Append(requestMethod);
            canonicalRequest.Append("\n");

            canonicalRequest.Append(uri);
            canonicalRequest.Append("\n");
            canonicalRequest.Append("\n");

            canonicalRequest.Append("content-type:application/json; charset=utf-8");
            canonicalRequest.Append("\n");

            canonicalRequest.Append(string.Format("host:{0}", host));
            canonicalRequest.Append("\n");

            canonicalRequest.AppendFormat("{0}:{1}", HeaderXHRworksDate, requestTimestamp);
            canonicalRequest.Append("\n");

            canonicalRequest.AppendFormat("{0}:{1}", HeaderXHRworksTarget, target);
            canonicalRequest.Append("\n");
            canonicalRequest.Append("\n");

            canonicalRequest.Append(GetSha256Hash(body));

            return canonicalRequest.ToString();
        }

        private static byte[] GetHMACSHA256Hash(string plainText, byte[] secret)
        {
            byte[] tmpSignatur = null;

            using (System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(secret))
            {
                tmpSignatur = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(plainText));
            }

            return tmpSignatur;
        }

        private static string GetSha256Hash(string data)
        {
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
                return GetHex(bytes);
            }
        }

        private static string GetHex(byte[] data)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }

        #endregion
    }
}