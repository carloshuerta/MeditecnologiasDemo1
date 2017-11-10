namespace SpeakerAPI.Models
{
    public class PersonModel
    {
        public string personId { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
        public string[] persistedFaceIds { get; set; }
    }
}