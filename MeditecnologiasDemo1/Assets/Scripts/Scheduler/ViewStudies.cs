using Assets.Scripts.General;
using Assets.Scripts.Scheduler;
using MixedRealityToolkit.UX.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewStudies : MonoBehaviour
{
    public int PatientID;

    // Use this for initialization
    private void OnEnable()
    {
        this.GetComponent<Button>().OnButtonClicked += OnButtonClicked;
    }

    private void OnButtonClicked(GameObject obj)
    {
        Debug.LogFormat("Loading Studies for Patient {0}.", PatientID);

        SessionData.Instance.ViewingPatientId = this.PatientID;
        // TODO: Should check if the patient exists.
        SessionData.Instance.ViewingPatientName = PatientsRepository.GetPatient(this.PatientID).Name;

        SceneManager.LoadScene("ViewStudies");
    }
}
