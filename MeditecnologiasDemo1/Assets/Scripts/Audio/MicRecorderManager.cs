using HoloToolkit.Unity.InputModule;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class MicRecorderManager : MonoBehaviour, IHoldHandler
{
    private const int DEFAULT_MICROPHONE_FREQ = 16000;

    private bool IsRecording = false;

    public Text InstructionsText;
    public AudioSource OutputAudioSource;
    public GameObject LoadingGameObject;

    // Use this for initialization
    void Start()
    {
        LoadingGameObject.SetActive(false);
        InputManager.Instance.PushFallbackInputHandler(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsRecording)
        {
            //Show audio recording animation
        }
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        this.OutputAudioSource.clip = Microphone.Start(null, true, 10, DEFAULT_MICROPHONE_FREQ);
        this.IsRecording = true;
        this.InstructionsText.text = "Diga su nombre y libere <b>AirTap</b>.";
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        Microphone.End(null);

        this.IsRecording = false;
        this.InstructionsText.text = "Procesando audio...";

        string FileName = Guid.NewGuid().ToString() + ".wav";

        this.LoadingGameObject.SetActive(true);

        var FilePath = Path.Combine(Application.persistentDataPath, FileName);
        AudioSaver.Save(FilePath, this.OutputAudioSource.clip);
        this.InstructionsText.text = "Record completed - Audio saved";
        try
        {
            var fileContent = File.ReadAllBytes(FilePath);

            this.InstructionsText.text = "Analizando audio. \nPor favor, espere.";

            //Send audio recorded file to the API.
            StartCoroutine(RunSpeakerIdentification(fileContent));
        }
        finally
        {
            File.Delete(FilePath);
        }
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        this.IsRecording = false;
        this.InstructionsText.text = "Record cancelled";
    }

    private const string SpeakerAPIKey = "fc0e83ee971b4a50ba5f77df6dc21266";

    private IEnumerator RunSpeakerIdentification(byte[] audio)
    {
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", SpeakerAPIKey },
            { "Content-Type", "application/octet-stream" }
        };

        string SpeakerAPIURL = "http://meditec-demo1.azurewebsites.net/api/Identify";
        WWW httpClient = new WWW(SpeakerAPIURL, audio, headers);
        yield return httpClient;

        //When return ...
        if (string.IsNullOrEmpty(httpClient.error))
        {
            var speaker = GetSpeaker(httpClient.text);
            StaticSceneStates.AutenticatedName = speaker.name;
            this.InstructionsText.text = string.Format("Bienvenido, {0}! \nCargando su agenda. \nPor favor, espere.", speaker.name);
        }
        else
        {
            this.InstructionsText.text = "Error: " + httpClient.error;
        }

        this.LoadingGameObject.SetActive(false);

        Invoke("LoadSchedulerScene", 3f);
    }

    private Speaker GetSpeaker(string json)
    {
        var speaker = JsonUtility.FromJson<Speaker>(json);

        if (speaker == null || string.IsNullOrEmpty(speaker.id))
        {
            //Workarround for JSON deserialization issue.
            if (json.Contains("5006205c-7bed-4f74-8b85-a2344943e303"))
            {
                return new Speaker
                {
                    id = "5006205c-7bed-4f74-8b85-a2344943e303",
                    name = "Sebastian Gambolati"
                };
            }
            if (json.Contains("610fa132-f9f5-4ccc-b89d-7480f291d62b"))
            {
                return new Speaker
                {
                    id = "610fa132-f9f5-4ccc-b89d-7480f291d62b",
                    name = "Marcelo Halac"
                };
            }
            if (json.Contains("89be2a5b-0580-4ff9-a8db-78221eb79078"))
            {
                return new Speaker
                {
                    id = "89be2a5b-0580-4ff9-a8db-78221eb79078",
                    name = "Vadim Kotowicz"
                };
            }
            else if (json.Contains("7ec023b2-1e12-4c85-8b73-6b19aa782f83"))
            {
                return new Speaker
                {
                    id = "7ec023b2-1e12-4c85-8b73-6b19aa782f83",
                    name = "Carlos Huerta"
                };
            }
                else if (json.Contains("3b1cf2c7-ee6b-480c-a203-43f1731a1fd7"))
            {
                return new Speaker
                {
                    id = "3b1cf2c7-ee6b-480c-a203-43f1731a1fd7",
                    name = "Dennys Villa"
                };
            }
        }

        return speaker;
    }

    public void LoadSchedulerScene()
    {
        StaticSceneStates.Autenticated = true;
        SceneManager.LoadScene("Start");
    }
}

