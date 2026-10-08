using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> APIPostAsync(this HttpClient client, string url, object model, Dictionary<string, string>? headers = null, int timeoutSeconds = 100)
        {
            client.BaseAddress = new Uri(url);
            HttpRequestMessage request = new(HttpMethod.Post, client.BaseAddress);
            request.Headers.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
            foreach (KeyValuePair<string, string> item in headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
            string jsonObject = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            request.Content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage? responseMessage = null;
            using (CancellationTokenSource cts = new())
            {
                if (timeoutSeconds != 0)
                {
                    cts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));
                }
                responseMessage = await client.SendAsync(request, cts.Token).ConfigureAwait(false);
            }
            return responseMessage;
        }
        public static async Task<HttpResponseMessage> APIGetAsync(this HttpClient client, string url, Dictionary<string, string>? headers = null, int timeoutSeconds = 100)
        {
            client.BaseAddress = new Uri(url);
            HttpRequestMessage request = new(HttpMethod.Get, client.BaseAddress);
            request.Headers.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
            foreach (KeyValuePair<string, string> item in headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
            HttpResponseMessage? responseMessage = null;
            using (CancellationTokenSource cts = new())
            {
                if (timeoutSeconds != 0)
                {
                    cts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));
                }
                responseMessage = await client.SendAsync(request, cts.Token).ConfigureAwait(false);
            }
            return responseMessage;
        }
    }
}