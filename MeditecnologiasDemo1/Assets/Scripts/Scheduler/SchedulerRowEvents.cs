using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.InputModule.InputHandlers;
using UnityEngine;

public class SchedulerRowEvents : MonoBehaviour, IInputClickHandler
{
    public int PersonId;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Row clicked");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
