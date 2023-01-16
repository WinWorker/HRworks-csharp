using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private const string AuthenticationUrl = @"/v2/authentication";

        #endregion

        #region Privates

        private static volatile System.Lazy<LoginHelper> lazyLoginHelper = new System.Lazy<LoginHelper>(delegate
        {
            return new LoginHelper();
        });

        /// <summary>
        /// Threadsicheres Singleton
        /// </summary>
        private static LoginHelper Login
        {
            get
            {
                return lazyLoginHelper.Value;
            }
        }

        private string accessKey = string.Empty;
        private string secretAccessKey = string.Empty;

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

        #endregion

        #region Constructor

        public Base(string accessKey, string secretAccessKey)
        {
            this.accessKey = accessKey;
            this.secretAccessKey = secretAccessKey;
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

        protected async System.Threading.Tasks.Task<T> GetAsync<T>(string target)
        {
            return await SendRequestAsync<T>(RequestMethod.Get, target, string.Empty);
        }

        protected async System.Threading.Tasks.Task<T> GetAsync<T>(string target, string queryString)
        {
            return await SendRequestAsync<T>(RequestMethod.Get, target, queryString);
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
                string targetFilename = string.Format("hrworks-dbg-{0}.json", target.Replace("/", "_"));
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
            System.DateTime now = System.DateTime.Now;

            await this.ReNewTokenIfNecessary(timeoutSeconds);

            if (string.IsNullOrEmpty(Login.AccessToken))
            {
                System.Net.Http.HttpResponseMessage httpResponseMessageNoApiKey = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                httpResponseMessageNoApiKey.ReasonPhrase = "No Token created.";

                Newtonsoft.Json.Linq.JObject errorAsJson = new Newtonsoft.Json.Linq.JObject();
                errorAsJson["message"] = httpResponseMessageNoApiKey.ReasonPhrase;
                httpResponseMessageNoApiKey.Content = new System.Net.Http.StringContent(errorAsJson.ToString(Newtonsoft.Json.Formatting.None));
                httpResponseMessageNoApiKey.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                return httpResponseMessageNoApiKey;
            }

            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            {
                TimeSpan timeout = TimeSpan.FromSeconds(timeoutSeconds);

                string url = string.Format("https://{0}{1}", Host, target);

                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.Timeout = timeout;
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Login.AccessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Date = new System.DateTimeOffset(now);

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

                        if (string.IsNullOrEmpty(jsonBody))
                        {
                            httpResponseMessage = await httpClient.GetAsync(url);
                        }
                        else
                        {
                            if (!jsonBody.StartsWith("?"))
                            {
                                jsonBody = "?" + jsonBody;
                            }

                            httpResponseMessage = await httpClient.GetAsync(url + jsonBody);
                        }

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

        private async System.Threading.Tasks.Task ReNewTokenIfNecessary(int timeoutSeconds = 60)
        {
            if (string.IsNullOrEmpty(Login.AccessToken) ||
                System.DateTime.Now.Subtract(Login.AccessTokenExpire).TotalSeconds >= 0)
            {
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

                    string authUrl = string.Format("https://{0}{1}", Host, AuthenticationUrl);

                    Newtonsoft.Json.Linq.JObject authAsJson = new Newtonsoft.Json.Linq.JObject();
                    authAsJson["accessKey"] = this.accessKey;
                    authAsJson["secretAccessKey"] = this.secretAccessKey;
                    System.Net.Http.StringContent bodyAuth = new System.Net.Http.StringContent(authAsJson.ToString(), Encoding.UTF8, "application/json");
                    System.Net.Http.HttpResponseMessage httpResponseMessageAuth = await httpClient.PostAsync(authUrl, bodyAuth);

                    httpResponseMessageAuth.EnsureSuccessStatusCode();

                    System.Net.Http.HttpContent httpContentAuth = httpResponseMessageAuth.Content;
                    string resultContent = httpContentAuth.ReadAsStringAsync().Result;

                    Newtonsoft.Json.Linq.JObject tokenAsJson = Newtonsoft.Json.Linq.JObject.Parse(resultContent);
                    string tmpAccessToken = tokenAsJson.GetValue("token", StringComparison.InvariantCultureIgnoreCase).ToString();
                    Login.LoadFromJwt(tmpAccessToken);
                }
            }
        }

        #endregion

        #region Private Classes

        private class LoginHelper
        {
            private string accessToken = string.Empty;
            private System.DateTime accessTokenExpire = System.DateTime.MinValue;
            private readonly System.TimeSpan DefaultAccessTokenTimeToLive = System.TimeSpan.FromMinutes(5);

            public string AccessToken
            {
                get
                {
                    return this.accessToken;
                }
            }

            public System.DateTime AccessTokenExpire
            {
                get
                {
                    return this.accessTokenExpire;
                }
            }

            public void LoadFromJwt(string jwt)
            {
                if (string.IsNullOrEmpty(jwt))
                {
                    return;
                }

                System.DateTime now = System.DateTime.Now;
                this.accessToken = jwt;
                this.accessTokenExpire = now.Add(DefaultAccessTokenTimeToLive);

                try
                {
                    // JWT decodieren
                    (JWTDecoder.JwtHeader Header, string Payload, string Verification) decodedToken = JWTDecoder.Decoder.DecodeToken(jwt);

                    if (!string.IsNullOrEmpty(decodedToken.Payload))
                    {
                        Newtonsoft.Json.Linq.JObject payloadAsJson = Newtonsoft.Json.Linq.JObject.Parse(decodedToken.Payload);
                        Newtonsoft.Json.Linq.JToken expireDateTimeAsJson = payloadAsJson.GetValue("exp", System.StringComparison.InvariantCultureIgnoreCase);

                        if (expireDateTimeAsJson != null)
                        {
                            long expireInSeconds = 0;
                            if (long.TryParse(expireDateTimeAsJson.ToString(), out expireInSeconds))
                            {
                                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expireInSeconds);

                                // wenn das "Verfallsdatum" größer als Now ist, dann merken.
                                if (dateTimeOffset.LocalDateTime.CompareTo(now) > 0)
                                {
                                    this.accessTokenExpire = dateTimeOffset.LocalDateTime;
                                    System.Console.WriteLine(string.Format("JWT expiration time: {0:dd.MM.yyyy HH:mm.ss}", this.accessTokenExpire));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion
    }
}