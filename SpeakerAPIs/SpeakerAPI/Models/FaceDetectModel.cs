using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeakerAPI.Models
{
    public class FaceDetectModel
    {
        public string faceId { get; set; }

        public FaceDetectRectangle faceRectangle { get; set; }

        public FaceDetectModel()
        {
            this.faceRectangle = new FaceDetectRectangle();
        }
    }

    public class FaceDetectRectangle
    {
        public int top { get; set; }

        public int left { get; set; }

        public int width { get; set; }

        public int height { get; set; }
    }
}