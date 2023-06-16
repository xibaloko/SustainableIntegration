using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLayer.Lib
{
    public interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
   
    public class StandardHttpClient : IHttpClientWrapper
    {
        private readonly HttpClient client;

        public StandardHttpClient()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
            return response;
        }

        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
