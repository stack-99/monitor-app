using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace MobApp
{
    public class RestAPICore
    {
        private static RestAPICore _instance = null;
        private static HttpClient Client = new HttpClient();

        public static RestAPICore GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RestAPICore();
                Client = new HttpClient(GetHttpClientHandler());
            }

            return _instance;
        }

        public void ConfigureClient(string apiKey)
        {
            Guid GapiKey = Guid.Parse(apiKey);

            // Sending the data
            if (Client.DefaultRequestHeaders.Contains("apiKey"))
                Client.DefaultRequestHeaders.Remove("apiKey");

            Client.DefaultRequestHeaders.Add("apiKey", GapiKey.ToString().ToUpper());
        }

        public void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        public HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        public async Task<HttpResponseMessage> SendHttpAsync(string url, HttpContent content)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                HttpClient client = new HttpClient();

                // Sending the data
                if (Client.DefaultRequestHeaders.Contains("apiKey"))
                    Client.DefaultRequestHeaders.Remove("apiKey");

                client.DefaultRequestHeaders.Add("apiKey", "<GET_KEY>");

                request.Content = content;
                return await client.SendAsync(request).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Use for APIS and expect a json response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendJsonAsync<T>(string url, T content)
        {
            var jsonToSend = JsonConvert.SerializeObject(content, Newtonsoft.Json.Formatting.None);

            var body = new StringContent(jsonToSend, Encoding.UTF8, "application/json");

            return await Client.PostAsync(url, body);

            //  return await SendHttpAsync(url, CreateHttpContent(content)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> SendGetJsonAsync(string url)
        {
            try
            {
                return await Client.GetAsync(url).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<dynamic> GetAsync(string uri)
        {
            return await Client.GetAsync(uri);
        }

        private static HttpClientHandler GetHttpClientHandler()
        {
            //b0a6719aeca8ee2e6d129896713daa81966e3070
            var httpClientHandler = new HttpClientHandler();

            /*
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;   //Is valid
                }

                // API cert 
                if (cert.GetCertHashString().ToLower() == "b0a6719aeca8ee2e6d129896713daa81966e3070")
                {
                    return true;
                }
                return false;
            };
            */
            return httpClientHandler;
        }
    }
}