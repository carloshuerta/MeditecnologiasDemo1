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
            new Speaker { identificationProfileId = "610fa132-f9f5-4ccc-b89d-7480f291d62b" , Name = "Marcelo Halac" },
            new Speaker { identificationProfileId = "89be2a5b-0580-4ff9-a8db-78221eb79078" , Name = "Vadim Kotowicz" },
            new Speaker { identificationProfileId = "3b1cf2c7-ee6b-480c-a203-43f1731a1fd7" , Name = "Dennys Villa" },
        };

        public IList<Speaker> ListSpeakers()
        {
            return this.speakerList;
        }
    }
}