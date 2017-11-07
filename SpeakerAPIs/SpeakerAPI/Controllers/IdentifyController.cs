namespace SpeakerAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Newtonsoft.Json;
    using SpeakerAPI.Models;

    public class IdentifyController : ApiController
    {
        private const string BASE_SPEAKER_URL = "https://westus.api.cognitive.microsoft.com/spid/v1.0";

        private const string SUBSCRIPTION_KEY_HEADER = "Ocp-Apim-Subscription-Key";
        private const string OPERATION_LOCATION_HEADER = "operation-location";
        
        private readonly HttpClient httpClient;

        public IdentifyController()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_SPEAKER_URL)
            };
        }

        [HttpPost]
        public async Task<string> Post()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues(SUBSCRIPTION_KEY_HEADER);
            var subscriptionKey = headerValues.FirstOrDefault();

            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.httpClient.DefaultRequestHeaders.Add(SUBSCRIPTION_KEY_HEADER, subscriptionKey);

            var audio = await this.Request.Content.ReadAsByteArrayAsync();

            string uri = @"https://westus.api.cognitive.microsoft.com/spid/v1.0/identify?identificationProfileIds=7f929fad-3919-4a7a-9fe4-3c1f272d5410,7ec023b2-1e12-4c85-8b73-6b19aa782f83,5006205c-7bed-4f74-8b85-a2344943e303&shortAudio=true";
            using (var content = new ByteArrayContent(audio))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(@"application/json");
                var response = await httpClient.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                if (response.Headers.Contains(OPERATION_LOCATION_HEADER))
                {
                    string operationLocationURL = response.Headers.GetValues(OPERATION_LOCATION_HEADER).First();
                    var operationResult = await GetOperationDetailAsync(operationLocationURL, subscriptionKey);

                    var operation = JsonConvert.DeserializeObject<OperationResult>(operationResult);

                    if (operation.status == OperationStatus.succeeded)
                    {
                        return operation.identifiedProfileId;
                    }
                }
            }
            
            return string.Empty;
        }

        private async Task<string> GetOperationDetailAsync(string operationURL, string subscripionKey)
        {
            var response = await this.httpClient.GetStringAsync(operationURL);
            return response;
        }
    }
}
