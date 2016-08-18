using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreLibrary.Utilities;

namespace TechStoreLibrary.Database
{
    public class WebServiceManager<T> where T : class
    {
        #region Attributes
        private string baseUrl;
        #endregion

        #region Properties
        /// <summary>
        /// API base url.
        /// </summary>
        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
            set
            {
                baseUrl = value;
            }
        }
        #endregion

        #region Constructors
        public WebServiceManager()
        {
            BaseUrl = EnumString.GetStringValue(ConnectionResource.LOCALAPI);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends a request to the web API to get all items.
        /// </summary>
        /// <returns>The list of items</returns>
        public async Task<List<T>> GetAllAsync(bool onlyInStock = false)
        {
            List<T> items = new List<T>();
            string url = string.Format("api/{0}s", typeof(T).Name.ToLower());

            if (onlyInStock)
                url = string.Format("{0}?stock=1", url);

            return await HttpClientCallerAsync<List<T>>(url, items);
        }

        /// <summary>
        /// Sends a request to the web API to get the item.
        /// </summary>
        /// <param name="id">The id of the item to get.</param>
        /// <returns>The requested item.</returns>
        public async Task<T> GetAsync(int id)
        {
            T item = default(T);
            string url = string.Format("api/{0}/{1}", typeof(T).Name.ToLower(), id);
            return await HttpClientCallerAsync<T>(url, item);
        }

        /// <summary>
        /// Sends a request to the web API to insert an item.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        /// <returns>The inserted item.</returns>
        public async Task<int> PostAsync(T item)
        {
            int result = default(int);
            bool typeNameHandling = false;

            if (item.GetType() == typeof(Cart))
            {
                typeNameHandling = true;
            }

            string url = string.Format("api/{0}", typeof(T).Name.ToLower());
            return await HttpClientPostSenderAsync<T>(url, item, result, typeNameHandling);
        }

        /// <summary>
        /// Sends a request to the web API to update an item.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>The updated item.</returns>
        public async Task<int> PutAsync(T item)
        {
            int result = default(int);
            string url = string.Format("api/{0}", typeof(T).Name.ToLower());
            return await HttpClientPutSenderAsync<T>(url, item, result);
        }

        /// <summary>
        /// Sends a request to the web API to delete an item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(T item)
        {
            int result = default(int);
            string url = string.Format("api/{0}", typeof(T).Name.ToLower());
            return await HttpClientDeleteSenderAsync<T, int>(url, item, result);
        }

        /// <summary>
        /// Sends a request to the web API via an HTTP GET method.
        /// </summary>
        /// <typeparam name="TItem">Type of the item to get.</typeparam>
        /// <param name="url">Web API url.</param>
        /// <param name="item">Object to hold the returned item.</param>
        /// <returns>The retrieved item.</returns>
        private async Task<TItem> HttpClientCallerAsync<TItem>(string url, TItem item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                item = await HandleResponseAsync(item, response);
            }

            return item;
        }

        /// <summary>
        /// Sends a request to the web API via an HTTP POST method.
        /// </summary>
        /// <typeparam name="TItem">Type of the item to insert.</typeparam>
        /// <param name="url">Web API url.</param>
        /// <param name="item">The item to insert.</param>
        /// <param name="result">Object to hold the returned item.</param>
        /// <returns>The inserted item.</returns>
        private async Task<int> HttpClientPostSenderAsync<TItem>(string url, TItem item, int result, bool typeNameHandling)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                HttpResponseMessage response = new HttpResponseMessage();

                if (typeNameHandling)
                {
                    JsonMediaTypeFormatter jsonMediaTypeFormatter = new JsonMediaTypeFormatter();
                    jsonMediaTypeFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    jsonMediaTypeFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
                    string debug = JsonConvert.SerializeObject(item, Formatting.Indented, jsonSerializerSettings);

                    response = await client.PostAsync(url, new ObjectContent<TItem>(item, jsonMediaTypeFormatter));
                    result = HandleResponse(item, response);
                }
                 else
                {
                    response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item, Formatting.None, jsonSerializerSettings), Encoding.UTF8, "application/json"));
                }

                result = HandleResponse(item, response);
            }

            return result;
        }

        /// <summary>
        /// Sends a request to the web API via an HTTP PUT method.
        /// </summary>
        /// <typeparam name="TItem">Type of the item to update.</typeparam>
        /// <param name="url">Web API url.</param>
        /// <param name="item">The item to update.</param>
        /// <param name="result">Value returned by the web API.</param>
        /// <returns>The updated item.</returns>
        private async Task<int> HttpClientPutSenderAsync<TItem>(string url, TItem item, int result)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string debugJson = JsonConvert.SerializeObject(item);
                var debug = new StringContent(debugJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(url, debug);
                result = HandleResponse(item, response);
            }

            return result;
        }
        
        /// <summary>
        /// Sends a request to the web API via an HTTP DELETE method.
        /// </summary>
        /// <typeparam name="TItem">Type of the item to delete.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="url">Web API url.</param>
        /// <param name="item">The item to delete.</param>
        /// <param name="result">The returned value.</param>
        /// <returns></returns>
        private async Task<int> HttpClientDeleteSenderAsync<TItem, TResult>(string url, TItem item, int result)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, url))
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.SendAsync(message);
                    result = HandleResponse(result, response);
                }
            }

            return result;
        }

        /// <summary>
        /// Handles the web API response to get an object from JSON data.
        /// </summary>
        /// <typeparam name="TItem">Type of the item.</typeparam>
        /// <param name="item">Object holding the result.</param>
        /// <param name="response">Response of the web API.</param>
        /// <returns>The returned data as an object.</returns>
        private async Task<TItem> HandleResponseAsync<TItem>(TItem item, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<TItem>(result);
            }

            return item;
        }

        /// <summary>
        /// Handles the web API response when not getting an object in return.
        /// </summary>
        /// <typeparam name="TItem">Type of the item.</typeparam>
        /// <param name="item">Object holding the result.</param>
        /// <param name="response">Response of the web API.</param>
        /// <returns>The returned data.</returns>
        private int HandleResponse<TItem>(TItem item, HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return 0;
            }

            return -1;
        }
        #endregion
    }
}
