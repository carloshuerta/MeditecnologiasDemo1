using MixedRealityToolkit.UX.Buttons;
using UnityEngine;

public class ViewStudyButton : MonoBehaviour
{
    public GameObject columnStudy;
    public GameObject toraxStudy;

    public int StudyId;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().OnButtonClicked += OnButtonClicked;

        //this.columnStudy = GameObject.Find("columna");
        //this.toraxStudy = GameObject.Find("Estudio3d");
    }

    private void OnButtonClicked(GameObject obj)
    {
        this.columnStudy.SetActive(false);
        this.toraxStudy.SetActive(false);

        if (this.StudyId == 1)
        {
            this.toraxStudy.SetActive(true);
        }
        else
        {
            this.columnStudy.SetActive(true);
        }
    }
}
