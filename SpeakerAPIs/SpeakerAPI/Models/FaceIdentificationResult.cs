namespace SpeakerAPI.Models
{
    public class FaceIdentificationResult
    {
        public string id { get; set; }

        public string name { get; set; }

        public double confidence { get; set; }

        public string userData { get; set; }
    }
}