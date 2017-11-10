using UnityEngine;
using UnityEngine.UI;

public class SchedulerManager : MonoBehaviour
{
    public Text CurrentDateTextBox;

    // Use this for initialization
    void Start()
    {
        this.CurrentDateTextBox.text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
    }
}
