using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLayer.Lib
{
    public interface IApiResources : IDisposable
    {
        string BaseURI { get; set; }

        // Standard Methods
        T GetAll<T>();
        T GetById<T>(string id);
        T Post<T>(object data);
        T Put<T>(string id, object data);
        T Delete<T>(string id);

        // Async Methods
        Task<T> GetAsync<T>();
        Task<T> GetAsync<T>(string id);
        Task<T> GetAsync<T>(string id, string apiUserToken);
        Task<T> GetAsync<T>(string id, string partOfUrl, string apiUserToken);
        Task<T> PostAsync<T>(object data);
        Task<T> PostAsync<T>(object data, string partOfUrl);
        Task<T> PostAsync<T>(object data, string partOfUrl, string apiUserToken);
        Task<T> PutAsync<T>(string id, object data);
        Task<T> DeleteAsync<T>(string id);
    }

    public class APIResource : IApiResources
    {
        private readonly IHttpClientWrapper client;
        private readonly JsonSerializerSettings JsonSettings;
        private readonly string _version;
        private readonly string _endpoint;
        private readonly string _apiVersion;
        private readonly string _apiKey;
        private string _baseURI;

        public string BaseURI
        {
            get { return _baseURI; }
            set { _baseURI = _endpoint + value; }
        }

        public APIResource(IHttpClientWrapper customClient, JsonSerializerSettings customJsonSerializerSettings = null)
        {
            client = customClient;
            JsonSettings = customJsonSerializerSettings ?? new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            _version = "1.0.5";
            _apiVersion = "v1";
            _endpoint = "https://localhost:7220/api";
            _apiKey = Client.Properties.ApiKey;

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new MissingFieldException("Chave de API não configurada.");
            }

            _baseURI = _endpoint;
        }

        public APIResource() : this(new StandardHttpClient(),
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
        {
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Standard Methods Implementation

        public T GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(string id)
        {
            throw new NotImplementedException();
        }

        public T Post<T>(object data)
        {
            throw new NotImplementedException();
        }

        public T Put<T>(string id, object data)
        {
            throw new NotImplementedException();
        }

        public T Delete<T>(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async Methods Implementation

        public async Task<T> GetAsync<T>()
        {
            var response = await SendRequestAsync(HttpMethod.Get, BaseURI).ConfigureAwait(false);
            return await ProcessResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>(string id)
        {
            var response = await GetAsync<T>(id, null, null).ConfigureAwait(false);
            return response;
        }

        public async Task<T> GetAsync<T>(string id, string apiUserToken)
        {
            var response = await GetAsync<T>(id, null, apiUserToken).ConfigureAwait(false);
            return response;
        }

        public async Task<T> GetAsync<T>(string id, string partOfUrl, string apiUserToken)
        {
            var completeUrl = GetCompleteUrl(partOfUrl, id);
            var response = await SendRequestAsync(HttpMethod.Get, completeUrl, null, apiUserToken).ConfigureAwait(false);
            return await ProcessResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<T> PostAsync<T>(object data)
        {
            var response = await PostAsync<T>(data, null, null).ConfigureAwait(false);
            return response;
        }

        public async Task<T> PostAsync<T>(object data, string partOfUrl)
        {
            var response = await PostAsync<T>(data, partOfUrl, null).ConfigureAwait(false);
            return response;
        }

        public async Task<T> PostAsync<T>(object data, string partOfUrl, string customApiToken)
        {
            var completeUrl = GetCompleteUrl(partOfUrl, null);
            var response = await SendRequestAsync(HttpMethod.Post, completeUrl, data, customApiToken).ConfigureAwait(false);
            return await ProcessResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<T> PutAsync<T>(string id, object data)
        {
            return await PutAsync<T>(data, id, null).ConfigureAwait(false);
        }

        public async Task<T> PutAsync<T>(object data, string partOfUrl)
        {
            return await PutAsync<T>(data, partOfUrl, null).ConfigureAwait(false);
        }

        public async Task<T> PutAsync<T>(object data, string partOfUrl, string customApiToken)
        {
            var completeUrl = GetCompleteUrl(partOfUrl, null);
            var response = await SendRequestAsync(HttpMethod.Put, completeUrl, data, customApiToken).ConfigureAwait(false);
            return await ProcessResponse<T>(response).ConfigureAwait(false);
        }

        public async Task<T> DeleteAsync<T>(string id)
        {
            return await DeleteAsync<T>(id, null).ConfigureAwait(false);
        }

        public async Task<T> DeleteAsync<T>(string id, string customApiToken)
        {
            var response = await SendRequestAsync(HttpMethod.Delete, $"{BaseURI}/{id}", null, customApiToken).ConfigureAwait(false);
            return await ProcessResponse<T>(response).ConfigureAwait(false);
        }
        #endregion

        #region Request And Response Async Treatment
        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(JsonConvert.DeserializeObject<T>(data, JsonSettings)).ConfigureAwait(false);
            }

            var errorMessage = await GetCompleteErrorResponseAsync(data, response).ConfigureAwait(false);
            throw new Exception(errorMessage);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string url, object data = null, string customToken = null)
        {
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                SetAutorizationHeader(customToken, requestMessage);

                await SetContent(data, requestMessage);

                var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                return response;
            }
        }

        private async Task SetContent(object data, HttpRequestMessage requestMessage)
        {
            if (data != null)
            {
                var content = await Task.FromResult(JsonConvert.SerializeObject(data, JsonSettings)).ConfigureAwait(false);
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }
        }

        private void SetAutorizationHeader(string customToken, HttpRequestMessage requestMessage)
        {
            if (!string.IsNullOrEmpty(customToken))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(customToken)));
            else
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(_apiKey)));
        }

        private string GetCompleteUrl(string partOfUrl, string id)
        {
            var url = string.IsNullOrEmpty(partOfUrl) ? $"{BaseURI}/{id}" : $"{BaseURI}/{partOfUrl}/{id}";

            if (url.Last().Equals('/'))
                url = url.Remove(url.Length - 1);

            return url;
        }

        private static async Task<string> GetCompleteErrorResponseAsync(string data, HttpResponseMessage response)
        {
            try
            {
                var jsonMessage = JsonConvert.DeserializeObject<ComplexErrorResponse>(data);

                return await Task.FromResult(JsonConvert.SerializeObject(new
                {
                    StatusCode = response.StatusCode,
                    ReasonPhase = response.ReasonPhrase,
                    Message = jsonMessage
                })).ConfigureAwait(false);

            }
            catch (Exception)
            {
                try
                {
                    var jsonMessage = JsonConvert.DeserializeObject<ErrorResponse>(data).Errors;
                    return await Task.FromResult(JsonConvert.SerializeObject(new
                    {
                        StatusCode = response.StatusCode,
                        ReasonPhase = response.ReasonPhrase,
                        Message = jsonMessage
                    })).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            }
        }
        #endregion

    }

    internal sealed class ComplexErrorResponse
    {
        public Dictionary<string, JArray> Errors { get; set; }
    }


    internal sealed class ErrorResponse
    {
        public string Errors { get; set; }
    }
}
