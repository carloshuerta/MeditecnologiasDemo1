using MixedRealityToolkit.UX.Buttons;
using UnityEngine;

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
    }
}
