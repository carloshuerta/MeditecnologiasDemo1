using MixedRealityToolkit.UX.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToScene : MonoBehaviour
{
    public string SceneName;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().OnButtonClicked += OnButtonClicked;
    }

    private void OnButtonClicked(GameObject obj)
    {
        SceneManager.LoadScene(this.SceneName);
    }
}
