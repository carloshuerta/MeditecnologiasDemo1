namespace SpeakerAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Newtonsoft.Json;
    using SpeakerAPI.Models;
    using SpeakerAPI.Repositories;

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
        public async Task<IHttpActionResult> Post()
        {
            SpeakersRepository speakersRepository = new SpeakersRepository();
            var speakerList = speakersRepository.ListSpeakers();

            IEnumerable<string> headerValues = this.Request.Headers.GetValues(SUBSCRIPTION_KEY_HEADER);
            var subscriptionKey = headerValues.FirstOrDefault();

            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient.DefaultRequestHeaders.Add(SUBSCRIPTION_KEY_HEADER, subscriptionKey);

            var audio = await this.Request.Content.ReadAsByteArrayAsync();

            string enrolledProfiles = string.Join(",", speakerList.Select(x => x.identificationProfileId));
            string uri = string.Format(@"https://westus.api.cognitive.microsoft.com/spid/v1.0/identify?identificationProfileIds={0}&shortAudio=true", enrolledProfiles);
            using (var content = new ByteArrayContent(audio))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(@"application/json");
                var response = await this.httpClient.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    return InternalServerError(new Exception(await response.Content.ReadAsStringAsync()));
                }

                if (response.Headers.Contains(OPERATION_LOCATION_HEADER))
                {
                    string operationLocationURL = response.Headers.GetValues(OPERATION_LOCATION_HEADER).First();

                    OperationResult operation = new OperationResult { status = OperationStatus.notstarted };
                    while (operation.status == OperationStatus.notstarted || operation.status == OperationStatus.running)
                    {
                        var operationResult = await GetOperationDetailAsync(operationLocationURL, subscriptionKey);

                        operation = JsonConvert.DeserializeObject<OperationResult>(operationResult);

                        if (operation.status == OperationStatus.succeeded)
                        {
                            if (operation.processingResult.confidence == IdentificationConfidence.Normal ||
                                operation.processingResult.confidence == IdentificationConfidence.High)
                            {
                                var identifiedSpeaker = speakerList.Single(x => x.identificationProfileId == operation.processingResult?.identifiedProfileId);

                                return Ok(new SpeakerResult
                                {
                                    id = identifiedSpeaker.identificationProfileId,
                                    name = identifiedSpeaker.Name
                                });
                            }
                            else
                            {
                                return BadRequest("No se pudo detectar la identidad. Por favor, vuelva a intentarlo.");
                            }
                        }
                        // TODO: Add error handling.

                        // Wait a bit to try again.
                        Thread.Sleep(300);
                    }
                }
            }

            return BadRequest();
        }

        private async Task<string> GetOperationDetailAsync(string operationURL, string subscripionKey)
        {
            var response = await this.httpClient.GetStringAsync(operationURL);
            return response;
        }
    }
}
