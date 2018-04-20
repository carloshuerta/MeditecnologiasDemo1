using MixedRealityToolkit.UX.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewPatientData : MonoBehaviour
{
    public int PatientID;

    // Use this for initialization
    private void OnEnable()
    {
        this.GetComponent<Button>().OnButtonClicked += OnButtonClicked;
    }

    private void OnButtonClicked(GameObject obj)
    {
        Debug.LogFormat("Loading data for Patient {0}.", PatientID);

        SceneManager.LoadScene("PacientData");
    }
}
