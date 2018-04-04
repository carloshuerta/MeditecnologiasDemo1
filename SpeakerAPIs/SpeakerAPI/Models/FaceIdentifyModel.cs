using System.Collections.Generic;

namespace SpeakerAPI.Models
{
    public class FaceIdentifyModel
    {
        public string faceId { get; set; }

        public IList<Candidates> candidates { get; set; }

        public FaceIdentifyModel()
        {
            this.candidates = new List<Candidates>();
        }
    }

    public class Candidates
    {
        public string personId { get; set; }
        public double confidence { get; set; }
    }
}