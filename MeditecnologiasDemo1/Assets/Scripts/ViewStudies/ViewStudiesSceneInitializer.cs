using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

public class ViewStudiesSceneInitializer : MonoBehaviour
{
    private Text txtName;

    // Use this for initialization
    void Start()
    {
        var txtName = this.transform.Find("txtName").gameObject;
        this.txtName = txtName.GetComponentInChildren<Text>();

        this.txtName.text = SessionData.Instance.ViewingPatientName;
    }
}
