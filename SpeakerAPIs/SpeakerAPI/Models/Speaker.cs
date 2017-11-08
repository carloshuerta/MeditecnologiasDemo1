using System;

namespace SpeakerAPI.Models
{
    public class Speaker
    {
        public string identificationProfileId { get; set; }

        public string locale { get; set; }

        public double enrollmentSpeechTime { get; set; }

        public int remainingEnrollmentSpeechTime { get; set; }

        public DateTime createdDateTime { get; set; }

        public DateTime lastActionDateTime { get; set; }

        public string enrollmentStatus { get; set; }

        public string Name { get; set; }
    }

    public class SpeakerResult
    {
        public string id { get; set; }

        public string name { get; set; }
    }
}