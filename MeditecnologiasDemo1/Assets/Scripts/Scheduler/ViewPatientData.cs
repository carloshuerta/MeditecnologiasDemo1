using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.InputModule.InputHandlers;
using MixedRealityToolkit.UX.Buttons;
using UnityEngine;

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
    }
}
