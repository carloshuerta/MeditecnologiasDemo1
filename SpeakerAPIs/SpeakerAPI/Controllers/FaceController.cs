namespace SpeakerAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Newtonsoft.Json;
    using SpeakerAPI.Models;

    public class FaceController : ApiController
    {
        private const string BASE_SPEAKER_URL = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/";
        private const string SUBSCRIPTION_KEY_HEADER = "Ocp-Apim-Subscription-Key";

        private readonly HttpClient httpClient;

        public FaceController()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_SPEAKER_URL)
            };
        }

        [HttpPost]
        public async Task<IList<PersonModel>> Post()
        {
            IEnumerable<string> headerValues = this.Request.Headers.GetValues(SUBSCRIPTION_KEY_HEADER);
            var subscriptionKey = headerValues.FirstOrDefault();

            var image = await this.Request.Content.ReadAsByteArrayAsync();

            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient.DefaultRequestHeaders.Add(SUBSCRIPTION_KEY_HEADER, subscriptionKey);

            var facesDetected = await DetectFacesInImage(image);
            var facesIdentified = await IdentifyFacesInImage(facesDetected, "devs");
            var personsIdentified = await IdentifyPersons(facesIdentified, "devs");

            return personsIdentified;
        }

        private async Task<IList<PersonModel>> IdentifyPersons(IList<FaceIdentifyModel> facesIdentified, string personGroupId)
        {
            string uri = string.Format(@"https://westcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/{0}/persons", personGroupId);

            var personsResponse = await this.httpClient.GetStringAsync(uri);
            var personsInGroup = JsonConvert.DeserializeObject<IList<PersonModel>>(personsResponse);

            return personsInGroup.Where(x => facesIdentified.Any(y => y.candidates.Any(z => z.personId == x.personId)))
                .ToList();
        }

        private async Task<IList<FaceDetectModel>> DetectFacesInImage(byte[] image)
        {
            string uri = @"https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=tue";

            using (var content = new ByteArrayContent(image))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(@"application/octet-stream");
                var response = await this.httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var detectResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<FaceDetectModel>>(detectResponse);
                }
                else
                {
                    //TODO: Throw an exception.
                    //this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, await response.Content.ReadAsStringAsync());
                }
                return null;
            }
        }

        private async Task<IList<FaceIdentifyModel>> IdentifyFacesInImage(IList<FaceDetectModel> facesDetected, string personGroupId)
        {
            string uri = @"https://westcentralus.api.cognitive.microsoft.com/face/v1.0/identify";

            var identifyRequestBody = new
            {
                personGroupId = personGroupId,
                faceIds = facesDetected.Select(x => x.faceId).ToList()
            };

            using (var content = new StringContent(JsonConvert.SerializeObject(identifyRequestBody), Encoding.Default, "application/json"))
            {
                var response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var identifiedFaces = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IList<FaceIdentifyModel>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    //TODO: Throw an exception.
                    //this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, await response.Content.ReadAsStringAsync());
                }
                return null;
            }
        }
    }
}
