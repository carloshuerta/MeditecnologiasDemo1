using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;
using UnityEngine.XR.WSA.WebCam;

public class PacientPhotoVerifier : MonoBehaviour
{
    private const string FILE_NAME = @"cognitive_analysis.jpg";

    private PhotoCapture photoCapture = null;

    public InteractiveToggle pacientVerifiedCheckBox;

    public void CaptureImageAndValidate()
    {
        CreatePhotoCapture();
    }

    private void Start()
    {
        this.pacientVerifiedCheckBox.HasSelection = false;
    }

    // This method request to create a PhotoCapture object.
    // When its finish created, call the OnPhotoCreated method.
    private void CreatePhotoCapture()
    {
        PhotoCapture.CreateAsync(false, this.OnPhotoCreated);
    }

    // This method store the PhotoCapture object just created and retrieve the high quality
    // available for the camera and then request to start capturing the photo with the
    // given camera parameters.
    private void OnPhotoCreated(PhotoCapture captureObject)
    {
        this.photoCapture = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters()
        {
            hologramOpacity = 0.0f,
            cameraResolutionWidth = cameraResolution.width,
            cameraResolutionHeight = cameraResolution.height,
            pixelFormat = CapturePixelFormat.BGRA32
        };
        captureObject.StartPhotoModeAsync(c, this.OnPhotoModeStarted);
    }

    // This method is called when we have access to the camera and can take photo with it.
    // We request to take the photo and store it in the storage.
    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(FILE_NAME);
            string filePath = Path.Combine(Application.persistentDataPath, filename);
            this.photoCapture.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, this.OnCapturedPhotoToDisk);
        }
        else
        {
            Debug.LogError("Unable to start photo mode.");
        }
    }

    // This method is called when the photo is finish taked (or not, so check the succes property)
    // We can read the file from disk and do anything we need with it.
    // Finally, we request to stop the photo mode to free the resource.
    private void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(FILE_NAME);
            string filePath = Path.Combine(Application.persistentDataPath, filename);

            byte[] image = File.ReadAllBytes(filePath);

            // We have the photo taken.
            GetFaceAPITags(image);
        }
        else
        {
            Debug.LogError("Failed to save Photo to disk.");
        }
        this.photoCapture.StopPhotoModeAsync(this.OnStoppedPhotoMode);
    }

    // This method is called when the photo mode is stopped and we can dispose the resources allocated.
    private void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        this.photoCapture.Dispose();
        this.photoCapture = null;
    }

    private void GetFaceAPITags(byte[] image)
    {
        StartCoroutine(RunFaceAPIAnalysis(image));
    }

    private const string FaceAPIUriBase = "http://meditec-demo1.azurewebsites.net";
    private const string FaceAPIKey = "89ca645cd8f343f9b08d5d4f720fd6b9";

    private IEnumerator RunFaceAPIAnalysis(byte[] image)
    {
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", FaceAPIKey },
            { "Content-Type", "application/octet-stream" }
        };

        string FaceAPIURL = FaceAPIUriBase + "/api/Face";
        WWW httpClient = new WWW(FaceAPIURL, image, headers);
        yield return httpClient;

        //When return ...
        var jsonResult = httpClient.text;

        List<string> tags = new List<string>();
        // http://answers.unity3d.com/questions/1148632/jsonutility-and-arrays-error-json-must-represent-a.html
        var jsonResults = "{\"values\":" + httpClient.text + "}";

        //TODO: Delete file after it was sent.

        if (string.IsNullOrEmpty(httpClient.error))
        {
            FaceAPIResult result = GetPacientData(jsonResults);

            if (result == null || result.values.Count() == 0)
            {
                Debug.Log("No se encontro ninguna cara o persona enrolada. Reintentando.");

                // Retry in 5 seconds.
                Invoke("CreatePhotoCapture", 5f);
            }
            else
            {
                if (result.values.Any(x => x.HCId == 635063))
                {
                    this.pacientVerifiedCheckBox.HasSelection = true;
                }
            }
        }
        else
        {
            Debug.Log(httpClient.error);

            //Retry...
            CreatePhotoCapture();
        }

        //this.FaceApiCaptionTextbox.text = string.Join("\n", result.values.Select(x => string.Format("Age: {0} - Gender: {1} - Glasses: {2}", x.attributes.age, x.attributes.gender, x.attributes.glasses)).ToArray());
    }

    private FaceAPIResult GetPacientData(string jsonResults)
    {
        var result = JsonUtility.FromJson<FaceAPIResult>(jsonResults);

        if (result != null && result.values != null)
        {
            foreach (var person in result.values)
            {
                if (person.id == "ced86bf7-463c-48f8-9909-3abc8089574e")
                {
                    person.HCId = 635063;
                }
                //else if ()
                //{

                //}

            }
        }

        return result;
    }
}