namespace SpeakerAPI.Repositories
{
    using System.Collections.Generic;
    using SpeakerAPI.Models;

    public class SpeakersRepository
    {
        private IList<Speaker> speakerList = new List<Speaker>
        {
            new Speaker { identificationProfileId = "5006205c-7bed-4f74-8b85-a2344943e303" , Name = "Sebastian Gambolati" },
            new Speaker { identificationProfileId = "7ec023b2-1e12-4c85-8b73-6b19aa782f83" , Name = "Carlos Huerta" },
        };

        public IList<Speaker> ListSpeakers()
        {
            return this.speakerList;
        }
    }
}